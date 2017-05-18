namespace ProjectAcademy
{
    public class Point
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
        /// <summary>
        /// Returns x coordinate
        /// </summary>
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        /// <summary>
        /// Returns y coordinate
        /// </summary>
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
    }
}
