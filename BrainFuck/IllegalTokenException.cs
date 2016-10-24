using System;
using BrainFuck.Tokens;

namespace BrainFuck
{
    public class IllegalTokenException : Exception
    {
        public IllegalTokenException(Token token)
            : base($"Illegal or misplaced token: '{token.Text}' (type '{token.Type}')")
        {
            Token = token;
        }

        public Token Token { get; }
    }
}