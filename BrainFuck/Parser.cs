using System;
using System.Collections.Generic;
using System.Linq;
using BrainFuck.Syntax;
using BrainFuck.Tokens;

namespace BrainFuck
{
    public class Parser
    {
        private static readonly Dictionary<TokenType, Func<SyntaxTree>> SyntaxTrees =
            new Dictionary<TokenType, Func<SyntaxTree>>
            {
                {TokenType.Trivia, () => new TriviaSyntax()},
                {TokenType.Increment, () => new IncrementInstructionSyntax()},
                {TokenType.Decrement, () => new DecrementInstructionSyntax()},
                {TokenType.MoveLeft, () => new MoveLeftInstructionSyntax()},
                {TokenType.MoveRight, () => new MoveRightInstructionSyntax()},
                {TokenType.Input, () => new InputInstructionSyntax()},
                {TokenType.Output, () => new OutputInstructionSyntax()},
            };

        public SyntaxTree Parse(Token[] tokens)
        {
            var index = 0;
            var depth = 0;
            return Parse(tokens, ref index, ref depth);
        }

        private static SyntaxTree Parse(IReadOnlyList<Token> tokens, ref int index, ref int depth, SyntaxTree root = null)
        {
            if(root == null)
            {
                root = new SyntaxTree();
            }

            Token previousToken = null;
            SyntaxTree currentTree = null;
            SyntaxTree previousTree = null;

            while(index < tokens.Count)
            {
                var token = tokens[index];
                index++;

                Func<SyntaxTree> treeFactory;
                if(SyntaxTrees.TryGetValue(token.Type, out treeFactory))
                {
                    // trivia or instruction token
                    if(previousToken?.Type == token.Type)
                    {
                        previousTree?.Add(token);
                    }
                    else
                    {
                        if (previousTree != null)
                        {
                            root.Add(previousTree);
                        }

                        currentTree = treeFactory.Invoke();
                        currentTree.Add(token);
                    }
                }
                else
                {
                    // control flow token
                    if(previousTree != null)
                    {
                        root.Add(previousTree);
                    }
                    switch(token.Type)
                    {
                        case TokenType.BeginLoop:
                            depth++;
                            currentTree = Parse(tokens, ref index, ref depth, new LoopBlockSyntax { token });
                            break;

                        case TokenType.EndLoop:
                            if(depth == 0)
                            {
                                throw new IllegalTokenException(token);
                            }
                            depth--;

                            root.Add(token);
                            return root;

                        default:
                            throw new IllegalTokenException(token);
                    }
                }

                previousToken = token;
                previousTree = currentTree;
            }

            if (previousTree != null)
            {
                root.Add(previousTree);
            }
            return root;
        }
    }
}