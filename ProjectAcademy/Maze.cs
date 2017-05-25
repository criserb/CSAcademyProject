using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ProjectAcademy
{
    public class Maze : GameWindow
    {
        private const int _lineThickness = 1;
        private Point _start, _exit;
        private Cell[,] _cells = new Cell[0, 0];
        // dimension of maze, _dim.X = width, _ dim.Y = height
        private Point _dim;
        public Maze(Point dim, Point s, Point e)
        {
            this._dim = new Point(dim);
            this._start = s; this._exit = e;
            // Filling array of cells
            FillArray(ref _cells, _dim.Y, _dim.X);
            // Clear start wall
            _cells[_start.Y, _start.X].WestWall = false;
            // Clear exit wall
            _cells[_exit.Y, _exit.X].EastWall = false;
        }
        public Cell[,] Cells
        {
            get { return _cells; }
            set { _cells = value; }
        }
        /// <summary>
        /// Create or Remove a line
        /// </summary>
        private void CreateLine(int X1, int Y1, int X2, int Y2, bool toCreate, Grid grid)
        {
            // Create a Line
            Line myLine = new Line();
            myLine.X1 = X1;
            myLine.Y1 = Y1;
            myLine.X2 = X2;
            myLine.Y2 = Y2;

            // Create a red Brush
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;

            // Set Line's width and color
            myLine.StrokeThickness = _lineThickness;
            myLine.Stroke = redBrush;

            // Add line to the Grid.         
            if (toCreate) grid.Children.Add(myLine);
            else grid.Children.Remove(myLine);
        }
        // Generate maze
        #region MazeGenerator
        private List<Direction> AnyUnvisitedNeighbors(Point currentCell)
        {
            List<Direction> unvisitedNeighbors = new List<Direction>();
            if (currentCell.Y - 1 >= 0)
                if (!_cells[currentCell.X, currentCell.Y - 1].Visited) unvisitedNeighbors.Add(Direction.left);
            if (currentCell.Y + 1 < _dim.X)
                if (!_cells[currentCell.X, currentCell.Y + 1].Visited) unvisitedNeighbors.Add(Direction.right);
            if (currentCell.X - 1 >= 0)
                if (!_cells[currentCell.X - 1, currentCell.Y].Visited) unvisitedNeighbors.Add(Direction.up);
            if (currentCell.X + 1 < _dim.Y)
                if (!_cells[currentCell.X + 1, currentCell.Y].Visited) unvisitedNeighbors.Add(Direction.down);
            return unvisitedNeighbors;
        }
        private Direction ChooseNeighbor(List<Direction> neighbors)
        {
            return neighbors[RandomInt(0, neighbors.Count)];
        }
        private Point RandomUnvisitedCell()
        {
            Point myPoint;
            for (int i = 0; i < _dim.Y; i++)
            {
                for (int j = 0; j < _dim.X; j++)
                {
                    if (!_cells[i, j].Visited)
                    {
                        myPoint = new Point(i, j);
                        return myPoint;
                    }
                }
            }
            myPoint = new Point();
            return myPoint;
        }
        private bool UnvisitedCellFromQueue(ref Point currentCell, ref Queue<Point> queue)
        {
            Point temp = new Point();
            do
            {
                if (queue.Count > 0)
                {
                    temp = queue.Dequeue();
                }
                else
                    return false;
            } while (AnyUnvisitedNeighbors(temp).Count <= 0);
            currentCell = temp;
            return true;
        }
        private void BreakWalls(ref Point currentCell, Direction dir)
        {
            if (dir == Direction.left)
            {
                _cells[currentCell.X, currentCell.Y].WestWall = false;
                _cells[currentCell.X, currentCell.Y - 1].EastWall = false;
                currentCell = new Point(currentCell.X, currentCell.Y - 1);
            }
            else if (dir == Direction.right)
            {
                _cells[currentCell.X, currentCell.Y].EastWall = false;
                _cells[currentCell.X, currentCell.Y + 1].WestWall = false;
                currentCell = new Point(currentCell.X, currentCell.Y + 1);
            }
            else if (dir == Direction.up)
            {
                _cells[currentCell.X, currentCell.Y].NorthWall = false;
                _cells[currentCell.X - 1, currentCell.Y].SouthWall = false;
                currentCell = new Point(currentCell.X - 1, currentCell.Y);
            }
            else if (dir == Direction.down)
            {
                _cells[currentCell.X, currentCell.Y].SouthWall = false;
                _cells[currentCell.X + 1, currentCell.Y].NorthWall = false;
                currentCell = new Point(currentCell.X + 1, currentCell.Y);
            }
        }
        public void GenerateMaze()
        {
            // Step1: Randomly select a node (or cell) N
            Point currentCell = new Point(_start.Y, _start.X);
            Queue<Point> queue = new Queue<Point>();
            List<Direction> unvisitedNeighbors = new List<Direction>();
            Direction dir;

            Step2:
            // Push the node N onto a queue Q
            queue.Enqueue(currentCell);
            // Step3: Mark the cell N as visited
            _cells[currentCell.X, currentCell.Y].Visited = true;

            Step4:
            // Randomly select an adjacent cell A of node N that has not been visited. If all the neighbors of N have been visited:
            //      - Continue to pop items off the queue Q until a node is encountered with at least one 
            //          non -visited neighbor - assign this node to N and go to step 4
            //      - If no nodes exist: stop
            unvisitedNeighbors = AnyUnvisitedNeighbors(currentCell);

            if (unvisitedNeighbors.Count <= 0) // All the neighbors of current cell have been visited
            {
                if (UnvisitedCellFromQueue(ref currentCell, ref queue))
                    goto Step4;
                else
                    return;
            }
            else
            {
                dir = ChooseNeighbor(unvisitedNeighbors);
                // Step5: Break the wall between N and A
                // Step6: Assign the value A to N
                BreakWalls(ref currentCell, dir);
                // Step7: Go to step 2
                goto Step2;
            }
        }
        #endregion All functions needed to generate the maze
        // Find escape from maze
        #region MazeFindPath
        public bool MazeFindPath(int y, int x)
        {
            //TODO: opcje wyboru koloru kropki, scian i tla
            //TODO: znajdowanie wyjscia z labiryntu
            //TODO: okienko jak grac
            //TODO: grafika i animacje
            //TODO: dzwieki
            //TODO: kolorowanie scian na inny kolor jesli wystapi kolizja
            // If x,y poza labiryntem, return false.
            if (x < 0 || x > _dim.X - 1 || y < 0 || y > _dim.Y - 1) return false;
            // If x,y jest wyjsciem, return true.
            if (new Point(y, x) == _exit) return true;
            // If x,y nie jest sciezka otwarta (kropka), return false.
            if (_cells[x, y].State != States.open_path && new Point(y, x) != _start) return false;
            // Zaznacz x,y jako sciezka eksplorowana.
            _cells[x, y].State = States.explored_path;
            MessageBox.Show(y.ToString() + x.ToString());
            // If find_path do gory od x,y jest true, return true.
            if (MazeFindPath(x, y - 1) == true)
            {
                return true;
            }
            // If find_path w prawo od x,y jest true, return true.
            if (MazeFindPath(x + 1, y) == true)
            {
                return true;
            }
            // If find_path w dol od x,y jest true, return true.
            if (MazeFindPath(x, y + 1) == true)
            {
                return true;
            }
            // If find_path w lewo od x,y jest true, return true.
            if (MazeFindPath(x - 1, y) == true)
            {
                return true;
            }
            // Zaznacz x,y jako sciezka prowadzaca do nikad.
            _cells[x, y].State = States.bad_path;
            return false;
        }
        public void ColorPath(Grid grid)
        {
            for (int i = 0; i < _dim.Y; i++)
            {
                for (int j = 0; j < _dim.X; j++)
                {
                    if (_cells[i,j].State == States.explored_path)
                    {
                        MessageBox.Show("dasda");
                        // Create a red Ellipse
                        Rectangle square = new Rectangle();
                        Canvas myCanvas = new Canvas();

                        // Set position of the ellipse
                        square.Margin = new Thickness(i * MainWindow.lineLengh, j * MainWindow.lineLengh + 5, 0, 0);

                        // Create a SolidColorBrush with a red color to fill the 
                        // Ellipse with
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();

                        // Describes the brush's color using RGB values. 
                        // Each value has a range of 0-255.
                        mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
                        square.Fill = mySolidColorBrush;
                        square.StrokeThickness = 2;
                        square.Stroke = Brushes.Black;

                        // Set the width and height of the Ellipse
                        square.Width = MainWindow.lineLengh;
                        square.Height = MainWindow.lineLengh;

                        // Create a Canvas
                        Canvas.SetLeft(square, myCanvas.ActualWidth / 2.0);
                        Canvas.SetTop(square, myCanvas.ActualHeight / 2.0);

                        // Add the Ellipse to the Grid.
                        myCanvas.Children.Add(square);
                        grid.Children.Add(myCanvas);
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// Fill the array with default value (0)
        /// </summary>
        private void FillArray(ref Cell[,] LineList, int Row, int Col)
        {
            bool[] NestedList = new bool[Col];
            ResizeArray(ref LineList, Row, Col);
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                    LineList[i, j] = new Cell();
            }
        }
        /// <summary>
        /// Change size of array
        /// </summary>
        private void ResizeArray<T>(ref T[,] original, int newCoNum, int newRoNum)
        {
            var newArray = new T[newCoNum, newRoNum];
            int columnCount = original.GetLength(1);
            int columnCount2 = newRoNum;
            int columns = original.GetUpperBound(0);
            for (int co = 0; co <= columns; co++)
                Array.Copy(original, co * columnCount, newArray, co * columnCount2, columnCount);
            original = newArray;
        }
        /// <summary>
        /// Render maze to the grid
        /// </summary>
        public void RenderMaze(Grid grid)
        {
            for (int i = 0; i < _dim.Y; i++)
            {
                for (int j = 0; j < _dim.X; j++)
                {
                    if (_cells[i, j].NorthWall) // up
                    {
                        CreateLine(j * lineLengh, i * lineLengh,
                            (lineLengh + j * lineLengh), i * lineLengh, true, grid);
                    }
                    else
                    {
                        CreateLine(j * lineLengh, i * lineLengh,
                            (lineLengh + j * lineLengh), i * lineLengh, false, grid);
                    }
                    if (_cells[i, j].SouthWall) // down
                    {
                        CreateLine(j * lineLengh, (i * lineLengh) + lineLengh + _lineThickness,
                            (j * lineLengh) + lineLengh, (i * lineLengh) + lineLengh + _lineThickness, true, grid);
                    }
                    else
                    {
                        CreateLine(j * lineLengh, (i * lineLengh) + lineLengh + _lineThickness,
                            (j * lineLengh) + lineLengh, (i * lineLengh) + lineLengh + _lineThickness, false, grid);
                    }
                    if (_cells[i, j].EastWall) // right
                    {
                        CreateLine((j * lineLengh) + lineLengh + _lineThickness, i * lineLengh,
                            (j * lineLengh) + lineLengh + _lineThickness, (i * lineLengh) + lineLengh, true, grid);
                    }
                    else
                    {
                        CreateLine((j * lineLengh) + lineLengh, i * lineLengh,
                           (j * lineLengh) + lineLengh, (i * lineLengh) + lineLengh, false, grid);
                    }
                    if (_cells[i, j].WestWall) // left
                    {
                        CreateLine(j * lineLengh, i * lineLengh,
                            j * lineLengh, lineLengh + (i * lineLengh), true, grid);
                    }
                    else
                    {
                        CreateLine(j * lineLengh, i * lineLengh,
                          j * lineLengh, (i * lineLengh) + lineLengh, false, grid);
                    }

                }
            }
        }
    }
}
