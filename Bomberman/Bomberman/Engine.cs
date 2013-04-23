using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input.Touch;
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

        private IList<Bomb> bombs = new List<Bomb>();

        private IList<Enemy> enemies = new List<Enemy>();
        public IList<Enemy> @Enemies
        {
            get { return enemies; }
        }


        public Engine()
        {
            Maze.GenerateRandom(4, 50);

      
            
            //TouchPanel.EnabledGestures = GestureType.Tap;
            player = new Player( Maze, bombs );
            //bombs.Add(new Bomb(100, 100));
            //bombs.Add(new Bomb(120, 200));
            //bombs.Add(new Bomb(180, 140));

            TouchPanel.EnabledGestures = GestureType.None;
            TouchPanel.EnabledGestures = GestureType.Hold;
            TouchPanel.EnabledGestures = GestureType.Tap;
            TouchPanel.EnabledGestures = GestureType.DoubleTap;
            player = new Player(maze, bombs);
            Enemy.Initialize(maze, player);
            Bomb.Initialize(maze, Enemies);
            //bombs.Add(new Bomb(100, 100, 0.0f));
            //bombs.Add(new Bomb(120, 200, 2.0f));
            //bombs.Add(new Bomb(180, 140, 4.0f));
            //enemies.Add(new Enemy(player.Position.X * MazeBlock.width, player.Position.Y * MazeBlock.height+40));
            enemies.Add(new Enemy());
            enemies.Add(new Enemy());
            enemies.Add(new Enemy());
            enemies.Add(new Enemy());
            enemies.Add(new Enemy());
        }

        public void Initialize()
        {
        }

        public void Draw()
        {
            maze.Draw();
            player.Draw();
            foreach (Bomb b in bombs)
                b.Draw();
            foreach (Enemy e in enemies)
                e.Draw();
        }

        public void Update(GameTime gameTime)
        {
            player.Update( gameTime);
            foreach (Bomb b in bombs)
                b.Update(gameTime);
            foreach (Enemy e in enemies)
                e.Update(gameTime);
        }

        internal void SetSpriteBatch(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            maze.SetSpriteBatch(spriteBatch);
            player.SpriteBatch = spriteBatch;
            GameObject.setSpriteBatch(spriteBatch);
        }
    }
}
