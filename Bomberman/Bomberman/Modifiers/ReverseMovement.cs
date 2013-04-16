using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Modifiers
{
    class ReverseMovement : EmptyModifier
    {
        public void onUpdate() {
            if (player.Touched) {
                if (player.Direction == 1) {
                    player.Direction = 3;
                }
                if (player.Direction == 3)
                {
                    player.Direction = 1;
                }
                if (player.Direction == 2)
                {
                    player.Direction = 4;
                }
                if (player.Direction == 4)
                {
                    player.Direction = 2;
                }
            }
        }
    }
}
