using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;

using Microsoft.Xna.Framework.Input.Touch;
namespace Bomberman
{
    [XmlRoot]
    public class Engine 
    {
        private Maze maze = new Maze();
        [XmlElement("Maze")]
        public Maze @Maze
        {
            get { return maze; }
            set { maze = value;}
        }
        private Player player;

            
        [XmlElement("Player")]
        public Player @Player {
            get { return player; }
            set { player = value; }
        }

        private IList<Bomb> bombs = new List<Bomb>();

        [XmlIgnore]
        public IList<Bomb> @Bombs
        {
            get { return bombs;}
            set { bombs = value; }
        }

       [XmlArray("Bombs")]
        [XmlArrayItem("Bomb")]
     
        public Bomb[] bombArray
        {
            get { return bombs.ToArray<Bomb>(); }
            set { bombs = new List<Bomb>(value); }
        }

        public void LoadBombs(Bomb[] newBombs)
        {
            bombs = new List<Bomb>(newBombs);
        }

        public Engine()
        {
            Maze.GenerateRandom(4, 50);
            TouchPanel.EnabledGestures = GestureType.None;
            TouchPanel.EnabledGestures = GestureType.Hold;
            TouchPanel.EnabledGestures = GestureType.Tap;
            TouchPanel.EnabledGestures = GestureType.DoubleTap;
            player = new Player( maze, bombs );
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
            player.Draw();
            foreach (Bomb b in bombs)
                b.Draw();
        }

        public void Update(GameTime gameTime)
        {
            player.Update( gameTime);
            foreach (Bomb b in bombs)
                b.Update(gameTime);
        }

        internal void SetSpriteBatch(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Debug.WriteLine("Set sprtieBatch in Maze");
            maze.SetSpriteBatch(spriteBatch);
            Debug.WriteLine("Set spriteBatch in player");
            player.SpriteBatch = spriteBatch;
            Debug.WriteLine("set spriteBatch in GameObject");
            GameObject.setSpriteBatch(spriteBatch);
        }
    }
}
