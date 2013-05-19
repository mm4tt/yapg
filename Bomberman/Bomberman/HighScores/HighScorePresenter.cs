using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.HighScores
{
    public abstract class HighScorePresenter
    {
        protected List<HighScore> highScores;
        public HighScorePresenter(List<HighScore> highScores)
        {
            this.highScores = highScores;
        }

        public virtual void PrePresent() {}
        abstract public string Present(HighScore score);

        public List<String> presentHighScores()
        {
            PrePresent();
            List<String> result = new List<string>();
            foreach(HighScore score in highScores)
                result.Add(Present(score));
            return result;

        }


        
    }
}
