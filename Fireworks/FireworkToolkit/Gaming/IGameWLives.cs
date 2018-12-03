using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Gaming
{
    public interface IGameWLives : IGame
    {
        /// <summary>
        /// Increases the number of  lifes the current player has by the specified amount
        /// </summary>
        /// <param name="amount">[optional] The amount to increase the life total by (1)</param>
        /// <returns>Returns the new number of lives that the player has.</returns>
        int IncreaseLifeCount(int amount = 1);

        /// <summary>
        /// Decreases the number of  lifes the current player has by the specified amount
        /// </summary>
        /// <param name="amount">[optional] The amount to decrease the life total by (1)</param>
        /// <returns>Returns the new number of lives that the player has.</returns>
        int DecreaseLifeCount(int amount = 1);

        /// <summary>
        /// Resets the life count to some specified value
        /// </summary>
        /// <param name="newAmount">[optional] The new amount of lives the player has (3)</param>
        void ResetLives(int newAmount = 3);
    }
}
