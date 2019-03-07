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

        /// <summary>
        /// Saves the current score to the list of scores and determines if it's within the top number of spots for high scores
        /// </summary>
        /// <param name="topRange">The top n scores to check against, 10 means to check the top 10 high scores</param>
        /// <returns>Returns if the current score is within the top n high scores, returns false otherwise.</returns>
        bool SaveCurrentScore(int topRange);

        /// <summary>
        /// Saves the given score to the list of scores and determines if it's within the top number of spots for high scores
        /// </summary>
        /// <param name="score">The score to save to the list</param>
        /// <param name="topRange">The top n scores to check against, 10 means to check the top 10 high scores</param>
        /// <returns>Returns if the given score is within the top n high scores, returns false otherwise.</returns>
        bool SaveScore(HighScore score, int topRange);

        /// <summary>
        /// Saves each of the given scores to the list of scores and determines if they are within the top number of spost for high scores
        /// </summary>
        /// <param name="scores"></param>
        /// <param name="topRange"></param>
        /// <returns>Returns a list of booleans that are true if the given score is whithin the top n high scores, and false otherwise</returns>
        List<bool> SaveScoreRange(ICollection<HighScore> scores, int topRange);

        /// <summary>
        /// Wipes the scores list.
        /// </summary>
        void ResetScoresList();

        /// <summary>
        /// Retrieves the top n high scores
        /// </summary>
        /// <param name="topRange">The number of top scores to return</param>
        /// <returns>Returns a list containing the n high scores</returns>
        List<HighScore> GetHighScores(int topRange);

        /// <summary>
        /// Checks to see if the given score is within the top range of high scores
        /// </summary>
        /// <param name="score">Score to check</param>
        /// <param name="topRange">Number of high scores to check it against</param>
        /// <returns>Returns true if the score is within the given range, returns false otherwise</returns>
        bool CheckScore(int score, int topRange);
    }
}
