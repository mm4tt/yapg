using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.HighScores;
using Bomberman.StateManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;


namespace Bomberman.Screens
{
    class HighScoreSceen : GameScreen
    {

        protected List<HighScore> scores;
        protected string title = "HighScores";
        protected HighScorePresenter presenter;
        protected List<TextBlock> textBlocks = new List<TextBlock>();
        protected int index;
        public HighScoreSceen(List<HighScore> scores, int index)
        {
            this.scores = scores;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            //presenter = new OnlyScoreHighScorePresenter(scores);
            this.index = index; 
            presenter = new ScoreAndDateHighScorePresenter(scores);
            foreach(String s in presenter.presentHighScores())
                textBlocks.Add(new TextBlock(s));
            EnabledGestures = GestureType.Tap;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            spriteBatch.Begin();

            // Draw all of the buttons
            foreach (TextBlock b in textBlocks)
                b.Draw(this);

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // Draw the menu title centered on the screen
            Vector2 titlePosition = new Vector2(graphics.Viewport.Width / 2, 80);
            Vector2 titleOrigin = font.MeasureString(title) / 2;
            Color titleColor = new Color(192, 192, 192) * TransitionAlpha;
            float titleScale = 1.25f;

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, title, titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        
        public override void HandleInput(GameTime gameTime, InputState input)
        {

            Boolean finish = false;
            foreach(GestureSample gt in input.Gestures){
                if (gt.GestureType == GestureType.Tap)
                    finish = true;
            }
            if(finish)
                LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
           
            
        }

        public override void Activate(bool instancePreserved)
        {
            float y = 140f;
            float center = ScreenManager.GraphicsDevice.Viewport.Bounds.Center.X;

            for(int i = 0 ; i < textBlocks.Count ;++i){
                TextBlock tb = textBlocks[i];
                tb.Position = new Vector2(center - tb.Size.X/2,y);
                y+= tb.Size.Y * 1.5f;
                if (i == index)
                    tb.TextColor = Color.Yellow;
            }
            base.Activate(instancePreserved);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            // Update opacity of the buttons
            foreach (TextBlock b in textBlocks)
            {
                b.Alpha = TransitionAlpha;
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


    }
}
