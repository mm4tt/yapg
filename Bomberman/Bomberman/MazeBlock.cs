using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;
namespace Bomberman
{
    
    public abstract class MazeBlock : Drawable
    {  
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
            texture = new Texture2D(graphicsDevice,Maze.BlockWidth, Maze.BlockHeight);
            Color[] colors = new Color[Maze.BlockWidth * Maze.BlockHeight];
            for (int i = 0; i < colors.Length; ++i)
                colors[i] = chosenColor;
            texture.SetData(colors);
        }

        protected Rectangle ComputePosition(uint x, uint y)
        {
            return new Rectangle((int)x * Maze.BlockWidth, (int)y * Maze.BlockHeight, Maze.BlockWidth, Maze.BlockHeight);
        }
    }
}
