using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Serialization;
using Bomberman.Bombs;
namespace Bomberman
{
    [KnownType(typeof(Explosion))]
    [KnownType(typeof(Bomb))]
    [KnownType(typeof(Player))]
    [KnownType(typeof(Enemy))]
    [DataContract()]
    public abstract class GameObject
    {
       
        public static bool collide(GameObject objectA, GameObject objectB)
        {
            //int ax1 = objectA.x, ay1 = objectA.y, bx1 = objectB.x, by1 = objectB.y;
            //int ax2 = ax1 + MazeBlock.width, ay2 = ay1 + MazeBlock.height, bx2 = bx1 + MazeBlock.width, by2 = by1 + MazeBlock.height;
            //return ax1 < bx2 && ax2 > bx1 && ay1 < by2 && ay2 > by1;
            return Math.Abs(objectA.x - objectB.x) + Math.Abs(objectA.y - objectB.y) < MazeBlock.width;
        }

        public static bool collide(int ax, int ay, int bx, int by)
        {
            return Math.Abs(ax - bx) + Math.Abs(ay - by) < MazeBlock.width;
        }

        public static Point add(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        [DataMember()]
        public bool IsDead { get; set; }


      
        protected int x;
        protected int y;
        [DataMember()]
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        [DataMember()]
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
       
     
        abstract public void Update(GameTime gt);

        abstract public void Draw(SpriteBatch spriteBatch, ContentManager contentManager);

        protected GameObject()
        {
            IsDead = false;
        }

    }
}
