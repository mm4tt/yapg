using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            p.FindBeginPosition();

            for (int n = NumberOfEnemies(engine.Level); n > 0; --n)
                engine.AddObject(new Enemy());
        }

        private int NumberOfEnemies( int level )
        {
            return (int)((Math.Sqrt(1 + 8 * level) - 1) / 2);
        }
            


        private Maze GenerateMaze()
        {
            Maze maze = new Maze();
            maze.GenerateRandom(1, 40);
            return maze;
        }



    }
}
