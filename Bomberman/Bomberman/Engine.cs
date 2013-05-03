using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace Bomberman
{
    [DataContract()]
    public class Engine
    {
        #region Singleton
        private static Engine instance = new Engine();
        public Engine()
        {
            Maze.GenerateRandom(4, 50);
        }
        public static Engine Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }

     
        #endregion


        private Maze maze = new Maze();
        [DataMember()]
        public Maze @Maze
        {
            get { return maze; }
            set { maze = value; }
        }
        private Player player;
        [DataMember()]
        public Player @Player {
            get { return player; }
            set { player = value; }
        }

        public void AddPlayer(Player p)
        {
            AddObject(player = p);
        }


        private List<GameObject> gameObjects = new List<GameObject>();

        /// <summary>
        ///     Method responsible for adding objects to the engine
        /// </summary>
        /// <param name="go">GameObject to add</param>
        public void AddObject( GameObject go )
        {
            gameObjects.Add(go);
        }


        [IgnoreDataMember()]
        public IEnumerable<Enemy> @Enemies
        {
            get 
            {
                foreach (var o in gameObjects)
                    if (o is Enemy)
                        yield return (Enemy)o;
            }
        }
        [IgnoreDataMember()]
        public IEnumerable<Bomb> @Bombs
        {
            get
            {
                foreach (var o in gameObjects)
                    if (o is Bomb)
                        yield return (Bomb)o;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            Maze.Draw(spriteBatch,contentManager);
            foreach (var o in gameObjects)
                o.Draw(spriteBatch, contentManager);
        }

        public void Update(GameTime gameTime)
        {
            int k=0;
            for (int i = 0; i < gameObjects.Count; ++i)
            {
                gameObjects[i].Update(gameTime);
                if (!gameObjects[i].IsDead)
                    gameObjects[k++] = gameObjects[i];
            }
            gameObjects.RemoveRange(k, gameObjects.Count - k);
        }
        #region SerializationStuff
        [DataMember()]
        public List<GameObject> GameObjectList
        {
            get
            {
                return gameObjects;
            }
            set
            {
                gameObjects = value;
            }
        }

        public void fixPlayer()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (Player.isThisOne(gameObject))
                    Player = (Player)gameObject;
            }
        }

        public void fixDependencies()
        {
           
            fixPlayer();
        }
        #endregion
    }
}
