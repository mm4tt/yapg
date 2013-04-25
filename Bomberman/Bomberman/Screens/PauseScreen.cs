using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.StateManager;

namespace Bomberman.Screens
{
    /// <summary>
    /// A basic pause screen for Windows Phone
    /// </summary>
    class PauseScreen : MenuScreen
    {
        public PauseScreen()
            : base("Paused")
        {
            // Create the "Resume" and "Exit" buttons for the screen

            Button resumeButton = new Button("Resume");
            resumeButton.Tapped += resumeButton_Tapped;
            MenuButtons.Add(resumeButton);

            Button exitButton = new Button("Exit");
            exitButton.Tapped += exitButton_Tapped;
            MenuButtons.Add(exitButton);

            Button saveButton = new Button("Save");
            saveButton.Tapped += saveButton_Tapped;
        }

        void saveButton_Tapped(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// The "Resume" button handler just calls the OnCancel method so that 
        /// pressing the "Resume" button is the same as pressing the hardware back button.
        /// </summary>
        void resumeButton_Tapped(object sender, EventArgs e)
        {
            OnCancel();
        }

        /// <summary>
        /// The "Exit" button handler uses the LoadingScreen to take the user out to the main menu.
        /// </summary>
        void exitButton_Tapped(object sender, EventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }

        protected override void OnCancel()
        {
            ExitScreen();
            base.OnCancel();
        }
    }
}
