using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    class Engine
    {
        private Maze maze = new Maze();
        public Maze @Maze
        {
            get { return maze; }
        }
        private Player player;
        public Player @Player {
            get { return player; }
        }

        public Engine()
        {
            Maze.GenerateRandom(4, 50);
            player = new Player( maze );

        }

        public void Draw()
        {
            maze.Draw();
            player.Draw();
        }

        public void Update(GameTime gameTime)
        {
            player.Update( gameTime);
        }

        internal void SetSpriteBatch(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            maze.SetSpriteBatch(spriteBatch);
            player.SpriteBatch = spriteBatch;
        }
    }
}
