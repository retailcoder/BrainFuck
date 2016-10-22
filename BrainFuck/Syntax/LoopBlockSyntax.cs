using System.Collections.Generic;
using System.Linq;

namespace BrainFuck.Syntax
{
    public sealed class LoopBlockSyntax : SyntaxTree
    {
        public LoopBlockSyntax() : this(new List<SyntaxNode>()) { }
        public LoopBlockSyntax(IList<SyntaxNode> nodes) : base(nodes) { }

        public override void Execute(ExecutionContext context)
        {
            var index = context.Pointer;
            while (context.IsTrue(index))
            {
                var nodes = this.AsEnumerable<SyntaxNode>().ToList();
                foreach (var node in nodes)
                {
                    node.Execute(context);
                    if (!context.IsTrue(index))
                    {
                        break;
                    }
                }
            }
        }
    }
}