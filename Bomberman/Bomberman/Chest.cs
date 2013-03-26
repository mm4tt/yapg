using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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

      

        public override void Draw(uint x,uint y)
        {
            base.Draw(x, y);
            Console.Write("S");
            
        }

        protected override void LoadGraphic()
        {
            SetOneColorTexture(Color.Green);
        }
    }
}
