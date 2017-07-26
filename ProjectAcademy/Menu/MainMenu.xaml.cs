using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProjectAcademy
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public static Color PlayerColor { get; set; }
        public static Color MazeLineColor { get; set; }
        public static Color MazeBackgroundColor { get; set; }
        public static SoundPlayer ButtonClickSound { get; set; }
        public static SoundPlayer PlayerWalkSound { get; set; }
        public static SoundPlayer SliderClickSound { get; set; }
        public static SoundPlayer Yeah { get; set; }
        public MainMenu()
        {
            InitializeComponent();
            LoadConfig();
        }
        /// <summary>
        /// Loading color options from file
        /// </summary>
        private void LoadConfig()
        {
            string configurationFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory.CurrentProjectFolder(), "Resources");
            String[] items = File.ReadAllText(configurationFile + "/Config.txt").
               Split(new String[] { " ", Environment.NewLine },
               StringSplitOptions.RemoveEmptyEntries);
            PlayerColor = (Color)ColorConverter.ConvertFromString(items[0]);
            MazeLineColor = (Color)ColorConverter.ConvertFromString(items[1]);
            MazeBackgroundColor = (Color)ColorConverter.ConvertFromString(items[2]);
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickSound.Play();
            Application.Current.Shutdown();
        }

        private void New_game_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickSound.Play();
            this.NavigationService.Navigate(new NewGame());
        }

        private void Ranking_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickSound.Play();
            this.NavigationService.Navigate(new Ranking());
        }

        private void Btn_New_How_To_Play_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickSound.Play();
            this.NavigationService.Navigate(new HowToPlay());
        }
        private void Btn_Options_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickSound.Play();
            this.NavigationService.Navigate(new Option());
        }

        private void Btn_About_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Hide();
            AboutWindow AboutWindow = new AboutWindow();
            AboutWindow.Show();
        }
    }
}
