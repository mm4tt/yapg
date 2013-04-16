using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input.Touch;
namespace Bomberman
{
    class Effect {
        const int TIME_STEP = 1000;
        Modifier mod;
        int time;
        public bool Active {
            get { return ( time > 0 );}
        }
        public Effect(Modifier m) {
            mod = m;
            time = m.getRespirationTime();
        }
        public void onUpdate() {
            time -= TIME_STEP;
            if (time > 0) {
                mod.onUpdate();
            }
        }
        public void onBegin() {
            mod.onBegin();
        }
        public void onEnd() {
            mod.onEnd();
        }
    }
    public class Player : GameObject
    {
        #region CONSTS

        const int NONE_DIRECTION = 0;
        const int INTERVAL_ACTION = 500;
        const int INITIAL_BOMBS_AVAILABLE = 1;
        const int INITIAL_EXPLOSION_RANGE = 1;
        const int UP = 1;
        const int RIGHT = 2;
        const int DOWN = 3;
        const int LEFT = 4;
        const int MODE_MOVEMENT_DEFAULT = 0;
        const int MODE_MOVEMENT_THROW = 1;

        #endregion
        #region FIELDS
        private List<Effect> effects;
        private Texture2D texture;
        private Point position;
        protected Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;
        protected uint width;
        protected uint height;
        private IList<Bomb> bombs;
        private Maze maze;
        float speed = 0;
        int interval = 0;
        private int direction;
        int bombsAvailable = INITIAL_BOMBS_AVAILABLE;
        int explosionRange = INITIAL_EXPLOSION_RANGE;
        bool reversedDirection = false;
        int movementMode = MODE_MOVEMENT_DEFAULT;
        #endregion
        #region ATTRIBUTES
        public int BombsAvailable {
            get { return bombsAvailable; }
            set { bombsAvailable = value; }
        }
        public int ExplosionRange {
            get { return explosionRange;  }
            set { explosionRange = value; }
        }
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public int MovementMode {
            get { return movementMode;  }
            set { this.movementMode = value; }
        }
        public float Speed
        {
            get { return speed; }
            set
            {
                if (value <= 0)
                {
                    speed = 1;
                }
                else
                {
                    speed = value;
                }
            }
        }
        public Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set
            {
                spriteBatch = value;
                LoadGraphic();
            }
        }
        public Point Position
        {
            set
            {
                position = value;
            }
            get { return position; }
        }
        #endregion
        #region INITIALIZATION
        public Player(Maze maze, IList<Bomb> bombs)
        {
            effects = new List<Effect>();
            this.maze = maze;
            width = 20;
            height = 20;
            FindBeginPosition();
            this.bombs = bombs;
            Speed = 1;
            direction = NONE_DIRECTION;
        }

        public void LoadGraphic()
        {
            texture = new Texture2D(spriteBatch.GraphicsDevice, (int)width, (int)height);
            Color[] colors = new Color[width * height];
            for (int i = 0; i < colors.Length; ++i)
                colors[i] = Color.Azure;
            texture.SetData(colors);
        }


        #endregion
        #region DRAW
        public override void Draw()
        {
            Draw(Position.X, Position.Y);
        }
        public void Draw(uint x, uint y)
        {
            Draw((int)x, (int)y);
        }
        void Draw(int x, int y)
        {
            //Debug.WriteLine("Draw " + x.ToString() + " "+ y.ToString() );
            spriteBatch.Draw(texture, ComputePosition(x, y), Color.Black);
        }
        #endregion
        #region HELPERS
        private void countEmptyNeighborhood(int x, int y, ref List<Point> visited)
        {

            if (maze.Block[(uint)x, (uint)y] is Empty)
            {

                visited.Add(new Point(x, y));
                Point p = new Point(x + 1, y);
                if (!visited.Contains(p))
                {
                    countEmptyNeighborhood(x + 1, y, ref visited);
                }
                p.X = x - 1;
                p.Y = y;
                if (!visited.Contains(p))
                {
                    countEmptyNeighborhood(x - 1, y, ref visited);
                }
                p.X = x;
                p.Y = y + 1;
                if (!visited.Contains(p))
                {
                    countEmptyNeighborhood(x, y + 1, ref visited);
                }
                p.X = x;
                p.Y = y - 1;
                if (!visited.Contains(p))
                {
                    countEmptyNeighborhood(x, y - 1, ref visited);
                }


            }


        }
        private void FindBeginPosition()
        {
            //int empties = 0;
            for (uint i = 0; i < Maze.Width; i++)
            {
                for (uint j = 0; j < Maze.Height; j++)
                {
                    if (maze.Block[i, j] is Empty)
                    {
                        List<Point> visited = new List<Point>();
                        countEmptyNeighborhood((int)i, (int)j, ref visited);
                        // Debug.WriteLine( visited.Count().ToString());
                        if (visited.Count() > 2)
                        {
                            SetPosition(i, j);


                            goto Found;
                        }
                        visited.Clear();
                    }
                }
            }
        Found: { }
        }

        public void UpdatePosition(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }
        public void SetPosition(int x, int y)
        {
            Position = new Point(x, y);
        }
        public void SetPosition(uint x, uint y)
        {
            Position = new Point((int)x, (int)y);
        }
        protected Rectangle ComputePosition(int x, int y)
        {
            return new Rectangle((int)x * (int)width, (int)y * (int)height, (int)width, (int)height);
        }

        #endregion
        #region LOGIC
        public void addModifier(Modifier m) {
            Effect effect = new Effect( m );
            this.effects.Add( effect );
            effect.onBegin();
        }
        public void setBomb() {
            Debug.WriteLine("setBomb");
            
            bombs.Add(new Bomb(Position.X*(int)width,Position.Y*(int)height ));
        }


        public void goInDirection(int dir)
        {
            switch (dir)
            {
                case UP:
                    {
                        if (maze.Block[(uint)Position.X, (uint)(Position.Y + 1)] is Empty)
                        {
                            UpdatePosition(Position.X, Position.Y + 1);
                        }

                        break;
                    }
                case RIGHT:
                    {
                        if (maze.Block[(uint)Position.X + 1, (uint)(Position.Y)] is Empty)
                        {
                            UpdatePosition(Position.X + 1, Position.Y);
                        }
                        break;
                    }
                case DOWN:
                    {
                        if (maze.Block[(uint)Position.X, (uint)(Position.Y - 1)] is Empty)
                        {
                            UpdatePosition(Position.X, Position.Y - 1);
                        }
                        break;
                    }
                case LEFT:
                    {
                        if (maze.Block[(uint)Position.X - 1, (uint)(Position.Y)] is Empty)
                        {
                            UpdatePosition(Position.X - 1, Position.Y);
                        }
                        break;
                    }
            }

        }
        public void move(Vector2 delta)
        {
            Debug.WriteLine("Move ");
            Debug.WriteLine(delta);
            float absX = delta.X < 0 ? -delta.X : delta.X;
            float absY = delta.Y < 0 ? -delta.Y : delta.Y;
            if (absX > absY)
            {
                if (delta.X < 0)
                {
                    if (maze.Block[(uint)Position.X - 1, (uint)(Position.Y)] is Empty)
                        direction = LEFT;
                }
                if (delta.X > 0)
                {
                    if (maze.Block[(uint)Position.X + 1, (uint)(Position.Y)] is Empty)
                        direction = RIGHT;
                }
            }
            else
            {
                if (delta.Y > 0)
                {
                    if (maze.Block[(uint)Position.X, (uint)(Position.Y + 1)] is Empty)
                        direction = UP;
                }
                if (delta.Y < 0)
                {
                    if (maze.Block[(uint)Position.X, (uint)(Position.Y - 1)] is Empty)
                        direction = DOWN;
                }
            }
            //interval =( INTERVAL_ACTION / Speed ) + 1;
        }

        public override void Update(GameTime gameTime)
        {
            interval += gameTime.ElapsedGameTime.Milliseconds;
            if (interval > INTERVAL_ACTION / Speed)
            {
                interval = 0;
                foreach (var effect in effects)
                {
                    if (effect.Active)
                    {
                        effect.onUpdate();
                    }
                    else {
                        effect.onEnd();
                        effects.Remove(effect);
                    }
                }
                goInDirection(direction);
               
            }


        }
        #endregion

    }
}
