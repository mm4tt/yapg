using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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


        public override void Draw(uint x,uint y)
        {
            base.Draw(x, y);
            Console.Write(" ");
        }

        protected override void LoadGraphic()
        {
            SetOneColorTexture(Microsoft.Xna.Framework.Color.White);
        }
    }
}
