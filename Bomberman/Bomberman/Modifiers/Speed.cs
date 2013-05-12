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

        public override void onBegin(Player player)
        {
            player.Speed = 2 * player.Speed;
        }
    }
    class SpeedModifier : Modifier
    {
        #region Singleton
        private static SpeedModifier instance = new SpeedModifier();
        private SpeedModifier() { }
        public static SpeedModifier Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override void onBegin(Player player)
        {
            player.Speed = player.Speed + (float)0.5;
        }
    }
}
