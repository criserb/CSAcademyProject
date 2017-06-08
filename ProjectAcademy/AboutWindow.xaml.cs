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
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            aboutText.Text = "Maze\nVersion 1.0\nCopyright © Krzysztof Bądkowski 2017\nKrzysztof Bądkowski\n\n";
            aboutText.Text += "This program is a simple game of finding the exit labyrinth. ";
            aboutText.Text += "About generation algorithms:\n";
            aboutText.Text += "The program uses Depth First Search algorithm to generate maze (more on: http://www.algosome.com/articles/maze-generation-depth-first.html) \n\n";
            aboutText.Text += "About sound effects:\n";
            aboutText.Text += "All sound effects come from the site: http://www.freesfx.co.uk \n\n";
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.ButtonClickSound.Play();
            App.Current.MainWindow.Show();
            this.Close();
        }
    }
}
