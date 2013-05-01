using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.GameSaving
{
    public interface IModiferSerializer
    {

        string Serialize(Modifier modifier);
        Modifier Deserialize(string tag);
    }
}
