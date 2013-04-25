using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    interface Drawable
    {
        
        void Draw(uint x, uint y, SpriteBatch spriteBatch, ContentManager contentManager);
    }
}
