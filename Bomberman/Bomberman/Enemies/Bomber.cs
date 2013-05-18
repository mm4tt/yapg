using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Serialization;

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
        public Bomber() : base()
        {
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

        protected override void step()
        {
            Engine.Instance.Maze.Destroy((uint)position.X, (uint)position.Y);

            Point p = add(position, dirs[(int)faced]);
            if ((!canPass(p.X, p.Y)) || random.Next(6) == 0)
            {
                if (Engine.Instance.Maze.Block[(uint)p.X, (uint)p.Y] == Obstacle.Instance && (bomb == null || bomb.state == Bomb.State.Dead))
                {
                    bomb = new Bomb(Position.X, Position.Y, 1, false);
                    Engine.Instance.AddObject(bomb);
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
                        bomb = new Bomb(Position.X, Position.Y, 1, false);
                        Engine.Instance.AddObject(bomb);
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
