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
        private Point _position, _startPosition;
        private Ellipse _avatar;
        /// <summary>
        /// Initialize player position in myPoint
        /// </summary>
        public Player(Point myPoint)
        {
            _startPosition = new Point(myPoint);
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
            _avatar.Margin = new Thickness(_startPosition.X * lineLengh + bound, _startPosition.Y * lineLengh + bound, 0, 0);

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
        public void UpdatePosition(Point position)
        {
            _avatar.Margin = new Thickness(position.X * lineLengh + bound, position.Y * lineLengh + bound, 0, 0);
        }
        public bool MazeCollision(int playerPositionX, int playerPositionY, Point dim)
        {
            if (playerPositionX >= 0 &&
            playerPositionX < dim.X &&
            playerPositionY >= 0 &&
            playerPositionY < dim.Y)
            {
                return false;
            }
            else return true;
        }
        public bool WallCollision(int playerPositionX, int playerPositionY, Cell[,] cell, Point dim, Direction dir)
        {
            switch (dir)
            {

                case Direction.up:
                    if (!cell[playerPositionY, playerPositionX].NorthWall && !cell[playerPositionY - 1, playerPositionX].SouthWall)
                    {
                        return false;
                    }
                    else return true;
                case Direction.down:
                    if (!cell[playerPositionY, playerPositionX].SouthWall && !cell[playerPositionY + 1, playerPositionX].NorthWall)
                    {
                        return false;
                    }
                    else return true;
                case Direction.right:
                    if (!cell[playerPositionY, playerPositionX].EastWall && !cell[playerPositionY, playerPositionX + 1].WestWall)
                    {
                        return false;
                    }
                    else return true;
                case Direction.left:
                    if (!cell[playerPositionY, playerPositionX].WestWall && !cell[playerPositionY, playerPositionX - 1].EastWall)
                    {
                        return false;
                    }
                    else return true;
            }
            return false;
        }
    }
}
