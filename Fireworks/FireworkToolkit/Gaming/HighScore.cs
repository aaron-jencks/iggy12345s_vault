using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Gaming
{
    public class HighScore : IComparable<HighScore>
    {
        #region Properties

        public string Name { get; set; } = "";

        public int Score { get; set; } = 0;

        #endregion

        public HighScore(string name = "", int score = 0)
        {
            Name = name;
            Score = score;
        }

        public int CompareTo(HighScore other)
        {
            return Score.CompareTo(other.Score);
        }
    }
}
