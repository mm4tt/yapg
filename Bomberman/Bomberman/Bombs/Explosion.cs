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

        // dla explozji statyczne pola nie byly by takie zle
        public void Load(ContentManager content)
        {
            tex = content.Load<Texture2D>("explosion");
        }
        private void Destroy(int x, int y)
        {
            foreach (var en in Engine.Instance.Enemies)
            {
                if (collide(en.Position.X * Maze.BlockWidth, en.Position.Y * Maze.BlockHeight, x, y))
                {
                    en.IsDead = true;
                    Engine.Instance.ScoreHolder.killedEnemy();
                }
            }
        }

        public Explosion(int x, int y)
        {
            this.x = x;
            this.y = y;
            int cx = x + Maze.BlockWidth / 2, cy = y + Maze.BlockHeight / 2;

            Engine.Instance.Maze.Destroy((uint)(cx / Maze.BlockWidth), (uint)(cy / Maze.BlockHeight));
            Destroy(x, y);
            Engine.Instance.Maze.Destroy((uint)(cx / Maze.BlockWidth) + 1, (uint)(cy / Maze.BlockHeight));
            Destroy(x + 1, y);
            Engine.Instance.Maze.Destroy((uint)(cx / Maze.BlockWidth) - 1, (uint)(cy / Maze.BlockHeight));
            Destroy(x - 1, y);
            Engine.Instance.Maze.Destroy((uint)(cx / Maze.BlockWidth), (uint)(cy / Maze.BlockHeight) + 1);
            Destroy(x, y + 1);
            Engine.Instance.Maze.Destroy((uint)(cx / Maze.BlockWidth), (uint)(cy / Maze.BlockHeight) - 1);
            Destroy(x, y - 1);
        }

        public override void Update(GameTime gt)
        {
        }

        private void DrawAt(int x, int y, SpriteBatch spriteBatch)
        {
            int cx = x + Maze.BlockWidth / 2, cy = y + Maze.BlockHeight / 2;
            if (Engine.Instance.Maze.isPassable((uint)(cx / Maze.BlockWidth), (uint)(cy / Maze.BlockHeight)))
                spriteBatch.Draw(tex, new Rectangle(x, y, Maze.BlockWidth, Maze.BlockHeight), Color.White);
        }

        public override void Draw(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            //if (tex == null || tex.GraphicsDevice != spriteBatch.GraphicsDevice)
            Load(contentManager);
            DrawAt(x, y - Maze.BlockHeight, spriteBatch);
            DrawAt(x - Maze.BlockWidth, y, spriteBatch);
            DrawAt(x, y, spriteBatch);
            DrawAt(x + Maze.BlockWidth, y, spriteBatch);
            DrawAt(x, y + Maze.BlockHeight, spriteBatch);
        }
    }
}
