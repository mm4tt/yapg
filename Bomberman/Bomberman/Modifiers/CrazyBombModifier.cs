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
<<<<<<< HEAD

        public override MazeBlock getBlock()
        {
            return CrazyBombChest.Instance;
        }

=======
>>>>>>> parent of 798b66f... Kolory modyfikatorow
        override public  void onUpdate(Player player)
        {
            player.setBomb();
        }
        override public int getRespirationTime()
        {
            return 1000000;
        }
    }
}
