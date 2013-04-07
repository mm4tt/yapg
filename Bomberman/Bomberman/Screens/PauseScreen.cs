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

            Button saveButton = new Button("Save");
            saveButton.Tapped += saveButton_Tapped;
            MenuButtons.Add(saveButton);

            Button exitButton = new Button("Exit");
            exitButton.Tapped += exitButton_Tapped;
            MenuButtons.Add(exitButton);
        }

        void saveButton_Tapped(object sender, EventArgs e)
        {
            GameSaver gameSaver = new GameSaver();
            GameScreen[] screens = ScreenManager.GetScreens();
            foreach (GameScreen screen in ScreenManager.GetScreens())
            {
                if (screen is GameplayScreen)
                {
                    GameplayScreen gameplayScreen = screen as GameplayScreen;
                    gameSaver.SaveGame(gameplayScreen.Engine);
                }
            }
         
            OnCancel();
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
        /// 
        /// 
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