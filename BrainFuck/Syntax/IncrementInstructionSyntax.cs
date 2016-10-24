namespace BrainFuck.Syntax
{
    public sealed class IncrementInstructionSyntax : InstructionSyntaxTree
    {
        protected override void ExecuteOnce(ExecutionContext context)
        {
            context.Increment();
        }
    }
}