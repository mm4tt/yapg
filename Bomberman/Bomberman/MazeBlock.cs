using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;
namespace Bomberman
{
    
    public abstract class MazeBlock : Drawable
    {
      
        public static int height = 20;
        public static int width = 20;
        
        protected  Texture2D texture;

        public virtual void Draw(uint x, uint y, SpriteBatch spriteBatch, ContentManager contentManager)
        {
            if (texture == null || texture.GraphicsDevice != spriteBatch.GraphicsDevice)
                LoadGraphic(spriteBatch.GraphicsDevice,contentManager);
            spriteBatch.Draw(texture, ComputePosition(x, y), Color.White);
        }
        protected abstract void LoadGraphic(GraphicsDevice graphicsDevice, ContentManager contentManager);

        protected void SetOneColorTexture(Color chosenColor, GraphicsDevice graphicsDevice)
        {
            texture = new Texture2D(graphicsDevice,width, height);
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
