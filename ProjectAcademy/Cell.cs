namespace ProjectAcademy
{
    public class Cell
    {
        private bool _northWall = true;
        private bool _southWall = true;
        private bool _westWall = true;
        private bool _eastWall = true;
        private bool _visited = false;
        private bool _fill = false;
        /// <summary>
        /// Up wall
        /// </summary>
        public bool Fill
        {
            get { return _fill; }
            set { _fill = value; }
        }
        public bool NorthWall
        {
            get { return _northWall; }
            set { _northWall = value; }
        }
        /// <summary>
        /// Down wall
        /// </summary>
        public bool SouthWall
        {
            get { return _southWall; }
            set { _southWall = value; }
        }
        /// <summary>
        /// Left wall
        /// </summary>
        public bool WestWall
        {
            get { return _westWall; }
            set { _westWall = value; }
        }
        /// <summary>
        /// Right wall
        /// </summary>
        public bool EastWall
        {
            get { return _eastWall; }
            set { _eastWall = value; }
        }
        public bool Visited
        {
            get { return _visited; }
            set { _visited = value; }
        }
    }
}
