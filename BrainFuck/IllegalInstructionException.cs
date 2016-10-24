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

    public abstract class IllegalMoveInstructionException : IllegalInstructionException
    {
        protected IllegalMoveInstructionException(string message, Token token, int position)
            : base(message, token, position)
        {
        }
    }

    public class IllegalMoveLeftInstructionException : IllegalMoveInstructionException
    {
        public IllegalMoveLeftInstructionException(Token token, int position) 
            : base("Instruction moved pointer before index 0.", token, position) { }
    }

    public class IllegalMoveRightInstructionException : IllegalMoveInstructionException
    {
        public IllegalMoveRightInstructionException(Token token, int position) 
            : base("Instruction moved pointer past the last available index.", token, position) { }
    }

}