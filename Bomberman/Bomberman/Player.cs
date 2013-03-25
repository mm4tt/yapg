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
        private Maze maze;
        public Player( Maze maze ) {
            this.maze = maze;
            width = 20;
            height = 20;
            for (uint i = 0; i < Maze.Width; i++) {
                for (uint j = 0; j < Maze.Height; j++) {
                    if (maze.Block[i, j] is Empty ) {
                        SetPosition( i, j );
                        goto Found;
                    }                
                }
            }
            Found:{}
        }
        
        public void LoadGraphic(){
            texture = new Texture2D( spriteBatch.GraphicsDevice , (int)width, (int)height);
            Color[] colors = new Color[width * height];
            for (int i = 0; i < colors.Length; ++i)
                colors[i] = Color.Azure;
            texture.SetData(colors);
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
        public void Draw() {
            Draw( Position.X, Position.Y );
        }
        public override void Draw(uint x, uint y) {
            Draw( (int)x, (int)y );
        }
        
        public void goTo( int x, int y ){
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
                    if (maze.Block[(uint)(Position.X + dirH), (uint)Position.X] is Empty)
                    {
                        UpdatePosition(Position.X + dirH, Position.Y);
                    }
            }
            else {
                //Debug.WriteLine("Horizontal");
                if (maze.Block[(uint)(Position.X + dirH), (uint)Position.X] is Empty)
                {
                    UpdatePosition(Position.X + dirH, Position.Y);
                }
                else
                    if (maze.Block[(uint)Position.X, (uint)(Position.Y + dirV)] is Empty)
                    {
                        UpdatePosition(Position.X, Position.Y + dirV);
                    }
            }
        }

        public void Update() {
            TouchCollection tc = TouchPanel.GetState();
            
            if (tc.Count() > 0) {

                TouchLocation touched = tc[0];
                //Debug.WriteLine("---------------");
                //Debug.WriteLine(touched.Position.ToString());
                goTo( (int)(touched.Position.X/MazeBlock.width),(int)(touched.Position.Y/MazeBlock.height));
               // tc.Clear();
                DrawAlone( Position.X, Position.Y );
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
