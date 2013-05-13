using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.GameSaving;
using Bomberman.StateManager;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;

namespace Bomberman.Screens
{
    class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen()
            : base("Main Menu")
        {
            // Create a button to start the game
            Button playButton = new Button("New Game");
            playButton.Tapped += playButton_Tapped;
            MenuButtons.Add(playButton);

            Button loadGame = new Button("Load Game");
            loadGame.Tapped += loadGame_Tapped;
            MenuButtons.Add(loadGame);

            // Create two buttons to toggle sound effects and music. This sample just shows one way
            // of making and using these buttons; it doesn't actually have sound effects or music
            BooleanButton sfxButton = new BooleanButton("Sound Effects", true);
            sfxButton.Tapped += sfxButton_Tapped;
            MenuButtons.Add(sfxButton);

            /*BooleanButton musicButton = new BooleanButton("Music", true);
            musicButton.Tapped += musicButton_Tapped;
            MenuButtons.Add(musicButton);*/



          
        }

        private void loadGame_Tapped(object sender, EventArgs e)
        {
            IGameSaver gameSaver = new DataContractSaver();
            Engine engine = gameSaver.LoadGame();
            if (engine != null)
                Engine.Instance = engine;
            LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
        }

        void playButton_Tapped(object sender, EventArgs e)
        {
            // When the "Play" button is tapped, we load the GameplayScreen
            Engine.Instance = null;
            LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
        }

        void sfxButton_Tapped(object sender, EventArgs e)
        {
            BooleanButton button = sender as BooleanButton;

            bool equals = !button.Text.Equals("Sound Effects: Off");

            Sound.Instance.Sfx = equals;
            if (equals)
            {
                MediaPlayer.Resume();
            }
            else
            {
                MediaPlayer.Pause();
            }
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
