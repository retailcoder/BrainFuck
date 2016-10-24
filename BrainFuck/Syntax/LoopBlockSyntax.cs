using System;

namespace BrainFuck.Syntax
{
    public sealed class LoopBlockSyntax : InstructionSyntaxTree
    {
        private const int MaxIterations = short.MaxValue;

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

                if (iterations == MaxIterations) { throw new InfiniteLoopException(); }
                iterations++;
            }
        }
    }

    public class InfiniteLoopException : Exception
    {
    }
}