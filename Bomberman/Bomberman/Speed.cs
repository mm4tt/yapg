using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    class DoubleSpeed : Modifier
    {
        #region Singleton
        private static DoubleSpeed instance = new DoubleSpeed();
        private DoubleSpeed() { }
        public static DoubleSpeed Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        Player player;
        public void apply(Player player)
        {
            player.addModifier(this);
            this.player = player;
        }
        public void onUpdate() { }
        public void onBegin() {
            player.Speed = 2 * player.Speed;
        }
        public void onEnd() { }
        public int getRespirationTime() {
            return 0;
        }
    }
}
