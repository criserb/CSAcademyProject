using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAcademy
{
    public class Point : GameWindow
    {
        private int _x, _y;
        public Point(int a, int b)
        {
            _x = a;
            _y = b;
        }
        public Point(Point p)
        {
            _x = p.X;
            _y = p.Y;
        }
        public Point() { }
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
    }
}
