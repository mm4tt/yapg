using System;
using System.Collections.Generic;
using System.Linq;
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

        public void KilledEnemy()
        {
            score += 5;
            Debug.WriteLine("Killed enemy. Score = " + score);
        }
        public void DestroyedObstacle()
        {
            score += 1;
            Debug.WriteLine("Destoyed obstacle . Score = " + score);
        }
        public void EnemyDestroyedObstacle()
        {
            score -= 1;
            Debug.WriteLine("Joke! Enemy destoyed obstacle . Score = " + score);
        }

        public void LevelPassed(int passedLevel)
        {
            score += 100;
            Debug.WriteLine("Level passed . Score = " + score);
        }
    }
}
