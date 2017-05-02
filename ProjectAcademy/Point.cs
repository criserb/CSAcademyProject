using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAcademy
{
    class Point : GameWindow
    {
        private int _x, _y;
        static private Random rand = new Random();
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
        /// <summary>
        /// Generate random point from minValue to max Value
        /// </summary>
        public void RandomPoint(int minValue, int maxValue)
        {
            this._x = rand.Next(minValue, maxValue);
            this._y = rand.Next(minValue, maxValue);
        }
    }
}
