using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.HighScores
{
    class HighScoreController
    {
        private HighScoreHolder highScoreHolder;


        private HighScoreHolder @HighScoreHolder
        { // lazy evaluation
            get
            {
                if (this.highScoreHolder == null)
                    highScoreHolder = new HighScoreSaver().LoadHighScore();
                return highScoreHolder;
            }
        }
      

        public int AddHighScore(int score)
        {
            return (HighScoreHolder.AddHighScore(new HighScore(score)));
        }

        public void SaveHighScore()
        {
            if (highScoreHolder != null)
                new HighScoreSaver().SaveHighScore(highScoreHolder);
        }

        public List<HighScore> Scores
        {
            get
            {
                return HighScoreHolder.HighScores;
            }
        }



       

    }
}
