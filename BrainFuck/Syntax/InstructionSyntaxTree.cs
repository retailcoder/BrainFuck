using System.Linq;

namespace BrainFuck.Syntax
{
    public abstract class InstructionSyntaxTree : SyntaxTree, IInstruction
    {
        protected abstract void ExecuteOnce(ExecutionContext context);

        public virtual void Execute(ExecutionContext context)
        {
            // ReSharper disable once UnusedVariable; instruction is the same for every token unless method is overridden.
            foreach (var instruction in Tokens)
            {
                ExecuteOnce(context);
            }
        }

        public override string ToString() => $"{GetType().Name} x{Tokens.Count()}";
    }
}