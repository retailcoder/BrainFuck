using System;
using System.Linq;

namespace BrainFuck.Tokens
{
    /// <summary>
    /// A value representing a span of characters in a document.
    /// </summary>
    public struct Span : IEquatable<Span>, IComparable<Span>
    {
        /// <summary>
        /// Creates an empty span starting and ending at column 0 of line 0.
        /// </summary>
        public static Span Empty => new Span();

        /// <summary>
        /// Creates a single-character span at specified line and column.
        /// </summary>
        /// <param name="line">The start/end document line.</param>
        /// <param name="column">The start/end document column.</param>
        public Span(int line, int column) : this(line, column, line, column) { }

        /// <summary>
        /// Creates a span within the specified document coordinates.
        /// </summary>
        /// <param name="startLine">The starting document line.</param>
        /// <param name="startColumn">The starting document column.</param>
        /// <param name="endLine">The ending document line.</param>
        /// <param name="endColumn">The ending document column.</param>
        public Span(int startLine, int startColumn, int endLine, int endColumn)
        {
            StartLine = startLine;
            EndLine = endLine;
            StartColumn = startColumn;
            EndColumn = endColumn;
        }
        
        /// <summary>
        /// The starting line position.
        /// </summary>
        public int StartLine { get; }

        /// <summary>
        /// The ending line position.
        /// </summary>
        public int EndLine { get; }

        /// <summary>
        /// The starting column position.
        /// </summary>
        public int StartColumn { get; }

        /// <summary>
        /// The ending column position.
        /// </summary>
        public int EndColumn { get; }

        /// <summary>
        /// The number of lines in the document coordinates.
        /// </summary>
        public int Lines => EndLine - StartLine + 1;

        /// <summary>
        /// Extends the span to the next line. Returns a new Span with the same start position, ending on next line at column 0.
        /// </summary>
        public Span NextLine => new Span(StartLine, StartColumn, EndLine + 1, 0);

        /// <summary>
        /// Returns a new span starting on the next line, at column 0
        /// </summary>
        public Span NewLine => new Span(EndLine + 1, 0, EndLine + 1, 0);

        /// <summary>
        /// Extends the span to the next column. Returns a new Span with the same start position, ending on the same line at the next column.
        /// </summary>
        public Span NextColumn => new Span(StartLine, StartColumn, EndLine, EndColumn + 1);

        /// <summary>
        /// Returns a new span starting on the same line, at the next column.
        /// </summary>
        public Span Next => new Span(EndLine, EndColumn + 1);

        /// <summary>
        /// Returns a new span starting at the end of the current position.
        /// </summary>
        public Span End => new Span(EndLine, EndColumn);

        public Span Combine(Span other)
        {
            return Combine(this, other);
        }

        public static Span Combine(params Span[] a)
        {
            var min = a.Min();
            var max = a.Max();
            return new Span(min.StartLine, min.StartColumn, max.EndLine, max.EndColumn);
        }

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
                ? $"L{EndLine}C{EndColumn}"
                : $"L{StartLine}C{StartColumn}:L{EndLine}C{EndColumn}";
        }
    }
}