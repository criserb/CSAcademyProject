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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectAcademy
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
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
