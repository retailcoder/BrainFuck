using System;
using System.Collections.Generic;

namespace BrainFuck.Tokens
{
    /// <summary>
    /// A base class for all language tokens.
    /// </summary>
    public abstract class Token : IEquatable<Token>, IComparable<Token>
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

        protected Token(Span position, int index, TokenType type)
            : this(position, index, Tokens[type])
        {
            Type = type;
        }

        protected Token(Span position, int index, string text)
        {
            Index = index;
            Type = TokenType.Trivia;
            Position = position;
            Text = text;
        }

        /// <summary>
        /// The type of token.
        /// </summary>
        public TokenType Type { get; }

        /// <summary>
        /// The position of the token in the source text.
        /// </summary>
        public Span Position { get; }

        /// <summary>
        /// The position of the token in the token stream.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// The text content of the token.
        /// </summary>
        public string Text { get; }

        public bool Equals(Token other)
        {
            return other != null
                && other.Type == Type
                && other.Position == Position
                && other.Text == Text;
        }

        public int CompareTo(Token other)
        {
            if (other == null)
            {
                return Position.CompareTo(Span.Empty);
            }

            return Position.CompareTo(other.Position);
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
        public TriviaToken(Span position, int index, string text)  : base(position, index, text) { }
    }

    /// <summary>
    /// A language token representing a "Move Left" instruction.
    /// </summary>
    public sealed class MoveLeftToken : Token
    {
        public static string Token => "<";
        public MoveLeftToken(Span position, int index) : base(position, index, TokenType.MoveLeft) { }
    }

    /// <summary>
    /// A language token representing a "Move Right" instruction.
    /// </summary>
    public sealed class MoveRightToken : Token
    {
        public static string Token => ">";
        public MoveRightToken(Span position, int index) : base(position, index, TokenType.MoveRight) { }
    }

    /// <summary>
    /// A language token representing a "Begin Loop" instruction.
    /// </summary>
    public sealed class BeginLoopToken : Token
    {
        public static string Token => "[";
        public BeginLoopToken(Span position, int index) : base(position, index, TokenType.BeginLoop) { }
    }

    /// <summary>
    /// A language token representing an "End Loop" instruction.
    /// </summary>
    public sealed class EndLoopToken : Token
    {
        public static string Token => "]";
        public EndLoopToken(Span position, int index) : base(position, index, TokenType.EndLoop) { }
    }

    /// <summary>
    /// A language token representing an "Increment" instruction.
    /// </summary>
    public sealed class IncrementToken : Token
    {
        public static string Token => "+";
        public IncrementToken(Span position, int index) : base(position, index, TokenType.Increment) { }
    }

    /// <summary>
    /// A language token representing a "Decrement" instruction.
    /// </summary>
    public sealed class DecrementToken : Token
    {
        public static string Token => "-";
        public DecrementToken(Span position, int index) : base(position, index, TokenType.Decrement) { }
    }

    /// <summary>
    /// A language token representing an "Input" instruction.
    /// </summary>
    public sealed class InputToken : Token
    {
        public static string Token => ",";
        public InputToken(Span position, int index) : base(position, index, TokenType.Input) { }
    }

    /// <summary>
    /// A language token representing an "Output" instruction.
    /// </summary>
    public sealed class OutputToken : Token
    {
        public static string Token => ".";
        public OutputToken(Span position, int index) : base(position, index, TokenType.Output) { }
    }
}