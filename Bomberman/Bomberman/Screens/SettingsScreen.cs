using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.StateManager;
using Bomberman.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace Bomberman.Screens
{
    class SettingsScreen : MenuScreen
    {
        public SettingsScreen()
            : base("Settings")
        {
            // Create a button to start the game
            Button musicEnable = new BMBoolButton("Music", Sound.Instance.Music);
            musicEnable.Tapped += music_Tapped;
            MenuButtons.Add(musicEnable);

            Button loadGame = new BombermanButton("Music Volume: "+(int)(MediaPlayer.Volume*10));
            loadGame.Tapped += musicVol_Tapped;
            MenuButtons.Add(loadGame);

            // Create two buttons to toggle sound effects and music. This sample just shows one way
            // of making and using these buttons; it doesn't actually have sound effects or music
            Button sfxButton = new BMBoolButton("Sounds", Sound.Instance.Sfx);
            sfxButton.Tapped += sfx_Tapped;
            MenuButtons.Add(sfxButton);

            /*BooleanButton musicButton = new BooleanButton("Music", true);
            musicButton.Tapped += musicButton_Tapped;
            MenuButtons.Add(musicButton);*/


            TransitionOnTime = TimeSpan.FromSeconds(1.0);
          
        }

        private void music_Tapped(object sender, EventArgs e)
        {
            BMBoolButton button = sender as BMBoolButton;
            if (MediaPlayer.Queue.ActiveSong.Name == Sound.Instance.s.Name)
            {
                bool equals = button.Value;
                Sound.Instance.Music = equals;
            }
            else
            {
                Debug.WriteLine(MediaPlayer.Queue.ActiveSong);
                if (button.Value)
                    ScreenManager.AddScreen(new WarningScreen(), ControllingPlayer);
            }
            
        }

        void musicVol_Tapped(object sender, EventArgs e)
        {
            MediaPlayer.Volume = (MediaPlayer.Volume + 0.1f) % 1.0f;

            BombermanButton button = sender as BombermanButton;
            button.Text = "Music Volume: " + (int)(MediaPlayer.Volume * 10);
        }

        void sfx_Tapped(object sender, EventArgs e)
        {
            BMBoolButton button = sender as BMBoolButton;

            bool equals = button.Value;

            Sound.Instance.Sfx = equals;
            
        }


        protected override void OnCancel()
        {
            ScreenManager.AddScreen(new MainMenuScreen(), ControllingPlayer);
            base.OnCancel();
        }
    }
}
