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
    /// Interaction logic for EndOfGame.xaml
    /// </summary>
    public partial class EndOfGame : Window
    {
        private string _time;
        private Point _dim;
        private bool _stopAnimation = false;
        public EndOfGame(string time, Point dim)
        {
            InitializeComponent();
            _time = time; _dim = dim;
            button_No.Foreground = textBlock.Foreground;
            button_Yes.Foreground = button_No.Foreground;
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory.CurrentProjectFolder(), "Resources");
            mediaTimeline.Source = new Uri(path + "/Flying.wmv");
            textBlock.Text = "Gratulations!\nYou managed to get through the maze. Do you want to save your score?";
            TextAnimation(time);
        }
        private async void TextAnimation(string time)
        {
            string score = "Your Time: " + time + " sec";
            char tmp;
            for (int i = 0; i < score.Length; i++)
            {
                if (!_stopAnimation)
                {
                    tmp = score[i];
                    textBlock_Time.Text += tmp;
                    MainMenu.ButtonClickSound.Play();
                    await Task.Delay(200);
                }
            }
        }

        private void button_No_Click(object sender, RoutedEventArgs e)
        {
            _stopAnimation = true;
            MainMenu.ButtonClickSound.Play();
            this.Close();
            App.Current.MainWindow.Show();
        }

        private void button_Yes_Click(object sender, RoutedEventArgs e)
        {
            _stopAnimation = true;
            MainMenu.ButtonClickSound.Play();
            string nick = Microsoft.VisualBasic.Interaction.InputBox("Please enter your nickname", "Saving score", "Your nickname");
            // Check if data base exist
            if (!Rank.IsDataBaseExist())
            {
                Rank.CreateDataBase();
                Rank.Add(nick, Convert.ToInt32(_time), _dim);
            }
            else
            {
                Rank.Add(nick, Convert.ToInt32(_time), _dim);
            }
            this.Close();
            App.Current.MainWindow.Show();
        }

        private void button_No_MouseEnter(object sender, MouseEventArgs e)
        {
            button_No.Foreground = Brushes.Gray;
        }

        private void button_Yes_MouseEnter(object sender, MouseEventArgs e)
        {
            button_Yes.Foreground = Brushes.Gray;
        }

        private void button_Yes_MouseLeave(object sender, MouseEventArgs e)
        {
            button_Yes.Foreground = textBlock.Foreground;
        }

        private void button_No_MouseLeave(object sender, MouseEventArgs e)
        {
            button_No.Foreground = textBlock.Foreground;
        }
    }
}
