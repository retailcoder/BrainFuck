using System;
using BrainFuck.Tokens;

namespace BrainFuck
{
    public abstract class IllegalInstructionException : InvalidOperationException
    {
        protected IllegalInstructionException(string message, Token token, int position)
            : base($"Token: '{token}' at {token.Position} (index {position})\n" + message)
        {
            Token = token;
            Position = position;
        }

        public Token Token { get; }
        public int Position { get; }
    }
}