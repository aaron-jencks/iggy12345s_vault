using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Interfaces
{
    /// <summary>
    /// An interface representing a firework
    /// </summary>
    public interface IFirework : IParticle
    {
        /// <summary>
        /// Causes the firework to explode with the given number of particles
        /// </summary>
        /// <param name="qty">The number of particles to generate</param>
        void Explode(int qty = 100);

        /// <summary>
        /// Determines if the firework has already exploded and has no particles remaining on the screen
        /// </summary>
        /// <returns>Returns true if the firework is done, returns false otherwise</returns>
        bool Done();
    }
}
