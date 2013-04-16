using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    class EmptyModifier :Modifier
    {   

        protected Player player;
        
        public void apply(Player player)
        {
            this.player = player;
        }

        public void onUpdate()
        {
            //throw new NotImplementedException();
        }

        public void onBegin()
        {
            //throw new NotImplementedException();
        }

        public void onEnd()
        {
            //throw new NotImplementedException();
        }

        public int getRespirationTime()
        {
            return 0;
        }
    }
}
