using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrainFuck.Tokens;

namespace BrainFuck.Syntax
{
    public abstract class SyntaxNode : IEnumerable<Token>
    {
        protected internal readonly IList<Token> Tokens;

        protected SyntaxNode(IList<Token> tokens)
        {
            Tokens = tokens;
        }

        public void AddToken(Token token)
        {
            Tokens.Add(token);
        }

        public Token this[int token] => Tokens[token];

        public Span Span => !Tokens.Any() 
            ? Span.Empty 
            : Tokens.First().Position.Combine(Tokens.Last().Position);


        IEnumerator<Token> IEnumerable<Token>.GetEnumerator()
        {
            return Tokens.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Tokens.GetEnumerator();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var token in Tokens.OrderBy(token => token.Index))
            {
                builder.Append(token);
            }
            return builder.ToString();
        }
    }

    public sealed class InvalidInstructionSyntax : SyntaxNode
    {
        public InvalidInstructionSyntax(Token token) : base(new List<Token> { token }) { }
    }

    public sealed class TriviaSyntax : SyntaxNode
    {
        public TriviaSyntax(Token token) : base(new List<Token> { token }) { }
    }

    public sealed class MoveLeftInstructionSyntax : SyntaxNode
    {
        public MoveLeftInstructionSyntax(Token token) : base(new List<Token> { token }) { }
    }

    public sealed class MoveRightInstructionSyntax : SyntaxNode
    {
        public MoveRightInstructionSyntax(Token token) : base(new List<Token> { token }) { }
    }

    public sealed class LoopBlockSyntax : SyntaxTree
    {
        public LoopBlockSyntax() : this(new List<SyntaxNode>()) { }
        public LoopBlockSyntax(IList<SyntaxNode> nodes) : base(nodes) { }
    }

    public sealed class IncrementInstructionSyntax : SyntaxNode
    {
        public IncrementInstructionSyntax(Token token) : base(new List<Token> { token }) { }
    }

    public sealed class DecrementInstructionSyntax : SyntaxNode
    {
        public DecrementInstructionSyntax(Token token) : base(new List<Token> { token }) { }
    }

    public sealed class InputInstructionSyntax : SyntaxNode
    {
        public InputInstructionSyntax(Token token) : base(new List<Token> { token }) { }
    }

    public sealed class OutputInstructionSyntax : SyntaxNode
    {
        public OutputInstructionSyntax(Token token) : base(new List<Token> { token }) { }
    }
}
