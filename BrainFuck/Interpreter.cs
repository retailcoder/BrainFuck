using BrainFuck.Syntax;

namespace BrainFuck
{
    public class Interpreter
    {
        private readonly ExecutionContext _context;

        public Interpreter(ExecutionContext context)
        {
            _context = context;
        }

        public void Execute(SyntaxTree tree)
        {
            foreach (var instruction in tree.Children)
            {
                (instruction as IInstruction)?.Execute(_context);
            }
        }
    }
}
