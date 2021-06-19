using System.Linq;

namespace Puzzle.Core
{
    public class Game
    {
        public long Score { get; private set; }
        public Board Board { get; private set; }
        public Piece[] Pieces { get; private set; }

        public Game()
        {
            Score = 0;
            Board = new Board();
            Pieces = NewPieces();
        }

        public Piece[] NewPieces() => new Piece[] { new Piece(), new Piece(), new Piece() };

        public bool UsePiece(int num, int lin, int col)
        {
            if (num < 0 || num >= Pieces.Length) return false;
            if (Pieces[num] == null) return false;
            if (!Board.CanPut(Pieces[num], lin, col)) return false;
            Board.Put(Pieces[num], lin, col);
            Score += Pieces[num].Points();
            Pieces[num] = null;
            CheckRound();
            return true;
        }

        private void CheckRound()
        {
            var lines = Board.FullLines();
            var columns = Board.FullColumns();
            var total = lines.Count + columns.Count;

            foreach (var line in lines) Board.ClearLine(line);
            foreach (var column in columns) Board.ClearColumn(column);

            Score += total * (total + 1) * 5;

            if (Pieces.All(p => p == null))
                Pieces = NewPieces();
        }

        public bool GameOver() => !Pieces.Where(p => p != null).Any(Board.CanPut);

    }
}
