using System.Collections.Generic;

namespace Puzzle.Core
{
    public class Board
    {
        public readonly int Size;

        /// <summary>
        /// Lines and Columns
        /// </summary>
        public int[,] Fields { get; private set; }

        public Board(int size = 8)
        {
            Size = size;
            Fields = new int[Size,Size];
        }

        public bool CanPut(Piece piece, int lin, int col)
        {
            for (int l = 0; l < piece.Lines; l++)
            {
                for (int c = 0; c < piece.Cols; c++)
                {
                    if (piece.Fields[l, c] > 0)
                    {
                        if (lin + l < 0 || lin + l >= Size || col + c < 0 || col + c >= Size) return false; // Out of bounds
                        if (Fields[lin + l, col + c] > 0) return false; // The field is busy
                    }
                }
            }
            return true;
        }

        public bool CanPut(Piece piece)
        {
            for (int l = 0; l <= Size - piece.Lines; l++)
                for (int c = 0; c <= Size - piece.Cols; c++)
                    if (CanPut(piece, l, c))
                        return true;
            return false;
        }

        public void Put(Piece piece, int lin, int col)
        {
            for (int l = 0; l < piece.Lines; l++)
                for (int c = 0; c < piece.Cols; c++)
                    Fields[lin + l, col + c] |= piece.Fields[l, c];
        }

        public List<int> FullLines()
        {
            var fullLines = new List<int>();
            for (int l = 0; l < Size; l++)
            {
                var hasEmptyField = false;
                for (int c = 0; c < Size; c++)
                {
                    if(Fields[l,c] == 0)
                    {
                        hasEmptyField = true;
                        break;
                    }
                }
                if (!hasEmptyField)
                    fullLines.Add(l);
            }
            return fullLines;
        }

        public List<int> FullColumns()
        {
            var fullColumns = new List<int>();
            for (int c = 0; c < Size; c++)
            {
                var hasEmptyField = false;
                for (int l = 0; l < Size; l++)
                {
                    if (Fields[l, c] == 0)
                    {
                        hasEmptyField = true;
                        break;
                    }
                }
                if (!hasEmptyField)
                    fullColumns.Add(c);
            }
            return fullColumns;
        }

        public void ClearAll()
        {
            for (int l = 0; l < Size; l++)
                for (int c = 0; c < Size; c++)
                    Fields[l, c] = 0;
        }

        public void ClearLine(int lin)
        {
            for (int c = 0; c < Size; c++)
                Fields[lin , c] = 0;
        }

        public void ClearColumn(int col)
        {
            for (int l = 0; l < Size; l++)
                Fields[l, col] = 0;
        }
    }
}
