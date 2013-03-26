using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Bomberman
{
    public abstract class GameObject
    {
        protected static SpriteBatch spriteBatch;

        public static void setSpriteBatch(SpriteBatch sb)
        {
            spriteBatch = sb;
        }

        int x, y;

        abstract public void Update(GameTime gt);

        abstract public void Draw();

    }
}
