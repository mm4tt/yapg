using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Modifiers
{
     #region Singleton
       
    class ReverseMovement : EmptyModifier
    {
        private static ReverseMovement instance = new ReverseMovement();
        private ReverseMovement() { }
        public static ReverseMovement Instance
        {
            get
            {
                return instance;
            }
        }
     #endregion
        public override void onUpdate()
        {
            if (player.Touched) {
                if (player.Direction == 1) {
                    player.Direction = 3;
                }else
                if (player.Direction == 3)
                {
                    player.Direction = 1;
                }else
                if (player.Direction == 2)
                {
                    player.Direction = 4;
                }else
                if (player.Direction == 4)
                {
                    player.Direction = 2;
                }
            }
        }
        public override int getRespirationTime()
        {
            return 10000;
        }
    }
}
