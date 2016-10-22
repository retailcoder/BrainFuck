using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrainFuck.Tokens;

namespace BrainFuck.Syntax
{
    public class SyntaxTree : SyntaxNode, IEnumerable<Token>, ICollection<SyntaxNode>
    {
        private readonly IList<SyntaxNode> _nodes;

        public SyntaxTree() : this(new List<SyntaxNode>()) { }

        public SyntaxTree(IList<SyntaxNode> nodes)
            : base(nodes.SelectMany(node => node).ToList())
        {
            _nodes = nodes;
        }

        IEnumerator<Token> IEnumerable<Token>.GetEnumerator()
        {
            return _nodes.SelectMany(node => node).AsEnumerable().GetEnumerator();
        }

        public IEnumerator<SyntaxNode> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(SyntaxNode item)
        {
            foreach (var token in item.Tokens)
            {
                Tokens.Add(token);
            }
            _nodes.Add(item);
        }

        public void Clear()
        {
            Tokens.Clear();
            _nodes.Clear();
        }

        public bool Contains(SyntaxNode item)
        {
            return _nodes.Contains(item);
        }

        public void CopyTo(SyntaxNode[] array, int arrayIndex)
        {
            _nodes.CopyTo(array, arrayIndex);
        }

        public bool Remove(SyntaxNode item)
        {
            return _nodes.Remove(item);
        }

        public int Count => _nodes.Count;
        public bool IsReadOnly => false;

        public override string ToString()
        {
            var builder = new StringBuilder();
            var tokens = _nodes.SelectMany(node => node.Tokens).OrderBy(token => token.Index);

            foreach(var token in tokens)
            {
                builder.Append(token);
            }
            return builder.ToString();
        }
    }
}