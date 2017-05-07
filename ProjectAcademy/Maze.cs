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
        private bool[,] _verticalLine = new bool[0, 0];
        private bool[,] _horizontalLine = new bool[0, 0];
        private int width, height;
        private Point _start, _exit;
        public Maze(int w, int h, Point s, Point e)
        {
            this._start = s; this._exit = e;
            this.width = w; this.height = h;
            // Filling line arrays
            FillArray(ref _verticalLine, w + 1, h); // +1 because last column (w=10, we need 11 columns)
            FillArray(ref _horizontalLine, h + 1, w); // +1 because last row (w=10, we need 11 rows)
            // Setting start and exit
            _verticalLine[this._start.X, this._start.Y] = false;
            _verticalLine[this._exit.X, this._exit.Y] = false;
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
            myLine.X2 = X1 + X2;
            myLine.Y2 = Y1 + Y2;

            // Create a red Brush
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;

            // Set Line's width and color
            myLine.StrokeThickness = 1;
            myLine.Stroke = redBrush;

            // Add line to the Grid.         
            if (toCreate) grid.Children.Add(myLine);
            else grid.Children.Remove(myLine);
        }

        public void RenderMaze(Grid grid)
        {
            for (int i = 1; i <= width + 1; i++)
            {
                for (int j = 1; j < height + 1; j++)
                {
                    switch (_verticalLine[i - 1, j - 1])
                    {
                        case true:
                            CreateLine(lineLengh * i, lineLengh * j, 0, lineLengh, true, grid);
                            break;
                        case false:
                            CreateLine(lineLengh * i, lineLengh * j, 0, lineLengh, false, grid);
                            break;
                    }
                }
            }
            for (int i = 1; i <= height + 1; i++)
            {
                for (int j = 1; j < width + 1; j++)
                {
                    switch (_horizontalLine[i - 1, j - 1])
                    {
                        case true:
                            CreateLine(lineLengh * j, lineLengh * i, lineLengh, 0, true, grid);
                            break;
                        case false:
                            CreateLine(lineLengh * i, lineLengh * j, 0, lineLengh, false, grid);
                            break;
                    }
                }
            }
        }
        private void FillArray(ref bool[,] LineList, int Row, int Col)
        {
            bool[] NestedList = new bool[Col];
            ResizeArray(ref LineList, Row, Col);
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                    LineList[i, j] = true;
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
        /// <summary>
        /// Generate random int from minValue to max Value
        /// </summary>
    }
}
