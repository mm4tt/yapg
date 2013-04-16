using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    class DoubleSpeed : EmptyModifier
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

        public void onBegin() {
            player.Speed = 2 * player.Speed;
        }
    }
}
