using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Bomberman.StateManager;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Bomberman.Levels;
using System.Diagnostics;
using Bomberman.Screens;
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
            BrandNew = false;
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


        bool nextLevel = true;
        public bool NextLevel {
            get { return nextLevel; }
            set { nextLevel = value;  }
        }
        ILevelGenerator levelGenerator = new SimpleLevelGenerator();
        public void LevelAccomplished()
        {
            scoreHolder.LevelPassed(Level);
            ++Level;
            Sound.Instance.Play("Send");

            GenerateLevel();
            
        }

        public void GenerateLevel()
        {
            // po zapisywaniu levelGenerator moze byc null
            if (levelGenerator == null)
                levelGenerator = new SimpleLevelGenerator();
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

        public enum So
        {
            Casual,
            Easy,
            Normal
        }

        [DataMember()]
        public So Difficulty
        {
            get;
            set;
        }

        [DataMember()]
        public bool BrandNew
        {
            get;
            set;
        }


        private Maze maze = new Maze();
        [DataMember()]
        //[IgnoreDataMember()]
        public Maze @Maze
        {
            get { return maze; }
            set { maze = value; }
        }
        private Player player;
        [DataMember()]
       // [IgnoreDataMember()]
        public Player @Player {
            get { return player; }
            set { player = value; }
        }

        public void AddPlayer(Player p)
        {
            AddObject(player = p);
        }
        private Panel panel;
        [IgnoreDataMember()]
        public Panel @Panel
        {
            get { return panel; }
            set { panel = value; }
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
            nextLevel = true;
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
                if(o is Bomb)
                    o.Draw(spriteBatch, contentManager);

            foreach (var o in gameObjects)
                if ( !(o is Bomb) )
                    o.Draw(spriteBatch, contentManager);

            Debug.Assert(Panel != null,"Panel is null");
            Debug.Assert(spriteBatch != null, "spriteBatch is null");
            
            Panel.Draw(0, 0, spriteBatch, contentManager);
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

           // Debug.Assert(levelGenerator != null, "ILevelGenerator is not null");

            if (!Player.Alive && LevelFailed != null) {
                LevelFailed();
            }
               
            if (Enemies.Count() == 0 )
                LevelAccomplished();
            
        }
        [IgnoreDataMember()]
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

        public void fixStuff()
        {
            fixDependencies();
            if (panel == null)
                panel = new Panel();
        }
        #endregion

        #region AccelometerStuff
        [IgnoreDataMember()]
        public int dx;
        [IgnoreDataMember()]
        public int dy;
        [IgnoreDataMember()]
        public Boolean accelometrOn = false;
        #endregion

        
        
    }
}
