using System.Collections.Generic;
using BrainFuck.Tokens;

namespace BrainFuck.Syntax
{
    public sealed class TriviaSyntax : SyntaxNode
    {
        public TriviaSyntax(Token token) : base(new List<Token> { token }) { }
    }
}