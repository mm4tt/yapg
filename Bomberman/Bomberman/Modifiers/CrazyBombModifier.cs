using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Modifiers
{
    class CrazyBombModifier : Modifier
    {
        #region Singleton
        private static CrazyBombModifier instance = new CrazyBombModifier();
        private CrazyBombModifier() { }
        public static CrazyBombModifier Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion
        public override MazeBlock getBlock()
        {
            return CrazyBombChest.Instance;
        }
        override public  void onUpdate(Player player)
        {
            player.setBomb();
        }
        override public int getRespirationTime()
        {
            return 30000;
        }
    }
}
