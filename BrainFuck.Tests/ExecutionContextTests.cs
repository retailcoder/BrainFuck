using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrainFuck.Tests
{
    [TestClass]
    public class ExecutionContextTests
    {
        [TestMethod]
        public void ExecutesIncrement()
        {
            var context = new ExecutionContext();
            context.Increment();
            Assert.AreEqual(1, context.Value);
        }

        [TestMethod]
        public void ExecutesDecrement()
        {
            var context = new ExecutionContext();
            context.Decrement();
            Assert.AreEqual(-1, context.Value);
        }

        [TestMethod]
        public void ExecutesMoveLeft()
        {
            const byte size = byte.MaxValue;
            var context = new ExecutionContext(size);
            context.MoveLeft();
            Assert.AreEqual(size, context.Pointer);
        }

        [TestMethod]
        public void ExecutesMoveRight()
        {
            var context = new ExecutionContext();
            context.MoveRight();
            Assert.AreEqual(1, context.Pointer);
        }

        [TestMethod]
        public void ExecutesAlphabet()
        {
            var code = @"++++++[>++++++++++>++++<<-]>+++++>++[-<.+>]";

            var lexer = new Lexer();
            var tokens = lexer.Tokenize(code).ToArray();

            var parser = new Parser();
            var tree = parser.Parse(tokens);
            var context = new ExecutionContext();
            var interpreter = new Interpreter(context);
            interpreter.Execute(tree);

            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ", context.StdOut);
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
            var lexer = new Lexer();
            var tokens = lexer.Tokenize(code).ToArray();
            var parser = new Parser();

            var tree = parser.Parse(tokens);
            var context = new ExecutionContext();
            var interpreter = new Interpreter(context);
            interpreter.Execute(tree);

            Assert.AreNotEqual(string.Empty, context.StdOut);
        }
    }
}