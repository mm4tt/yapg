using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Bomberman
{
    class GameSaver
    {
        private static string folder = "GameSaves";
        private static string defaultGameName = "gameState.xml";
        public void SaveGame(Engine engine,string saveName)
        {
            Debug.WriteLine("Engine is null : " + (engine == null));
            //SerializableEngine sengine = new SerializableEngine(engine);
        
            Debug.WriteLine("Entered saving stuff");
            using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Debug.WriteLine("Inside of the first using directive");
                string filePath = System.IO.Path.Combine(folder, saveName);

                if (!isf.DirectoryExists(folder))
                    isf.CreateDirectory(folder);

                using (var stream = isf.CreateFile(filePath))
                {
                    Debug.WriteLine("Inside of the second using directive");
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Engine));
                    /*var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Engine), new Type[] {typeof(Maze), typeof(MazeBlock),
                        typeof(Empty), typeof(Obstacle), typeof(Player)});*/
 
                    serializer.Serialize(stream, engine);
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        public Engine LoadGame( string saveName)
        {
            Engine newEngine = null;
            Debug.WriteLine("In loaded game");
            using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Debug.WriteLine("Inside isf");
                string filePath = System.IO.Path.Combine(folder, saveName);
                if (isf.FileExists(filePath))
                {
                    Debug.WriteLine("File " + filePath + " exists ");
                    var stream = isf.OpenFile(filePath, System.IO.FileMode.Open);
                    Debug.WriteLine("Created stream");
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Engine));
                    newEngine = serializer.Deserialize(stream) as Engine;

                    stream.Close();
                    stream.Dispose();
                }
            }

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
