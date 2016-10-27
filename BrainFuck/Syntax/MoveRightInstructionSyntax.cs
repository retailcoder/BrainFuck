namespace BrainFuck.Syntax
{
    public sealed class MoveRightInstructionSyntax : InstructionSyntaxTree
    {
        protected override void ExecuteOnce(ExecutionContext context) => context?.MoveRight();
    }
}