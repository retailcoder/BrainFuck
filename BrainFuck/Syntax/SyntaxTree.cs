using System.Collections;
using System.Collections.Generic;
using System.Text;
using BrainFuck.Tokens;

namespace BrainFuck.Syntax
{
    public class SyntaxTree : ICollection<Token>, ICollection<SyntaxTree>
    {
        private readonly IList<Token> _tokens = new List<Token>();

        public IEnumerable<Token> Tokens => _tokens;

        private readonly IList<SyntaxTree> _children = new List<SyntaxTree>();
        public IEnumerable<SyntaxTree> Children => _children;

        IEnumerator<Token> IEnumerable<Token>.GetEnumerator() => _tokens.GetEnumerator();
        IEnumerator<SyntaxTree> IEnumerable<SyntaxTree>.GetEnumerator() => _children.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<SyntaxTree>)this).GetEnumerator();

        public void Add(Token item)
        {
            _tokens.Add(item);
        }

        public void Add(SyntaxTree item)
        {
            foreach(var token in item.Tokens)
            {
                _tokens.Add(token);
            }
            _children.Add(item);
        }

        void ICollection<Token>.Clear()
        {
            _tokens.Clear();
        }

        void ICollection<SyntaxTree>.Clear()
        {
            _tokens.Clear();
            _children.Clear();
        }

        public bool Contains(Token item) => _tokens.Contains(item);
        public bool Contains(SyntaxTree item) => _children.Contains(item);

        public void CopyTo(Token[] array, int arrayIndex)
        {
            _tokens.CopyTo(array, arrayIndex);
        }

        public void CopyTo(SyntaxTree[] array, int arrayIndex)
        {
            _children.CopyTo(array, arrayIndex);
        }

        public bool Remove(Token item) => _tokens.Remove(item);
        public bool Remove(SyntaxTree item)
        {
            foreach (var token in item.Tokens)
            {
                _tokens.Remove(token);
            }
            return _children.Remove(item);
        }

        int ICollection<Token>.Count => _tokens.Count;
        int ICollection<SyntaxTree>.Count => _children.Count;

        bool ICollection<Token>.IsReadOnly => false;
        bool ICollection<SyntaxTree>.IsReadOnly => false;

        public override string ToString()
        {
            var builder = new StringBuilder();                                  
            foreach(var token in Tokens)
            {
                builder.Append(token);
            }
            return builder.ToString();
        }
    }
}