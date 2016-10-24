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
            var code = "ABC";

            var lexer = new Lexer();
            var tokens = lexer.Tokenize(code);

            Assert.AreEqual(1, tokens.Count());
        }

        [TestMethod]
        public void TriviaTokenSpansTriviaLength()
        {
            var code = "ABC";

            var lexer = new Lexer();
            var token = lexer.Tokenize(code).First();

            Assert.AreEqual(new Span(0, 0, 0, 2), token.Position);
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

        [TestMethod]
        public void TokenizesCodeReviewPost57382()
        {
            // "fizzbuzz in brainfuck"
            var code = @"
++++++++++[>++++++++++<-]>>++++++++++>->>>>>>>>>>>>>>>>-->+++++++[->++
++++++++ <]>[->+> +> +> +<<<<]++ +>> +++>>> ++++++++[-< ++++< ++++< ++++>>>]++++
+[-< ++++< ++++>>] >> --> ++++++[->+++++++++++<] >[->+> +> +> +<<<<]++++ +>> +> ++
++++> ++++++> ++++++++[-< ++++< ++++< ++++>>>]++++++[-< +++< +++< +++>>>] >> -->
---+[-< +] -<[+[->+] -<<->>> +>[-]++[--> ++]-- > +++[---++[--< ++]---- >> -< +>[+
+++[----< ++++]--[>]++[--> ++]-- <] > ++[--+[-< +]->>[-]++++ +[----> ++++]-- >[
->+<] >>[.>]++[--> ++]]-- > +++]-- - +[-< +]->> -[+>>> +[-< +]->>> ++++++++++<<[-
> +> -[> +>>] >[+[-< +>] > +>>] <<<<<<] >>[-] >>> ++++++++++<[->-[> +>>] >[+[-< +>] >
+>>] <<<<<] >[-] >>[> ++++++[-< ++++++++>] <.<< +> +>[-]] <[<[->-<]++++++[->+++
+++++<] >.[-]] << ++++++[-< ++++++++>] <.[-] <<[-< +>] +[-< +]->>] +[-] <<<.>>> +[
-< +] -<<]
";
            var lexer = new Lexer();
            var tokens = lexer.Tokenize(code);

            Assert.AreEqual(code.Count(c => c == IncrementToken.Token[0]), tokens.OfType<IncrementToken>().Count());
        }

        [TestMethod]
        public void SpanStartsAtL0C0()
        {
            var code = ".";

            var lexer = new Lexer();
            var token = lexer.Tokenize(code).Single();

            var span = token.Position;
            var expected = new Span(0, 0, 0, 0);
            Assert.AreEqual(expected, span);
        }

        [TestMethod]
        public void NewLineCharStartsNewLine()
        {
            var code = "ABC" + Environment.NewLine;

            var lexer = new Lexer();
            var tokens = lexer.Tokenize(code).ToArray();
            var combined = Span.Combine(tokens.Select(t => t.Position).ToArray());

            Assert.AreEqual(2, combined.Lines);
        }

        [TestMethod]
        public void NextNewLineCharIsOnNextLine()
        {
            var code = "ABC" + Environment.NewLine + Environment.NewLine;

            var lexer = new Lexer();
            var tokens = lexer.Tokenize(code).ToArray();
            var combined = Span.Combine(tokens.Select(t => t.Position).ToArray());

            Assert.AreEqual(3, combined.Lines);
        }

        [TestMethod]
        public void MultilineSpanReflectsNumberOfLines()
        {
            var code = "line1" + Environment.NewLine + "line2";

            var lexer = new Lexer();
            var token = lexer.Tokenize(code).Single();
            var expected = new Span(0, 0, 1, 4);
            Assert.AreEqual(expected, token.Position);
        }

        [TestMethod]
        public void TokenSpanReflectsTokenPosition()
        {
            var code = "+++++>";

            var lexer = new Lexer();
            var token = lexer.Tokenize(code).OfType<MoveRightToken>().Single();
            var expected = new Span(0, 5);
            Assert.AreEqual(expected, token.Position);
        }

        [TestMethod]
        public void TokenSpanReflectsTokenPositionInMultilineInput()
        {
            var code = "+++" + Environment.NewLine + "some trivia" + Environment.NewLine + "+++.<<<";

            var lexer = new Lexer();
            var token = lexer.Tokenize(code).OfType<OutputToken>().Single();
            var expected = new Span(2, 3);
            Assert.AreEqual(expected, token.Position);
        }

        [TestMethod]
        public void TriviaTokenSpanReflectsPositionInMultilineInput()
        {
            var code = "+" + Environment.NewLine + "abc" + Environment.NewLine + "+.<";

            var lexer = new Lexer();
            var token = lexer.Tokenize(code).OfType<TriviaToken>().Single();
            var expected = new Span(0, 1, 2, 0);

            Assert.AreEqual(expected, token.Position);
        }
    }
}
