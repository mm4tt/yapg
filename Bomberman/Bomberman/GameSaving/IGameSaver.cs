using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.GameSaving
{
    interface IGameSaver
    {
        void SaveGame(Engine engine, string fileName);
        Engine LoadGame(string fileName);
        void SaveGame(Engine engine);

        Engine LoadGame();
    }
}
