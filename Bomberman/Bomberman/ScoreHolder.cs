using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;
namespace Bomberman
{
    [DataContract()]
    public class ScoreHolder
    {
        private int score = 0;

        [DataMember()]
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public void killedEnemy()
        {
            score += 5;
            Debug.WriteLine("Killed enemy. Score = " + score);
        }
        public void destroyedObstacle()
        {
            score += 1;
            Debug.WriteLine("Destoyed obstacle . Score = " + score);
        }

        public void nextLevel(int oldLevel)
        {
            score += 100;
            Debug.WriteLine("Level passed . Score = " + score);
        }
    }
}
