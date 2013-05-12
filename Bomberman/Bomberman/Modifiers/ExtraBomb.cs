using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    class ExtraBomb : Modifier
    {
        #region Singleton
        private static ExtraBomb instance = new ExtraBomb();
        private ExtraBomb() { }
        public static ExtraBomb Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion
        public override MazeBlock getBlock()
        {
            return ExtraBombChest.Instance;
        }

        public override void onBegin(Player player)
        {
            player.BombsAvailable += 1;
        }

        public override int getRespirationTime()
        {
            return 0;
        }
    }
}
