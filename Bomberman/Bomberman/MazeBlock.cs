using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Reflection;

namespace Bomberman
{  
    [XmlInclude(typeof(Wall))]
    [XmlInclude(typeof(Empty))]
    [XmlInclude(typeof(Chest))]
    [XmlInclude(typeof(Obstacle))]
    public abstract class MazeBlock : Drawable
    {
        protected Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;

        public static int height = 20;
        public static int width = 20;
        [XmlIgnore]
        public Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set
            {
                spriteBatch = value;
                LoadGraphic();
            }
                  
        }
        
        protected  Texture2D texture;

        public virtual void Draw(uint x, uint y)
        {
            spriteBatch.Draw(texture, ComputePosition(x, y), Color.White);
        }
        protected abstract void LoadGraphic();
        protected void SetOneColorTexture(Color chosenColor)
        {
            texture = new Texture2D(spriteBatch.GraphicsDevice, width, height);
            Color[] colors = new Color[width * height];
            for (int i = 0; i < colors.Length; ++i)
                colors[i] = chosenColor;
            texture.SetData(colors);
        }

        protected Rectangle ComputePosition(uint x, uint y)
        {
            return new Rectangle((int)x * width, (int) y * height, width, height);
        }

        public virtual string getTypeString()
        {
            return this.ToString();
        }

        public static MazeBlock getMazeBlock(string gtype)
        {
            Type mType = Type.GetType(gtype);
        /*    Debug.WriteLine("Type : is null ? " + mType == null);
            Debug.WriteLine("Type : " + mType.FullName);
            foreach (MethodInfo method in mType.GetMethods())
            {
                Debug.WriteLine(method.IsStatic);
                Debug.WriteLine(method.Name);
            }*/
            return (MazeBlock)mType.GetMethod("get_Instance").Invoke(null, null);
        }
    }
}
