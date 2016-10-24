namespace BrainFuck.Syntax
{
    public sealed class OutputInstructionSyntax : InstructionSyntaxTree
    {
        protected override void ExecuteOnce(ExecutionContext context)
        {
            context.Output();
        }
    }
}