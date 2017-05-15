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
using System.Threading;

//TODO: generowanie labiryntu
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
            // Filling cell array
            FillArray(ref _cells, _dim.Y, _dim.X);
            // Clear start wall
            _cells[_start.Y, _start.X].WestWall = false;
            // Clear exit wall
            _cells[_exit.Y, _exit.X].EastWall = false;
            //test
            //for (int i = 0; i < 10; i++)
            //{
            //    //TODO:usunac te petle
            //    _cells[6, i].EastWall = false;
            //    _cells[6, i].WestWall = false;
            //}
            //for (int i = 0; i < 10; i++)
            //{
            //    //TODO:usunac te petle
            //    _cells[i, 5].NorthWall = false;
            //    _cells[i, 5].SouthWall = false;
            //}
            // end test
        }
        public Cell[,] Cells
        {
            get { return _cells; }
            set { _cells = value; }
        }

        /// <summary>
        /// Create or Remove a line
        /// </summary>
        public void CreateLine(int X1, int Y1, int X2, int Y2, bool toCreate, Grid grid)
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
        public void RenderMaze(Grid grid)
        {
            for (int i = 0; i < _dim.Y; i++)
            {
                for (int j = 0; j < _dim.X; j++)
                {
                    if (_cells[i, j].NorthWall) // up
                    {
                        CreateLine(lineLengh + j * lineLengh, lineLengh + i * lineLengh,
                            (lineLengh + j * lineLengh) + lineLengh, lineLengh + i * lineLengh, true, grid);
                    }
                    else
                    {
                        CreateLine(lineLengh + j * lineLengh, lineLengh + i * lineLengh,
                            (lineLengh + j * lineLengh) + lineLengh, lineLengh + i * lineLengh, false, grid);
                    }
                    if (_cells[i, j].SouthWall) // down
                    {
                        CreateLine(lineLengh + j * lineLengh, lineLengh + (i * lineLengh) + lineLengh + _lineThickness,
                            lineLengh + (j * lineLengh) + lineLengh, lineLengh + (i * lineLengh) + lineLengh + _lineThickness, true, grid);
                    }
                    else
                    {
                        CreateLine(lineLengh + j * lineLengh, lineLengh + (i * lineLengh) + lineLengh + _lineThickness,
                            lineLengh + (j * lineLengh) + lineLengh, lineLengh + (i * lineLengh) + lineLengh + _lineThickness, false, grid);
                    }
                    if (_cells[i, j].EastWall) // right
                    {
                        CreateLine(lineLengh + (j * lineLengh) + lineLengh + _lineThickness, lineLengh + i * lineLengh,
                            lineLengh + (j * lineLengh) + lineLengh + _lineThickness, lineLengh + (i * lineLengh) + lineLengh, true, grid);
                    }
                    else
                    {
                        CreateLine(lineLengh + (j * lineLengh) + lineLengh, lineLengh + i * lineLengh,
                            lineLengh + (j * lineLengh) + lineLengh, lineLengh + (i * lineLengh) + lineLengh, false, grid);
                    }
                    if (_cells[i, j].WestWall) // left
                    {
                        CreateLine(lineLengh + j * lineLengh, lineLengh + i * lineLengh,
                            lineLengh + j * lineLengh, lineLengh + (i * lineLengh) + lineLengh, true, grid);
                    }
                    else
                    {
                        CreateLine(lineLengh + j * lineLengh, lineLengh + i * lineLengh,
                           lineLengh + j * lineLengh, lineLengh + (i * lineLengh) + lineLengh, false, grid);
                    }
                }
            }
        }
        private bool AnyUnvisitedCells()
        {
            foreach (var item in Cells)
            {
                if (!item.Visited)
                    return true;
            }
            return false;
        }
        public List<Direction> AnyUnvisitedNeighbors(Point currentCell)
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
        public void GenerateMaze()
        {
            Point currentCell = new Point(_start.Y, _start.X);
            _cells[currentCell.X, currentCell.Y].Visited = true;
            Stack<Point> stack = new Stack<Point>();
            List<Direction> unvisitedNeighbors = new List<Direction>();
            Direction dir;
            while (AnyUnvisitedCells())
            {
                unvisitedNeighbors = AnyUnvisitedNeighbors(currentCell);
                if (unvisitedNeighbors.Count != 0)
                {
                    stack.Push(currentCell);
                    dir = ChooseNeighbor(unvisitedNeighbors);
                    if (dir == Direction.left)
                    {
                        _cells[currentCell.X, currentCell.Y].WestWall = false;
                        _cells[currentCell.X, currentCell.Y - 1].EastWall = false;
                        currentCell.Y--;
                    }
                    else if (dir == Direction.right)
                    {
                        _cells[currentCell.X, currentCell.Y].EastWall = false;
                        _cells[currentCell.X, currentCell.Y + 1].WestWall = false;
                        currentCell.Y++;
                    }
                    else if (dir == Direction.up)
                    {
                        _cells[currentCell.X, currentCell.Y].NorthWall = false;
                        _cells[currentCell.X - 1, currentCell.Y].SouthWall = false;
                        currentCell.X--;
                    }
                    else if (dir == Direction.down)
                    {
                        _cells[currentCell.X, currentCell.Y].SouthWall = false;
                        _cells[currentCell.X + 1, currentCell.Y].NorthWall = false;
                        currentCell.X++;
                    }
                    _cells[currentCell.X, currentCell.Y].Visited = true;
                }
                else if (stack.Count > 0)
                {
                    currentCell = stack.Pop();
                }
                else
                {
                    currentCell = RandomUnvisitedCell();
                    _cells[currentCell.X, currentCell.Y].Visited = true;
                }
            }
        }
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
    }
}
