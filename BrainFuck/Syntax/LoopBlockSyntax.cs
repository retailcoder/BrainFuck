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
            while(true)
            {
                foreach (var instruction in Children)
                {
                    (instruction as IInstruction)?.Execute(context);
                }

                if (!context.IsTrue())
                {
                    return;
                }

                if (iterations == MaxIterations)
                {
                    throw new InfiniteLoopException();
                }
                iterations++;
            }
        }
    }

    public class InfiniteLoopException : Exception
    {
    }
}