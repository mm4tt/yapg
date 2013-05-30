using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Bomberman.Menu
{
    class BMBoolButton : BombermanButton
    {
        bool value;

        public bool Value
        {
            get { return value; }
        }

        public BMBoolButton(string text, bool value)
            : base(text)
        {
            Text += ":  ";
            this.value = value;
        }

        protected override void OnTapped()
        {
            // When tapped we need to toggle the value and regenerate the text
            value = !value;

            base.OnTapped();
        }

        public override void Draw(StateManager.GameScreen screen)
        {
            base.Draw(screen);

            Texture2D img;
            if(value)
                img = screen.ScreenManager.YesTex;
            else
                img = screen.ScreenManager.NoTex;
            
            SpriteBatch spriteBatch = screen.ScreenManager.SpriteBatch;
            spriteBatch.Draw(img, iconPlace, Color.White);
        }
    }
}
