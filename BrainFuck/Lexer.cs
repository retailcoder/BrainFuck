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
                var currentLine = 0;
                var builderStartLine = 0;
                var currentColumn = 0;
                var builderStartColumn = 0;
                var builder = new StringBuilder();

                while (reader.Peek() > 0)
                {
                    var character = (char) reader.Read();
                    var next = (char) reader.Peek();
                    if ((character == '\n' || character == '\r') && (next == '\r' || next == '\n'))
                    {
                        builder.Append(character);
                        character = (char) reader.Read();
                        builder.Append(character);
                        currentColumn = 0;
                        currentLine++;
                        continue;
                    }

                    var position = new Span(currentLine, currentColumn);
                    Token token;

                    if (IsToken(position, character, out token))
                    {
                        if (builder.Length != 0)
                        {
                            var triviaSpan = new Span(builderStartLine, currentLine, builderStartColumn, currentColumn);
                            yield return new TriviaToken(triviaSpan, builder.ToString());
                        }
                        yield return token;
                        builder.Clear();
                    }
                    else
                    {
                        builderStartLine = currentLine;
                        builderStartColumn = currentColumn;
                        builder.Append(character);
                    }

                    currentColumn++;
                }

                if (builder.Length != 0)
                {
                    var triviaSpan = new Span(builderStartLine, currentLine, builderStartColumn, currentColumn);
                    yield return new TriviaToken(triviaSpan, builder.ToString());
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

            public IEnumerable<Token> Tokenize(byte[] input)
            {
                return Tokenize(input.ToString());
            }

            private static readonly IDictionary<string, Func<Span, Token>>
                TokenFactories = new Dictionary<string, Func<Span, Token>>
                {
                    {MoveLeftToken.Token, span => new MoveLeftToken(span)},
                    {MoveRightToken.Token, span => new MoveRightToken(span)},
                    {BeginLoopToken.Token, span => new BeginLoopToken(span)},
                    {EndLoopToken.Token, span => new EndLoopToken(span)},
                    {IncrementToken.Token, span => new IncrementToken(span)},
                    {DecrementToken.Token, span => new DecrementToken(span)},
                    {InputToken.Token, span => new InputToken(span)},
                    {OutputToken.Token, span => new OutputToken(span)},
                };

            private bool IsToken(Span position, char input, out Token token)
            {
                Func<Span, Token> factory;
                if (TokenFactories.TryGetValue(input.ToString(), out factory))
                {
                    token = factory.Invoke(position);
                    return true;
                }

                token = null;
                return false;
            }
        }
    }