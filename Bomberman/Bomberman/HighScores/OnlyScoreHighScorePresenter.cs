using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.HighScores
{
    class OnlyScoreHighScorePresenter : HighScorePresenter
    {
        public OnlyScoreHighScorePresenter(List<HighScore> scores) : base(scores)
        {   
        }

        public override string Present(HighScore score)
        {
            return score.score.ToString();
        }
    }
}
