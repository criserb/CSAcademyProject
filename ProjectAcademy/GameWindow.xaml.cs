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
        protected const int lineLengh = 20;
        protected const int bound = 25;
        private Point _start, _exit;
        public GameWindow()
        {
            InitializeComponent();
        }
        public GameWindow(int w, int h)
        {
            InitializeComponent();
            InitializeObjects(w, h);
            this.Width = bound * 3 + (lineLengh * w) - lineLengh;
            this.Height = bound * 4 + (lineLengh * h);
            // Setting position of objects on grid
            SettingPositions();
            // Render maze
            _maze.RenderMaze(gameGrid);
            // Render player at start position
            _player.Render(gameGrid);
        }
        private void InitializeObjects(int w, int h)
        {
            // Generate start and exit point
            this._start = new Point(0, RandomInt(0, h));
            this._exit = new Point(w, RandomInt(0, h));
            this._dim = new Point(w, h);
            this._maze = new Maze(w, h, _start, _exit);
            this._player = new Player(_start);
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
                        if (!_player.Collision(_player.Position.X, _player.Position.Y - lineLengh, _dim.Y, _dim.X))
                        {
                            _player.Position.Y -= lineLengh;
                            _player.UpdatePosition(_player.Position.X, _player.Position.Y);
                        }
                        break;
                    }
                case Key.Down:
                    {
                        if (!_player.Collision(_player.Position.X, _player.Position.Y + lineLengh, _dim.Y, _dim.X))
                        {
                            _player.Position.Y += lineLengh;
                            _player.UpdatePosition(_player.Position.X, _player.Position.Y);
                        }
                        break;
                    }
                case Key.Left:
                    {
                        if (!_player.Collision(_player.Position.X - lineLengh, _player.Position.Y, _dim.Y, _dim.X))
                        {
                            _player.Position.X -= lineLengh;
                            _player.UpdatePosition(_player.Position.X, _player.Position.Y);
                        }
                        break;
                    }
                case Key.Right:
                    {
                        if (!_player.Collision(_player.Position.X + lineLengh, _player.Position.Y, _dim.Y, _dim.X))
                        {
                            _player.Position.X += lineLengh;
                            _player.UpdatePosition(_player.Position.X, _player.Position.Y);
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
        public int RandomInt(int minValue, int maxValue)
        {
            return rand.Next(minValue, maxValue);
        }
    }
}
