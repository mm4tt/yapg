using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public override void Draw()
        {
            Console.Write(".");
        }
    }
}
