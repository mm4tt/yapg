using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Bomberman.Modifiers
{
    class DispersionEnemyModifier : Modifier
    {
        #region Singleton
        private static DispersionEnemyModifier instance = new DispersionEnemyModifier();
        private DispersionEnemyModifier() { }
        public static DispersionEnemyModifier Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion
<<<<<<< HEAD

=======
        public override MazeBlock getBlock()
        {
            return DispersionEnemyChest.Instance;
        }
>>>>>>> i003_z002b
        public override void onBegin(Player player)
        {
            Point p = player.Position;
            float range = 30;
          
            foreach (Enemy enemy in Engine.Instance.Enemies) {
                Point e = enemy.Position;
                float de = ( e.X - p.X ) * ( e.X - p.X ) + ( e.Y - p.Y ) * ( e.Y - p.Y );
                if (de < range) {
                    float power = range - de;
                    float tmp_de = de;
                    int sigx = e.X - p.X < 0 ? -1 : 1;
                    int sigy = e.Y - p.Y < 0 ? -1 : 1;
                    int x = e.X;
                    int y = e.Y;
                    for (int i = 0; i < power; i++) {
                        if (e.X - p.X > e.Y - p.Y)
                        {
                            if (Engine.Instance.Maze.Block[(uint)(x + sigx), (uint)(y)] is Empty)
                            {
                                x += sigx;
                                //enemy.Position = new Point(enemy.Position.X + sigx, enemy.Position.Y);
                            }
                            else if (Engine.Instance.Maze.Block[(uint)(x), (uint)(y +  sigy)] is Empty)
                            {
                                //enemy.Position = new Point(enemy.Position.X, enemy.Position.Y + sigy);
                                y += sigy;
                            }
                        }
                        else {
                            if (Engine.Instance.Maze.Block[(uint)(x), (uint)(y + sigy)] is Empty)
                            {
                                //enemy.Position = new Point(enemy.Position.X, enemy.Position.Y + sigy);
                                y += sigy;
                            }
                            else if (Engine.Instance.Maze.Block[(uint)(x + sigx), (uint)(y)] is Empty)
                            {
                                x += sigx;
                                //enemy.Position = new Point(enemy.Position.X + sigx, enemy.Position.Y);
                            }

                        }
                        enemy.Position = new Point(x, y);
                    }
                }
            }
        }
    }
}
