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
   
    public class Player : GameObject
    {
        private Point position;
       
        private Texture2D texture;
        protected Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;
        protected uint width;
        protected uint height;
        private IList<Bomb> bombs;
        private Maze maze;
        float speed = 0;
        int interval = 0;
        public float Speed{
            get { return speed; }
            set {
                if (value <= 0)
                {
                    speed = 1;
                }
                else {
                    speed = value;
                }
            }
        }
        public Player(Maze maze, IList<Bomb> bombs )
        {
            this.maze = maze;
            width = 20;
            height = 20;
            FindBeginPosition();
            this.bombs = bombs;
            Speed = 1;
        }
        
        public void LoadGraphic(){
            texture = new Texture2D( spriteBatch.GraphicsDevice , (int)width, (int)height);
            Color[] colors = new Color[width * height];
            for (int i = 0; i < colors.Length; ++i)
                colors[i] = Color.Azure;
            texture.SetData(colors);
        }
        public void LeaveBomb() {
            bombs.Add(new Bomb(Position.X*(int)width,Position.Y*(int)height ));
        }
        public Point Position {
            set { 
                position = value;
            }
            get { return position; }
        }
        public void UpdatePosition( int x, int y ){
            position.X = x;
            position.Y = y;
        }
        public void SetPosition( int x, int y ){
            Position = new Point( x, y );
        }
        public void SetPosition( uint x, uint y)
        {
            Position = new Point( (int)x, (int)y);
        }
        protected Rectangle ComputePosition(int x, int y)
        {
            return new Rectangle((int)x * (int)width, (int)y * (int)height, (int)width, (int)height);
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
        public override void Draw() {
            Draw( Position.X, Position.Y );
        }
        public  void Draw(uint x, uint y) {
            Draw( (int)x, (int)y );
        }
        
        public void goTo( int x, int y ){
            if (x == Position.X && y == Position.Y)
                return;
            int absHorizontal = Position.X > x  ? (int)(Position.X - x) : (int)(x - Position.X);
            int absVertical = Position.Y > y ? (int)(Position.Y - y) : (int)(y - Position.Y);
            int dirH = Position.X < x ? 1 : -1;
            int dirV = Position.Y < y ? 1 : -1;
            
            if (absHorizontal < absVertical)
            {
                //Debug.WriteLine("Vertical");
                if (maze.Block[(uint)Position.X, (uint)(Position.Y + dirV)] is Empty)
                {
                    UpdatePosition(Position.X, Position.Y + dirV);
                }
                else
                    if (maze.Block[(uint)(Position.X + dirH), (uint)Position.Y] is Empty &&  Position.Y != y)
                    {
                        UpdatePosition(Position.X + dirH, Position.Y);
                    }
            }
            else {
                //Debug.WriteLine("Horizontal");
                if (maze.Block[(uint)(Position.X + dirH), (uint)Position.Y] is Empty)
                {
                    UpdatePosition(Position.X + dirH, Position.Y);
                }
                else
                    if (maze.Block[(uint)Position.X, (uint)(Position.Y + dirV)] is Empty && Position.X != x )
                    {
                        UpdatePosition(Position.X, Position.Y + dirV);
                    }
            }
        }


        private void countEmptyNeighborhood( int x, int y,  ref List<Point> visited )
        {
            
            if (maze.Block[(uint)x, (uint)y] is Empty)
            {
              
                visited.Add(new Point(x,y) );
                Point p = new Point( x + 1, y );
                if (!visited.Contains(p)) {
                    countEmptyNeighborhood(x + 1, y,  ref visited);
                }
                p.X = x - 1;
                p.Y = y;
                if (!visited.Contains(p))
                {
                    countEmptyNeighborhood(x - 1, y,  ref visited);
                }
                p.X = x;
                p.Y = y + 1;
                if (!visited.Contains(p))
                {
                    countEmptyNeighborhood(x, y + 1 ,  ref visited);
                }
                p.X = x;
                p.Y = y - 1;
                if (!visited.Contains(p))
                {
                    countEmptyNeighborhood(x, y - 1,  ref visited);
                }
                
                
            }
           

        }

       
        public void FindBeginPosition() {
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
                        if ( visited.Count() > 2 ) {
                            SetPosition(i, j);

                           
                            goto Found;
                        }
                        visited.Clear();
                    }
                }
            }
            Found: { }
        }

        public override void Update(GameTime gameTime)
        {
            interval += gameTime.ElapsedGameTime.Milliseconds;
            if (TouchPanel.IsGestureAvailable) {
                if (TouchPanel.ReadGesture().GestureType == GestureType.DoubleTap) {
                    LeaveBomb();
                }
               
            }
           
            if (interval > 500.0 / Speed) {
                interval = 0;
                TouchCollection tc = TouchPanel.GetState();
                
                if (tc.Count() > 0)
                {

                    TouchLocation touched = tc[0];
                    //Debug.WriteLine("---------------");
                    //Debug.WriteLine(touched.Position.ToString());
                    goTo((int)(touched.Position.X / MazeBlock.width), (int)(touched.Position.Y / MazeBlock.height));
                    // tc.Clear();
                    // DrawAlone( Position.X, Position.Y );
                } 
            }
        }
        void DrawAlone( int x, int y ) {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, ComputePosition(x, y), Color.Black);
            spriteBatch.End();
        }
        void Draw( int x,  int y) {
            //Debug.WriteLine("Draw " + x.ToString() + " "+ y.ToString() );
            spriteBatch.Draw(texture, ComputePosition(x, y), Color.Black);
        }   
    }
}
