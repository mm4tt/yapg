using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.StateManager;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Devices.Sensors;
using System.Diagnostics;

namespace Bomberman.Screens
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        Random random = new Random();
        float pauseAlpha;
        InputAction pauseAction;
        

        #endregion




        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                true);

            EnabledGestures = GestureType.DoubleTap | GestureType.Flick | GestureType.Tap | GestureType.Hold;
 
            
        }

        public void LevelFailed()
        {
            //powinno sie dodac jakis HighScoreScreen, ale to w przyszlosci
            
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
            
        }

        /// <summary>
        /// C   Code for the game initialization. 
        ///     It's temporary solution. Will be moved to a level manager
        /// </summary>
        void InitializeGame()
        {
            if (Engine.Instance == null)
            {
                Engine.Instance = new Engine();
                Engine.Instance.GenerateLevel();
            }
        }
        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void Activate(bool instancePreserved)
        {
            
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");


                InitializeGame();

                // A real game would probably have more content than this sample, so
                // it would take longer to load. We simulate that by delaying for a
                // while, giving you a chance to admire the beautiful loading screen.
                // Thread.Sleep(1000);

                // once the load has finished, we use ResetElapsedTime to tell the game's
                // timing mechanism that we have just finished a very long frame, and that
                // it should not try to catch up.
                ScreenManager.Game.ResetElapsedTime();
            }




            if (Microsoft.Phone.Shell.PhoneApplicationService.Current.State.ContainsKey("Engine"))
            {
                Engine.Instance = (Engine)Microsoft.Phone.Shell.PhoneApplicationService.Current.State["Engine"];
                Engine.Instance.fixDependencies();
            }

            if (Engine.Instance.LevelFailedEmpty)
                Engine.Instance.LevelFailed += new Engine.LevelFailedEventHandler(LevelFailed);

        }


        public override void Deactivate()
        {
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State["Engine"] = Engine.Instance;
            base.Deactivate();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void Unload()
        {
            content.Unload();
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State.Remove("Engine");
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
               
               Engine.Instance.Update(gameTime);
              
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (pauseAction.Evaluate(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
#if WINDOWS_PHONE
                ScreenManager.AddScreen(new PauseScreen(), ControllingPlayer);
#else
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
#endif
            }
            else
            {
                bool hold = false;
                foreach (var gesture in input.Gestures)
                {

                    if (gesture.GestureType == GestureType.Flick)
                    {

                        Engine.Instance.Player.move(gesture.Delta);
                    }
                    else if (gesture.GestureType == GestureType.DoubleTap)
                    {

                        Engine.Instance.Player.setBomb();
                    }
                    else if (gesture.GestureType == GestureType.Tap)
                    {

                        Engine.Instance.Player.stop();
                    }
                    else if (gesture.GestureType == GestureType.Hold)
                    {
                        Debug.WriteLine("Byl hold bijacz");
                        //StartAccelerometer();
                        hold = true;
                    }
                    else if (gesture.GestureType == GestureType.None)
                    {
                        Debug.WriteLine("No gesture");
                    }
                }

                if (hold)
                    StartAccelerometer();
                else
                    StopAccelerometer();
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Black, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            //spriteBatch.DrawString(gameFont, "// TODO", playerPosition, Color.Green);

            // spriteBatch.DrawString(gameFont, "Insert Gameplay Here",
            //  enemyPosition, Color.DarkRed);


            Engine.Instance.Draw(spriteBatch,content);
           
            
                spriteBatch.End();
            
         



            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }


        #endregion

        #region AccelometerRegion
        Accelerometer accelerometer;
        static int threshold = 30;

        void StartAccelerometer()
        {
            if (IsActive)
            {
                if (accelerometer == null)
                {
                    accelerometer = new Accelerometer { TimeBetweenUpdates = TimeSpan.FromMilliseconds(20) };
                    accelerometer.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(AccelerometerCurrentValueChanged);
                    accelerometer.Start();
                }

                Engine.Instance.accelometrOn = true;
            }
        }

        void StopAccelerometer()
        {
            if (IsActive)
            {
                Engine.Instance.accelometrOn = false;
                if (accelerometer != null)
                {
                    accelerometer.Stop();
                    accelerometer = null;
                }
            }
        }
        // zamieniam x z y bo gramy w ustawieniu poziomym
        void AccelerometerCurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            if (accelerometer.IsDataValid)
            {
                float Vx = e.SensorReading.Acceleration.X * 200;
                float Vy = e.SensorReading.Acceleration.Y * 200;
                if (Math.Abs(Vx) > Math.Abs(Vy))
                {
                    Engine.Instance.dy = Math.Sign(Vx);
                    Engine.Instance.dx = 0;
                }
                else
                {
                    Engine.Instance.dx = Math.Sign(Vy);
                    Engine.Instance.dy = 0;
                }
                
                Debug.WriteLine("Accel : " + Vx);
                Debug.WriteLine("Accel : " + Vy);
            }
        }
        #endregion

    }
}
