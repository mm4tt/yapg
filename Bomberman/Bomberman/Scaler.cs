using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bomberman
{
    class StdGameScaler
    {
        static int OFFSET = 2;
        static void setOffset(int offset) {
            OFFSET = offset;
        }
        #region Singleton
        private static StdGameScaler instance = new StdGameScaler();
        private StdGameScaler() { }
        float speed;
        public static StdGameScaler Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion
        public Point cast(int x, int y) {
            return new Point(x, y + OFFSET);
        }
        public Point cast(uint x, uint y)
        {
            return new Point((int)x, (int)y + OFFSET);
        }
        public Point cast( Point p )
        {
            return new Point( p.X, p.Y + OFFSET);
        }
        public int blockWidth() {
            return Maze.ScreenWidth / Maze.Width;
        }
        public int blockHeight( ){
            return Maze.ScreenHeight / (Maze.Height + OFFSET);
        }
    }
}
