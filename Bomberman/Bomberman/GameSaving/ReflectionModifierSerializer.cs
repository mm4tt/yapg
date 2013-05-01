using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.GameSaving
{
    public class ReflectionModifierSerializer : IModiferSerializer
    {
        private static string None = "None";
        public string Serialize(Modifier modifier)
        {
            return modifier == null ? None : modifier.ToString();
        }

        public Modifier Deserialize(string gtype)
        {
            if (gtype.Equals(None))
                return null;
            Type mType = Type.GetType(gtype);

            var getInstanceMethod = mType.GetMethod("get_Instance");
            if (getInstanceMethod != null)
            {
                return (Modifier)getInstanceMethod.Invoke(null, null);
            }
            else
            {
                var contructor = mType.GetConstructor(System.Type.EmptyTypes);
                return contructor != null ? (Modifier)contructor.Invoke(null) : null;
            }
        }
    }
}
