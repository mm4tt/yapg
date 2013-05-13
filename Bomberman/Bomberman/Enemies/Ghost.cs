using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Bomberman
{
    class Ghost : Enemy
    {
        public Ghost() : base()
        {
            speed = 0.002f;
        }

        public Ghost(int x, int y)
            : base(x, y)
        {
            speed = 0.002f;
        }

        public override void Load(ContentManager content)
        {
            tex = new Texture2D[1];
            tex[0] = content.Load<Texture2D>("ghost1");
        }

        protected override bool canPass(int x, int y)
        {
            return Engine.Instance.Maze.Block[(uint)x, (uint)y] != Wall.Instance;
        }

        protected override void castAI()
        {
            if (Math.Max(Math.Abs(this.position.X - Engine.Instance.Player.Position.X), Math.Abs(this.position.Y - Engine.Instance.Player.Position.Y)) < 5)
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
