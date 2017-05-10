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
//TODO: generowanie labiryntu
namespace ProjectAcademy
{
    public class Maze : GameWindow
    {
        private const int _lineThickness = 1;
        private int _width, _height;
        private Point _start, _exit;
        private Cell[,] _cells = new Cell[0, 0];
        private Stack<Cell> _stackOfCells;
        private Point _currentCell;
        public Maze(int w, int h, Point s, Point e)
        {
            this._currentCell = new Point();
            this._stackOfCells = new Stack<Cell>();
            this._start = s; this._exit = e;
            this._width = w; this._height = h;
            // Filling cell array
            FillArray(ref _cells, h, w);
            _cells[_start.Y, _start.X].WestWall = false;
            //_cells[_exit.Y, _exit.X].EastWall = false;
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
        private bool AnyUnvisitedCells()
        {
            foreach (var item in Cells)
            {
                if (!item.Visited)
                    return true;
                else return false;
            }
            return true;
        }
        public void RenderMaze(Grid grid)
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
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
        public void GenerateMaze()
        {
            while (AnyUnvisitedCells())
            {
                _currentCell.X = _start.Y; _currentCell.Y = _start.X;
                _cells[_currentCell.X, _currentCell.Y].Visited = true;
                //TODO: zrobic znajdowanie nieodwiedzonych sasiadow
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
        void ResizeArray<T>(ref T[,] original, int newCoNum, int newRoNum)
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
