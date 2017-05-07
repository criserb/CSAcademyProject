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
    class Player : GameWindow
    {
        private Point _position;
        private Ellipse _avatar;
        private Point _startPosition;
        /// <summary>
        /// Initialize player position in myPoint
        /// </summary>
        public Player(Point myPoint)
        {
            _startPosition = new Point(myPoint.X * lineLengh + bound, myPoint.Y * lineLengh + bound);
            _position = new Point(_startPosition);
        }

        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public void Render(Grid grid)
        {
            // Create a red Ellipse
            _avatar = new Ellipse();

            // Set position of the ellipse
            _avatar.Margin = new Thickness(_startPosition.X, _startPosition.Y, 0, 0);

            // Create a SolidColorBrush with a red color to fill the 
            // Ellipse with.
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            // Describes the brush's color using RGB values. 
            // Each value has a range of 0-255.
            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            _avatar.Fill = mySolidColorBrush;
            _avatar.StrokeThickness = 2;
            _avatar.Stroke = Brushes.Black;

            // Set the width and height of the Ellipse.
            _avatar.Width = lineLengh / 2;
            _avatar.Height = lineLengh / 2;

            // Create a Canvas
            Canvas myCanvas = new Canvas();
            Canvas.SetLeft(_avatar, myCanvas.ActualWidth / 2.0);
            Canvas.SetTop(_avatar, myCanvas.ActualHeight / 2.0);

            // Add the Ellipse to the Grid.
            myCanvas.Children.Add(_avatar);
            grid.Children.Add(myCanvas);
        }
        public void UpdatePosition(int updatedXCoord, int updatedYCoord)
        {
            _avatar.Margin = new Thickness(updatedXCoord, updatedYCoord, 0, 0);
        }
        public bool Collision(int playerPositionX, int playerPositionY, int rows, int cols)
        {
                if (playerPositionX >= bound &&
                playerPositionX < bound + cols * lineLengh &&
                playerPositionY >= bound &&
                playerPositionY < bound + rows * lineLengh
                )
            {
                return false;
            }
            else return true;
        }
    }
}
