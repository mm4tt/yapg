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
                if (collide(en.Position.X * MazeBlock.width, en.Position.Y * MazeBlock.height, x, y))
                    en.IsDead = true;
            }
        }

        public Explosion(int x, int y)
        {
            this.x = x;
            this.y = y;
            int cx = x + MazeBlock.width / 2, cy = y + MazeBlock.height / 2;

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

        private void DrawAt(int x, int y, SpriteBatch spriteBatch)
        {
            int cx = x + MazeBlock.width / 2, cy = y + MazeBlock.height / 2;
            if (Engine.Instance.Maze.isPassable((uint)(cx / MazeBlock.width), (uint)(cy / MazeBlock.height)))
                spriteBatch.Draw(tex, new Vector2(x, y), Color.White);
        }

        public override void Draw(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            //if (tex == null || tex.GraphicsDevice != spriteBatch.GraphicsDevice)
            Load(contentManager);
            DrawAt(x, y - MazeBlock.height, spriteBatch);
            DrawAt(x - MazeBlock.width, y, spriteBatch);
            DrawAt(x, y, spriteBatch);
            DrawAt(x + MazeBlock.width, y, spriteBatch);
            DrawAt(x, y + MazeBlock.height, spriteBatch);
        }
    }
}
