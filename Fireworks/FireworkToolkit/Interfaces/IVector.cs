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
        Dictionary<char, double> AllComponents();

        /// <summary>
        /// Adds a component to the given vector
        /// </summary>
        /// <param name="component">Component to add to the vector</param>
        void AddComponent(KeyValuePair<char, double> component);

        /// <summary>
        /// Removes a component from the given vector
        /// </summary>
        /// <param name="c">Component to remove</param>
        /// <returns>Returns the removed component</returns>
        KeyValuePair<char, double> RemoveComponent(char c);

        /// <summary>
        /// Returns a new vector with the same properties of the current vector
        /// </summary>
        /// <returns></returns>
        IVector Clone();
    }
}
