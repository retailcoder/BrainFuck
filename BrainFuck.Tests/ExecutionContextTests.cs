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
            #region code and output strings
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
            var output = @"1
2
Fizz
4
Buzz
Fizz
7
8
Fizz
Buzz
11
Fizz
13
14
FizzBuzz
16
17
Fizz
19
Buzz
Fizz
22
23
Fizz
Buzz
26
Fizz
28
29
FizzBuzz
31
32
Fizz
34
Buzz
Fizz
37
38
Fizz
Buzz
41
Fizz
43
44
FizzBuzz
46
47
Fizz
49
Buzz
Fizz
52
53
Fizz
Buzz
56
Fizz
58
59
FizzBuzz
61
62
Fizz
64
Buzz
Fizz
67
68
Fizz
Buzz
71
Fizz
73
74
FizzBuzz
76
77
Fizz
79
Buzz
Fizz
82
83
Fizz
Buzz
86
Fizz
88
89
FizzBuzz
91
92
Fizz
94
Buzz
Fizz
97
98
Fizz
Buzz
".Replace("\r",string.Empty);
#endregion

            var lexer = new Lexer();
            var tokens = lexer.Tokenize(code).ToArray();
            var parser = new Parser();

            var tree = parser.Parse(tokens);
            var context = new ExecutionContext();
            var interpreter = new Interpreter(context);
            interpreter.Execute(tree);

            Assert.AreEqual(output, context.StdOut);
        }
    }
}