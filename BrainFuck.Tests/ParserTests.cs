using System.Linq;
using BrainFuck.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrainFuck.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void GivenTriviaToken_IssuesTriviaSyntaxNode()
        {
            var parser = new Parser();
            var tree = parser.Parse(new Token[] {new TriviaToken(Span.Empty, 0, "test")});
            Assert.AreEqual(1, tree.Children.Count());
            Assert.AreEqual(1, tree.Tokens.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalTokenException))]
        public void GivenIllegalToken_ThrowsIllegalTokenException()
        {
            var parser = new Parser();
            parser.Parse(new Token[] {new EndLoopToken(Span.Empty, 0)});
        }

        [TestMethod]
        public void GivenBeginLoopToken_IssuesNestedSyntaxTree()
        {
            var parser = new Parser();
            var tree = parser.Parse(new Token[]
            {
                new BeginLoopToken(Span.Empty, 0),
                new IncrementToken(Span.Empty.Next, 1),
                new EndLoopToken(Span.Empty.Next.Next.Next, 2), 
            });

            Assert.AreEqual(1, tree.Children.Count());
        }

        [TestMethod]
        public void GivenConsecutiveInstructions_RegroupsSyntaxTree()
        {
            var parser = new Parser();
            var tree = parser.Parse(new Token[]
            {
                new IncrementToken(Span.Empty.OffSet(0), 0),
                new IncrementToken(Span.Empty.OffSet(1), 1),
                new IncrementToken(Span.Empty.OffSet(2), 2),
                new IncrementToken(Span.Empty.OffSet(3), 3), 
            });

            Assert.AreEqual(1, tree.Children.Count());
            Assert.AreEqual(4, tree.Tokens.Count());
        }

        [TestMethod]
        public void GivenConsecutiveInstructions_RegroupsSyntaxTreeByTokenType()
        {
            var parser = new Parser();
            var tree = parser.Parse(new Token[]
            {
                new IncrementToken(Span.Empty.OffSet(0), 0),
                new IncrementToken(Span.Empty.OffSet(1), 1),
                new DecrementToken(Span.Empty.OffSet(2), 2),
                new DecrementToken(Span.Empty.OffSet(3), 3),
            });

            Assert.AreEqual(2, tree.Children.Count());
        }

        [TestMethod]
        public void ParsesAlphabet()
        {
            var code = @"++++++[>++++++++++>++++<<-]>+++++>++[-<.+>]";

            var lexer = new Lexer();
            var tokens = lexer.Tokenize(code).ToArray();

            var parser = new Parser();
            var tree = parser.Parse(tokens);

            Assert.AreEqual(code.Length, tree.Tokens.Count());
        }

        [TestMethod]
        public void ParsesCodeReviewPost57382()
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
            var tokens = lexer.Tokenize(code).ToArray();
            var parser = new Parser();

            var tree = parser.Parse(tokens);
            Assert.AreEqual(code, tree.ToString());
        }

        [TestMethod]
        public void TokenIndexMatchesInputPosition()
        {
            var tokens = new Token[]
            {
                new IncrementToken(Span.Empty.OffSet(0), 0),
                new BeginLoopToken(Span.Empty.OffSet(1), 1),
                new DecrementToken(Span.Empty.OffSet(2), 2),
                new EndLoopToken(Span.Empty.OffSet(3), 3),
                new OutputToken(Span.Empty.OffSet(4), 4),
            };

            var parser = new Parser();
            var tree = parser.Parse(tokens);

            Assert.AreEqual(tokens.Length, tree.Tokens.Count());
            Assert.IsTrue(tokens.SequenceEqual(tree.Tokens));
        }
    }
}