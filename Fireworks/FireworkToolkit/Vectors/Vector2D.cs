using FireworkToolkit.Interfaces;
using FireworkToolkit.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Vectors
{
    public class Vector2D : AVector
    {
        #region Properties

        /// <summary>
        /// The X component of this vector
        /// </summary>
        public double X { get => components['X']; set => components['X'] = value; }

        /// <summary>
        /// The Y component of this vector
        /// </summary>
        public double Y { get => components['Y']; set => components['Y'] = value; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new 2D vector with the given arguments
        /// </summary>
        /// <param name="x">X-component of the vector</param>
        /// <param name="y">Y-component of the vector</param>
        public Vector2D(double x = 0, double y = 0)
        {
            components.Add('X', x);
            components.Add('Y', y);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calculates the angle for this vector using the arctangent
        /// </summary>
        /// <returns>Returns the angle in Radians of ArcTangent(Y/X)</returns>
        public override double Angle()
        {
            return Math.Atan(Y / X);
        }

        public override IVector Clone()
        {
            return new Vector2D(X, Y);
        }

        #endregion
    }
}
