using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.StateManager;
using Bomberman.Menu;
using Microsoft.Xna.Framework;

namespace Bomberman.Screens
{
    class WarningScreen : TextScreen
    {
        public WarningScreen()
            : base("Are You sure?", new string[]{"This will stop", "Your current music!"})
        {
            // Create a button to start the game
            Button playButton = new BombermanButton("OK");
            playButton.Tapped += ok_Tapped;
            MenuButtons.Add(playButton);

            TransitionOnTime = TimeSpan.FromSeconds(1.0);
          
        }

        private void ok_Tapped(object sender, EventArgs e)
        {
            Sound.Instance.PlayMusic();
            ScreenManager.AddScreen(new SettingsScreen(), ControllingPlayer);
        }


        protected override void OnCancel()
        {
            ScreenManager.AddScreen(new SettingsScreen(), ControllingPlayer);
            base.OnCancel();
        }
    }
}
