using System.Collections.Generic;
using BrainFuck.Tokens;

namespace BrainFuck.Syntax
{
    public sealed class InvalidInstructionSyntax : SyntaxNode
    {
        public InvalidInstructionSyntax(Token token) : base(new List<Token> { token }) { }
    }
}