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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void slider_Width_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBox_Width.Text = ((int)slider_Width.Value).ToString();
        }

        private void slider_Height_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBox_Height.Text = ((int)slider_Height.Value).ToString();
        }

        private void Btn_Generate_Maze_Click(object sender, RoutedEventArgs e)
        {
            GameWindow GameWindow = new GameWindow();
            GameWindow.Show();
            this.Close();
        }
    }
}
