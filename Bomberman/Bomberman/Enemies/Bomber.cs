using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace Bomberman
{
    [DataContract()]
    public class Bomber : Enemy
    {
        Bomb bomb;

        [DataMember()]
        public Bomb @Bomb
        {
            get { return bomb; }
            set { bomb = value; }
        }

        Queue<int> track = new Queue<int>();

        [DataMember()]
        public int[] @Track
        {
            get { return track.ToArray(); }
            set
            {
                if (track == null)
                {
                    track = new Queue<int>(value);
                }
                else
                {
                    track.Clear();
                    int l = value.Length;
                    for (int i = 0; i < l; ++i)
                        track.Enqueue(value[i]);
                }
            }
        }

        public Bomber() : base()
        {
            Point p;
            int tries = 0;
            do
            {
                p = new Point(random.Next((int)Maze.Width - 1), random.Next((int)Maze.Height - 1));
                tries++;
            } while ((Math.Max(Math.Abs(p.X - Engine.Instance.Player.Position.X), Math.Abs(p.Y - Engine.Instance.Player.Position.Y)) < 4 ||
                !Engine.Instance.Maze.isPassable((uint)p.X, (uint)p.Y) ||
                Engine.Instance.Maze.freeSpace((uint)p.X, (uint)p.Y) < 5) &&
                tries < 1000000);

            if (tries < 1000000)
                position = p;

            speed = 0.0015f;
        }

        public Bomber(int x, int y)
            : base(x, y)
        {
            speed = 0.0015f;
        }

        public override void Load(ContentManager content)
        {
            tex = new Texture2D[1];
            tex[0] = content.Load<Texture2D>("ghost2");
        }

        protected override bool canPass(int x, int y)
        {
            if(bomb == null || bomb.state == Bomb.State.Dead)
                return Engine.Instance.Maze.isPassable((uint)x, (uint)y) && !Bomb.exist(x, y);
            else
                return Engine.Instance.Maze.isPassable((uint)x, (uint)y) && !Bomb.exist(x, y) &&
                    Math.Abs(x - bomb.Position.X) + Math.Abs(y - bomb.Position.Y) > 
                    Math.Abs(this.position.X - bomb.Position.X) + Math.Abs(this.position.Y - bomb.Position.Y);
        }

        private List<int> modRay()
        {
            List<int> ray = new List<int>();

            for (uint i = 0; i < Maze.Width; i++)
            {
                for (uint j = 0; j < Maze.Height; j++)
                {
                    if (Engine.Instance.Maze.Modifier[i, j] != null)
                    {
                        Point p = position;
                        List<int> l = new List<int>();
                        while (p.X != i || p.Y != j)
                        {
                            Point q = p;
                            if (Math.Abs(p.X - i) < Math.Abs(p.Y - j))
                            {
                                if (p.Y > j)
                                {
                                    q = add(p, dirs[(int)Faced.North]);
                                    if (canPass(q.X, q.Y))
                                    {
                                        l.Add((int)Faced.North);
                                    }
                                    else
                                    {
                                        l = new List<int>();
                                        break;
                                    }
                                }
                                else
                                {
                                    q = add(p, dirs[(int)Faced.South]);
                                    if (canPass(q.X, q.Y))
                                    {
                                        l.Add((int)Faced.South);
                                    }
                                    else
                                    {
                                        l = new List<int>();
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (p.X > i)
                                {
                                    q = add(p, dirs[(int)Faced.West]);
                                    if (canPass(q.X, q.Y))
                                    {
                                        l.Add((int)Faced.West);
                                    }
                                    else
                                    {
                                        l = new List<int>();
                                        break;
                                    }
                                }
                                else
                                {
                                    q = add(p, dirs[(int)Faced.East]);
                                    if (canPass(q.X, q.Y))
                                    {
                                        l.Add((int)Faced.East);
                                    }
                                    else
                                    {
                                        l = new List<int>();
                                        break;
                                    }
                                }
                            }
                            p = q;
                            Debug.WriteLine("$P " + p);
                        }
                        if (l.Count > 0 && (ray.Count == 0 || l.Count < ray.Count))
                            ray = l;
                    }
                }
            }
            return ray;
        }

        private bool placeBomb()
        {
            for (int i = 0; i < 4; i++)
            {
                Point dir = dirs[i];
                Point p = add(position, dir);
                if ( canPass( p.X, p.Y ) )
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Point dir_p = dirs[j];
                        Point q = add(p, dir_p);
                        if (canPass(q.X, q.Y) && !q.Equals(position))
                        {
                            bomb = new Bomb(Position.X, Position.Y, 1, false);
                            Engine.Instance.AddObject(bomb);
                            track.Enqueue(i);
                            track.Enqueue(j);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        protected override void step()
        {
            Engine.Instance.Maze.Destroy((uint)position.X, (uint)position.Y);

            Point p = add(position, dirs[(int)faced]);
            List<int> ray = modRay();
            Debug.WriteLine(ray.Count);

            if (track.Count > 0)
            {
                faced = (Faced)(track.Dequeue() % 4);
                offset = 1.0f;
            }
            else if (ray.Count > 0)
            {
                faced = (Faced)ray.ToArray()[0];
                offset = 1.0f;
                return;
            }
            else if ((!canPass(p.X, p.Y)) || random.Next(6) == 0)
            {
                if (Engine.Instance.Maze.Block[(uint)p.X, (uint)p.Y] == Obstacle.Instance && (bomb == null || bomb.state == Bomb.State.Dead))
                {
                    if (placeBomb())
                        return;
                }

                

                for (int it = (int)faced + (((int)faced + 1) % 2) + 1; it < (int)faced + (((int)faced + 1) % 2) + 5; it++)
                {
                    p = add(position, dirs[it % 4]);
                    if (canPass(p.X, p.Y))
                    {
                        faced = (Faced)(it % 4);
                        offset = 1.0f;
                        break;
                    }
                    else if (Engine.Instance.Maze.Block[(uint)p.X, (uint)p.Y] == Obstacle.Instance && (bomb == null || bomb.state == Bomb.State.Dead))
                    {
                        if (placeBomb())
                            return;
                    }
                }
            }
            else
            {
                offset = 1.0f;
            }
        }
    }
}
