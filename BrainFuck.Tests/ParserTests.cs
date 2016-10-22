using System.Linq;
using BrainFuck.Syntax;
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
            var tree = Parser.Parse(new Token[] {new TriviaToken(Span.Empty, 0, "test")});
            Assert.AreEqual(1, tree.OfType<TriviaSyntax>().Count());
        }

        [TestMethod]
        public void GivenBeginLoopToken_IssuesNestedSyntaxTree()
        {
            var tree = Parser.Parse(new Token[]
            {
                new BeginLoopToken(Span.Empty, 0),
                new IncrementToken(Span.Empty.Next, 1),
                new DecrementToken(Span.Empty.Next.Next, 2),
                new EndLoopToken(Span.Empty.Next.Next.Next, 3), 
            });

            Assert.AreEqual(1, tree.Count);
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
            var tokens = Lexer.Tokenize(code).ToArray();
            var tree = Parser.Parse(tokens);
            
            Assert.AreEqual(code, tree.ToString());
        }

        [TestMethod]
        public void ExecutesCodeReviewPost57382()
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
            var tokens = Lexer.Tokenize(code).ToArray();
            var tree = Parser.Parse(tokens);

            var interpreter = new Interpreter();
            interpreter.Execute(tree);
        }
    }
}