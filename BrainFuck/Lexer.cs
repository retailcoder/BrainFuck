using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrainFuck.Tokens;

namespace BrainFuck
{
    /// <summary>
    /// An object responsible for tokenizing an input stream.
    /// </summary>
    public sealed class Lexer
    {
        /// <summary>
        /// Yields tokens from the input stream.
        /// </summary>
        /// <param name="input">Any stream of BrainFuck source code.</param>
        public IEnumerable<Token> Tokenize(System.IO.Stream input)
        {
            var reader = new System.IO.StreamReader(input);
            var currentTokenPosition = Span.Empty;
            var currentTriviaSpan = Span.Empty;
            var builder = new StringBuilder();

            var tokenCount = 0;

            while (reader.Peek() > 0)
            {
                var current = (char) reader.Read();
                var next = (char) reader.Peek();

                if (IsNewLine(current, next))
                {
                    builder.Append(current);
                    currentTriviaSpan = currentTriviaSpan.NextLine;
                    currentTokenPosition = currentTokenPosition.NewLine;

                    if (Environment.NewLine.Length == 2)
                    {
                        current = (char) reader.Read();
                        builder.Append(current);
                    }

                    continue;
                }

                Token token;
                if (IsToken(currentTokenPosition, tokenCount, current, out token))
                {
                    // if we were building a trivia token, we need to yield it first:
                    if (builder.Length != 0)
                    {
                        yield return new TriviaToken(currentTriviaSpan, tokenCount, builder.ToString());
                        tokenCount++;
                    }

                    yield return token;
                    tokenCount++;

                    currentTriviaSpan = currentTokenPosition.Next;
                    currentTokenPosition = currentTriviaSpan.End;
                    builder.Clear();
                }
                else
                {
                    builder.Append(current);
                }

                if (next != 0)
                {
                    currentTriviaSpan = currentTriviaSpan.NextColumn;
                }
            }

            if (builder.Length != 0)
            {
                currentTriviaSpan = currentTriviaSpan.PreviousColumn;
                yield return new TriviaToken(currentTriviaSpan, tokenCount, builder.ToString());
                builder.Clear();
            }
        }

        /// <summary>
        /// Returns tokens from input string.
        /// </summary>
        /// <param name="input">BrainFuck source code</param>
        public IEnumerable<Token> Tokenize(string input)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                var writer = new System.IO.StreamWriter(stream, Encoding.Default);
                writer.Write(input);
                writer.Flush();
                stream.Position = 0;

                var tokens = Tokenize(stream).ToList();
                writer.Dispose();

                return tokens;
            }
        }

        private static bool IsNewLine(char character, char next)
        {
            return new string(new[] {character, next}).Equals(Environment.NewLine)
                   || Environment.NewLine.Equals(character.ToString());
        }

        private static readonly IDictionary<string, Func<Span, int, Token>>
            TokenFactories = new Dictionary<string, Func<Span, int, Token>>
            {
                [MoveLeftToken.Token] = (span, index) =>  new MoveLeftToken(span, index),
                [MoveRightToken.Token] = (span, index) => new MoveRightToken(span, index),
                [BeginLoopToken.Token] = (span, index) => new BeginLoopToken(span, index),
                [EndLoopToken.Token] = (span, index) => new EndLoopToken(span, index),
                [IncrementToken.Token] = (span, index) => new IncrementToken(span, index),
                [DecrementToken.Token] = (span, index) => new DecrementToken(span, index),
                [InputToken.Token] = (span, index) => new InputToken(span, index),
                [OutputToken.Token] = (span, index) => new OutputToken(span, index),
            };

        private static bool IsToken(Span position, int index, char input, out Token token)
        {
            Func<Span, int, Token> factory;
            if (TokenFactories.TryGetValue(input.ToString(), out factory))
            {
                token = factory.Invoke(position, index);
                return true;
            }

            token = null;
            return false;
        }
    }
}