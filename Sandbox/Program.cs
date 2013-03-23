using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman;
using System.IO;
using System.Threading;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Maze M = new Maze();

            bool run = true;



            new Thread(() =>
                {
                    StringWriter sw = new StringWriter();
                    Console.SetOut(sw);
                    int i = 0;
                    while (run)
                    {
                        M.Draw();
                        Console.WriteLine("Iteration: " + i++);
                        System.IO.File.WriteAllText("Maze.maz", sw.ToString());
                        sw.GetStringBuilder().Clear();
                        Thread.Sleep(100);
                    }

                    sw.Close();
                }
                ).Start();
            var t = new Thread(() => M.GenerateRandom(4, 50));
            t.Start();

            Console.WriteLine("Press any key to abort");
            Console.ReadKey();
            run = false;
            t.Abort();
        
           
        }
    }
}
