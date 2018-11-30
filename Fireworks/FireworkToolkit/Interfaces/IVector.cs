using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Interfaces
{
    /// <summary>
    /// An interface for representing a vector
    /// </summary>
    public interface IVector
    {
        /// <summary>
        /// Gets the magnitude of the components of the vector
        /// </summary>
        /// <returns>Returns a double value representing ||V||</returns>
        double Magnitude();

        /// <summary>
        /// Gets the angle of the components of the vector
        /// </summary>
        /// <returns>Returns a double value representing the angle of the vector with respect to the normal axis</returns>
        double Angle();

        /// <summary>
        /// Returns a dictionary of all of the components within this vector
        /// </summary>
        /// <returns></returns>
        IDictionary<char, double> AllComponents();

        /// <summary>
        /// Returns a new vector with the same properties of the current vector
        /// </summary>
        /// <returns></returns>
        IVector Clone();
    }
}
