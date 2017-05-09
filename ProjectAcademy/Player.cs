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
        public bool Collision(int playerPositionX, int playerPositionY, Point dim)
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
        //    public bool Collision(Point position, Cell[,] cell, direction dir)
        //    {
        //        switch (dir)
        //        {
        //            case direction.up:
        //                if (!cell[position.Y, position.X].NorthWall)
        //                {
        //                    if (position.X - 1 <= 0)
        //                    {
        //                        return false;
        //                    }
        //                    else if (!cell[position.Y, position.X - 1].SouthWall) return false;
        //                    return true;
        //                }
        //                else return true;
        //            case direction.down:
        //                if (!cell[position.Y, position.X].SouthWall)
        //                {
        //                    if (position.X + 1 >= h)
        //                    {
        //                        return false;
        //                    }
        //                    else if (!cell[position.Y, position.X + 1].NorthWall) return false;
        //                    return true;
        //                }
        //                else return true;
        //            case direction.right:
        //                if (!cell[position.Y, position.X].EastWall)
        //                {
        //                    if (position.Y + 1 >= w)
        //                    {
        //                        return false;
        //                    }
        //                    else if (!cell[position.Y + 1, position.X].WestWall) return false;
        //                    return true;
        //                }
        //                else return true;
        //            case direction.left:
        //                if (!cell[position.Y, position.X].WestWall)
        //                {
        //                    if (position.Y + 1 >= 0)
        //                    {
        //                        return false;
        //                    }
        //                    else if (!cell[position.Y - 1, position.X].EastWall) return false;
        //                    return true;
        //                }
        //                else return true;
        //        }
        //        return true;
        //    }
    }
}
