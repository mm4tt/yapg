using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    public interface Modifier
    {
        void apply(Player P);
    }
}