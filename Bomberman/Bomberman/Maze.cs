using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    public class Maze
    {
        public const uint Height=24;
        public const uint Width=40;


        public Maze()
        {
            for (uint x = 0; x < Width; ++x)
                for (uint y = 0; y < Height; ++y)
                    blocks[x, y] = (x == 0 || x + 1 == Width || y == 0 || y + 1 == Height) ? (MazeBlock)Wall.Instance : Empty.Instance;

            wBlock = new ArrayWrapper<MazeBlock>(blocks);
            wModifier = new ArrayWrapper<Modifier>(modifiers);
            
        }


        public void GenerateRandom(int threshold, int percent)
        {
            this.threshold = threshold;
            GenerateRandom(1, 1, (int)Width - 2, (int)Height - 2);
            FillObstacles(percent);
        }

        private void FillObstacles(int percent)
        {
            int empty = 0;
            for (uint x = 0; x < Width; ++x)
                for (uint y = 0; y < Height; ++y)
                    if (blocks[x, y] == Empty.Instance)
                        ++empty;

            int obstacles = percent * empty / 100;
            while (obstacles > 0)
            {
                int x = rnd.Next(0, (int)Width),
                    y = rnd.Next(0, (int)Height);
                if (blocks[x, y] == Empty.Instance)
                {
                    blocks[x, y] = Obstacle.Instance;
                    --obstacles;
                }
            }
        }

        private int threshold = 4;
        private Random rnd = new Random();


        private void GenerateRandom(int x0, int y0, int x1, int y1)
        {
            if (x1 - x0 < threshold || y1 - y0 < threshold)
                return;
            int dx,dy;
            do
            {
                dx = rnd.Next(1, x1 - x0);
                dy = rnd.Next(1, y1 - y0); //maybe not uniform distrubution ?
            } while (blocks[x0 + dx, y0 - 1] == Empty.Instance || blocks[x0 + dx, y1 + 1] == Empty.Instance || blocks[x0 - 1, y0 + dy] == Empty.Instance || blocks[x1 + 1, y0 + dy] == Empty.Instance);

            //generate walls
            for (int x = x0; x <= x1; ++x)
                blocks[x, y0 + dy] = Wall.Instance;
            for (int y = y0; y <= y1; ++y)
                blocks[x0 + dx, y] = Wall.Instance;


            //create holes in the 3 walls (maybe more holes with some probability based on a wall length)
            bool wasZero = generateHolesX(x0, x0 + dx - 1,y0+dy) == 0;
            if (wasZero) while (generateHolesX(x0 + dx + 1, x1, y0 + dy) == 0) ;
            else wasZero = generateHolesX(x0 + dx + 1, x1, y0 + dy) == 0;
            if (wasZero) while (generateHolesY(y0, y0+dy-1, x0+dx) == 0) ;
            else wasZero = generateHolesY(y0, y0 + dy - 1, x0 + dx) == 0;
            if (wasZero) while (generateHolesY(y0 + dy + 1, y1, x0 + dx) == 0) ;
            else wasZero = generateHolesY(y0 + dy + 1, y1, x0 + dx) == 0;


            /*
            int ommit = rnd.Next(4);
            if (ommit != 0) blocks[rnd.Next(x0,x0+dx), y0 + dy] = Empty.Instance;
            if (ommit != 1) blocks[rnd.Next(x0+dx+1,x1+1) , y0 + dy] = Empty.Instance;
            if (ommit != 2) blocks[x0 + dx, rnd.Next(y0, y0+dy) ] = Empty.Instance;
            if (ommit != 3) blocks[x0 + dx, rnd.Next(y0 + dy + 1, y1 + 1)] = Empty.Instance;
            */




            GenerateRandom(x0, y0, x0 + dx - 1, y0 + dy - 1);
            GenerateRandom(x0+dx+1, y0, x1, y0 + dy - 1);
            GenerateRandom(x0, y0+dy+1, x0 + dx - 1, y1);
            GenerateRandom(x0 + dx + 1, y0 + dy + 1, x1,y1);
        }

        private int generateHolesX(int x0, int x1, int y)
        {
            Console.Error.WriteLine("genX {0} {1} {2}", x0, x1, y);
            int holes = 0;
            int tries = rnd.Next((x1 - x0) / threshold)+1;
            for (int i = 0; i < tries; ++i)
            {
                int dx = rnd.Next(x0, x1+1);
                if (blocks[dx, y + 1] == Empty.Instance && blocks[dx, y - 1] == Empty.Instance)
                {
                    ++holes;
                    blocks[dx, y] = Empty.Instance;
                }
            }
            return holes;
        }

        private int generateHolesY(int y0, int y1, int x)
        {

            Console.Error.WriteLine("genY {0} {1} {2}", y0, y1, x);
            int holes = 0;
            int tries = rnd.Next((y1 - y0) / threshold)+1 ;
            for (int i = 0; i < tries; ++i)
            {
                int dy = rnd.Next(y0, y1 + 1);
                if (blocks[x+1,dy] == Empty.Instance && blocks[x-1, dy] == Empty.Instance)
                {
                    ++holes;
                    blocks[x, dy] = Empty.Instance;
                }
            }
            return holes;
        }


        public void Draw()
        {
            for (uint y = 0; y < Height; ++y)
            {
                for (uint x = 0; x < Width; ++x)
                {
                    blocks[x, y].Draw(x, y);
                    if (modifiers[x, y] != null)
                        Chest.Instance.Draw(x, y);
                }
                Console.WriteLine();
            }
        }

        public void Destroy(uint x, uint y)
        {
            modifiers[x, y] = null;
            if (blocks[x, y] is Obstacle)
            {
                blocks[x, y] = Empty.Instance;
                Random random = new Random(DateTime.Now.Millisecond);
                int i = random.Next(100);
                if (i < 10)
                {
                    modifiers[x, y] = DoubleSpeed.Instance;
                }
                else if (i < 80)
                {
                    modifiers[x, y] = ExtraBomb.Instance;
                }
            }
        }


        public ArrayWrapper<MazeBlock> Block
        {
            get
            {
                return wBlock;
            }
        }


        public ArrayWrapper<Modifier> Modifier
        {
            get
            {
                return wModifier;
            }
        }

        private ArrayWrapper<MazeBlock> wBlock;
        private ArrayWrapper<Modifier>  wModifier;

        private MazeBlock[,] blocks = new MazeBlock[Width, Height];
        private Modifier[,] modifiers = new Modifier[Width, Height];

        public void SetSpriteBatch(SpriteBatch spriteBatch)
        {
            foreach (MazeBlock block in blocks)
            {
                block.SpriteBatch = spriteBatch;
            }
            Chest.Instance.SpriteBatch = spriteBatch;
        }
    }

    public class ArrayWrapper<T>
    {
        T[,] A;
        public ArrayWrapper(T[,] A)
        {
            this.A = A;
        }

        public T this[uint x, uint y]
        {
            get
            {
                return A[x, y];
            }
        }
    }

    
}
