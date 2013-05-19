using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO.IsolatedStorage;
using System.Diagnostics;

namespace Bomberman.HighScores
{
    class HighScoreSaver
    {

        private static string folder = "HighScores";
        private static string fileName = "HighScores.xml";

        public HighScoreHolder LoadHighScore()
        {
            HighScoreHolder result = null;
            using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                
                Debug.WriteLine("Inside of the first using directive");
                string filePath = System.IO.Path.Combine(folder, fileName);
                if (isf.FileExists(filePath))
                {
                
                    var stream = isf.OpenFile(filePath,System.IO.FileMode.Open);
                    Debug.WriteLine("Inside of the second using directive");
                    DataContractSerializer ser = new DataContractSerializer(typeof(HighScoreHolder));
                   
                    result =  ser.ReadObject(stream) as HighScoreHolder;//ser.WriteObject(stream, engine);

                    stream.Close();
                    stream.Dispose();
                }
            }
            return result != null ? result : (new HighScoreHolder());
        }

        public void SaveHighScore(HighScoreHolder highScores)
        {

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
                    DataContractSerializer ser = new DataContractSerializer(typeof(HighScoreHolder));

                    ser.WriteObject(stream, highScores);

                    stream.Close();
                    stream.Dispose();
                }
            }
        }
    }
}
