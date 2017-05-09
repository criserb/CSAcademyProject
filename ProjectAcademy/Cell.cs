using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAcademy
{
    class Cell
    {
        private bool _northWall = true;
        private bool _southWall = true;
        private bool _westWall = true;
        private bool _eastWall = true;
        private bool _visited = false;
        
        /// <summary>
        /// up wall
        /// </summary>
        public bool NorthWall
        {
            get { return _northWall; }
            set { _northWall = value; }
        }
        /// <summary>
        /// down wall
        /// </summary>
        public bool SouthWall
        {
            get { return _southWall; }
            set { _southWall = value; }
        }
        /// <summary>
        /// left wall
        /// </summary>
        public bool WestWall
        {
            get { return _westWall; }
            set { _westWall = value; }
        }
        /// <summary>
        /// right wall
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
