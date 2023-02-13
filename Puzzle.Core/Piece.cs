using System;

namespace Puzzle.Core
{
    public sealed class Piece
    {
        public int[,] Fields { get; private set; }
        public int Lines => Fields.GetLength(0);
        public int Cols => Fields.GetLength(1);

        private static Random Rand = new Random((int)DateTime.Now.Ticks);

        public Piece()
        {
            var pos = Rand.Next(Models.Length);
            Fields = Models[pos];
        }

        public long Points()
        {
            var sum = 0L;
            for (int l = 0; l < Lines; l++)
                for (int c = 0; c < Cols; c++)
                    sum += Fields[l, c] > 0 ? 1 : 0;
            return sum;
        }

        public const int MaxLength = 5;
        private static readonly int[][,] Models = new int[][,]
        {
            new[,] {
                { 1 }
            },
            new[,] {
                { 1, 1}
            },
            new[,] {
                { 1 },
                { 1 }
            },
            new[,] {
                { 1, 0 },
                { 1, 1 }
            },
            new[,] {
                { 1, 1 },
                { 1, 0 }
            },
            new[,] {
                { 1, 1 },
                { 0, 1 }
            },
            new[,] {
                { 0, 1 },
                { 1, 1 }
            },
            new[,] {
                { 1, 1 },
                { 1, 1 }
            },
            new[,] {
                { 1, 1, 1 },
                { 1, 0, 0 }
            },
            new[,] {
                { 1, 0, 0 },
                { 1, 1, 1 }
            },
            new[,] {
                { 0, 0, 1 },
                { 1, 1, 1 }
            },
            new[,] {
                { 1, 1, 1 },
                { 0, 0, 1 }
            },
            new[,] {
                { 0, 1, 1 },
                { 1, 1, 0 }
            },
            new[,] {
                { 1, 1, 0 },
                { 0, 1, 1 }
            },
            new[,] {
                { 0, 1, 0 },
                { 1, 1, 1 }
            },
            new[,] {
                { 1, 1, 1 },
                { 0, 1, 0 }
            },
            new[,] {
                { 1, 0 },
                { 1, 0 },
                { 1, 1 }
            },
            new[,] {
                { 0, 1 },
                { 0, 1 },
                { 1, 1 }
            },
            new[,] {
                { 1, 1 },
                { 0, 1 },
                { 0, 1 }
            },
            new[,] {
                { 1, 1 },
                { 1, 0 },
                { 1, 0 }
            },
            new[,] {
                { 0, 1 },
                { 1, 1 },
                { 1, 0 }
            },
            new[,] {
                { 1, 0 },
                { 1, 1 },
                { 0, 1 }
            },
            new[,] {
                { 1, 1, 1 },
                { 1, 1, 1 },
                { 1, 1, 1 }
            },
            new[,] {
                { 1, 1, 1 }
            },
            new[,] {
                { 1, 1, 1, 1 }
            },
            new[,] {
                { 1, 1, 1, 1, 1 }
            },
            new[,] {
                { 1 },
                { 1 },
                { 1 }
            },
            new[,] {
                { 1 },
                { 1 },
                { 1 },
                { 1 }
            },
            new[,] {
                { 1 },
                { 1 },
                { 1 },
                { 1 },
                { 1 }
            }
        };

    }
}
