using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Gaming
{
    public interface IGame
    {
        /// <summary>
        /// Increases the score by the specified amount
        /// </summary>
        /// <param name="amount">[optional] The amount to increase the score by (1)</param>
        /// <returns>Returns the new score amount</returns>
        int IncreaseScore(int amount = 1);

        /// <summary>
        /// Decreases the score by the specified amount
        /// </summary>
        /// <param name="amount">[optional] The amount to decrease the score by (1)</param>
        /// <returns>Returns the new score amount</returns>
        int DecreaseScore(int amount = 1);

        /// <summary>
        /// Resets the score to zero.
        /// </summary>
        void ResetScore();

        /// <summary>
        /// Determines if the current game object has finished yet.
        /// </summary>
        /// <returns>Returns true if the current game has ended, returns false otherwise.</returns>
        bool IsOver();

        /// <summary>
        /// Determines if the current game object is running.
        /// </summary>
        /// <returns>Returns true if the game is still running, returns false otherwise.</returns>
        bool IsRunning();

        /// <summary>
        /// Gets the player's name via a dialog box
        /// </summary>
        /// <returns>Returns a string representing the player's name</returns>
        string GetPlayerName();
    }
}
