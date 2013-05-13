using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Modifiers
{
    class BombRangeModifier : Modifier
    {
         #region Singleton
        private static BombRangeModifier instance = new BombRangeModifier();
        private BombRangeModifier() { }
        public static BombRangeModifier Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion
        public override MazeBlock getBlock()
        {
            return BombRangeChest.Instance;
        }

        public override void onBegin(Player player)
        {
            player.ExplosionRange += 1;
        }
    }
}
