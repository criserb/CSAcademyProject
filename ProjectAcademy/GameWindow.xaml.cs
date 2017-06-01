using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading;

namespace ProjectAcademy
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Player _player;
        private Maze _maze;
        // dimension of maze, _dim.X = width, _ dim.Y = height
        private Point _dim;
        private int _count = 0;
        private bool _stopCounting = false;
        static protected Random rand = new Random();
        protected Direction dir;
        protected readonly int lineLengh = MainWindow.lineLengh;
        protected readonly int bound = MainWindow.bound;
        private Point _start, _exit;
        private int _hintsCount = 2;
        private bool _refresh = false;
        private Brush _backgroundColor;
        public Brush BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }
        public enum Direction
        {
            up,
            down,
            right,
            left
        }
        public GameWindow()
        {
            InitializeComponent();
        }
        public GameWindow(int w, int h)
        {
            InitializeComponent();
            if (w > (SystemParameters.WorkArea.Width / lineLengh - 2) / 2 &&
                h > (SystemParameters.WorkArea.Height / lineLengh - 2) / 2)
                this.WindowState = WindowState.Maximized;
            this._dim = new Point(w, h);
            InitializeObjects();
            this.Width = bound * 3 + (lineLengh * w) - lineLengh;
            this.Height = bound * 4 + (lineLengh * h);
            // Setting background color
            _backgroundColor = new SolidColorBrush(MainMenu.MazeBackgroundColor);
            mazeGrid.Background = _backgroundColor;
            // Setting position of objects on grid
            SettingPositions();
            // Generate maze
            _maze.Generate();
            // Render maze
            _maze.Render(mazeGrid);
            // Render player at start position
            _player.Render(mazeGrid);
            // Find escape from maze
           // _maze.FindPath();
        }
        private void SettingPositions()
        {
            Btn_Show_Solution.Content = $"Hints: {_hintsCount}";
            Btn_Show_Solution.Margin = new Thickness(bound - 8, this.Height - bound * 3 + 4, 0, 0);
            Btn_Back.Margin = new Thickness(bound + (Btn_Show_Solution.Width - 11), this.Height - bound * 3 + 4, 0, 0);
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            Btn_labels.IsHitTestVisible = false;
            Btn_labels.Content = "Time: " + _count.ToString() + " Sec";
            Btn_labels.Margin = new Thickness(this.Width - 5.5 * bound + 2 - 20, this.Height - bound * 3 + 4, 0, 0);
        }
        private void InitializeObjects()
        {
            // Generate start and exit point
            this._start = new Point(0, _dim.Y - 1);
            this._exit = new Point(_dim.X - 1, 0);
            this._maze = new Maze(_dim, _start, _exit);
            this._maze.LineColor = MainMenu.MazeLineColor;
            this._player = new Player(_start);
            this._player.Color = MainMenu.PlayerColor;
        }
        private void End()
        {
            _player.Position.X++;
            _player.UpdatePosition();
            _player.Remove(mazeGrid);
            _stopCounting = true;
            Thread.Sleep(200);
            if (MessageBox.Show("Gratulations! You managed to get through the maze in time " +
                this._count.ToString() + " seconds! Do you want to save your score?",
                "Win", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string nick = Microsoft.VisualBasic.Interaction.InputBox("Please enter your nickname", "Saving score", "Your nickname");
                // Check if data base exist
                if (!Rank.IsDataBaseExist())
                {
                    Rank.CreateDataBase();
                    Rank.Add(nick, Convert.ToInt32(this._count), _dim);
                }
                else
                {
                    Rank.Add(nick, Convert.ToInt32(this._count), _dim);
                }
            }
            App.Current.MainWindow.Show();
            this.Close();
        }
        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Show();
            this.Close();
        }
        /// <summary>
        /// Show path to exit
        /// </summary>
        private void Btn_Show_Solution_Click(object sender, RoutedEventArgs e)
        {
            --_hintsCount;
            Btn_Show_Solution.Content = $"Hints: {_hintsCount}";
            if (_hintsCount == 0)
                Btn_Show_Solution.IsEnabled = false;
            _maze.ColorPath(gameGrid, Colors.LightGreen);
            _refresh = true;
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    {
                        if (!_player.MazeCollision(_player.Position.X, _player.Position.Y - 1, _dim))
                        {
                            if (!_player.WallCollision(_player.Position.X, _player.Position.Y, _maze.Cells, _dim, Direction.up))
                            {
                                _player.Position.Y--;
                                _player.UpdatePosition();
                            }
                        }
                        break;
                    }
                case Key.Down:
                    {
                        if (!_player.MazeCollision(_player.Position.X, _player.Position.Y + 1, _dim))
                        {
                            if (!_player.WallCollision(_player.Position.X, _player.Position.Y, _maze.Cells, _dim, Direction.down))
                            {
                                _player.Position.Y++;
                                _player.UpdatePosition();
                            }
                        }
                        break;
                    }
                case Key.Left:
                    {
                        if (!_player.MazeCollision(_player.Position.X - 1, _player.Position.Y, _dim))
                        {
                            if (!_player.WallCollision(_player.Position.X, _player.Position.Y, _maze.Cells, _dim, Direction.left))
                            {
                                _player.Position.X--;
                                _player.UpdatePosition();
                            }
                        }
                        break;
                    }
                case Key.Right:
                    {
                        if (_player.Position.X == _dim.X - 1 && _player.Position.Y == 0)
                        {
                            End();
                        }
                        if (!_player.MazeCollision(_player.Position.X + 1, _player.Position.Y, _dim))
                        {
                            if (!_player.WallCollision(_player.Position.X, _player.Position.Y, _maze.Cells, _dim, Direction.right))
                            {
                                _player.Position.X++;
                                _player.UpdatePosition();
                            }
                        }
                        break;
                    }
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (_refresh)
            {
                Thread.Sleep(3000);
                _maze.ColorPath(gameGrid, Colors.White);
                _refresh = false;
                _count += 9;
            }
            if (!_stopCounting)
                Btn_labels.Content = "Time: " + (++_count).ToString() + " Sec";
        }
        /// <summary>
        /// Generate random int from minValue to maxValue
        /// </summary>
        public int RandomInt(int minValue, int maxValue)
        {
            return rand.Next(minValue, maxValue);
        }
    }
}
