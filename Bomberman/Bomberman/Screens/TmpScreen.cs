using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.StateManager;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Bomberman.Screens
{
    class TmpScreen : MenuScreen
    {
        Button ok;
        public void AddAction(EventHandler<EventArgs> handler) {
            ok.Tapped += handler;
        }
        public TmpScreen( string message ) :base( message ){
            ok = new Button("Ok");
            ok.Tapped += OkButton_Tapped;
            MenuButtons.Add(ok);
            TitlePositionY = 180;
        }
   
        void OkButton_Tapped(object sender, EventArgs e) {
            ExitScreen();
        }
     
       
    }
}
