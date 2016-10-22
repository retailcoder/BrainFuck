using System;
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

        public virtual void Execute(ExecutionContext context)
        {
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
}
