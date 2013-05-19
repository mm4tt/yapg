using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.StateManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.HighScores
{
    class TextBlock
    {
        public string Text = "TextBlock";

        public Vector2 Position = Vector2.Zero;

        public Vector2 Size = new Vector2(250, 75);

        public int BorderThickness = 1;

        public Color BorderColor = new Color(200, 200, 200);

        public Color FillColor = new Color(100, 100, 100) * .75f;

        public Color TextColor = Color.White;

        public float Alpha = 0f;

        public TextBlock(string text)
        {
            Text = text;
        }

        public void Draw(GameScreen screen)
        {
      
            // Grab some common items from the ScreenManager
            SpriteBatch spriteBatch = screen.ScreenManager.SpriteBatch;
            SpriteFont font = screen.ScreenManager.Font;
            Texture2D blank = screen.ScreenManager.BlankTexture;

            // Compute the button's rectangle
            Rectangle r = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)Size.X,
                (int)Size.Y);

            // Fill the button
            spriteBatch.Draw(blank, r, FillColor * Alpha);

            // Draw the border
            spriteBatch.Draw(
                blank,
                new Rectangle(r.Left, r.Top, r.Width, BorderThickness),
                BorderColor * Alpha);
            spriteBatch.Draw(
                blank,
                new Rectangle(r.Left, r.Top, BorderThickness, r.Height),
                BorderColor * Alpha);
            spriteBatch.Draw(
                blank,
                new Rectangle(r.Right - BorderThickness, r.Top, BorderThickness, r.Height),
                BorderColor * Alpha);
            spriteBatch.Draw(
                blank,
                new Rectangle(r.Left, r.Bottom - BorderThickness, r.Width, BorderThickness),
                BorderColor * Alpha);

            // Draw the text centered in the button
            Vector2 textSize = font.MeasureString(Text);
            Vector2 textPosition = new Vector2(r.Center.X, r.Center.Y) - textSize / 2f;
            textPosition.X = (int)textPosition.X;
            textPosition.Y = (int)textPosition.Y;
            spriteBatch.DrawString(font, Text, textPosition, TextColor * Alpha);
        }

    }
}
