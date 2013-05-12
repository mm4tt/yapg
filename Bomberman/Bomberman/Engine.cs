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
using Bomberman.Levels;
namespace Bomberman
{
    [DataContract()]
    public class Engine
    {
        #region Singleton
        private static Engine instance;
        public Engine()
        {
            Level = 1;
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

        

        ILevelGenerator levelGenerator = new SimpleLevelGenerator();
        public void LevelAccomplished()
        {
            scoreHolder.LevelPassed(Level);
            ++Level;
           
            GenerateLevel();
        }

        public void GenerateLevel()
        {
            levelGenerator.GenerateLevel(this);
        }


        public delegate void LevelFailedEventHandler();
        public event LevelFailedEventHandler LevelFailed;
     
        private ScoreHolder scoreHolder = new ScoreHolder();
        [DataMember()]
        public ScoreHolder @ScoreHolder
        {
            get { return scoreHolder; }
            set { scoreHolder = value; }
        }

        [DataMember()]
        public int Level
        {
            get;
            set;
        }


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

        private int level = 1;
        private int score = 0;

        [DataMember()]
        public int Level
        {
            get { return level; }
            set{ level = value;}
        }
        [DataMember()]
        public int Score
        {
            get{ return score;}
            set{ score = value;}
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

        public void Clear()
        {
            gameObjects.Clear();
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
            int k = 0;
            for (int i = 0; i < gameObjects.Count; ++i)
            {
                gameObjects[i].Update(gameTime);
                if (!gameObjects[i].IsDead)
                    gameObjects[k++] = gameObjects[i];
            }
            gameObjects.RemoveRange(k, gameObjects.Count - k);


            if (!Player.Alive && LevelFailed!=null )
                LevelFailed();
            if (Enemies.Count() == 0 )
                LevelAccomplished();
        }

        public bool LevelFailedEmpty
        {
            get { return LevelFailed == null;  }
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

        #region AccelometerStuff
        public int dx;
        public int dy;
        public Boolean accelometrOn;
        #endregion

        
        
    }
}
