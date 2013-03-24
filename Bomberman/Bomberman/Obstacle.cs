using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
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

      

        public override void Draw(uint x,uint y)
        {
            base.Draw(x, y);
            Console.Write(".");
            
        }

        protected override void LoadGraphic()
        {
            SetOneColorTexture(Color.Blue);
        }
    }
}
