using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.IO;

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
            // Setting position of objects on grid
            SettingPositions();
            // Generate maze
            _maze.GenerateMaze();
            // Render maze
            _maze.RenderMaze(mazeGrid);
            // Render player at start position
            _player.Render(mazeGrid);
        }
        private void SettingPositions()
        {
            Btn_Show_Solution.Margin = new Thickness(bound - 10, this.Height - bound * 3 + 4, 0, 0);
            Btn_Back.Margin = new Thickness(bound + 10, this.Height - bound * 3 + 4, 0, 0);
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
            this._player = new Player(_start);
        }
        private void End()
        {
            _player.Position.X++;
            _player.UpdatePosition(_player.Position);
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
                                _player.UpdatePosition(_player.Position);
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
                                _player.UpdatePosition(_player.Position);
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
                                _player.UpdatePosition(_player.Position);
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
                                _player.UpdatePosition(_player.Position);
                            }
                        }
                        break;
                    }
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!_stopCounting)
                Btn_labels.Content = "Time: " + (++_count).ToString() + " Sec";
        }

        private void Btn_Show_Solution_Click(object sender, RoutedEventArgs e)
        {
            _maze.FindPath();
            _maze.ColorPath(gameGrid);
        }
        /// <summary>
        /// Generate random int from minValue to max Value
        /// </summary>
        public int RandomInt(int minValue, int maxValue)
        {
            return rand.Next(minValue, maxValue);
        }
    }
}
