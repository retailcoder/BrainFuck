    using System;
    using System.Collections.Generic;

    namespace BrainFuck.Tokens
    {
        /// <summary>
        /// A base class for all language tokens.
        /// </summary>
        public abstract class Token : IEquatable<Token>
        {
            private static readonly IDictionary<TokenType, string> Tokens =
                new Dictionary<TokenType, string>
            {
                { TokenType.MoveLeft, MoveLeftToken.Token },
                { TokenType.MoveRight, MoveRightToken.Token },
                { TokenType.BeginLoop, BeginLoopToken.Token },
                { TokenType.EndLoop, EndLoopToken.Token },
                { TokenType.Increment, IncrementToken.Token },
                { TokenType.Decrement, DecrementToken.Token },
                { TokenType.Input, InputToken.Token },
                { TokenType.Output, OutputToken.Token },
            };

            protected Token(Span position, TokenType type)
                : this(position, Tokens[type])
            {
                Type = type;
            }

            protected Token(Span position, string text)
            {
                Type = TokenType.Trivia;
                Position = position;
                Text = text;
            }

            public TokenType Type { get; }
            public Span Position { get; }
            public string Text { get; }

            public bool Equals(Token other)
            {
                return other != null
                    && other.Type == Type
                    && other.Position == Position
                    && other.Text == Text;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        /// <summary>
        /// A trivia token, representing any non-code content.
        /// </summary>
        public sealed class TriviaToken : Token
        {
            public TriviaToken(Span position, string text) 
                : base(position, text)
            {
            }
        }

        /// <summary>
        /// A language token representing a "Move Left" instruction.
        /// </summary>
        public sealed class MoveLeftToken : Token
        {
            public static string Token => "<";

            public MoveLeftToken(Span position)
                : base(position, TokenType.MoveLeft)
            {
            }
        }

        /// <summary>
        /// A language token representing a "Move Right" instruction.
        /// </summary>
        public sealed class MoveRightToken : Token
        {
            public static string Token => ">";

            public MoveRightToken(Span position)
                : base(position, TokenType.MoveRight)
            {
            }
        }

        /// <summary>
        /// A language token representing a "Begin Loop" instruction.
        /// </summary>
        public sealed class BeginLoopToken : Token
        {
            public static string Token => "[";

            public BeginLoopToken(Span position)
                : base(position, TokenType.BeginLoop)
            {
            }
        }

        /// <summary>
        /// A language token representing an "End Loop" instruction.
        /// </summary>
        public sealed class EndLoopToken : Token
        {
            public static string Token => "]";

            public EndLoopToken(Span position)
                : base(position, TokenType.EndLoop)
            {
            }
        }

        /// <summary>
        /// A language token representing an "Increment" instruction.
        /// </summary>
        public sealed class IncrementToken : Token
        {
            public static string Token => "+";

            public IncrementToken(Span position)
                : base(position, TokenType.Increment)
            {
            }
        }

        /// <summary>
        /// A language token representing a "Decrement" instruction.
        /// </summary>
        public sealed class DecrementToken : Token
        {
            public static string Token => "-";

            public DecrementToken(Span position)
                : base(position, TokenType.Decrement)
            {
            }
        }

        /// <summary>
        /// A language token representing an "Input" instruction.
        /// </summary>
        public sealed class InputToken : Token
        {
            public static string Token => ",";

            public InputToken(Span position)
                : base(position, TokenType.Input)
            {
            }
        }

        /// <summary>
        /// A language token representing an "Output" instruction.
        /// </summary>
        public sealed class OutputToken : Token
        {
            public static string Token => ".";

            public OutputToken(Span position)
                : base(position, TokenType.Output)
            {
            }
        }
    }