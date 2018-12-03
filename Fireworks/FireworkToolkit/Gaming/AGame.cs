using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Gaming
{
    public abstract class AGame : IGame
    {
        #region Properties

        /// <summary>
        /// The current score in the game
        /// </summary>
        public int Score { get; protected set; } = 0;

        /// <summary>
        /// A list of scores that have been saved into this environment
        /// </summary>
        public List<HighScore> ScoresList { get; private set; } = new List<HighScore>();

        #endregion

        #region Constructors

        public AGame(int initialScore = 0)
        {
            Score = initialScore;
        }

        #endregion

        #region Methods

        public virtual int IncreaseScore(int amount = 1)
        {
            Score += amount;
            return Score;
        }

        public virtual int DecreaseScore(int amount = 1)
        {
            Score -= amount;
            return Score;
        }


        public abstract bool IsOver();

        public abstract bool IsRunning();

        public abstract string GetPlayerName();

        public virtual void ResetScore()
        {
            Score = 0;
        }

        public virtual bool SaveCurrentScore(int topRange = 10)
        {
            HighScore highscore = new HighScore(GetPlayerName(), Score);
            return SaveScore(highscore, topRange);
        }

        public virtual bool SaveScore(HighScore score, int topRange = 10)
        {
            ScoresList.Add(score);
            ScoresList.Sort();
            return ScoresList.IndexOf(score) < topRange;
        }

        public virtual List<bool> SaveScoreRange(ICollection<HighScore> scores, int topRange = 10)
        {
            List<bool> results = new List<bool>(scores.Count);
            foreach (HighScore score in scores)
                SaveScore(score);
            foreach (HighScore score in scores)
                results.Add(ScoresList.IndexOf(score) < topRange);
            return results;
        }

        public virtual void ResetScoresList()
        {
            ScoresList.Clear();
        }

        public virtual List<HighScore> GetHighScores(int topRange = 10)
        {
            List<HighScore> results = new List<HighScore>(topRange);
            for (int i = 0; i < topRange; i++)
                results.Add(ScoresList[i]);
            return results;
        }

        public virtual bool CheckScore(int score, int topRange = 10)
        {
            for (int i = 0; i < ((topRange < ScoresList.Count) ? topRange : ScoresList.Count); i++)
                if (score > ScoresList[i].Score)
                    return true;
            return false;
        }

        #endregion
    }
}
