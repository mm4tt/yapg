using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    class ExtraBomb : Modifier
    {
        #region Singleton
        private static ExtraBomb instance = new ExtraBomb();
        private ExtraBomb() { }
        public static ExtraBomb Instance
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
        public void onUpdate() { }
        public void onBegin() { }
        public void onEnd() { }
        public int getRespirationTime() {
            return 0;
        }
    }
}
