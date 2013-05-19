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

        public Vector2 Transform(int x, int y)
        {
            return new Vector2(x * Maze.BlockWidth, (y + OFFSET) * Maze.BlockHeight);//
        }
        public Vector2 Transform(uint x, uint y)
        {
            return Transform((int)x, (int)y);
        }
        public Vector2 Transform(Point p)
        {
            return Transform(p.X, p.Y);
        }

        public Rectangle GetRectangle(Vector2 v)
        {
            return new Rectangle((int)v.X, (int)v.Y, Maze.BlockWidth, Maze.BlockHeight);
        }
    }
}
