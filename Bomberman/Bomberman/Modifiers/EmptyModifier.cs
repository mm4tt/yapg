using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    class EmptyModifier :Modifier
    {   

        protected Player player;
        
        public virtual void apply(Player player)
        {
            this.player = player;
        }

        public virtual void onUpdate()
        {
            //throw new NotImplementedException();
        }

        public virtual void onBegin()
        {
            //throw new NotImplementedException();
        }

        public virtual void onEnd()
        {
            //throw new NotImplementedException();
        }

        public virtual int getRespirationTime()
        {
            return 0;
        }
    }
}
