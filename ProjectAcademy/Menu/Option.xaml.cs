using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;

namespace ProjectAcademy
{
    /// <summary>
    /// Interaction logic for Option.xaml
    /// </summary>
    public partial class Option : Page
    {
        private SolidColorBrush _playerBrush;
        private SolidColorBrush _lineBrush;
        private SolidColorBrush _backgroundBrush;
        private bool _change = false;
        private bool _saved = false;
        private Maze _maze;
        private Player _player;
        public Option()
        {
            InitializeComponent();
            SetColors();
            _maze = new Maze(new Point(10, 10), new Point(0, 9), new Point(9, 0));
            _player = new Player(new Point(0, 9));
            _maze.LineColor = MainMenu.MazeLineColor;
            _maze.Generate();
            _maze.Render(previewGrid);
            _player.Color = MainMenu.PlayerColor;
            _player.Render(previewGrid);
        }
        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (_change && !_saved)
            {
                if (MessageBox.Show("Are you sure to back without saving changes?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    this.NavigationService.Navigate(new MainMenu());
            }
            else this.NavigationService.Navigate(new MainMenu());
        }
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            string configurationFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory.CurrentProjectFolder(), "Resources");
            string text = ToHexColor(MainMenu.PlayerColor) + ' ' + ToHexColor(MainMenu.MazeLineColor) + ' ' + ToHexColor(MainMenu.MazeBackgroundColor);
            File.WriteAllText(configurationFile + "/Config.txt", text);
            _saved = true;
        }
        private string ToHexColor(Color color)
        {
            return String.Format("#{0}{1}{2}",
                                 color.R.ToString("X2"),
                                 color.G.ToString("X2"),
                                 color.B.ToString("X2"));
        }
        private void SetColors()
        {
            _playerBrush = new SolidColorBrush(MainMenu.PlayerColor);
            myEllipse.Fill = _playerBrush;
            _lineBrush = new SolidColorBrush(MainMenu.MazeLineColor);
            myRectangle.Stroke = _lineBrush;
            _backgroundBrush = new SolidColorBrush(MainMenu.MazeBackgroundColor);
            myRectangleBackground.Fill = _backgroundBrush;
            previewGrid.Background = _backgroundBrush;
        }
        private Color ChangeColor(out SolidColorBrush brush)
        {
            Color color;
            color = ColorPicker();
            brush = new SolidColorBrush(color);
            _change = true;
            return color;
        }
        private void Btn_change_player_color_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.PlayerColor = ChangeColor(out _playerBrush);
            myEllipse.Fill = _playerBrush;
            _player.Remove(previewGrid);
            _player = new Player(new Point(0, 9));
            _player.Color = MainMenu.PlayerColor;
            _player.Render(previewGrid);
        }
        private void Btn_change_line_color_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.MazeLineColor = ChangeColor(out _lineBrush);
            myRectangle.Stroke = _lineBrush;
            _maze.LineColor = MainMenu.MazeLineColor;
            _maze.Render(previewGrid);
        }

        private void Btn_change_background_color_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.MazeBackgroundColor = ChangeColor(out _backgroundBrush);
            myRectangleBackground.Fill = _backgroundBrush;
            previewGrid.Background = _backgroundBrush;
        }
        private System.Windows.Media.Color ColorPicker()
        {
            System.Windows.Forms.ColorDialog colorDialog =
                       new System.Windows.Forms.ColorDialog();
            colorDialog.AllowFullOpen = true;
            colorDialog.ShowDialog();

            System.Windows.Media.Color col = new System.Windows.Media.Color();
            col.A = colorDialog.Color.A;
            col.B = colorDialog.Color.B;
            col.G = colorDialog.Color.G;
            col.R = colorDialog.Color.R;
            return col;
        }

        private void btn_default_avatar_color_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.PlayerColor = Player.DefaultColor;
            _playerBrush = new SolidColorBrush(MainMenu.PlayerColor);
            myEllipse.Fill = _playerBrush;
            _player.Remove(previewGrid);
            _player = new Player(new Point(0, 9));
            _player.Color = MainMenu.PlayerColor;
            _player.Render(previewGrid);
            _change = true;
        }

        private void btn_default_line_color_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.MazeLineColor = Maze.DefaultLineColor;
            _lineBrush = new SolidColorBrush(MainMenu.MazeLineColor);
            myRectangle.Stroke = _lineBrush;
            _maze.LineColor = MainMenu.MazeLineColor;
            _maze.Render(previewGrid);
            _change = true;
        }

        private void btn_default_background_color_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.MazeBackgroundColor = Maze.DefaultBackgroundColor;
            _backgroundBrush = new SolidColorBrush(MainMenu.MazeBackgroundColor);
            myRectangleBackground.Fill = _backgroundBrush;
            _maze.BackgroundColor = _backgroundBrush;
            previewGrid.Background = _backgroundBrush;
            _change = true;
        }
    }
}
