using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.GameSaving
{
    public class ReflectionMazeBlockSerializer : IMazeBlockSerializer
    {
        public string Serilalize(MazeBlock mazeBlock)
        {
            return mazeBlock.ToString();
        }
        public MazeBlock Deserialize(string record)
        {
            Type mType = Type.GetType(record);

            var getInstanceMethod = mType.GetMethod("get_Instance");
            return (MazeBlock)getInstanceMethod.Invoke(null, null);
            
            
        }
    }
}
