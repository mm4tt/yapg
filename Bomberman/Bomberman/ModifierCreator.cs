using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    class ModifierCreator
    {
        private static string None = "None";
        public static Modifier getModifier(string gtype)
        {
            if (gtype.Equals(None))
                return null;
            Type mtype = Type.GetType(gtype);
            return (Modifier)mtype.GetMethod("Instance").Invoke(null, null);
        }

        public static string getTypeString(Modifier modifer){
            return modifer == null ? None : modifer.getTypeString();
        }
    }
}
