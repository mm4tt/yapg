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
        #region Singleton
        private static Engine instance = new Engine();
        private Engine()
        {
            Maze.GenerateRandom(4, 50);
        }
        public static Engine Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        

        private Maze maze = new Maze();
        public Maze @Maze
        {
            get { return maze; }
        }
        private Player player;
        public Player @Player {
            get { return player; }
        }

        public void AddPlayer(Player p)
        {
            AddObject(player = p);
        }


        private List<GameObject> gameObjects = new List<GameObject>();
        //TAK DODAJEMY COKOLWIEK!!! Np bombe
        public void AddObject( GameObject go )
        {
            gameObjects.Add(go);
        }



        public IEnumerable<Enemy> @Enemies
        {
            get 
            {
                foreach (var o in gameObjects)
                    if (o is Enemy)
                        yield return (Enemy)o;
            }
        }
        public IEnumerable<Bomb> @Bombs
        {
            get
            {
                foreach (var o in gameObjects)
                    if (o is Bomb)
                        yield return (Bomb)o;
            }
        }

        public void Draw()
        {
            Maze.Draw();
            foreach (var o in gameObjects)
                o.Draw();
        }

        public void Update(GameTime gameTime)
        {
            int k = 0;
            for (int i = 0; i < gameObjects.Count; ++i)
            {
                gameObjects[i].Update(gameTime);
                if (!gameObjects[i].IsDead)
                    gameObjects[k++] = gameObjects[i];
            }
            gameObjects.RemoveRange(k, gameObjects.Count - k);
        }

        #region AccelometerStuff
        public int dx;
        public int dy;
        public Boolean accelometrOn;
        #endregion

        internal void SetSpriteBatch(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            maze.SetSpriteBatch(spriteBatch);
            player.SpriteBatch = spriteBatch;
            GameObject.setSpriteBatch(spriteBatch);
        }
    }
}
