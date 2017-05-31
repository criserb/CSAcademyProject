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
    /// Interaction logic for Option.xaml
    /// </summary>
    public partial class Option : Page
    {
        private SolidColorBrush _solidColorBrush;
        private Color _color;
        public Option()
        {
            InitializeComponent();
        }
        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());
        }
        private void SetColors()
        {
            // ustawic kolor z configu
        }
        private void Btn_change_player_color_Click(object sender, RoutedEventArgs e)
        {
            _color = ColorPicker();
           // Player.Color = _color;
            _solidColorBrush.Color = _color;
            myEllipse.Fill = _solidColorBrush;
        }
        private void Btn_change_line_color_Click(object sender, RoutedEventArgs e)
        {
            _color = ColorPicker();
           // Maze.LineColor = _color;
            _solidColorBrush.Color = _color;
            myRectangle.Stroke = _solidColorBrush;
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
                   }
}
