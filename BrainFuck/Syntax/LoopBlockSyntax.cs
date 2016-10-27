using System;
using System.Linq;

namespace BrainFuck.Syntax
{
    public sealed class LoopBlockSyntax : InstructionSyntaxTree
    {
        protected override void ExecuteOnce(ExecutionContext context)
        {
            throw new NotSupportedException();
        }

        public override void Execute(ExecutionContext context)
        {
            var iterations = 0;
            while(context.IsTrue())
            {
                foreach (var instruction in Children)
                {
                    (instruction as IInstruction)?.Execute(context);
                }

                if (iterations == context.Size) { throw new InfiniteLoopException(); }
                iterations++;
            }
        }

        public override string ToString() => $"{GetType()} ({Children.Count()} instructions)";
    }

    public class InfiniteLoopException : Exception { }
}