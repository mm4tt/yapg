using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    class Speed : Modifier
    {
        #region Singleton
        private static Speed instance = new Speed();
        private Speed() { }
        public static Speed Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion


        public void apply(Player P)
        {
            throw new NotImplementedException();
        }

        public string getTypeString()
        {
            return this.ToString();
        }
    }
}
