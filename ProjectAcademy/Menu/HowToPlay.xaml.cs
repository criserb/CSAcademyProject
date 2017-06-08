using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        private int[] _moves = { 8, 8, 8, 8, 6, 8, 6, 6, 6, 8, 4, 4, 4, 6, 6, 6, 5, 4, 4, 4, 5,
            5, 6, 8, 6, 6, 5, 5, 6, 8, 8, 8, 8, 8, 6, 6, 8, 8, 6, 5, 6, 8 };
        public HowToPlay()
        {
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
            _maze.Render(animationGrid);
            // Render player at start position
            _player.Render(animationGrid);
            BeginAnimation();
        }
        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.ButtonClickSound.Play();
            this.NavigationService.Navigate(new MainMenu());
        }
        private async void BeginAnimation()
        {
            var converter = new BrushConverter();
            Brush playerBrushColor = (Brush)converter.ConvertFromString(_player.Color.ToString());
            while (true)
            {
                await Wait(1000);
                foreach (var item in _moves)
                {
                    switch (item)
                    {
                        case 8:
                            _player.Position.Y--;
                            _player.UpdatePosition();
                            btn_up.Foreground = playerBrushColor;
                            break;
                        case 6:
                            _player.Position.X++;
                            _player.UpdatePosition();
                            btn_right.Foreground = playerBrushColor;
                            break;
                        case 5:
                            _player.Position.Y++;
                            _player.UpdatePosition();
                            btn_down.Foreground = playerBrushColor;
                            break;
                        case 4:
                            _player.Position.X--;
                            _player.UpdatePosition();
                            btn_left.Foreground = playerBrushColor;
                            break;
                    }
                    await Wait(1000);
                    ButtonsDefaultColors();
                }
                _player.Avatar.Visibility = Visibility.Hidden;
                await Wait(3000);
                _player.Position = new Point(_start);
                _player.UpdatePosition();
                _player.Avatar.Visibility = Visibility.Visible;
            }
        }
        private async Task Wait(int msec)
        {
            await Task.Delay(msec);
        }
        private void ButtonsDefaultColors()
        {
            Brush color = Brushes.Black;
            btn_up.Foreground = color;
            btn_down.Foreground = color;
            btn_right.Foreground = color;
            btn_left.Foreground = color;
        }
        private void ReadPresentationMaze()
        {
            string configurationFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.CurrentProjectFolder(), "Resources");
            String[] items = File.ReadAllText(configurationFile + "/PresentationMaze.txt").
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
