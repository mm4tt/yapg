using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    class Obstacle : MazeBlock
    {
        #region Singleton
        private static Obstacle instance = new Obstacle();
        private Obstacle() { }
        public static Obstacle Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        protected override void LoadGraphic(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //SetOneColorTexture(Color.Blue, graphicsDevice);
            //texture = contentManager.Load<Texture2D>("MazeBlock\\Obstacle-icone");
            texture = contentManager.Load<Texture2D>("MazeBlock\\Wall");
            
        }
    }
}
