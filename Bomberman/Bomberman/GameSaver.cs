using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;


namespace Bomberman
{
    class GameSaver
    {
        private static string folder = "GameSaves";
        private static string defaultGameName = "gameState.xml";
        public void SaveGame(Engine engine,string saveName)
        {

            using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string filePath = System.IO.Path.Combine(folder, saveName);

                if (!isf.DirectoryExists(folder))
                    isf.CreateDirectory(folder);

                using (var stream = isf.CreateFile(filePath))
                {    
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Engine));
                    serializer.Serialize(stream, engine);
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        public Engine LoadGame( string saveName)
        {
            Engine newEngine = null;
            using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string filePath = System.IO.Path.Combine(folder, saveName);
                if (isf.FileExists(filePath))
                {
                    var stream = isf.OpenFile(filePath, System.IO.FileMode.Open);
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Engine));
                    newEngine = serializer.Deserialize(stream) as Engine;

                    stream.Close();
                    stream.Dispose();
                }
            }

            return newEngine;
        }

        public void saveGame(Engine engine)
        {
            SaveGame(engine, defaultGameName);
        }

        public Engine LoadGame()
        {
            return LoadGame(defaultGameName);
        }
    }
}
