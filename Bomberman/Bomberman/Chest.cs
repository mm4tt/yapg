using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    class Chest : MazeBlock
    {
        #region Singleton
        private static Chest instance = new Chest();
        private Chest() { }
        public static Chest Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        protected override void LoadGraphic(GraphicsDevice graphicDevice)
        {
            SetOneColorTexture(Color.Green, graphicDevice);
        }
    }
}
