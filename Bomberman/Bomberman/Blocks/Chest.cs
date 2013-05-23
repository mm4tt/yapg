using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    class Chest : MazeBlock
    {
        #region Singleton
        private static Chest instance = new Chest();
        protected Chest() { }
        public static Chest Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        protected override void LoadGraphic(GraphicsDevice graphicDevice, ContentManager contentManager)
        {
            //SetOneColorTexture(Color.Green, graphicDevice);
            texture = contentManager.Load<Texture2D>("Chests\\Chest");
        }
    }
    class BombRangeChest : Chest
    {
        #region Singleton
        private static BombRangeChest instance = new BombRangeChest();
        private BombRangeChest() { }
        public static BombRangeChest Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion
        protected override void LoadGraphic(GraphicsDevice graphicDevice, ContentManager contentManager)
        {
            //SetOneColorTexture(Color.Aqua, graphicDevice);
            texture = contentManager.Load<Texture2D>("Chests\\ChestInverted");
        }
    }
    class CrazyBombChest : Chest
    {
        #region Singleton
        private static CrazyBombChest instance = new CrazyBombChest();
        private CrazyBombChest() { }
        public static CrazyBombChest Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion
        protected override void LoadGraphic(GraphicsDevice graphicDevice, ContentManager contentManager)
        {
            //SetOneColorTexture(Color.Pink, graphicDevice);
            texture = contentManager.Load<Texture2D>("Chests\\Chest");
        }
    }
    class ExtraBombChest : Chest
    {
        #region Singleton
        private static ExtraBombChest instance = new ExtraBombChest();
        private ExtraBombChest() { }
        public static ExtraBombChest Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion
        protected override void LoadGraphic(GraphicsDevice graphicDevice, ContentManager contentManager)
        {
            //SetOneColorTexture(Color.DarkGray, graphicDevice);
            texture = contentManager.Load<Texture2D>("Chests\\ChestGreen");
        }
    }
    class DispersionEnemyChest : Chest
    {
        #region Singleton
        private static DispersionEnemyChest instance = new DispersionEnemyChest();
        private DispersionEnemyChest() { }
        public static DispersionEnemyChest Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion
        protected override void LoadGraphic(GraphicsDevice graphicDevice, ContentManager contentManager)
        {
            //SetOneColorTexture(Color.LavenderBlush, graphicDevice);
            texture = contentManager.Load<Texture2D>("Chests\\ChestRed");
        }
    }
    class MovementThrowableChest : Chest
    {
        #region Singleton
        private static MovementThrowableChest instance = new MovementThrowableChest();
        private MovementThrowableChest() { }
        public static MovementThrowableChest Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion
        protected override void LoadGraphic(GraphicsDevice graphicDevice, ContentManager contentManager)
        {
            //SetOneColorTexture(Color.BlanchedAlmond, graphicDevice);
            texture = contentManager.Load<Texture2D>("Chests\\ChestFunky");
        }
    }
    class ReverseMovementChest : Chest
    {
        #region Singleton
        private static ReverseMovementChest instance = new ReverseMovementChest();
        private ReverseMovementChest() { }
        public static ReverseMovementChest Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion
        protected override void LoadGraphic(GraphicsDevice graphicDevice, ContentManager contentManager)
        {
            //SetOneColorTexture(Color.PaleGreen, graphicDevice);
            texture = contentManager.Load<Texture2D>("Chests\\ChestLaplace");
        }
    }
    class SpeedChest : Chest
    {
        #region Singleton
        private static SpeedChest instance = new SpeedChest();
        private SpeedChest() { }
        public static SpeedChest Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion
        protected override void LoadGraphic(GraphicsDevice graphicDevice, ContentManager contentManager)
        {
            //SetOneColorTexture(Color.Thistle, graphicDevice);
            texture = contentManager.Load<Texture2D>("Chests\\ChestPurple");
        }
    }
    class DoubleSpeedChest : Chest
    {
        #region Singleton
        private static DoubleSpeedChest instance = new DoubleSpeedChest();
        private DoubleSpeedChest() { }
        public static DoubleSpeedChest Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion
        protected override void LoadGraphic(GraphicsDevice graphicDevice, ContentManager contentManager)
        {
            //SetOneColorTexture(Color.Peru, graphicDevice);
            texture = contentManager.Load<Texture2D>("Chests\\ChestGrey");
        }
    }
}