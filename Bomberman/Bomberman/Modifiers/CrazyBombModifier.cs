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
