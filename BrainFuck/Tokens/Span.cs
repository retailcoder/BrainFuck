using System;

namespace BrainFuck.Tokens
{
    public struct Span : IEquatable<Span>, IComparable<Span>
    {
        public static Span Empty => new Span();

        public Span(int line, int column) : this(line, line, column, column) { }

        public Span(int startLine, int endLine, int startColumn, int endColumn)
        {
            StartLine = startLine;
            EndLine = endLine;
            StartColumn = startColumn;
            EndColumn = endColumn;
        }

        public int StartLine { get; }
        public int EndLine { get; }
        public int StartColumn { get; }
        public int EndColumn { get; }

        public int Lines => EndLine - StartLine + 1;

        public static bool operator ==(Span x, Span y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Span x, Span y)
        {
            return !x.Equals(y);
        }

        public bool Equals(Span other)
        {
            return other.StartLine == StartLine
                   && other.EndLine == EndLine
                   && other.StartColumn == StartColumn
                   && other.EndColumn == EndColumn;
        }

        public int CompareTo(Span other)
        {
            if (Equals(other))
            {
                return 0;
            }

            if (other.StartLine < StartLine || (other.StartLine == StartLine && other.StartColumn < StartColumn))
            {
                return -1;
            }

            return 1;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals((Span)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Compute(StartLine, EndLine, StartColumn, EndColumn);
        }

        public override string ToString()
        {
            return Lines == 1
                ? $"L{StartLine}C{StartColumn}"
                : $"L{StartLine}C{StartColumn}:L{EndLine}C{EndColumn}";
        }
    }
}