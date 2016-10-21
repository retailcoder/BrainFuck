using System.Collections;
using System.Collections.Generic;
using System.Text;
using BrainFuck.Tokens;

namespace BrainFuck.Syntax
{
    public class SyntaxTree
    {
        
    }

    public abstract class SyntaxNode
    {
        private readonly Token[] _tokens;

        protected SyntaxNode(Token[] tokens)
        {
            _tokens = tokens;
        }

        public Token this[int token] => _tokens[token];

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (var i = 0; i < _tokens.Length; i++)
            {
                builder.Append(_tokens[i]);
            }
            return builder.ToString();
        }
    }

    public sealed class TriviaSyntax : SyntaxNode
    {
        public TriviaSyntax(Token[] tokens) 
            : base(tokens)
        {
        }
    }

    public sealed class MoveLeftInstructionSyntax : SyntaxNode
    {
        public MoveLeftInstructionSyntax(Token[] tokens) 
            : base(tokens)
        {
        }
    }

    public sealed class MoveRightInstructionSyntax : SyntaxNode
    {
        public MoveRightInstructionSyntax(Token[] tokens)
            : base(tokens)
        {
        }
    }

    public sealed class LoopBlockSyntax : SyntaxNode, ICollection<SyntaxNode>
    {
        public LoopBlockSyntax(Token[] tokens) 
            : base(tokens)
        {
            _children = new List<SyntaxNode>();
        }

        private readonly List<SyntaxNode> _children;

        public IEnumerator<SyntaxNode> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(SyntaxNode item)
        {
            _children.Add(item);
        }

        public void Clear()
        {
            _children.Clear();
        }

        public bool Contains(SyntaxNode item)
        {
            return _children.Contains(item);
        }

        public void CopyTo(SyntaxNode[] array, int arrayIndex)
        {
            _children.CopyTo(array, arrayIndex);
        }

        public bool Remove(SyntaxNode item)
        {
            return _children.Remove(item);
        }

        public int Count => _children.Count;
        public bool IsReadOnly => false;


    }

    public sealed class IncrementSyntax : SyntaxNode
    {
        public IncrementSyntax(Token[] tokens) 
            : base(tokens)
        {
        }
    }

    public sealed class DecrementSyntax : SyntaxNode
    {
        public DecrementSyntax(Token[] tokens) 
            : base(tokens)
        {
        }
    }

    public sealed class InputInstructionSyntax : SyntaxNode
    {
        public InputInstructionSyntax(Token[] tokens) 
            : base(tokens)
        {
        }
    }

    public sealed class OutputInstructionSyntax : SyntaxNode
    {
        public OutputInstructionSyntax(Token[] tokens) 
            : base(tokens)
        {
        }
    }
}
