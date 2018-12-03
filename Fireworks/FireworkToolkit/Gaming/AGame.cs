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

        public int Score { get; protected set; } = 0;

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

        #endregion
    }
}
