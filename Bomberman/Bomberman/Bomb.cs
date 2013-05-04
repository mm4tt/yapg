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
    class Explosion : GameObject
    {
        static Texture2D tex;

        public static void Load(ContentManager content)
        {
            tex = content.Load<Texture2D>("explosion");
        }

        /*
        private void Destroy(int x, int y, Maze m)
        {
            if (m.getBlock(x, y) == MazeBlock.Obstacle)
            {
                m.setBlock(x, y, MazeBlock.Empty);
                Random random = new Random(DateTime.Now.Millisecond);
                int i;
                if ((i=random.Next(100)) < 30)
                {
                    Modifier mod = new Modifier(x-16, y-16, (ModyfierType)(i%2));
                }
            }
        }
         */
        private void Destroy(int x, int y)
        {
            foreach( var en in Engine.Instance.Enemies )
            {
                if (collide(en.Position.X * MazeBlock.width, en.Position.Y * MazeBlock.height, x, y))
                    en.IsDead = true;
            }
        }

        public Explosion(int x, int y)
        {
            this.x = x;
            this.y = y;
            int cx = x + MazeBlock.width / 2, cy = y + MazeBlock.height/2;

            Engine.Instance.Maze.Destroy((uint)(cx / MazeBlock.width), (uint)(cy / MazeBlock.height));
            Destroy(x, y);
            Engine.Instance.Maze.Destroy((uint)(cx / MazeBlock.width) + 1, (uint)(cy / MazeBlock.height));
            Destroy(x + 1, y);
            Engine.Instance.Maze.Destroy((uint)(cx / MazeBlock.width) - 1, (uint)(cy / MazeBlock.height));
            Destroy(x - 1, y);
            Engine.Instance.Maze.Destroy((uint)(cx / MazeBlock.width), (uint)(cy / MazeBlock.height) + 1);
            Destroy(x, y + 1);
            Engine.Instance.Maze.Destroy((uint)(cx / MazeBlock.width), (uint)(cy / MazeBlock.height) - 1);
            Destroy(x, y - 1);
        }

       
        public override void Update(GameTime gt)
        {
        }

        private void DrawAt(int x, int y)
        {
            int cx = x + MazeBlock.width / 2, cy = y + MazeBlock.height / 2;
            if (Engine.Instance.Maze.isPassable((uint)(cx / MazeBlock.width), (uint)(cy / MazeBlock.height)))
                spriteBatch.Draw(tex, new Vector2(x, y), Color.White);
        }

        public override void Draw()
        {
            DrawAt(x, y - MazeBlock.height);
            DrawAt(x - MazeBlock.width, y);
            DrawAt(x, y);
            DrawAt(x + MazeBlock.width, y);
            DrawAt(x, y + MazeBlock.height);
        }
    }

    public class Bomb : GameObject
    {
        static Texture2D[] tex;
        enum State {Active, Exploding, Dead}


        public static void Load(ContentManager content)
        {
            tex = new Texture2D[2];
            tex[0] = content.Load<Texture2D>("bomb");
            tex[1] = content.Load<Texture2D>("bomb1");
            Explosion.Load(content);
        }


        class BombTicker : Ticker
        {
            public BombTicker(float p) : base(p) {}

            public int GetRemaining()
            {
                return remaining;
            }
        }

        int x, y;
        int i;
        BombTicker timer;
        State state;
        Explosion explosion;
        Point position = new Point(0,0);
        public Point Position {
            get {
                position.X = x;
                position.Y = y;
                return position;
            }
        }
        public bool isActive(){
            if (state == State.Active)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isDead()
        {
            if (state == State.Dead)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Bomb()
        {
            this.x = 0;
            this.y = 0;
        }

        public Bomb(int x, int y)
        {
            this.x = x;
            this.y = y;
            timer = new BombTicker(1.1f);
            state = State.Active;
        }

        public Bomb(int x, int y, float elapsed)
        {
            this.x = x;
            this.y = y;
            i = (int)(elapsed / 1.2f)*2;
            if (elapsed % 1.2f > 1.1f)
            {
                ++i;
                timer = new BombTicker((elapsed % 1.2f)-1.1f);
            }else
                timer = new BombTicker(elapsed % 1.2f);

            state = State.Active;
        }

        public float ElapsedTime()
        {
            return (i / 2) * 1.2f + (i%2)*1.1f + (float)timer.GetRemaining()/1000;
        }
        public override void Update(GameTime gt)
        {
            if (state!=State.Dead && timer.Update(gt))
            {
                if (state == State.Active)
                {
                    if (i++ > 4)
                    {
                        timer = new BombTicker(1);
                        state = State.Exploding;
                        explosion = new Explosion(x, y);
                    }
                    else timer = new BombTicker(0.1f + (i + 1) % 2);
                }
                else
                {
                    state = State.Dead;
                    this.IsDead = true;
                }
            }
        }

        public override void Draw()
        {
            if (state == State.Exploding)
                explosion.Draw();
            else if (state == State.Active)
            {
                spriteBatch.Draw(tex[i % 2], new Vector2(x, y), Color.White);
            }
        }
    }
}
