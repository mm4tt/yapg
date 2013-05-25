using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Bomberman
{
    [KnownType(typeof(Ghost))]
    [KnownType(typeof(Bomber))]
    [DataContract()]
    public class Enemy : GameObject
    {

        protected Texture2D[] tex;
        protected static Random random = new Random(DateTime.Now.Millisecond);
        protected float speed; // prêdkoœæ w polach/ms
        public enum State { Active, Dead };
        public enum Faced { South=0, North, West, East };
        protected static Point[] dirs = new Point[] { new Point(0, 1), new Point(0, -1), new Point(-1, 0), new Point(1, 0)};


        [DataMember()]
        public State state
        {
            get;
            set;
        }
        public Faced faced = Faced.North;
        protected Point position;

        [DataMember()]
        public Point Position
        {
            set
            {
                position = value;
            }
            get { return position; }
        }

        [DataMember()]
        public Faced mFaced
        {
            get { return faced; }
            set { faced = value; }
        }


        private Point? previousPosition = null;
        [DataMember()]
        public Point PreviousPosition
        {
            get
            {
                if (previousPosition == null)
                    return position;
                else
                    return (Point)previousPosition;
            }
            set { previousPosition = value; }
        }
        


        public Enemy()
        {
            Point p;
            do
                p = new Point(random.Next((int)Maze.Width-1), random.Next((int)Maze.Height-1));
            while (Math.Max(Math.Abs(p.X - Engine.Instance.Player.Position.X), Math.Abs(p.Y - Engine.Instance.Player.Position.Y)) < 4 || !Engine.Instance.Maze.isPassable((uint)p.X, (uint)p.Y));
            position = p;
            speed = 0.003f;
        }

        public Enemy(int x, int y)
        {
            int cx = x + Maze.BlockWidth / 2, cy = y + Maze.BlockHeight / 2;
            position = new Point(cx / Maze.BlockWidth, cy / Maze.BlockHeight);
            state = State.Active;
            speed = 0.003f;
        }

        public virtual void Load(ContentManager content)
        {
            tex = new Texture2D[1];
            tex[0] = content.Load<Texture2D>("ghost");
            //Explosion.Load(content);
        }

        protected class QNode
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
                    if (r.X == Engine.Instance.Player.Position.X && r.Y == Engine.Instance.Player.Position.Y)
                    {
                        if (t.parent == null)
                            return new Point(0, 0);
                        while (t.parent.parent != null)
                            t = t.parent;
                        return new Point(t.p.X - t.parent.p.X, t.p.Y - t.parent.p.Y);

                    }
                    if (canPass(r.X, r.Y) && Math.Abs(r.X - position.X) < 6 && Math.Abs(r.Y - position.Y) < 6 && !color[r.X, r.Y])
                    {
                        q.Enqueue(new QNode(r, t));
                        color[r.X, r.Y] = true;
                    }
                }
            }
            return new Point(0,0);
        }

        protected virtual bool canPass(int x, int y)
        {
            return Engine.Instance.Maze.isPassable((uint)x, (uint)y) && !Bomb.exist(x, y);
        }

        protected virtual void step()
        {
            Point p = add(position, dirs[(int)faced]);
            if ((!canPass(p.X, p.Y)) || random.Next(8) == 0)
            {
                for (int it = (int)faced + (((int)faced + 1) % 2) + 1; it < (int)faced + (((int)faced + 1) % 2) + 5; it++)
                {
                    p = add(position, dirs[it % 4]);
                    if (canPass(p.X, p.Y))
                    {
                        faced = (Faced)(it % 4);
                        offset = 1.0f;
                        break;
                    }
                }
            }
            else
            {
                offset = 1.0f;
            }
        }

        protected virtual void castAI()
        {
            if (Math.Max(Math.Abs(this.position.X - Engine.Instance.Player.Position.X), Math.Abs(this.position.Y - Engine.Instance.Player.Position.Y)) < 9)
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

        protected float offset = 0.0f;

 

        protected void moveFromAccelometer()
        {
            int nx = position.X + Engine.Instance.dx;
            int ny = position.Y + Engine.Instance.dy;
            if (nx < 0 || ny < 0 || nx >= Maze.BlockHeight || ny >= Maze.BlockWidth) // wyje¿d¿amy poza polansze
                return;
            else
            {
                uint rnx = (uint)nx;
                uint rny = (uint)ny;
                if (!Engine.Instance.Maze.isPassable(rnx, rny)) // nie da siê do niego ruszyæ
                    return;
                position.X = nx;
                position.Y = ny;
            }
        }

        protected void nextMoveArtificialIntelignece()
        {
            if (Math.Max(Math.Abs(this.position.X - Engine.Instance.Player.Position.X), Math.Abs(this.position.Y - Engine.Instance.Player.Position.Y)) < 9)
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
        public override void Update(GameTime gt)
        {
            if (this.position.X == Engine.Instance.Player.Position.X && this.position.Y == Engine.Instance.Player.Position.Y)
            {
                Engine.Instance.Player.Alive = false;
            }
            else if (Engine.Instance.Player.Alive)
            {
                if (offset > 0.0f)
                {
                    offset -= gt.ElapsedGameTime.Milliseconds * speed;
                    if (offset <= 0.0f)
                    {
                        PreviousPosition = Position;
                        switch (faced)
                        {
                            case Faced.North: //y = (int)((position.Y - (1 - offset)) * Maze.BlockHeight);
                                position.Y--;
                                break;
                            case Faced.South: //y = (int)((position.Y + (1 - offset)) * Maze.BlockHeight);
                                position.Y++;
                                break;
                            case Faced.West: //x = (int)((position.X - (1 - offset)) * Maze.BlockWidth);
                                position.X--;
                                break;
                            case Faced.East: //x = (int)((position.X + (1 - offset)) * Maze.BlockWidth);
                                position.X++;
                                break;
                        }
                    }
                    
                    if (offset <= 0.0f && Engine.Instance.accelometrOn) // nast¹pi³ jakiœ ruch i ackelometr jesst czynny spróbuj siê przesunaæ o tyle ile trzeba
                    {
                        moveFromAccelometer();
                    }
                }
                if (offset <= 0.0f)
                {
                    castAI();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            if (state == State.Active)
            {
                //Point p = StdGameScaler.Instance.cast(position);
                Load(contentManager);

                var p0 = StdGameScaler.Instance.Transform(PreviousPosition);
                var p1 = StdGameScaler.Instance.Transform(position);

                var p = p0 + (p1 - p0) * (1-offset);
              
              spriteBatch.Draw(tex[0], StdGameScaler.Instance.GetRectangle(p), Color.White);
            }
        }

        #region SERIALIZATIONSTUFF
        [DataMember()]
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        #endregion
    }
}
