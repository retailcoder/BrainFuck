namespace BrainFuck.Syntax
{
    public sealed class MoveLeftInstructionSyntax : InstructionSyntaxTree
    {
        protected override void ExecuteOnce(ExecutionContext context) => context?.MoveLeft();
    }
}