using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bomberman
{
    [DataContract()]
    abstract public class Ticker
    {
        protected int remaining = 0;

        [DataMember()]
        public int Remaning
        {
            get { return remaining; }
            set { remaining = value; }
        }

        public Ticker(float length)
        {
            remaining = (int)(length * 1000);
        }

        public bool Update(GameTime gt)
        {
            remaining -= gt.ElapsedGameTime.Milliseconds;

            if (remaining < 0)
            {
                function();
                return true;
            }
            else return false;
        }

        void function() { }
    }
}
