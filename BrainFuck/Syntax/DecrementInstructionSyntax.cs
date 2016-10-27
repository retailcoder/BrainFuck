namespace BrainFuck.Syntax
{
    public sealed class DecrementInstructionSyntax : InstructionSyntaxTree
    {
        protected override void ExecuteOnce(ExecutionContext context) => context?.Decrement();
    }
}