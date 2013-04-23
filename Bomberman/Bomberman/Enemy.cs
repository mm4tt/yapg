using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Bomberman
{
    public class Enemy : GameObject
    {
        static Texture2D[] tex;
        static Maze maze;
        static Player player;
        static Random random = new Random(DateTime.Now.Millisecond);
        const float speed = 0.003f ; // pr�dko�� w polach/ms
        enum State { Active, Dead };
        enum Faced { South=0, North, West, East };
        static Point[] dirs = new Point[] { new Point(0, 1), new Point(0, -1), new Point(-1, 0), new Point(1, 0)};

        State state;
        Faced faced = Faced.North;
        Point position;

        public Point Position
        {
            set
            {
                position = value;
            }
            get { return position; }
        }

        public static void Initialize(Maze m, Player p)
        {
            maze = m;
            player = p;
        }

        public Enemy()
        {
            Point p;
            do
                p = new Point(random.Next((int)Maze.Width-1), random.Next((int)Maze.Height-1));
            while (Math.Max(Math.Abs(p.X - player.Position.X), Math.Abs(p.Y - player.Position.Y)) < 4 || !maze.isPassable((uint)p.X, (uint)p.Y));
            position = p;
            x = p.X * MazeBlock.width;
            y = p.Y * MazeBlock.height;
        }

        public Enemy(int x, int y)
        {
            this.x = x;
            this.y = y;

            int cx = x + MazeBlock.width / 2, cy = y + MazeBlock.height/2;
            position = new Point(cx / MazeBlock.width, cy / MazeBlock.height);
            state = State.Active;
        }

        public static void Load(ContentManager content)
        {
            tex = new Texture2D[1];
            tex[0] = content.Load<Texture2D>("ghost");
            Explosion.Load(content);
        }

        class QNode
        {
            public Point p;
            public QNode parent;

            public QNode(Point p, QNode parent)
            {
                this.p = p;
                this.parent = parent;
            }
        }

        protected Point findPath()
        {
            Queue<QNode> q = new Queue<QNode>();
            bool[,] color = new bool[Maze.Width, Maze.Height];
            Point p = this.position, r;
            QNode t = new QNode(p, null);
            q.Enqueue(t);
            while (q.Count != 0)
            {
                t = q.Dequeue();
                p = t.p;
                for (int i = 0; i < 4; i++)
                {
                    r = add(p, dirs[i]);
                    if (r.X == player.Position.X && r.Y == player.Position.Y)
                        goto found;
                    if (maze.isPassable((uint)r.X, (uint)r.Y) && Math.Abs(r.X - position.X) < 6 && Math.Abs(r.Y - position.Y) < 6 && !color[r.X,r.Y])
                    {
                        q.Enqueue(new QNode(r, t));
                        color[r.X, r.Y] = true;
                    }
                }
            }
            return new Point(0,0);
        found:
            while (t.parent.parent != null)
                t = t.parent;
            return new Point(t.p.X - t.parent.p.X, t.p.Y - t.parent.p.Y);
        }

        protected void step()
        {
            Point p = add(position, dirs[(int)faced]);
            if ((!maze.isPassable((uint)p.X, (uint)p.Y)) || random.Next(8) == 0)
            {
                for (int it = (int)faced + (((int)faced + 1) % 2) + 1; it < (int)faced + (((int)faced + 1) % 2) + 5; it++)
                {
                    //if (!maze.isPassable((uint)p.X, (uint)p.Y) || it != (int)faced / 2 || it == (int)faced)
                    //{
                        p = add(position, dirs[it % 4]);
                        if (maze.isPassable((uint)p.X, (uint)p.Y))
                        {
                            faced = (Faced)(it % 4);
                            offset = 1.0f;
                            break;
                        }
                    //}
                }
            }
            else
            {
                offset = 1.0f;
            }
        }

        protected float offset = 0.0f;

        public override void Update(GameTime gt)
        {
            if (collide(this, player))
            {
                //TODO: Game Over
            }
            else
            {
                if (offset > 0.0f)
                {
                    offset -= gt.ElapsedGameTime.Milliseconds * speed;
                    switch (faced)
                    {
                        case Faced.North: y = (int)((position.Y - (1 - offset)) * MazeBlock.height);
                            if (offset <= 0.0f)
                                position.Y--;
                            break;
                        case Faced.South: y = (int)((position.Y + (1 - offset)) * MazeBlock.height);
                            if (offset <= 0.0f)
                                position.Y++;
                            break;
                        case Faced.West: x = (int)((position.X - (1 - offset)) * MazeBlock.width);
                            if (offset <= 0.0f)
                                position.X--;
                            break;
                        case Faced.East: x = (int)((position.X + (1 - offset)) * MazeBlock.width);
                            if (offset <= 0.0f)
                                position.X++;
                            break;
                    }
                }
                else
                {
                    if (Math.Max(Math.Abs(this.position.X - player.Position.X), Math.Abs(this.position.Y - player.Position.Y)) < 9)
                    {
                        Point p = findPath();
                        if (p.Y > 0)
                            faced = Faced.South;
                        else if (p.Y < 0)
                            faced = Faced.North;
                        else if (p.X < 0)
                            faced = Faced.West;
                        else if (p.X > 0)
                            faced = Faced.East;
                        if (p.X == 0 && p.Y == 0)
                            step();
                        else
                            offset = 1.0f;
                    }
                    else
                    {
                        step();
                    }
                }
            }
        }

        public override void Draw()
        {
            if (state == State.Active)
            {
                spriteBatch.Draw(tex[0], new Vector2(x, y), Color.White);
            }
        }
    }
}