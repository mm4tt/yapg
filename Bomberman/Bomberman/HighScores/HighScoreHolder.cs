using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Bomberman.HighScores
{
   
   [DataContract()]
   public class HighScoreHolder
    {
        static int maxScores = 3;

        
        private List<HighScore> highScores = new List<HighScore>();

       [DataMember()]
        public List<HighScore> HighScores
        {
            get { return highScores; }
            set { highScores = value; }
        }

        public int AddHighScore(HighScore score)
        {
            highScores.Add(score);
            highScores.Sort();
            highScores = highScores.Take(Math.Min(maxScores, highScores.Count)).ToList();
            return highScores.IndexOf(score);
        }

    }
}
