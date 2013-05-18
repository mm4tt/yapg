using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Bomberman.GameSaving
{
    public class DataContractSaver : IGameSaver
    {
        private static string folder = "GameSaves";
        private static string defaultGameName = "gameState.xml";
        public void SaveGame(Engine engine, string fileName)
        {
            Debug.WriteLine("Engine is null : " + (engine == null));

            Debug.WriteLine("Entered saving stuff");
            using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Debug.WriteLine("Inside of the first using directive");
                string filePath = System.IO.Path.Combine(folder, fileName);

                if (!isf.DirectoryExists(folder))
                    isf.CreateDirectory(folder);

                using (var stream = isf.CreateFile(filePath))
                {
                    Debug.WriteLine("Inside of the second using directive");
                    DataContractSerializer ser = new DataContractSerializer(typeof(Engine));
      
                    ser.WriteObject(stream , engine);
                  
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        public Engine LoadGame(string fileName)
        {
            Engine newEngine = null;
            Debug.WriteLine("In loaded game");
            using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Debug.WriteLine("Inside isf");
                string filePath = System.IO.Path.Combine(folder, fileName);
                if (isf.FileExists(filePath))
                {
                    Debug.WriteLine("File " + filePath + " exists ");
                    var stream = isf.OpenFile(filePath, System.IO.FileMode.Open);
                    Debug.WriteLine("Created stream");
                    var serializer = new DataContractSerializer(typeof(Engine));
                    newEngine = serializer.ReadObject(stream) as Engine;

                    stream.Close();
                    stream.Dispose();
                }
            }
            if (newEngine != null)
                newEngine.fixStuff();
            return newEngine;
        }

        public void SaveGame(Engine engine)
        {
            SaveGame(engine, defaultGameName);
        }

        public Engine LoadGame()
        {
            return LoadGame(defaultGameName);
        }
    }
}
