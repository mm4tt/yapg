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

        public Engine()
        {
            Maze.GenerateRandom(4, 50);
        }

        public void Draw()
        {
            maze.Draw();
        }

        public void Update()
        {

        }

        internal void SetSpriteBatch(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            maze.SetSpriteBatch(spriteBatch);
        }
    }
}
