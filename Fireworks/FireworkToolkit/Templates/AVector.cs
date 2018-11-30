using FireworkToolkit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Templates
{
    public abstract class AVector : IVector
    {
        #region Properties

        /// <summary>
        /// Represents the components of the vector
        /// </summary>
        protected Dictionary<char, double> components { get; private set; } = new Dictionary<char, double>();

        #endregion

        #region Methods

        public virtual IDictionary<char, double> AllComponents()
        {
            return components;
        }

        public abstract double Angle();

        public abstract IVector Clone();

        public virtual double Magnitude()
        {
            double result = 0;

            foreach(KeyValuePair<char, double> comp in components)
            {
                result += Math.Pow(comp.Value, 2);
            }

            return Math.Sqrt(result);
        }

        #region Operators

        public static AVector operator +(AVector a, AVector b)
        {
            IVector v = a.Clone();

            return new AVector(a.x + b.x, a.y + b.y);
        }

        public static AVector operator -(AVector a, AVector b)
        {
            return new AVector(a.x - b.x, a.y - b.y);
        }

        public static AVector operator *(AVector a, AVector b)
        {
            return new AVector(a.x * b.x, a.y * b.y);
        }

        public static AVector operator /(AVector a, AVector b)
        {
            return new AVector(a.x / b.x, a.y / b.y);
        }

        public static AVector operator +(AVector a, double b)
        {
            return new AVector(a.x + b, a.y + b);
        }

        public static AVector operator -(AVector a, double b)
        {
            return new AVector(a.x - b, a.y - b);
        }

        public static AVector operator -(double a, AVector b)
        {
            return new AVector(a - b.x, a - b.y);
        }

        public static AVector operator *(AVector a, double b)
        {
            return new AVector(a.x * b, a.y * b);
        }

        public static AVector operator /(AVector a, double b)
        {
            return new AVector(a.x / b, a.y / b);
        }

        public static AVector operator /(double a, AVector b)
        {
            return new AVector(a / b.x, a / b.y);
        }

        #endregion

        #endregion
    }
}
