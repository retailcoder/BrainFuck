using System.Collections.Generic;
using BrainFuck.Tokens;

namespace BrainFuck.Syntax
{
    public sealed class IncrementInstructionSyntax : SyntaxNode
    {
        public IncrementInstructionSyntax(Token token) : base(new List<Token> { token }) { }

        public override void Execute(ExecutionContext context)
        {
            context.Increment();
        }
    }
}