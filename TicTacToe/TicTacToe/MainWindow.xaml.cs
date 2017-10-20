using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
using System.Threading.Tasks;

namespace TicTacToe
{
    //Command definitions.
    namespace MyCommands
    {
        public class Commands
        {
            public static RoutedUICommand startCommand = new RoutedUICommand("Start command", "startCommand", typeof(Commands));
            public static RoutedUICommand restartCommand = new RoutedUICommand("Restart command", "restartCommand", typeof(Commands));
            public static RoutedUICommand exitCommand = new RoutedUICommand("Exit command", "exitCommand", typeof(Commands));
            public static RoutedUICommand helpCommand = new RoutedUICommand("Help command", "helpCommand", typeof(Commands));
            public static RoutedUICommand rulesCommand = new RoutedUICommand("Rules command", "rulesCommand", typeof(Commands));
            public static RoutedUICommand shortCutsCommand = new RoutedUICommand("Shortcuts command", "shortCutsCommand", typeof(Commands));
        }
    }
    public partial class MainWindow : Window
    {
        private Rectangle[,] rectangles;
        private int[,] moves;
        private static bool CIRCLE = true, CROSS = false;
        private static int X = 1, O = 4, XWIN = 3, OWIN = 12;
        private static int HEADS = 1, TAILS = 2;
        private static bool ONEPLAYER = true, TWOPLAYER = false;
        private bool turn;
        private bool inGame;
        private bool mode;
        private int numMoves;
        private string player1, player2;
        public MainWindow()
        {
            InitializeComponent();

            //Defining mouse event handler.
            Mouse.Capture(this);
            MouseLeftButtonDown += OnMouseLeftButtonDown;
            InitializeBoard();        

        }

        private void InitializeBoard()
        {
            numMoves = 0;
            radioButtonOnePlayer.IsEnabled = true;
            radioButtonTwoPlayers.IsEnabled = true;
            inGame = false;
            buttonStart.Content = "Start";
            buttonStart.Command = MyCommands.Commands.startCommand;
            menuItemStart.IsEnabled = true;
            imageCoin.Source = null;
            labelAnnounce.Content = "Hit start to begin!";

            rectangles = new Rectangle[3, 3];
            moves = new int[3, 3];
            gridBoard.Children.Clear();
            gridBoard.RowDefinitions.Clear();
            gridBoard.ColumnDefinitions.Clear();

            //Creating 3 rows and columns.
            for (int numGridDefinitions = 0; numGridDefinitions < 3; numGridDefinitions++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                RowDefinition rowDef = new RowDefinition();
                gridBoard.ColumnDefinitions.Add(colDef);
                gridBoard.RowDefinitions.Add(rowDef);
            }


            for (int col = 0; col < 3; col++)
            {              
                for (int row = 0; row < 3; row++)
                {
                    DrawBoard(col, row);
                }
            }
        }

        private void DrawBoard(int col, int row)
        {
            int rightThickness = 0;
            int topThickness = 0;
            int leftThickness = (col == 0 || col == 1) ? 4 : 0;
            int botThickness = (row == 0 || row == 1) ? 4 : 0;

            //Creating border around each tic tac toe rectangle.
            Brush brush = new SolidColorBrush(Colors.White);
            Border border = new Border();
            border.Background = brush;
            border.BorderThickness = new Thickness(rightThickness, topThickness, leftThickness, botThickness);           
            Grid.SetColumn(border, col);
            Grid.SetRow(border, row);

            //Adding a rectangle to keep track of points inside each rectangle.
            Rectangle rectangle = new Rectangle();
            Grid.SetColumn(rectangle, col);
            Grid.SetRow(rectangle, row);
            rectangles[col, row] = rectangle;

            //Add border and rectangle to grid.
            gridBoard.Children.Add(border);
            gridBoard.Children.Add(rectangle);
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (inGame)
            {
                if(mode == TWOPLAYER || (mode == ONEPLAYER && turn == CIRCLE))
                {
                    imageCoin.Source = null;
                    Point p = e.GetPosition(this);
                    CheckPointHitRectangle(p);

                }
                
            }
            
        }

        private void CheckPointHitRectangle(Point p)
        {
            for(int col = 0; col < 3; col++)
            {
                for(int row = 0; row < 3; row++)
                {
                    Point recPosition = rectangles[col, row].TransformToAncestor(this).Transform(new Point(0, 0));

                    double topBound = recPosition.Y;
                    double botBound = recPosition.Y + rectangles[col, row].ActualHeight;
                    double leftBound = recPosition.X;
                    double rightBound = recPosition.X + rectangles[col, row].ActualWidth;

                    if(p.X > leftBound && p.X < rightBound && p.Y > topBound && p.Y < botBound)
                    {                       
                        DrawPiece(col, row, rectangles[col, row]);
                        
                        col = 3;
                        row = 3;
                    }
                }
            }
           
        }

        private void DrawPiece(int col, int row, Rectangle rec)
        {

            int margin = 8;
            int thickness = 4;

            if (turn == CROSS)
            {
                DrawX(col, row, rec, margin, thickness);

            }
            else if(turn == CIRCLE)
            {
                DrawO(col, row, rec, margin, thickness);
            }
            CheckBoard();

            ComputerTurn();
            
            
        }

        private void ComputerTurn()
        {
            int margin = 8;
            int thickness = 4;

            if (mode == ONEPLAYER && turn == CROSS && inGame)
            {
                Random random = new Random();

                int ccol = random.Next(0, 3);
                int crow = random.Next(0, 3);

                while (moves[ccol, crow] == X || moves[ccol, crow] == O)
                {
                    ccol = random.Next(0, 3);
                    crow = random.Next(0, 3);
                }

                DrawX(ccol, crow, rectangles[ccol, crow], margin, thickness);
                CheckBoard();
            }
        }

        private void DrawX(int col, int row, Rectangle rec, int margin, int thickness)
        {
            Brush brush = new SolidColorBrush(Colors.Red);
            //Draw first line.
            Line line1 = new Line();
            line1.X1 = margin;
            line1.X2 = rec.ActualWidth - margin;
            line1.Y1 = margin;
            line1.Y2 = rec.ActualHeight - margin;
            line1.StrokeThickness = thickness;
            line1.Stroke = brush;

            Grid.SetColumn(line1, col);
            Grid.SetRow(line1, row);

            gridBoard.Children.Add(line1);

            //Draw second line.
            Line line2 = new Line();
            line2.X1 = rec.ActualWidth - margin;
            line2.X2 = margin;
            line2.Y1 = margin;
            line2.Y2 = rec.ActualHeight - margin;
            line2.StrokeThickness = thickness;
            line2.Stroke = brush;

            Grid.SetColumn(line2, col);
            Grid.SetRow(line2, row);

            gridBoard.Children.Add(line2);

            //Set the move piece.
            moves[col, row] = X;
            turn = !turn;
            numMoves++;
            labelAnnounce.Content = player1 + "'s turn!";
        }

        private void DrawO(int col, int row, Rectangle rec, int margin, int thickness)
        {
            Brush brush = new SolidColorBrush(Colors.Blue);
            //Draw first line.
            Ellipse oval = new Ellipse();
            oval.Margin = new Thickness(margin, margin, margin, margin);
            oval.StrokeThickness = thickness;
            oval.Stroke = brush;

            Grid.SetColumn(oval, col);
            Grid.SetRow(oval, row);

            gridBoard.Children.Add(oval);

            //Set the move piece.
            moves[col, row] = O;
            turn = !turn;
            numMoves++;
            labelAnnounce.Content = player2 + "'s turn!";
        }

        private void CheckBoard()
        {
            if (CheckVerticalWin()) { return; }
            if (CheckHorizontalWin()) { return; }
            if (CheckDiagonalWin()) { return; }        
            
            if(numMoves == 9)
            {
                EndGame("T");
            }
        }

        private bool CheckDiagonalWin()
        {
            int sum = 0;
            sum = moves[0, 0] + moves[1, 1] + moves[2, 2];
            if (CheckWinner(sum))
            {
                return true;
            }

            sum = moves[2, 0] + moves[1, 1] + moves[0, 2];
            if (CheckWinner(sum))
            {
                return true;
            }

            return false;
        }
        private bool CheckHorizontalWin()
        {
            int sum = 0;      
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    sum += moves[col, row];
                }
                if (CheckWinner(sum))
                {
                    return true;
                }
                sum = 0;
            }

            return false;
        }
        private bool CheckVerticalWin()
        {
            int sum = 0;
            for (int col = 0; col < 3; col++)
            {
                for (int row = 0; row < 3; row++)
                {
                    sum += moves[col, row];
                }
                if (CheckWinner(sum))
                {
                    return true;
                }
                sum = 0;
            }
            return false;
        }
        private bool CheckWinner(int winner)
        {
            if (winner == XWIN)
            {
                EndGame("X");
                return true;
            }
            else if (winner == OWIN)
            {
                EndGame("O");
                return true;
            }

            return false;
        }

        private void EndGame(string w)
        {
            inGame = false;
            string winner = (w == "O") ? player1 : player2;


            string outcome = "";

            if(w == "O")
            {
                outcome = player1 + " has won!";
            }else if(w == "X")
            {
                outcome = player2 + " has won!";
            }
            else if(w == "T")
            {
                outcome = "Its a tie!";
            }
            labelAnnounce.Content = outcome;

            MessageBoxResult result = MessageBox.Show(outcome + " Would you like to play again?", "Again", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                InitializeBoard();

            }
            else
            {
                Close();
            }

        }

        //Command function implementations.
        private void Start()
        {
            if (!inGame)
            {
                inGame = true;
                buttonStart.Content = "Restart";
                buttonStart.Command = MyCommands.Commands.restartCommand;
                radioButtonOnePlayer.IsEnabled = false;
                radioButtonTwoPlayers.IsEnabled = false;
                menuItemStart.IsEnabled = false;

                if (radioButtonOnePlayer.IsChecked == true)
                {
                    mode = ONEPLAYER;
                    player1 = "Player 1";
                    player2 = "Computer";

                }
                else
                {
                    mode = TWOPLAYER;
                    player1 = "Player 1";
                    player2 = "Player 2";
                }

                labelO.Content = player1 + " is O's";
                labelX.Content = player2 + " is X's";
                //Coin toss
                BitmapImage source = new BitmapImage();
                source.BeginInit();

                Random random = new Random();
                int coin = random.Next(1, 3);
                coin = random.Next(1, 3);
                if (coin == HEADS)
                {
                    source.UriSource = new Uri("coinHeads.png", UriKind.Relative);
                    source.EndInit();
                    imageCoin.Source = source;
                    turn = CIRCLE;
                    labelAnnounce.Content = player1 + " goes first!";

                }
                else if (coin == TAILS)
                {
                    source.UriSource = new Uri("coinTails.png", UriKind.Relative);
                    source.EndInit();
                    imageCoin.Source = source;
                    turn = CROSS;
                    labelAnnounce.Content = player2 + " goes first!";
                }

                ComputerTurn();
            }else
            {
                MessageBox.Show("Game already started!");
            }

        }
        private void Restart()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure? Current game will be reset.", "Restart", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                InitializeBoard();
          
            }
        }
        private void Exit()
        {
            Close();
        }

        private void Help()
        {
            int arch = (IntPtr.Size == 8) ? 64 : 32;
            string netVersion = Environment.Version.ToString();

            string str = "Name: Sergio Ramirez\n" +
                         "Version: Tic Tac Toe 1.0 - " + arch + " bit\n" +
                         ".Net version: " + netVersion + "\n";

            MessageBox.Show(str, "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Rules()
        {
            string str = "\u2022 A coin roll will happen. Heads player 1 wins - Tails player 2 wins.";

            MessageBox.Show(str, "Rules", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ShortCuts()
        {
            string str = "Start game \t Ctrl+S\n" +
                         "Restart game \t Ctrl+R\n" +
                         "Exit game \t\t Ctrl+E\n" +
                         "About \t\t Ctrl+H\n" +
                         "Game rules \t Ctrl+L\n" +
                         "Shortcuts \t\t Ctrl+K\n";
                         

            MessageBox.Show(str, "Keyboard Shortcuts", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;

            }
        }

        //Execute command functions.
        private void StartCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Start();
        }

        private void RestartCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Restart();
        }
        private void ExitCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Exit();
        }
        private void HelpCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Help();
        }
        private void RulesCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Rules();
        }
        private void ShortCutsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShortCuts();
        }

    }


}


