using System;
using System.Collections.Generic;
using BrainFuck.Syntax;
using BrainFuck.Tokens;

namespace BrainFuck
{
    public static class Parser
    {
        private static readonly Dictionary<TokenType, Func<Token, SyntaxNode>> NodeFactories =
            new Dictionary<TokenType, Func<Token, SyntaxNode>>
            {
                {TokenType.Trivia, token => new TriviaSyntax(token)},
                {TokenType.Increment, token => new IncrementInstructionSyntax(token)},
                {TokenType.Decrement, token => new DecrementInstructionSyntax(token)},
                {TokenType.MoveLeft, token => new MoveLeftInstructionSyntax(token)},
                {TokenType.MoveRight, token => new MoveRightInstructionSyntax(token)},
                {TokenType.Input, token => new InputInstructionSyntax(token)},
                {TokenType.Output, token => new OutputInstructionSyntax(token)},
            };

        public static SyntaxTree Parse(Token[] tokens)
        {
            var index = 0;
            return Parse(tokens, ref index);
        }

        private static SyntaxTree Parse(IReadOnlyList<Token> tokens, ref int index, SyntaxTree root = null)
        {
            if (root == null)
            {
                root = new SyntaxTree();
            }

            while (index < tokens.Count)
            {
                var token = tokens[index];
                index++;

                Func<Token, SyntaxNode> factory;
                if (NodeFactories.TryGetValue(token.Type, out factory))
                {
                    root.Add(factory.Invoke(token));
                }
                else
                {
                    switch(token.Type)
                    {
                        case TokenType.BeginLoop:
                            var block = new LoopBlockSyntax();
                            block.AddToken(token);
                            root.Add(Parse(tokens, ref index, block));
                            break;

                        case TokenType.EndLoop:
                            root.AddToken(token);
                            return root;
                    }
                }
            }

            return root;
        }
    }
}