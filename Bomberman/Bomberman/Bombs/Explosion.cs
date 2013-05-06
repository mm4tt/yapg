using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Bomberman.Bombs
{
    [DataContract()]
    public class Explosion : GameObject
    {
        //static Texture2D tex;
        private Texture2D tex;
        int range;
        List<Point> fire = new List<Point>();
        static Point[] dirs = new Point[] { new Point(0, 1), new Point(0, -1), new Point(-1, 0), new Point(1, 0) };

        Point position;

        [DataMember()]
        public Point Position
        {
            set
            {
                position = value;
            }
            get { return position; }
        }

        public void Load(ContentManager content)
        {
            tex = content.Load<Texture2D>("explosion");
        }
        private void Destroy(int x, int y)
        {
            Engine.Instance.Maze.Destroy((uint)x, (uint)y);

            foreach (var en in Engine.Instance.Enemies)
            {
                if (en.Position.X == x && en.Position.Y == y)
                {
                    en.IsDead = true;
                    Engine.Instance.ScoreHolder.KilledEnemy();
                }
            }
        }

        public Explosion(int x, int y, int range)
        {
            position = new Point(x, y);
            this.range = range;
            

            Destroy(x, y);
            fire.Add(new Point(x, y));

            for (int i = 0; i < dirs.Length; i++)
            {
                for (int j = 1; j <= range; j++)
                {
                    int dx = x + j * dirs[i].X;
                    int dy = y + j*dirs[i].Y;
                    if( dx >= Maze.Width || dx < 0 || dy >= Maze.Height || dy < 0 )
                        break;
                    if (!Engine.Instance.Maze.isSolid((uint)dx, (uint)dy))
                    {
                        fire.Add(new Point(dx, dy));
                    }
                    if (!Engine.Instance.Maze.isPassable((uint)dx, (uint)dy))
                    {
                        Engine.Instance.Maze.Destroy((uint)dx, (uint)dy);
                        break;
                    }
                    Destroy(dx, dy);
                }
            }
        }

        public override void Update(GameTime gt)
        {
        }


        public override void Draw(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            //if (tex == null || tex.GraphicsDevice != spriteBatch.GraphicsDevice)
            Load(contentManager);

            spriteBatch.Draw(tex, new Rectangle(position.X * Maze.BlockWidth, position.Y * Maze.BlockHeight, Maze.BlockWidth, Maze.BlockHeight), Color.White);

            foreach (Point p in fire)
            {
                spriteBatch.Draw(tex, new Rectangle(p.X * Maze.BlockWidth, p.Y * Maze.BlockHeight, Maze.BlockWidth, Maze.BlockHeight), Color.White);
            }
        }
    }
}
