using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Bomberman.Modifiers;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Serialization;
using Bomberman.GameSaving;
namespace Bomberman
{
    [DataContractAttribute()]
    public class Maze 

    {
        public const int ScreenWidth = 800;
        public const int ScreenHeight = 480;
        public const int Height=15;
        public const int Width = 25;
        public static int BlockHeight
        {
            get { return StdGameScaler.Instance.blockHeight(); }
        }
        public static int BlockWidth
        {
            get { return ScreenWidth / Width; }
        }


        private List<Point> explosions = new List<Point>(); // ? tutaj czy nie lepiej w konstrukotrze ?
        public void clearExplosions() {
            explosions.Clear();
        }
        [DataMember()]
        public List<Point> Explosions {
            get { return explosions; }
            set { explosions = value; }
        }
        public Maze()
        {
            for (uint x = 0; x < Width; ++x)
                for (uint y = 0; y < Height; ++y)
                    blocks[x, y] = (x == 0 || x + 1 == Width || y == 0 || y + 1 == Height) ? (MazeBlock)Wall.Instance : Empty.Instance;

            wBlock = new ArrayWrapper<MazeBlock>(blocks);
            wModifier = new ArrayWrapper<Modifier>(modifiers);
           
            
        }

        /// <summary>
        /// Generates a random maze
        /// </summary>
        /// <param name="threshold">Maximum space beetwen walls</param>
        /// <param name="percent">Amount of the obstacles(%)</param>
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
            if (x1 - x0 <= threshold || y1 - y0 <= threshold)
                return;
            int dx,dy;
            do
            {
                dx = rnd.Next(1, x1 - x0);
                dy = rnd.Next(1, y1 - y0); //maybe not uniform distrubution ?
            } while (blocks[x0 + dx, y0 - 1] == Empty.Instance && blocks[x0 + dx, y1 + 1] == Empty.Instance && blocks[x0 - 1, y0 + dy] == Empty.Instance && blocks[x1 + 1, y0 + dy] == Empty.Instance);

            //generate walls
            for (int x = x0; x <= x1; ++x)
                blocks[x, y0 + dy] = Wall.Instance;
            for (int y = y0; y <= y1; ++y)
                blocks[x0 + dx, y] = Wall.Instance;


            //create holes in the 3 walls (maybe more holes with some probability based on a wall length)
            bool wasZero = generateHolesX(x0, x0 + dx - 1,y0+dy) == 0;
            if (wasZero) 
                while (generateHolesX(x0 + dx + 1, x1, y0 + dy) == 0) ;
            else 
                wasZero = generateHolesX(x0 + dx + 1, x1, y0 + dy) == 0;
            if (wasZero) 
                while (generateHolesY(y0, y0+dy-1, x0+dx) == 0) ;
            else 
                wasZero = generateHolesY(y0, y0 + dy - 1, x0 + dx) == 0;
            if (wasZero) 
                while (generateHolesY(y0 + dy + 1, y1, x0 + dx) == 0) ;
            else 
                wasZero = generateHolesY(y0 + dy + 1, y1, x0 + dx) == 0;


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
            if (x1 - x0 < 0)
                return 0;
            Console.Error.WriteLine("genX {0} {1} {2}", x0, x1, y);
            int holes = 0;
            int tries = 1; // rnd.Next((x1 - x0) / threshold) + 1;
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
            if (y1 - y0 < 0)
                return 0;
            Console.Error.WriteLine("genY {0} {1} {2}", y0, y1, x);
            int holes = 0;
            int tries = 1;//rnd.Next((y1 - y0) / threshold)+1 ;
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
        public void Draw(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            for (uint y = 0; y < Height; ++y)
            {
                for (uint x = 0; x < Width; ++x)
                {

                   Point p = StdGameScaler.Instance.cast(x, y);

                    blocks[x, y].Draw((uint)p.X, (uint)p.Y,spriteBatch,contentManager);
                     if (modifiers[x, y] != null)
                         modifiers[x, y].getBlock().Draw((uint)p.X, (uint)p.Y, spriteBatch, contentManager);
                }
                Console.WriteLine();
            }
        }

        static Random random = new Random(DateTime.Now.Millisecond);

        public bool Destroy(uint x, uint y)
        {
            modifiers[x, y] = null;
            if (blocks[x, y] is Obstacle)
            {
                blocks[x, y] = Empty.Instance;
                int i = random.Next(100);
                Engine.Instance.ScoreHolder.DestroyedObstacle();

                if (i < 5)
                 {
                   // Debug.WriteLine("DoubleSpeed");
                     modifiers[x, y] = DoubleSpeed.Instance;
                 }

                else if (i < 15)
                 {

                    //Debug.WriteLine("ExtraBomb");
                     modifiers[x, y] = ExtraBomb.Instance;

                }
                else if (i < 20)
                 {

                   // Debug.WriteLine("Movement");
                     modifiers[x, y] = MovementThrowable.Instance;
                 }
                else if (i < 25)
                 {
                   // Debug.WriteLine("Reverse");
                     modifiers[x, y] = ReverseMovement.Instance;
                 }
                else if (i < 30)
                {
                    //Debug.WriteLine("Reverse");
                    modifiers[x, y] = CrazyBombModifier.Instance;
                }
                else if (i < 40) {
                    modifiers[x, y] = DispersionEnemyModifier.Instance;
               }
                else if (i < 50)
                {
                    modifiers[x, y] = BombRangeModifier.Instance;
                }
                else if (i < 60)
                {
                    modifiers[x, y] = SpeedModifier.Instance;
                }

            }
            explosions.Add(new Point((int)x, (int)y));
            return false;
        }
        public void dig( int x, int y ){
            blocks[(uint)x, (uint)y] = Empty.Instance;
        }
        public bool isPassable(uint x, uint y)
        {
            return (blocks[x, y] is Empty);
        }

        public bool isSolid(uint x, uint y)
        {
            return !((blocks[x, y] is Empty) || (blocks[x, y] is Obstacle));
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
        public void destroyModifier( uint x, uint y) {
            modifiers[ x,y ] = null;
        }
        private ArrayWrapper<MazeBlock> wBlock;
        private ArrayWrapper<Modifier>  wModifier;

        private MazeBlock[,] blocks = new MazeBlock[Width, Height];
        private Modifier[,] modifiers = new Modifier[Width, Height];
        #region DataContractSerializationStuff
        
        
        [DataMember()]
        public string[] LinearMazeBlock
        {
            get
            {
                string[] tmp = new string[Width * Height];
                IMazeBlockSerializer ser = new ReflectionMazeBlockSerializer();
                for (int x = 0; x < Width; ++x)
                {
                    for (int y = 0; y < Height; ++y)
                    {
                        tmp[x * Height + y] = ser.Serilalize(blocks[x, y]);
                    }
                }
                return tmp;
            }
            set
            {
                Debug.WriteLine("In set LinearMazeBlock");
                string[] tmp = value;
                blocks = new MazeBlock[Width, Height];
                IMazeBlockSerializer ser = new ReflectionMazeBlockSerializer();
                for (int x = 0; x < Width; ++x)
                {
                    for (int y = 0; y < Height; ++y)
                    {
                        Debug.WriteLine(tmp[x * Height + y]);
                        blocks[x, y] = ser.Deserialize(tmp[x * Height + y]); //MazeBlock.getMazeBlock(tmp[x * Height + y]);
                    }
                }
                wBlock = new ArrayWrapper<MazeBlock>(blocks);
            }
        }
        [DataMember()]
        public string[] LinearModifiers
        {
            get
            {
                string[] tmp = new string[Width * Height];
                IModiferSerializer ser = new ReflectionModifierSerializer();
                for (int x = 0; x < Width; ++x)
                {
                    for (int y = 0; y < Height; ++y)
                    {
                        //tmp[x * Height + y] = modifiers[x, y].getTypeString();
                        tmp[x * Height + y] = ser.Serialize(modifiers[x, y]);
                        //tmp[x * Height + y] = "ZadenMadafakaNiePodskoczyDoPolaka";
                    }
                }
                return tmp;
            }

            set
            {
                Debug.WriteLine("Gonna setup linearModifiers");
                string[] tmp = value;
                modifiers = new Modifier[Width, Height];
                IModiferSerializer ser = new ReflectionModifierSerializer();
                for (int x = 0; x < Width; ++x)
                {
                    for (int y = 0; y < Height; ++y)
                    {
                        Debug.WriteLine(tmp[x * Height + y]);
                      
                        modifiers[x, y] = ser.Deserialize(tmp[x * Height + y]);
                    }
                }
                wModifier = new ArrayWrapper<Modifier>(modifiers);
            }
        }

        #endregion

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
