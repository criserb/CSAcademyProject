using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace ProjectAcademy
{
    /// <summary>
    /// Interaction logic for HowToPlay.xaml
    /// </summary>
    public sealed partial class HowToPlay : Page
    {
        private Maze _maze;
        private Player _player;
        private Point _start, _exit;
        private Point _dim;
        // 8 - up, 6 - right, 5 - down
        private int[] _moves = { 8, 8, 8, 8, 6, 5, 6, 8, 6, 6, 8, 6, 6, 5, 5, 6, 8, 8, 8, 8, 8, 6, 6, 8, 8, 6, 5, 6, 8, 6 };
        System.Windows.Threading.DispatcherTimer timer1;
        public HowToPlay()
        {
            timer1 = new System.Windows.Threading.DispatcherTimer();
            timer1.Tick += new EventHandler(timer1_tick);
            timer1.Interval = new TimeSpan(0, 0, 5); //hours minutes seconds
            InitializeComponent();
            _dim = new Point(10, 10);
            _start = new Point(0, _dim.Y - 1); _exit = new Point(_dim.X - 1, 0);
            _maze = new Maze(_dim, _start, _exit);
            _player = new Player(_start);
            // Setting colors
            _maze.LineColor = Maze.DefaultLineColor;
            _player.Color = Player.DefaultColor;
            ReadPresentationMaze();
            // Render maze
            _maze.RenderMaze(animationGrid);
            // Render player at start position
            _player.Render(animationGrid);
            BeginAnimation();
        }
        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());
        }
        private void BeginAnimation()
        {
            foreach (var item in _moves)
            {
                switch (item)
                {
                    case 8:
                        _player.Position.Y--;
                        _player.UpdatePosition(_player.Position);
                        break;
                    case 6:
                        _player.Position.X++;
                        _player.UpdatePosition(_player.Position);
                        break;
                    case 5:
                        _player.Position.Y++;
                        _player.UpdatePosition(_player.Position);
                        break;
                }
                timer1.Start();
            }
        }
        void timer1_tick(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        private void ReadPresentationMaze()
        {
            String[] items = File.ReadAllText(@"PresentationMaze.txt").
               Split(new String[] { " ", Environment.NewLine },
               StringSplitOptions.RemoveEmptyEntries);
            int count = -1;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    _maze.Cells[i, j].NorthWall = Convert.ToBoolean(items[++count]);
                    _maze.Cells[i, j].SouthWall = Convert.ToBoolean(items[++count]);
                    _maze.Cells[i, j].WestWall = Convert.ToBoolean(items[++count]);
                    _maze.Cells[i, j].EastWall = Convert.ToBoolean(items[++count]);
                }
            }
        }
    }
}
