using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.HighScores
{
    class ScoreAndDateHighScorePresenter : HighScorePresenter
    {
        int padding = -1;
      

        public ScoreAndDateHighScorePresenter(List<HighScore> scores) : base(scores)
        {
          
        }

        public override string Present(HighScore score)
        {
            return paddScore(score.score) + "   " + score.date.ToShortDateString();
        }

        string paddScore(int score)
        {
            string scoreString = score.ToString();
            int z = padding - scoreString.Length;
            StringBuilder padd = new StringBuilder();
            for (int i = 0; i < z; ++i)
                padd.Append(" ");
            padd.Append(scoreString);
            return padd.ToString();
        }

        public override void PrePresent()
        {
            int m = -1;
            foreach (HighScore score in highScores)
                m = Math.Max(m, score.score);
            padding =(int) Math.Ceiling(Math.Log10((double)(m + 1)));


        }
    }

   

    

}
