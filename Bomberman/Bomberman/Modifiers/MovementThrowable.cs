using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Modifiers
{
    class MovementThrowable : EmptyModifier
    {
        public void onBegin() {
            player.MovementMode = Player.MODE_MOVEMENT_THROW;
        }
        public void onEnd() {
            player.MovementMode = Player.MODE_MOVEMENT_DEFAULT;
        }
    }
}
