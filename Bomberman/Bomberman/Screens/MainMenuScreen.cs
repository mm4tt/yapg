using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.StateManager;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Bomberman.Screens
{
    class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen()
            : base("Main Menu")
        {
            // Create a button to start the game
            Button playButton = new Button("Play");
            playButton.Tapped += playButton_Tapped;
            MenuButtons.Add(playButton);

            // Create two buttons to toggle sound effects and music. This sample just shows one way
            // of making and using these buttons; it doesn't actually have sound effects or music
            BooleanButton sfxButton = new BooleanButton("Sound Effects", true);
            sfxButton.Tapped += sfxButton_Tapped;
            MenuButtons.Add(sfxButton);

            BooleanButton musicButton = new BooleanButton("Music", true);
            musicButton.Tapped += musicButton_Tapped;
          //  MenuButtons.Add(musicButton);

            Button loadButton = new Button("Load");
            loadButton.Tapped += loadButton_Tapped;
            MenuButtons.Add(loadButton);
        }

        private void loadButton_Tapped(object sender, EventArgs e)
        {
            Engine engine = new GameSaver().LoadGame();
            Debug.WriteLine("Loaded the game");
            Debug.WriteLine("Engine is null " + (engine == null));
            if (engine == null)
            {
                LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
            }
            else
                LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen(engine));
        }

        void playButton_Tapped(object sender, EventArgs e)
        {
            // When the "Play" button is tapped, we load the GameplayScreen
            LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
        }

        void sfxButton_Tapped(object sender, EventArgs e)
        {
            BooleanButton button = sender as BooleanButton;

            // In a real game, you'd want to store away the value of 
            // the button to turn off sounds here. :)
        }

        void musicButton_Tapped(object sender, EventArgs e)
        {
            BooleanButton button = sender as BooleanButton;

            // In a real game, you'd want to store away the value of 
            // the button to turn off music here. :)
        }

        protected override void OnCancel()
        {
            ScreenManager.Game.Exit();
            base.OnCancel();
        }
    }
}
