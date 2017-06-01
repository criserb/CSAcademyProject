using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ProjectAcademy
{
    class Player : GameWindow
    {
        private Point _position, _startPosition;
        private Ellipse _avatar;
        private Canvas _myCanvas = new Canvas();
        private Color _color = Colors.Black;
        public Ellipse Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public static Color DefaultColor
        {
            get { return Colors.OrangeRed; }
        }
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
        /// <summary>
        /// Render player avatar to the game grid
        /// </summary>
        public void Render(Grid grid)
        {
            // Create a red Ellipse
            _avatar = new Ellipse();

            // Set position of the ellipse
            _avatar.Margin = new Thickness(_startPosition.X * MainWindow.lineLengh + 5, _startPosition.Y * MainWindow.lineLengh + 5, 0, 0);

            // Create a SolidColorBrush with a red color to fill the 
            // Ellipse with
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            // Describes the brush's color using RGB values. 
            // Each value has a range of 0-255.
            mySolidColorBrush.Color = _color;
            _avatar.Fill = mySolidColorBrush;

            // Set the width and height of the Ellipse
            _avatar.Width = MainWindow.lineLengh / 2;
            _avatar.Height = MainWindow.lineLengh / 2;

            // Create a Canvas
            Canvas.SetLeft(_avatar, _myCanvas.ActualWidth / 2.0);
            Canvas.SetTop(_avatar, _myCanvas.ActualHeight / 2.0);

            // Add the Ellipse to the Grid.
            _myCanvas.Children.Add(_avatar);
            grid.Children.Add(_myCanvas);
        }
        /// <summary>
        /// Update position of player
        /// </summary>
        public void UpdatePosition(Point position)
        {
            _avatar.Margin = new Thickness(position.X * MainWindow.lineLengh + 5, position.Y * MainWindow.lineLengh + 5, 0, 0);
        }
        /// <summary>
        /// Remove player avatar from the gmae grid
        /// </summary>
        public void Remove(Grid grid)
        {
            grid.Children.Remove(_myCanvas);
        }
        #region Collision_checkers
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
        #endregion
    }
}