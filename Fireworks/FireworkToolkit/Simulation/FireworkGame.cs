using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Simulation
{
    public class FireworkGame : FireworksSim
    {
        #region Properties

        /// <summary>
        /// Score for the current game
        /// </summary>
        public int Score { get; set; } = 0;

        #endregion
    }
}
