using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Bomberman.Modifiers
{
    class MovementThrowable : EmptyModifier
    {
         #region Singleton
        private static MovementThrowable instance = new MovementThrowable();
        private MovementThrowable() { }
        float speed;
        public static MovementThrowable Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion
        public override void onBegin()
        {
            speed = player.Speed;
            player.MovementMode = Player.MODE_MOVEMENT_THROW;
        }
        public override void onEnd()
        {
            Debug.Assert(player != null, "Player not null");
            player.MovementMode = Player.MODE_MOVEMENT_DEFAULT;
            player.Speed = speed;
        }
        public override int getRespirationTime()
        {
            return 10000;
        }
    }
}
