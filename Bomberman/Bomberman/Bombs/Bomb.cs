using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Runtime.Serialization;
using Bomberman.Bombs;

namespace Bomberman
{
    

    [DataContract()]
    public class Bomb : GameObject
    {
        private  Texture2D[] tex;
        public enum State {Active, Exploding, Dead}

        [DataMember()]
        public int range
        {
            get;
            set;
        }

        public void Load(ContentManager content)
        {
            tex = new Texture2D[2];
            tex[0] = content.Load<Texture2D>("bomb");
            tex[1] = content.Load<Texture2D>("bomb1");
           // Explosion.Load(content);
        }


        [DataMember()]
        public int i
        {
            get;
            set;
        }
        [DataMember()]
        public bool playered
        {
            get;
            set;
        }
        [DataMember()]
        public  BombTicker timer
        {
            get;
            set;
        }
        [DataMember()]
        public State state
        {
            get;
            set;
        }
        [DataMember()]
        public Explosion explosion
        {
            get;
            set;
        }
        Point position;
        [DataMember()]
        public Point Position {
            get {
                return position;
            }
            set
            {
                position = value;
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
        public bool isDeadFun()
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

        public static bool exist(int x, int y)
        {
            foreach (Bomb b in Engine.Instance.Bombs)
            {
                if (b.position.X == x && b.position.Y == y)
                    return true;
            }
            return false;
        }

        public Bomb()
        {
            range = 1;
            playered = false;
        }

        public Bomb(int x, int y, int range)
        {
            position = new Point(x, y);
            this.range = range;
            timer = new BombTicker(1.1f);
            state = State.Active;
            playered = false;
        }

        public Bomb(int x, int y, int range, bool player)
        {
            position = new Point(x, y);
            this.range = range;
            timer = new BombTicker(1.1f);
            state = State.Active;
            playered = player;
        }

        public Bomb(int x, int y, int range, float elapsed)
        {
            position = new Point(x, y);
            this.range = range;
            i = (int)(elapsed / 1.2f)*2;
            if (elapsed % 1.2f > 1.1f)
            {
                ++i;
                timer = new BombTicker((elapsed % 1.2f)-1.1f);
            }else
                timer = new BombTicker(elapsed % 1.2f);

            playered = false;
            state = State.Active;
        }

        public float ElapsedTime()
        {
            return (i / 2) * 1.2f + (i%2)*1.1f + (float)timer.Remaning/1000;
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
                        Sound.Instance.Play("Sbomb");
                        state = State.Exploding;
                        explosion = new Explosion(this);
                    }
                    else
                    {
                        timer = new BombTicker(0.1f + (i + 1) % 2);
                        Sound.Instance.Play("Stick");
                    }
                }
                else
                {
                    state = State.Dead;
                    this.IsDead = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            if (state == State.Exploding)
            {
                
                explosion.Load(contentManager);
                explosion.Draw(spriteBatch, contentManager);
            }
            else if (state == State.Active)
            {
                //if (tex == null)
                Point p = StdGameScaler.Instance.cast(position);
                    Load(contentManager);
                    spriteBatch.Draw(tex[i % 2], new Rectangle((int)(p.X * Maze.BlockWidth), (int)(p.Y * Maze.BlockHeight), Maze.BlockWidth, Maze.BlockHeight), Color.White);
            }
        }
    }

 
}
