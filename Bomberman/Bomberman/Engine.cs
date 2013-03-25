using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bomberman
{
    class Engine
    {
        private Maze maze = new Maze();
        public Maze @Maze
        {
            get { return maze; }
        }

        private IList<Bomb> bombs = new List<Bomb>();

        public Engine()
        {
            Maze.GenerateRandom(4, 50);
            bombs.Add(new Bomb(100, 100));
            bombs.Add(new Bomb(120, 200));
            bombs.Add(new Bomb(180, 140));
        }

        public void Initialize()
        {
            Bomb.setMaze(maze);
        }

        public void Draw()
        {
            maze.Draw();
            foreach (Bomb b in bombs)
                b.Draw();
        }

        public void Update(GameTime gameTime)
        {
            foreach (Bomb b in bombs)
                b.Update(gameTime);
        }

        internal void SetSpriteBatch(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            maze.SetSpriteBatch(spriteBatch);
            GameObject.setSpriteBatch(spriteBatch);
        }
    }
}
