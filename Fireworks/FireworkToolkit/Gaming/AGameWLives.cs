using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Gaming
{
    public abstract class AGameWLives : AGame, IGameWLives
    {
        #region Properties

        public int Lives { get; protected set; } = 3;

        #endregion

        #region Constructors

        public AGameWLives(int initialScore = 0, int initialLifeCount = 3) : base(initialScore)
        {
            ResetLives(initialLifeCount);
        }

        #endregion

        #region Methods

        public int IncreaseLifeCount(int amount = 1)
        {
            ResetLives(Lives + amount);
            return Lives;
        }

        public void ResetLives(int newAmount = 3)
        {
            Lives = newAmount;
        }

        public override bool IsOver()
        {
            return Lives == 0;
        }

        #endregion
    }
}
