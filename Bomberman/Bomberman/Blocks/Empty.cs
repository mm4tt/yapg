using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    class Empty : MazeBlock
    {
        #region Singleton
        private static Empty instance = new Empty();
        private Empty() { }
        public static Empty Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion


        protected override void LoadGraphic(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //SetOneColorTexture(Microsoft.Xna.Framework.Color.White,graphicsDevice);
            texture = contentManager.Load<Texture2D>("MazeBlock\\Empty");
        }
    }
}
