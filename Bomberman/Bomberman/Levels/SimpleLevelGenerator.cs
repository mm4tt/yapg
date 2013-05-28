using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Bomberman.StateManager;
using Bomberman.Screens;
namespace Bomberman.Levels
{
    class SimpleLevelGenerator : ILevelGenerator
    {
        
        public void GenerateLevel(Engine engine)
        {
            Player p = engine.Player;
            if (p == null)
                p = new Player();
            engine.Clear();
            engine.AddPlayer(p);
            engine.Maze = GenerateMaze();
            engine.Panel = new Panel();
            p.clear();
            p.FindBeginPosition();

            for (int n = NumberOfEnemies(engine.Level); n > 0; --n)
                engine.AddObject(new Enemy());

            for (int n = NumberOfGhosts(engine.Level); n > 0; --n)
                engine.AddObject(new Ghost());

            for (int n = NumberOfBombers(engine.Level); n > 0; --n)
                engine.AddObject(new Bomber());
            
            
        }

        private int NumberOfEnemies(int level)
        {
            return (int)((Math.Sqrt(1 + 8 * level) - 1) / 2);
        }

        private int NumberOfBombers(int level)
        {
            return (int)((Math.Sqrt(1 + 8 * level) - 1) / 2);
        }

        private int NumberOfGhosts( int level )
        {
            if (level < 4) {
                return 0;
            }
            return (int)((Math.Sqrt(1 + 8 *( level-3 )) - 1) / 4 + 1);
        }
            

        private void findGraph(  Maze maze,  int[,] tab, int x, int y, int color ){
            if (!( maze.Block[(uint)x + 1, (uint)y] is Wall) && tab[(uint)x + 1, (uint)y] != color )
            {
                tab[(uint)x + 1, (uint)y] = color;
                findGraph(maze, tab, x + 1, y, color);
            }
            if (!(maze.Block[(uint)(x - 1), (uint)y] is Wall) && tab[(uint)(x - 1), (uint)y] != color)
            {
                tab[(uint)(x - 1), (uint)y] = color;
                findGraph(maze, tab, x - 1, y, color);
            }
            if (!(maze.Block[(uint)x,(uint)y+1] is Wall) && tab[(uint)x, (uint)y + 1] != color)
            {
                tab[(uint)x, (uint)y + 1] = color;
                findGraph(maze, tab, x, y + 1, color);
            }
            if (!(maze.Block[(uint)x , (uint)(y - 1)] is Wall) && tab[(uint)x, (uint)(y - 1)] != color)
            {
                tab[(uint)x, (uint)(y - 1)] = color;
                findGraph(maze, tab, x, y - 1, color);
            }
        }
        private void dig(Point from, Point to, Maze maze,int[,] tab, int color, ref bool make_holes ){
            if (from.X == to.X && from.Y == to.Y || ( tab[from.X, from.Y] > 0 && tab[from.X, from.Y] != color ) ) {
                make_holes = true;
                return;
            }
            if (from.X != to.X)
            {
                int dir = to.X - from.X < 0 ? -1 : 1;
                dig(new Point(from.X + dir, from.Y), to,  maze, tab,color, ref make_holes  );
            }
            else {
                int dir = to.Y - from.Y < 0 ? -1 : 1;
                dig(new Point(from.X, from.Y + dir), to, maze, tab, color , ref make_holes );
            }
            if (tab[from.X, from.Y] == color) {
                make_holes = false;
            }
            if ( make_holes && maze.Block[(uint)from.X, (uint)from.Y] is Wall) {
                maze.dig(from.X, from.Y);
            }
        }
        private Maze GenerateMaze()
        {   
            int[,] tab = new int[Maze.Width,Maze.Height];
            
            Maze maze = new Maze();
            maze.GenerateMonastery(1, 40);

           /* for (int i = 0; i < Maze.Width; i++) {
                for (int j = 0; j < Maze.Height; j++) {
                    if (maze.Block[(uint)i, (uint)j] is Wall)
                    {
                        tab[i, j] = -1;
                    }
                    else {
                        tab[i, j] = 0;
                    }
                }
            }
            int color = 1;
            List<Point> points = new List<Point>();
           
            for (int i = 0; i < Maze.Width; i++)
            {
                for (int j = 0; j < Maze.Height; j++) {
                    if (tab[i, j] == 0) {
                        findGraph(maze, tab, i, j, color);
                        points.Add(new Point(i, j));  
                        color++;
                    }
                }
            }
            for (int i = 1; i < points.Count(); i++) { 
                bool make_holes = false;
                dig(points[i - 1], points[i], maze, tab, i, ref make_holes);
            }
            for (int i = 0; i < Maze.Width; i++)
            {
                string line = "";
                for (int j = 0; j < Maze.Height; j++)
                {
                    line += tab[i, j].ToString() + " ";
                }
                Debug.WriteLine(line);
            }*/
            return maze;
        }



    }
}
