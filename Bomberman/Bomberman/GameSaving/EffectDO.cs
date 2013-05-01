using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Bomberman.GameSaving
{
    [DataContract()]
    public class EffectDO
    {
       

        public EffectDO(string p, int time1)
        {
            modifier = p;
            time = time1;
        }

        [DataMember()]
        public int time
        {
            get;
            set;
        }
        [DataMember()]
        public string modifier
        {
            get;
            set;
        }

        public Effect createEffect(IModiferSerializer modSerializer)
        {
            return new Effect(modSerializer.Deserialize(modifier), time);
        }
        
    }
}
