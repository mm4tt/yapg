using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    class Wall : MazeBlock
    {
        #region Singleton
        private static Wall instance = new Wall();
        private Wall() { }
        public static Wall Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion


        protected override void LoadGraphic(GraphicsDevice graphicsDevice)
        {
            SetOneColorTexture(Color.Brown, graphicsDevice);
        }
    }


}
