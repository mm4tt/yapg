using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

namespace Bomberman
{
    public abstract class Modifier
    {
        // protected Player player;
        public abstract MazeBlock getBlock();

        public virtual void apply(Player player)
        {
            //   this.player = player;
        }

        public virtual void onUpdate(Player player)
        {
            //throw new NotImplementedException();
        }

        public virtual void onBegin(Player player)
        {
            //throw new NotImplementedException();
        }

        public virtual void onEnd(Player player)
        {
            //throw new NotImplementedException();
        }

        public virtual int getRespirationTime()
        {
            return 0;
        }
    }
}