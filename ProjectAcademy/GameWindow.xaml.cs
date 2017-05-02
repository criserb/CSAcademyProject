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
//TODO: rozdzielic labirynt na klase
namespace ProjectAcademy
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        protected bool[,] _verticalLine = new bool[0, 0];
        protected bool[,] _horizontalLine = new bool[0, 0];
        private const int _break = 20;
        private const int _border = 25;
        private int _count = 0;
        private Point _start, _exit;
        private Player _player;
        public GameWindow()
        {
            InitializeComponent();
        }
        //TODO: przejrzec i zmienic generowanie start i exit
        public GameWindow(int w, int h)
        {
            InitializeComponent();
            this.Width = _border * 3 + (_break * w) - _break;
            this.Height = _border * 4 + (_break * h);
            FillArray(ref _verticalLine, w + 1, h); // +1 because last column (w=10, we need 11 columns)l
            FillArray(ref _horizontalLine, h + 1, w); // +1 because last row (w=10, we need 11 rows)
            SettingPositions();
            _start = new Point();
            _exit = new Point();
            _start.RandomPoint(0, h);
            _start.X = 0;
            _exit.RandomPoint(0, h);
            _exit.X = w;
            _verticalLine[_start.X, _start.Y] = false;
            _verticalLine[_exit.X, _exit.Y] = false;
            LabyrinthGrid(ref _verticalLine, w, ref _horizontalLine, h);
            _player = new Player(_start);
            _player.Render();
        }
        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    {
                        MessageBox.Show("UP");
                        break;
                    }
                case Key.Down:
                    {
                        MessageBox.Show("DOWN");
                        break;
                    }
                case Key.Left:
                    {
                        MessageBox.Show("LEFT");
                        break;
                    }
                case Key.Right:
                    {
                        MessageBox.Show("RIGHT");
                        break;
                    }
            }
        }
        private void SettingPositions()
        {
            Btn_Back.Margin = new Thickness(_border - 5, this.Height - _border * 3 + 4, 0, 0);
            lbl_Time.Margin = new Thickness(this.Width - 6 * _border, this.Height - _border * 3, 0, 0);
            lbl_Time_Count.Margin = new Thickness(this.Width - 7 * _border, this.Height - _border * 3, 0, 0);
            lbl_Time_Sec.Margin = new Thickness(this.Width - 3 * _border, this.Height - _border * 3, 0, 0);
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lbl_Time_Count.Content = (++_count).ToString();
        }
        private void LabyrinthGrid(ref bool[,] VerticalLine, int w, ref bool[,] HorizontallLine, int h)
        {
            for (int i = 1; i <= w + 1; i++)
            {
                for (int j = 1; j < h + 1; j++)
                {
                    switch (VerticalLine[i - 1, j - 1])
                    {
                        case true:
                            CreateLine(_break * i, _break * j, 0, _break, true);
                            break;
                        case false:
                            CreateLine(_break * i, _break * j, 0, _break, false);
                            break;
                    }
                }
            }
            for (int i = 1; i <= h + 1; i++)
            {
                for (int j = 1; j < w + 1; j++)
                {
                    switch (HorizontallLine[i - 1, j - 1])
                    {
                        case true:
                            CreateLine(_break * j, _break * i, _break, 0, true);
                            break;
                        case false:
                            CreateLine(_break * i, _break * j, 0, _break, false);
                            break;
                    }
                }
            }
        }
        //private void GenerateLabyrinthGrid(int w, int h)
        //{
        //    for (int i = Border; i < Border + h * Break; i += Break)
        //    {
        //        for (int j = Border; j < Border + w * Break; j += Break)
        //        {
        //            CreateLine(j, i, 0, Break, true);
        //            CreateLine(j, i, Break, 0, true);
        //        }
        //    }
        //    for (int i = Border; i < Border + w * Break; i += Break)
        //    {
        //        CreateLine(i, Border + h * Break, Break, 0, true);
        //    }
        //    for (int i = Border; i < Border + h * Break; i += Break)
        //    {
        //        CreateLine(Border + w * Break, i, 0, Break, true);
        //    }
        //}
        /// <summary>
        /// Create line from coords (x,y) to coords (v,h), create = true - creating line, create = false - deleting line
        /// </summary>
        /// <param name="x">LinePoint.X1</param>
        /// <param name="y">LinePoint.Y1</param>
        /// <param name="v">LinePoint.X2</param>
        /// <param name="h">LinePoint.Y2</param>
        /// <param name="create">true-create/false-delete</param>
        private void CreateLine(int x, int y, int v, int h, bool create)
        {
            // Create a Line
            Line redLine = new Line();
            redLine.X1 = x;
            redLine.Y1 = y;
            redLine.X2 = x + v;
            redLine.Y2 = y + h;

            // Create a red Brush
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;

            // Set Line's width and color
            redLine.StrokeThickness = 1;
            redLine.Stroke = redBrush;

            // Add line to the Grid.         
            if (create) gameGrid.Children.Add(redLine);
            else gameGrid.Children.Remove(redLine);
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
    }
}
