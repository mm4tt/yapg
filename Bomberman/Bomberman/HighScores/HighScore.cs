using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Bomberman.HighScores{

    [DataContract()]
    public class HighScore : IComparable
    {
        [DataMember()]
        public int score;
        [DataMember()]
        public DateTime date;

        public HighScore(int score)
        {
            this.score = score;
            this.date = DateTime.Now;
        }

        int IComparable.CompareTo(object obj)
        {
            HighScore other = (HighScore)obj;
            if (other.score < this.score) return -1;
            if (other.score > this.score) return 1;
            else
            {
                return this.date.CompareTo(other.date);
            }
        }
    }
}
