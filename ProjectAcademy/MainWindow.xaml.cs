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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MenuViewGrid();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void New_game_Click(object sender, RoutedEventArgs e)
        {
            GenerateMazeViewGrid();
        }

        private void Ranking_Click(object sender, RoutedEventArgs e)
        { 
            Window1 Window1 = new Window1();
            this.Close();
            Window1.Show();
        }

        private void slider_Width_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.textBox_Width.Text = ((int)slider_Width.Value).ToString();
        }

        private void slider_Height_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.textBox_Height.Text = ((int)slider_Height.Value).ToString();
        }

        private void Btn_Generate_Maze_Click(object sender, RoutedEventArgs e)
        {
            GameWindow GameWindow = new GameWindow(Convert.ToInt32(textBox_Width.Text), Convert.ToInt32(textBox_Height.Text));
            GameWindow.Show();
            this.Close();
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MenuViewGrid();
        }

        private void GenerateMazeViewGrid()
        {
            this.Btn_Exit.Visibility = Visibility.Hidden;
            this.Btn_New_game.Visibility = Visibility.Hidden;
            this.Btn_Ranking.Visibility = Visibility.Hidden;
            this.Btn_Generate_Maze.Visibility = Visibility.Visible;
            this.Btn_Back.Visibility = Visibility.Visible;
            this.slider_Height.Visibility = Visibility.Visible;
            this.slider_Width.Visibility = Visibility.Visible;
            this.textBox_Height.Visibility = Visibility.Visible;
            this.textBox_Width.Visibility = Visibility.Visible;
            this.lbl_Height.Visibility = Visibility.Visible;
            this.lbl_Width.Visibility = Visibility.Visible;
            this.groupBoxlabyrinthDiemnsion.Visibility = Visibility.Visible;
        }
        private void MenuViewGrid()
        {
            this.Btn_Exit.Visibility = Visibility.Visible;
            this.Btn_New_game.Visibility = Visibility.Visible;
            this.Btn_Ranking.Visibility = Visibility.Visible;
            this.Btn_Generate_Maze.Visibility = Visibility.Hidden;
            this.Btn_Back.Visibility = Visibility.Hidden;
            this.slider_Height.Visibility = Visibility.Hidden;
            this.slider_Width.Visibility = Visibility.Hidden;
            this.textBox_Height.Visibility = Visibility.Hidden;
            this.textBox_Width.Visibility = Visibility.Hidden;
            this.lbl_Height.Visibility = Visibility.Hidden;
            this.lbl_Width.Visibility = Visibility.Hidden;
            this.groupBoxlabyrinthDiemnsion.Visibility = Visibility.Hidden;
        }
    }
}
