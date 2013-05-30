using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.StateManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.Menu
{
    class BombermanButton : Button
    {
        public static Point FLOAT_SPACE = new Point(10, 20);
        public static Point FLOAT_SPEED = new Point(2, 1);
        protected static double ransom = 0.0;
        protected double seed;
        protected double time;
        protected Vector2 iconPlace = new Vector2(0, 0);

        public BombermanButton(string text) : base(text)
        {
            seed = ransom;
            ransom += 0.5;
            time = 0.0;
        }

        public void Update(GameTime gt)
        {
            time += Math.PI*gt.ElapsedGameTime.Milliseconds/(double)1000;
        }

        public override void Draw(GameScreen screen)
        {
            SpriteBatch spriteBatch = screen.ScreenManager.SpriteBatch;
            SpriteFont font = screen.ScreenManager.BtnFont;
            Vector2 textSize = font.MeasureString(Text);
            Rectangle r = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)Size.X,
                (int)Size.Y);
            Vector2 textPosition = new Vector2(r.Center.X, r.Center.Y) - textSize / 2f;
            //textPosition.X = (int)textPosition.X + (int)(FLOAT_SPACE.X * Math.Cos(seed + time * FLOAT_SPEED.X));
            //textPosition.Y = (int)textPosition.Y + (int)(FLOAT_SPACE.Y * Math.Sin(seed + time * FLOAT_SPEED.Y));

            for (int i = 0; i < Text.Length; i++)
            {
                Vector2 letterPosition = new Vector2(r.Center.X, r.Center.Y);

                letterPosition.X = (int)textPosition.X + 
                    (int)(font.MeasureString(Text.Substring(0, i)).X) + 
                    (int)(FLOAT_SPACE.X * Math.Cos(seed + time * FLOAT_SPEED.X + i * 0.1));
                letterPosition.Y = (int)textPosition.Y + (int)(FLOAT_SPACE.Y * Math.Sin(seed + time * FLOAT_SPEED.Y + i * 0.1));

                spriteBatch.DrawString(font, Text.Substring(i, 1), letterPosition, TextColor * Alpha);

            }
            if (Text.Length > 2)
            {
                int i = Text.Length - 2;
                iconPlace.X = textPosition.X +
                        (font.MeasureString(Text.Substring(0, i)).X) +
                        (int)(FLOAT_SPACE.X * Math.Cos(seed + time * FLOAT_SPEED.X + i * 0.1));
                iconPlace.Y = textPosition.Y + (int)(FLOAT_SPACE.Y * Math.Sin(seed + time * FLOAT_SPEED.Y + i * 0.1));
            }
        }
    }
}
