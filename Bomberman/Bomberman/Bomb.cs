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

        int x, y;
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

        public Explosion(int x, int y, Maze m)
        {
            this.x = x;
            this.y = y;
            int cx = x + MazeBlock.width / 2, cy = y + MazeBlock.height/2;

            m.Destroy((uint)(cx / MazeBlock.width), (uint)(cy / MazeBlock.height));
            m.Destroy((uint)(cx / MazeBlock.width)+1, (uint)(cy / MazeBlock.height));
            m.Destroy((uint)(cx / MazeBlock.width)-1, (uint)(cy / MazeBlock.height));
            m.Destroy((uint)(cx / MazeBlock.width), (uint)(cy / MazeBlock.height)+1);
            m.Destroy((uint)(cx / MazeBlock.width), (uint)(cy / MazeBlock.height)-1);
        }

        public override void Update(GameTime gt)
        {
        }

        public override void Draw()
        {
            spriteBatch.Draw(tex, new Vector2(x, y-32), Color.White);
            spriteBatch.Draw(tex, new Vector2(x - 32, y), Color.White);
            spriteBatch.Draw(tex, new Vector2(x, y), Color.White);
            spriteBatch.Draw(tex, new Vector2(x + 32, y), Color.White);
            spriteBatch.Draw(tex, new Vector2(x, y + 32), Color.White);
        }
    }

    public class Bomb : GameObject
    {
        static Texture2D[] tex;
        static Maze maze;
        enum State {Active, Exploding, Dead}

        public static void setMaze(Maze m)
        {
            maze = m;
        }

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

        }

        int x, y;
        int i;
        BombTicker timer;
        State state;
        Explosion explosion;

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
                        explosion = new Explosion(x, y, maze);
                    }
                    else timer = new BombTicker(0.1f + (i + 1) % 2);
                }
                else
                {
                    state = State.Dead;
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
