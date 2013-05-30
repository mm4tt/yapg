using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.GameSaving;
using Bomberman.StateManager;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;
using Bomberman.Menu;

namespace Bomberman.Screens
{
    class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen()
            : base("Main Menu")
        {
            // Create a button to start the game
            Button playButton = new BombermanButton("New Game");
            playButton.Tapped += playButton_Tapped;
            MenuButtons.Add(playButton);

            Button loadGame = new BombermanButton("Load Game");
            loadGame.Tapped += loadGame_Tapped;
            MenuButtons.Add(loadGame);

            // Create two buttons to toggle sound effects and music. This sample just shows one way
            // of making and using these buttons; it doesn't actually have sound effects or music
            Button sfxButton = new BMBoolButton("Sound Effects", true);
            sfxButton.Tapped += sfxButton_Tapped;
            MenuButtons.Add(sfxButton);

            /*BooleanButton musicButton = new BooleanButton("Music", true);
            musicButton.Tapped += musicButton_Tapped;
            MenuButtons.Add(musicButton);*/


            TransitionOnTime = TimeSpan.FromSeconds(1.0);
          
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
            ScreenManager.AddScreen(new NewGameScreen(), ControllingPlayer);
        }

        void sfxButton_Tapped(object sender, EventArgs e)
        {
            BMBoolButton button = sender as BMBoolButton;

            bool equals = button.Value;

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
