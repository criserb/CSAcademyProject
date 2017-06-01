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
        public MainMenu()
        {
            InitializeComponent();
            LoadConfig();
        }
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
            Application.Current.Shutdown();
        }

        private void New_game_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new NewGame());
        }

        private void Ranking_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Ranking());
        }

        private void Btn_New_How_To_Play_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HowToPlay());
        }
        private void Btn_Options_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Option());
        }
    }
}
