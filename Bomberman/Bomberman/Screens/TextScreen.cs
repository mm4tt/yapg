using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Bomberman.StateManager;

namespace Bomberman.Screens
{
    class TextScreen : MenuScreen
    {
        private List<string> text;

        public TextScreen(string title, string[] text) : base(title)
        {
            this.text = new List<string>(text);
        }

        public override void Activate(bool instancePreserved)
        {
            // When the screen is activated, we have a valid ScreenManager so we can arrange
            // our buttons on the screen
            base.Activate(instancePreserved);
            float y = (float)TitlePositionY + 75 + (text.Count-1) * 50 + 70f;
            float center = ScreenManager.GraphicsDevice.Viewport.Bounds.Center.X;
            for (int i = 0; i < MenuButtons.Count; i++)
            {
                Button b = MenuButtons[i];

                b.Position = new Vector2(center - b.Size.X / 2, y);
                y += b.Size.Y * 1.5f;
            }

        }

        public override void  Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
 	         base.Draw(gameTime);
             GraphicsDevice graphics = ScreenManager.GraphicsDevice;
             SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
             SpriteFont font = ScreenManager.LblFont;

             spriteBatch.Begin();


             // Make the menu slide into place during transitions, using a
             // power curve to make things look more interesting (this makes
             // the movement slow down as it nears the end).
             float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

             // Draw the menu title centered on the screen
             for (int i = 0; i < text.Count; i++)
             {
                 Vector2 titlePosition = new Vector2(graphics.Viewport.Width / 2, TitlePositionY + 75 + i*50);
                 Vector2 titleOrigin = font.MeasureString(text[i]) / 2;
                 Color titleColor = new Color(192, 192, 192) * TransitionAlpha;
                 float titleScale = 1.0f;

                 titlePosition.Y -= transitionOffset * 100;

                 spriteBatch.DrawString(font, text[i], titlePosition, titleColor, 0,
                                        titleOrigin, titleScale, SpriteEffects.None, 0);
             }
             spriteBatch.End();
        }
    }
}
