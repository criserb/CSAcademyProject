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

namespace ProjectAcademy
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Player _player;
        private Maze _maze;
        private Point _dim;
        private int _count = 0;
        static protected Random rand = new Random();
        protected Direction dir;
        protected int w, h;
        protected const int lineLengh = 20;
        protected const int bound = 25;
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
            this.w = w; this.h = h;
            InitializeObjects();
            this.Width = bound * 3 + (lineLengh * w) - lineLengh;
            this.Height = bound * 4 + (lineLengh * h);
            // Setting position of objects on grid
            SettingPositions();
            // Generate maze
            //_maze.GenerateMaze();
            // Render maze
            _maze.RenderMaze(gameGrid);
            // Render player at start position
            _player.Render(gameGrid);
        }
        private void InitializeObjects()
        {
            // Generate start and exit point
            // TODO: generowac normalnie start i exit
            this._start = new Point(0, RandomInt(0, h));
            //MessageBox.Show("Start: " + _start.X.ToString() + " " + _start.Y.ToString());
            //this._exit = new Point(w - 1, RandomInt(0, h));
            this._dim = new Point(w, h);
            this._maze = new Maze(w, h, _start, _exit);
            this._player = new Player(_start);
           // MessageBox.Show("Player_position" + _player.Position.X.ToString() + " " + _player.Position.Y.ToString());
        }
        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
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
        private void SettingPositions()
        {
            Btn_Back.Margin = new Thickness(bound - 5, this.Height - bound * 3 + 4, 0, 0);
            lbl_Time.Margin = new Thickness(this.Width - 6 * bound, this.Height - bound * 3, 0, 0);
            lbl_Time_Value.Margin = new Thickness(this.Width - 7 * bound, this.Height - bound * 3, 0, 0);
            lbl_Time_Sec.Margin = new Thickness(this.Width - 3 * bound, this.Height - bound * 3, 0, 0);
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lbl_Time_Value.Content = (++_count).ToString();
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
