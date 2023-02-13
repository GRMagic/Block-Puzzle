using Puzzle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Puzzle.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game game = new Game();
        private int? SelectedPiece = null;
        private List<Piece> PaintedPieces = new();

        private readonly Image[,] ImageControls;
        private readonly Canvas CanvasSelectedControls = new() { Visibility = Visibility.Hidden };

        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new("Assets/TileRed.png", UriKind.Relative)),
        };

        private Image[,] SetupBoardCanvas(Board gameBoard)
        {
            Image[,] imageControls = new Image[gameBoard.Size, gameBoard.Size];
            var cellSize = GameCanvas.Width / gameBoard.Size;
            
            for (var r = 0; r < gameBoard.Size; r++)
            {
                for (var c = 0; c < gameBoard.Size; c++) 
                {
                    var imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize,
                    };
                    Canvas.SetTop(imageControl, r * cellSize);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }

            GameCanvas.Children.Add(CanvasSelectedControls);

            return imageControls;
        }

        private void SetupPieceCanvas(Canvas canvas, int index)
        {
            var piece = game.Pieces[index];
            canvas.Background = new SolidColorBrush(Color.FromArgb(0x50, 0x10, 0x10, 0x10));
            if (index == this.SelectedPiece)
                canvas.Background = new SolidColorBrush(Color.FromArgb(0x50, 0xf0, 0x10, 0x10));

            canvas.Children.Clear();
            if (piece == null) return;

            var cellSize = canvas.Width / Piece.MaxLength;
            var startTop = canvas.Height/2 - (cellSize * piece.Lines)/2;
            var startLeft = canvas.Width / 2 - (cellSize * piece.Cols) / 2;

            for (var r = 0; r < piece.Lines; r++)
            {
                for (var c = 0; c < piece.Cols; c++)
                {
                    if (piece.Fields[r, c] > 0)
                    {
                        var image = new Image()
                        {
                            Width = cellSize,
                            Height = cellSize,
                            Source = tileImages[piece.Fields[r, c]]
                        };
                        Canvas.SetTop(image, r * cellSize + startTop);
                        Canvas.SetLeft(image, c * cellSize + startLeft);
                        canvas.Children.Add(image);
                    }
                }
            }
        }

        private void SetupSelectedPieceCanvas()
        {
            CanvasSelectedControls.Children.Clear();
            if (SelectedPiece == null) return;
            var piece = game.Pieces[SelectedPiece.Value];

            var cellSize = GameCanvas.Width / game.Board.Size;
            CanvasSelectedControls.Height = cellSize * piece.Lines;
            CanvasSelectedControls.Width = cellSize * piece.Cols;


            for (var r = 0; r < piece.Lines; r++)
            {
                for (var c = 0; c < piece.Cols; c++)
                {
                    if (piece.Fields[r, c] > 0)
                    {
                        var image = new Image()
                        {
                            Width = cellSize,
                            Height = cellSize,
                            Source = tileImages[piece.Fields[r, c]]
                        };
                        Canvas.SetTop(image, r * cellSize);
                        Canvas.SetLeft(image, c * cellSize);
                        CanvasSelectedControls.Children.Add(image);
                    }
                }
            }
        }

        private void DrawGrid(Board gameBoard)
        {
            for (var r = 0; r < gameBoard.Size; r++)
                for (var c = 0; c < gameBoard.Size; c++)
                    ImageControls[r, c].Source = tileImages[gameBoard[r,c]];
        }

        private void DrawSelectPiece(Point point)
        {
            Canvas.SetTop(CanvasSelectedControls, point.Y - CanvasSelectedControls.Height / 2);
            Canvas.SetLeft(CanvasSelectedControls, point.X - CanvasSelectedControls.Width / 2);
        }

        private void Draw()
        {
            PaintNewPieces();
            DrawGrid(game.Board);
            SetupPieceCanvas(PieceACanvas, 0);
            SetupPieceCanvas(PieceBCanvas, 1);
            SetupPieceCanvas(PieceCCanvas, 2);

            this.ScoreText.Text = $"Score: {game.Score}";
            this.FinalScoreText.Text = this.ScoreText.Text;
        }

        private void PaintNewPieces()
        {
            var already = game.Pieces.Where(p => p != null).Any(PaintedPieces.Contains);
            if (!already)
            {
                PaintPiece(game.Pieces[0]);
                PaintPiece(game.Pieces[1]);
                PaintPiece(game.Pieces[2]);

                PaintedPieces.Clear();
                PaintedPieces.AddRange(game.Pieces);
            }
        }

        private void PaintPiece(Piece piece)
        {
            if (piece == null) return;
            var color = Random.Shared.Next(1, 7);
            for (var r = 0; r < piece.Lines; r++)
            {
                for (var c = 0; c < piece.Cols; c++)
                {
                    if (piece.Fields[r, c] > 0)
                        piece.Fields[r, c] = color;
                }
            }
        }

        private Cursor CursorDefault = null;

        public MainWindow()
        {
            InitializeComponent();
            ImageControls = SetupBoardCanvas(game.Board);
        }

        private void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            game = new();
            Draw();
            GameOverMenu.Visibility = Visibility.Hidden;
        }

        private void GameCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            var point = Mouse.GetPosition(GameCanvas);
            DrawSelectPiece(point);
        }

        private void PieceACanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Select(0);
            Draw();
        }

        private void PieceBCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Select(1);
            Draw();
        }

        private void PieceCCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Select(2);
            Draw();
        }

        private void Select(int piece)
        {
            if (SelectedPiece == piece)
            {
                SelectedPiece = null;
            }
            else
            {
                SelectedPiece = piece;
            }
            SetupSelectedPieceCanvas();
        }

        private void GameCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (SelectedPiece == null) return;

            var point = e.GetPosition(GameCanvas);
            var y = point.Y - CanvasSelectedControls.Height / 2;
            var x = point.X - CanvasSelectedControls.Width / 2;

            var cellSize = GameCanvas.Width / game.Board.Size;
            int lin = (int)((y + cellSize / 2) / cellSize);
            int col = (int)((x + cellSize / 2) / cellSize);

            UsePiece(lin, col);
        }

        private void UsePiece(int lin, int col)
        {
            if (SelectedPiece == null) return;

            game.UsePiece(SelectedPiece.Value, lin, col);
            Select(SelectedPiece.Value);
            Draw();

            if (game.GameOver())
                GameOverMenu.Visibility = Visibility.Visible;
        }

        private void GameCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            CanvasSelectedControls.Visibility = Visibility.Visible;
        }

        private void GameCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            CanvasSelectedControls.Visibility = Visibility.Hidden;
        }
    }
}
