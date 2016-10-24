namespace BrainFuck.Syntax
{
    public interface IInstruction
    {
        void Execute(ExecutionContext context);
    }
}