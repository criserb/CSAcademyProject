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
    class Player : GameWindow
    {
        private Ellipse _avatar;
        private Point _position;
        /// <summary>
        /// Initialize player position in myPoint
        /// </summary>
        public Player(Point myPoint)
        {
            _position = myPoint;
            _avatar = new Ellipse();
        }
        public void Render()
        {
            // Create a StackPanel to contain the shape.
            StackPanel myStackPanel = new StackPanel();

            // Create a red Ellipse.
            Ellipse myEllipse = new Ellipse();

            // Create a SolidColorBrush with a red color to fill the 
            // Ellipse with.
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            // Describes the brush's color using RGB values. 
            // Each value has a range of 0-255.
            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            myEllipse.Fill = mySolidColorBrush;
            myEllipse.StrokeThickness = 2;
            myEllipse.Stroke = Brushes.Black;

            // Set the width and height of the Ellipse.
            myEllipse.Width = 50;
            myEllipse.Height = 50;

            // Add the Ellipse to the StackPanel.
            //myStackPanel.Children.Add(myEllipse);
            gameGrid.Children.Add(myEllipse);


        }
    }
}
