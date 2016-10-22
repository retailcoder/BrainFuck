using System;
using BrainFuck.Syntax;

namespace BrainFuck
{
    public class Interpreter
    {
        public void Execute(SyntaxTree tree)
        {
            var context = new ExecutionContext();
            foreach (var node in tree)
            {
                node.Execute(context);
            }
        }
    }
}
