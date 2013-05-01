using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.GameSaving
{
    public interface IMazeBlockSerializer 
    {
        string Serilalize(MazeBlock mazeBlock);
        MazeBlock Deserialize(string record);
    }
}
