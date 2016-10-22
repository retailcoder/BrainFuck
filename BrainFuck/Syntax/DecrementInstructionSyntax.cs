using System.Collections.Generic;
using BrainFuck.Tokens;

namespace BrainFuck.Syntax
{
    public sealed class DecrementInstructionSyntax : SyntaxNode
    {
        public DecrementInstructionSyntax(Token token) : base(new List<Token> { token }) { }

        public override void Execute(ExecutionContext context)
        {
            context.Decrement();
        }
    }
}