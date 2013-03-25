using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    public abstract class GameObject : Drawable
    {
        public abstract void Draw( uint x, uint y );
    }
}
