using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    public class Modifier
    {
        // protected Player player;
        public virtual MazeBlock getBlock()
        {
            return Chest.Instance;
        }
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