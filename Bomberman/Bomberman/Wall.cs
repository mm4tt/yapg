using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public override void Draw()
        {
            Console.Write("@");
        }
    }


}
