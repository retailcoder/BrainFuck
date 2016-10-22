using System.Collections.Generic;
using BrainFuck.Tokens;

namespace BrainFuck.Syntax
{
    public sealed class InputInstructionSyntax : SyntaxNode
    {
        public InputInstructionSyntax(Token token) : base(new List<Token> { token }) { }

        public override void Execute(ExecutionContext context)
        {
            context.Input();
        }
    }
}