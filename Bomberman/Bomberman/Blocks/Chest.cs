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
            SetOneColorTexture(Color.Beige, graphicDevice);
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
}
