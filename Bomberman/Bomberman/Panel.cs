using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Diagnostics;
namespace Bomberman
{
    public class Panel
    {
        public Panel() { }

        SpriteFont font = null;
        public void Draw(uint x, uint y, SpriteBatch spriteBatch, ContentManager content)
        {
           
            if ( font == null ) 
                font = content.Load<SpriteFont>("panelfont");
            string line1 = "Level: " + Engine.Instance.Level.ToString();
            line1 += " Scores: " + Engine.Instance.ScoreHolder.Score.ToString();
            int activebombs = 0;
            foreach (var bomb in Engine.Instance.Bombs) {
                if (bomb.isActive()) {
                    activebombs++;
                } 
            }
            line1 += " Bombs: " + ( Engine.Instance.Player.BombsAvailable - activebombs).ToString();
            if (Engine.Instance.Player.MovementMode == Player.MODE_MOVEMENT_DEFAULT)
            {
                line1 += " Speed: " + Engine.Instance.Player.Speed.ToString();
            }
            else {
                line1 += " Speed: N";
            }
            line1 += " Range: " + Engine.Instance.Player.ExplosionRange.ToString();
            
            spriteBatch.DrawString(font, line1, new Vector2(0, 2), Color.White);

            string line2 = "Effects: ";
            spriteBatch.DrawString(font, line2, new Vector2(0, Maze.BlockHeight ), Color.White);
            int offset = 3;
            foreach( var effect in Engine.Instance.Player.Effects ){
                effect.Draw(offset ,1, spriteBatch, content);
                offset += 1;
            }
            
        }



       
    }
}
