namespace BrainFuck.Syntax
{
    public sealed class InputInstructionSyntax : InstructionSyntaxTree
    {
        protected override void ExecuteOnce(ExecutionContext context) => context?.Input();
    }
}