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
            SetOneColorTexture(Color.Green, graphicDevice);
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
            SetOneColorTexture(Color.Aqua, graphicDevice);
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
            SetOneColorTexture(Color.Pink, graphicDevice);
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
            SetOneColorTexture(Color.DarkGray, graphicDevice);
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
            SetOneColorTexture(Color.LavenderBlush, graphicDevice);
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
            SetOneColorTexture(Color.BlanchedAlmond, graphicDevice);
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
            SetOneColorTexture(Color.PaleGreen, graphicDevice);
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
            SetOneColorTexture(Color.Thistle, graphicDevice);
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
            SetOneColorTexture(Color.Peru, graphicDevice);
        }
    }
}
