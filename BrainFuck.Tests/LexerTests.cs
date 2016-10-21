using System;
using System.Linq;
using BrainFuck.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrainFuck.Tests
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void TokenizesTriviaToken()
        {
            var code = "_"; // invalid token
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);

            Assert.AreEqual(1, tokens.Count());
        }

        [TestMethod]
        public void TokenizesMultipleCharacterTriviaToken()
        {
            var code = "any non token character is ignored in BrainFuck";
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);

            Assert.AreEqual(1, tokens.Count());
        }

        [TestMethod]
        public void TokenizesMultilineTriviaToken()
        {
            var code = Environment.NewLine + "any non token character " +
                       Environment.NewLine + "is ignored in BrainFuck";
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);

            Assert.AreEqual(1, tokens.Count());
        }

        [TestMethod]
        public void TokenizesMoveLeftToken()
        {
            var code = MoveLeftToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);

            Assert.AreEqual(1, tokens.OfType<MoveLeftToken>().Count());
        }

        [TestMethod]
        public void TokenizesMoveRightToken()
        {
            var code = MoveRightToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);

            Assert.AreEqual(1, tokens.OfType<MoveRightToken>().Count());
        }

        [TestMethod]
        public void TokenizesIncrementToken()
        {
            var code = IncrementToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);

            Assert.AreEqual(1, tokens.OfType<IncrementToken>().Count());
        }

        [TestMethod]
        public void TokenizesDecrementToken()
        {
            var code = DecrementToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);

            Assert.AreEqual(1, tokens.OfType<DecrementToken>().Count());
        }

        [TestMethod]
        public void TokenizesBeginLoopToken()
        {
            var code = BeginLoopToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);

            Assert.AreEqual(1, tokens.OfType<BeginLoopToken>().Count());
        }

        [TestMethod]
        public void TokenizesEndLoopToken()
        {
            var code = EndLoopToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);

            Assert.AreEqual(1, tokens.OfType<EndLoopToken>().Count());
        }

        [TestMethod]
        public void TokenizesInputToken()
        {
            var code = InputToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);

            Assert.AreEqual(1, tokens.OfType<InputToken>().Count());
        }

        [TestMethod]
        public void TokenizesOutputToken()
        {
            var code = OutputToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);

            Assert.AreEqual(1, tokens.OfType<OutputToken>().Count());
        }

        [TestMethod]
        public void TokenizesMixedTokens()
        {
            var code = "this is a comment" + Environment.NewLine +
                       IncrementToken.Token + IncrementToken.Token + OutputToken.Token;

            var lexer = new Lexer();
            var tokens = lexer.Tokenize(code).ToList();

            Assert.AreEqual(1, tokens.OfType<TriviaToken>().Count());
            Assert.AreEqual(2, tokens.OfType<IncrementToken>().Count());
            Assert.AreEqual(1, tokens.OfType<OutputToken>().Count());
        }

        [TestMethod]
        public void InstructionTokenLength()
        {
            var code = IncrementToken.Token;
            var lexer = new Lexer();
            var tokens = lexer.Tokenize(code);

            var token = tokens.OfType<IncrementToken>().FirstOrDefault();
            Assert.IsNotNull(token);
            Assert.AreEqual(1, token.Text.Length);
        }

        [TestMethod]
        public void TriviaTokenLength()
        {
            var code = "expected length is 21";
            var lexer = new Lexer();
            var tokens = lexer.Tokenize(code);

            var token = tokens.OfType<TriviaToken>().FirstOrDefault();
            Assert.IsNotNull(token);
            Assert.AreEqual(code.Length, token.Text.Length);
        }


        [TestMethod]
        public void TokensRoundTripToString()
        {
            var code = "this is a comment" + Environment.NewLine +
                       IncrementToken.Token + IncrementToken.Token + OutputToken.Token;

            var lexer = new Lexer();
            var tokens = string.Join(string.Empty, lexer.Tokenize(code).Select(token => token.Text).ToList());

            Assert.AreEqual(code, tokens);
        }

        [TestMethod]
        public void MoveLeftTokenRoundTripsToString()
        {
            var code = MoveLeftToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);
            Assert.AreEqual(code, tokens.Single().Text);
        }

        [TestMethod]
        public void MoveRightTokenRoundTripsToString()
        {
            var code = MoveRightToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);
            Assert.AreEqual(code, tokens.Single().Text);
        }

        [TestMethod]
        public void IncrementTokenRoundTripsToString()
        {
            var code = IncrementToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);
            Assert.AreEqual(code, tokens.Single().Text);
        }

        [TestMethod]
        public void DecrementTokenRoundTripsToString()
        {
            var code = DecrementToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);
            Assert.AreEqual(code, tokens.Single().Text);
        }

        [TestMethod]
        public void BeginLoopTokenRoundTripsToString()
        {
            var code = BeginLoopToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);
            Assert.AreEqual(code, tokens.Single().Text);
        }

        [TestMethod]
        public void EndLoopTokenRoundTripsToString()
        {
            var code = EndLoopToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);
            Assert.AreEqual(code, tokens.Single().Text);
        }

        [TestMethod]
        public void InputTokenRoundTripsToString()
        {
            var code = InputToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);
            Assert.AreEqual(code, tokens.Single().Text);
        }

        [TestMethod]
        public void OutputTokenRoundTripsToString()
        {
            var code = OutputToken.Token;
            var lexer = new Lexer();

            var tokens = lexer.Tokenize(code);
            Assert.AreEqual(code, tokens.Single().Text);
        }

        [TestMethod]
        public void TokenPositionCountsLines()
        {
            var code = "this is a comment" + Environment.NewLine;

            var lexer = new Lexer();
            var tokens = lexer.Tokenize(code).ToList();

            Assert.AreEqual(2, tokens.OfType<TriviaToken>().First().Position.Lines);
        }
    }
}
