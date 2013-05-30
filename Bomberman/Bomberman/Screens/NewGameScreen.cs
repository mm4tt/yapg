using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.StateManager;
using Bomberman.Menu;
using Microsoft.Xna.Framework;

namespace Bomberman.Screens
{
    class NewGameScreen : MenuScreen
    {
        public NewGameScreen()
            : base("Difficulty")
        {
            // Create a button to start the game
            Button playButton = new BombermanButton("Casual");
            playButton.Tapped += casual_Tapped;
            MenuButtons.Add(playButton);

            Button loadGame = new BombermanButton("Easy");
            loadGame.Tapped += easy_Tapped;
            MenuButtons.Add(loadGame);

            // Create two buttons to toggle sound effects and music. This sample just shows one way
            // of making and using these buttons; it doesn't actually have sound effects or music
            Button sfxButton = new BombermanButton("Normal");
            sfxButton.Tapped += normal_Tapped;
            MenuButtons.Add(sfxButton);

            /*BooleanButton musicButton = new BooleanButton("Music", true);
            musicButton.Tapped += musicButton_Tapped;
            MenuButtons.Add(musicButton);*/


            TransitionOnTime = TimeSpan.FromSeconds(1.0);
          
        }

        private void casual_Tapped(object sender, EventArgs e)
        {
            Engine.Instance = new Engine();
            Engine.Instance.BrandNew = true;
            Engine.Instance.Difficulty = Engine.So.Casual;
            LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
        }

        void easy_Tapped(object sender, EventArgs e)
        {
            Engine.Instance = new Engine();
            Engine.Instance.BrandNew = true;
            Engine.Instance.Difficulty = Engine.So.Easy;
            LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
        }

        void normal_Tapped(object sender, EventArgs e)
        {
            Engine.Instance = new Engine();
            Engine.Instance.BrandNew = true;
            Engine.Instance.Difficulty = Engine.So.Normal;
            LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
        }


        protected override void OnCancel()
        {
            ScreenManager.AddScreen(new MainMenuScreen(), ControllingPlayer);
            base.OnCancel();
        }
    }
}
