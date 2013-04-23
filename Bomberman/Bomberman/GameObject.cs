using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Bomberman
{
    public abstract class GameObject
    {
        protected static SpriteBatch spriteBatch;

        public static void setSpriteBatch(SpriteBatch sb)
        {
            spriteBatch = sb;
        }

        public static bool collide(GameObject objectA, GameObject objectB)
        {
            //int ax1 = objectA.x, ay1 = objectA.y, bx1 = objectB.x, by1 = objectB.y;
            //int ax2 = ax1 + MazeBlock.width, ay2 = ay1 + MazeBlock.height, bx2 = bx1 + MazeBlock.width, by2 = by1 + MazeBlock.height;
            //return ax1 < bx2 && ax2 > bx1 && ay1 < by2 && ay2 > by1;
            return Math.Abs((objectA.x - objectB.x) + (objectA.y - objectB.y)) < MazeBlock.width;
        }

        public static bool collide(int ax, int ay, int bx, int by)
        {
            return Math.Abs((ax - bx) + (ay - by)) < MazeBlock.width;
        }

        public static Point add(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        protected int x, y;

        abstract public void Update(GameTime gt);

        abstract public void Draw();

    }
}
