using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    public abstract class MazeBlock : Drawable
    {
        protected Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;

        public static int height = 20;
        public static int width = 20;
        public Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set
            {
                spriteBatch = value;
                LoadGraphic();
            }
                  
        }
        
        protected  Texture2D texture;

        public virtual void Draw(uint x, uint y)
        {
            spriteBatch.Draw(texture, ComputePosition(x, y), Color.White);
        }
        protected abstract void LoadGraphic();
        protected void SetOneColorTexture(Color chosenColor)
        {
            texture = new Texture2D(spriteBatch.GraphicsDevice, width, height);
            Color[] colors = new Color[width * height];
            for (int i = 0; i < colors.Length; ++i)
                colors[i] = chosenColor;
            texture.SetData(colors);
        }

        protected Rectangle ComputePosition(uint x, uint y)
        {
            return new Rectangle((int)x * width, (int) y * height, width, height);
        }
    }
}
