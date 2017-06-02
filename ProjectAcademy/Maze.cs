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
        private Color _lineColor;
        // dimension of maze, _dim.X = width, _ dim.Y = height
        private Point _dim;
        public Color LineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
        }
        public static Color DefaultLineColor
        {
            get { return Colors.Black; }
        }
        public static Color DefaultBackgroundColor
        {
            get { return Colors.White; }
        }
        public Maze(Point dim, Point s, Point e)
        {
            this._dim = dim;
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

            // Create a Brush
            SolidColorBrush lineBrush = new SolidColorBrush();
            lineBrush.Color = _lineColor;

            // Set Line's width and color
            myLine.StrokeThickness = _lineThickness;
            myLine.Stroke = lineBrush;

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
        public void Generate()
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
                {
                    //SaveMazesWallsToFile();
                    return;
                }
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
        /// <summary>
        /// Saving to maze to file "PresentationMaze.txt" 
        /// </summary>
        private void SaveMazeToFile()
        {
            string text = String.Empty;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    text = _cells[i, j].NorthWall.ToString() + ' ' +
                        _cells[i, j].SouthWall.ToString() + ' ' + _cells[i, j].WestWall.ToString() + ' ' + _cells[i, j].EastWall.ToString();
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"PresentationMaze.txt", true))
                    {
                        file.WriteLine(text);
                    }
                }
            }
        }
        #endregion All functions needed to generate the maze
        // Find escape from maze
        #region MazePathFinder
        public void FindPath()
        {
            Player _player = new Player(_start);
            Cells[_player.Position.Y, _player.Position.X].Fill = true;
            while (_player.Position.X != _exit.X && _player.Position.Y != _exit.Y)
            {
                if (!_player.WallCollision(_player.Position.X, _player.Position.Y, Cells, _dim, Direction.right))
                {
                    if (!Cells[_player.Position.Y, _player.Position.X].Fill)
                        _player.Position.X++;
                }
                else if (!_player.WallCollision(_player.Position.X, _player.Position.Y, Cells, _dim, Direction.up))
                {
                    if (!Cells[_player.Position.Y, _player.Position.X].Fill)
                        _player.Position.Y--;
                }
                else if (!_player.WallCollision(_player.Position.X, _player.Position.Y, Cells, _dim, Direction.left))
                {
                    if (!Cells[_player.Position.Y, _player.Position.X].Fill)
                        _player.Position.X--;
                }
                else if (!_player.WallCollision(_player.Position.X, _player.Position.Y, Cells, _dim, Direction.down))
                {
                    if (!Cells[_player.Position.Y, _player.Position.X].Fill)
                        _player.Position.Y++;
                }
                Cells[_player.Position.Y, _player.Position.X].Fill = true;
            }
        }
        public void ColorPath(Grid grid, Color color)
        {
            for (int i = 0; i < _dim.Y; i++)
            {
                for (int j = 0; j < _dim.X; j++)
                {
                    if (_cells[i, j].State == States.explored_path)
                    {
                        CreateRectangle(
                            (lineLengh - _lineThickness) + (j * lineLengh),
                            (lineLengh - _lineThickness) + (i * lineLengh),
                            grid, color);
                    }
                }
            }
        }/// <summary>
         /// Create rectangle on (X,Y) on grid with specified color
         /// </summary>
        private void CreateRectangle(int X, int Y, Grid grid, Color color)
        {
            // Create a Rectangle
            Rectangle myRectangle = new Rectangle();
            // Set Rectangle's width and height
            myRectangle.Width = lineLengh;
            myRectangle.Height = lineLengh;

            // Create a Brush
            SolidColorBrush Brush = new SolidColorBrush();
            Brush.Color = color;

            // Set Rectangle's color and margin
            myRectangle.Fill = Brush;
            myRectangle.Margin = new Thickness(X, Y, 0, 0);
            myRectangle.HorizontalAlignment = HorizontalAlignment.Left;
            myRectangle.VerticalAlignment = VerticalAlignment.Top;

            // Add rectangle to the Grid
            grid.Children.Add(myRectangle);
        }
        #endregion
        /// <summary>
        /// Fill the array with new instances of cells
        /// </summary>
        public void FillArray(ref Cell[,] LineList, int Row, int Col)
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
        public void Render(Grid grid)
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
                            ((j * lineLengh) + lineLengh + _lineThickness) - 1, (i * lineLengh) + lineLengh, true, grid);
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
