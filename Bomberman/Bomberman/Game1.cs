using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using Bomberman.StateManager;
using Bomberman.Screens;

namespace Bomberman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        ScreenManager screenManager;
        ScreenFactory screenFactory;

        public Game1()
        {
            Debug.WriteLine("Game starting...");   
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            graphics.IsFullScreen = true;
            //InitializePortraitGraphics();

            // Create the screen factory and add it to the Services
            screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            // Create the screen manager component.
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            // Hook events on the PhoneApplicationService so we're notified of the application's life cycle
            Microsoft.Phone.Shell.PhoneApplicationService.Current.Launching +=
                new EventHandler<Microsoft.Phone.Shell.LaunchingEventArgs>(GameLaunching);
            Microsoft.Phone.Shell.PhoneApplicationService.Current.Activated +=
                new EventHandler<Microsoft.Phone.Shell.ActivatedEventArgs>(GameActivated);
            Microsoft.Phone.Shell.PhoneApplicationService.Current.Deactivated +=
                new EventHandler<Microsoft.Phone.Shell.DeactivatedEventArgs>(GameDeactivated);

        }

        void GameLaunching(object sender, Microsoft.Phone.Shell.LaunchingEventArgs e)
        {
            AddInitialScreens();
        }

        void GameActivated(object sender, Microsoft.Phone.Shell.ActivatedEventArgs e)
        {
            // Try to deserialize the screen manager
            if (!screenManager.Activate(e.IsApplicationInstancePreserved))
            {
                // If the screen manager fails to deserialize, add the initial screens
                AddInitialScreens();
            }
        }

        void GameDeactivated(object sender, Microsoft.Phone.Shell.DeactivatedEventArgs e)
        {

            // Serialize the screen manager when the game deactivated
            screenManager.Deactivate();

            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            //Bomb.Load(this.Content);
            //Enemy.Load(this.Content);
            //engine.SetSpriteBatch(spriteBatch);
        }

        private void AddInitialScreens()
        {
            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);

            // We have different menus for Windows Phone to take advantage of the touch interface

            screenManager.AddScreen(new MainMenuScreen(), null);
        }


        private void InitializePortraitGraphics()
        {
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
        }
   


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
