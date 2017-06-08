using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Interaction logic for NewGame.xaml
    /// </summary>
    public partial class NewGame : Page
    {
        public SoundPlayer SliderClick { get; set; }
        public NewGame()
        {
            InitializeComponent();
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory.CurrentProjectFolder(), "Resources");
            slider_Width.Maximum = (SystemParameters.WorkArea.Width / MainWindow.lineLengh - 2);
            slider_Height.Maximum = (SystemParameters.WorkArea.Height / MainWindow.lineLengh - 2);
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
            MainMenu.ButtonClickSound.Play();
            App.Current.MainWindow.Hide();
            GameWindow GameWindow = new GameWindow(Convert.ToInt32(textBox_Width.Text), Convert.ToInt32(textBox_Height.Text));
            GameWindow.Show();
            this.NavigationService.Navigate(new MainMenu());
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.ButtonClickSound.Play();
            this.NavigationService.Navigate(new MainMenu());
        }
        private void btn_max_width_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.ButtonClickSound.Play();
            slider_Width.Value = slider_Width.Maximum;
        }

        private void btn_max_height_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.ButtonClickSound.Play();
            slider_Height.Value = slider_Height.Maximum;
        }
        private void slider_Width_GotMouseCapture(object sender, MouseEventArgs e)
        {
            MainMenu.SliderClickSound.Play();
        }
    }
}
