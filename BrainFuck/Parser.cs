using System;
using System.Collections.Generic;
using BrainFuck.Syntax;
using BrainFuck.Tokens;

namespace BrainFuck
{
    public class Parser
    {
        private static readonly Dictionary<TokenType, Func<SyntaxTree>> SyntaxTrees =
            new Dictionary<TokenType, Func<SyntaxTree>>
            {
                [TokenType.Trivia] = () => new TriviaSyntax(),
                [TokenType.Increment] = () => new IncrementInstructionSyntax(),
                [TokenType.Decrement] = () => new DecrementInstructionSyntax(),
                [TokenType.MoveLeft] = () => new MoveLeftInstructionSyntax(),
                [TokenType.MoveRight] = () => new MoveRightInstructionSyntax(),
                [TokenType.Input] = () => new InputInstructionSyntax(),
                [TokenType.Output] = () => new OutputInstructionSyntax(),
            };

        public SyntaxTree Parse(Token[] tokens)
        {
            var index = 0;
            var depth = 0;
            return Parse(tokens, ref index, ref depth);
        }

        private static SyntaxTree Parse(IReadOnlyList<Token> tokens, ref int index, ref int depth, SyntaxTree parent = null)
        {
            if(parent == null) { parent = new SyntaxTree(); }

            Token previousToken = null;
            SyntaxTree currentTree = null;
            SyntaxTree previousTree = null;

            for(var i = index; index < tokens.Count; i++)
            {
                var token = tokens[index];
                index++;

                Func<SyntaxTree> treeFactory;
                if(SyntaxTrees.TryGetValue(token.Type, out treeFactory))
                {
                    // trivia or instruction token
                    if(previousToken?.Type == token.Type)
                    {
                        // same token as before, add to previous tree:
                        AddIfNotNull(previousTree, token);
                    }
                    else
                    {
                        // new token, add previous tree to root and get a new tree:
                        AddIfNotNull(parent, previousTree);

                        currentTree = treeFactory.Invoke();
                        AddIfNotNull(currentTree, token);
                    }
                }
                else
                {
                    // new control flow token, add previous tree to root:
                    AddIfNotNull(parent, previousTree);

                    switch(token.Type)
                    {
                        case TokenType.BeginLoop:
                            depth++;
                            // note: recursive
                            currentTree = Parse(tokens, ref index, ref depth, new LoopBlockSyntax { token });
                            break;

                        case TokenType.EndLoop:
                            if(depth == 0) { throw new IllegalTokenException(token); }
                            depth--;

                            AddIfNotNull(parent, token);
                            return parent;

                        default:
                            throw new IllegalTokenException(token);
                    }
                }

                previousToken = token;
                previousTree = currentTree;
            }

            AddIfNotNull(parent, previousTree);
            return parent;
        }

        private static void AddIfNotNull(SyntaxTree tree, Token token)
        {
            tree?.Add(token);
        }

        private static void AddIfNotNull(SyntaxTree root, SyntaxTree tree)
        {
            if (tree != null)
            {
                root?.Add(tree);
            }
        }
    }
}