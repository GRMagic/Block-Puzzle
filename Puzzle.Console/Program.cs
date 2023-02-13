using Puzzle.Core;

namespace Puzzle.Console
{
    class Program
    {
        static void Main(string[] args)
        {   
            System.Console.Title = "Puzzle!";
            var game = new Game();
            do
            {
                Print(game);

                System.Console.Write("\nChoice the piece (a,b,c): ");
                var num = System.Console.ReadKey().KeyChar - 'a';
                System.Console.Write("\nChoice the line (1-8): ");
                var lin = System.Console.ReadKey().KeyChar - '1';
                System.Console.Write("\nChoice the column (1-8): ");
                var col = System.Console.ReadKey().KeyChar - '1';

                game.UsePiece(num, lin, col);
            } while (!game.GameOver());
            Print(game);
        }

        public static void Print(Game game)
        {
            System.Console.Clear();
            System.Console.WriteLine($"Score: {game.Score}");
            Print(game.Board);
            Print(game.Pieces);
        }

        public static void Print(Board board)
        {
            System.Console.Write(" ");
            for (int l = -1; l < board.Size; l++)
            {
                if (l >= 0) System.Console.Write(l+1);
                for (int c = 0; c < board.Size; c++)
                {
                    if (l < 0) System.Console.Write(" " + (c+1));
                    else System.Console.Write(board[l, c] > 0 ? " ■" : " o");
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine();
        }

        public static void Print(Piece[] pieces)
        {
            var key = 'a';
            for(int i=0; i<pieces.Length; i++)
            {
                System.Console.WriteLine((char)(key + i) + ")");
                if (pieces[i] == null) continue;
                Print(pieces[i]);
            }   
        }

        public static void Print(Piece piece)
        {
            for (int l = 0; l < piece.Lines; l++)
            {
                for (int c = 0; c < piece.Cols; c++)
                {
                    System.Console.Write(piece.Fields[l, c] > 0 ? " ■" : "  ");
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine();
        }
    }
}
