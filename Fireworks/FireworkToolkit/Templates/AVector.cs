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
            AVector v = (AVector)a.Clone();

            foreach(char c in b.AllComponents().Keys)
            {
                if (v.AllComponents().ContainsKey(c))
                    v.AllComponents()[c] += b.AllComponents()[c];
                else
                    v.AllComponents().Add(new KeyValuePair<char, double>(c, b.AllComponents()[c]));
            }

            return v;
        }

        public static AVector operator -(AVector a, AVector b)
        {
            AVector v = (AVector)a.Clone();

            foreach (char c in b.AllComponents().Keys)
            {
                if (v.AllComponents().ContainsKey(c))
                    v.AllComponents()[c] -= b.AllComponents()[c];
                else
                    v.AllComponents().Add(new KeyValuePair<char, double>(c, -1 * b.AllComponents()[c]));
            }

            return v;
        }

        public static AVector operator *(AVector a, AVector b)
        {
            AVector v = (AVector)a.Clone();

            foreach (char c in b.AllComponents().Keys)
            {
                if (v.AllComponents().ContainsKey(c))
                    v.AllComponents()[c] *= b.AllComponents()[c];
                else
                    v.AllComponents().Add(new KeyValuePair<char, double>(c, 0));
            }

            return v;
        }

        public static AVector operator /(AVector a, AVector b)
        {
            AVector v = (AVector)a.Clone();

            foreach (char c in b.AllComponents().Keys)
            {
                if (v.AllComponents().ContainsKey(c))
                    v.AllComponents()[c] /= b.AllComponents()[c];
                else
                    v.AllComponents().Add(new KeyValuePair<char, double>(c, 0));
            }

            return v;
        }

        public static AVector operator +(AVector a, double b)
        {
            AVector v = (AVector)a.Clone();

            foreach (char c in v.AllComponents().Keys)
            {
                v.AllComponents()[c] += b;
            }

            return v;
        }

        public static AVector operator -(AVector a, double b)
        {
            AVector v = (AVector)a.Clone();

            foreach (char c in v.AllComponents().Keys)
            {
                v.AllComponents()[c] -= b;
            }

            return v;
        }

        public static AVector operator -(double a, AVector b)
        {
            AVector v = (AVector)b.Clone();

            foreach (char c in v.AllComponents().Keys)
            {
                v.AllComponents()[c] = a - v.AllComponents()[c];
            }

            return v;
        }

        public static AVector operator *(AVector a, double b)
        {
            AVector v = (AVector)a.Clone();

            foreach (char c in v.AllComponents().Keys)
            {
                v.AllComponents()[c] *= b;
            }

            return v;
        }

        public static AVector operator *(double b, AVector a)
        {
            AVector v = (AVector)a.Clone();

            foreach (char c in v.AllComponents().Keys)
            {
                v.AllComponents()[c] *= b;
            }

            return v;
        }

        public static AVector operator /(AVector a, double b)
        {
            AVector v = (AVector)a.Clone();

            foreach (char c in v.AllComponents().Keys)
            {
                v.AllComponents()[c] /= b;
            }

            return v;
        }

        public static AVector operator /(double a, AVector b)
        {
            AVector v = (AVector)b.Clone();

            foreach (char c in v.AllComponents().Keys)
            {
                v.AllComponents()[c] = a / v.AllComponents()[c];
            }

            return v;
        }

        #endregion

        #endregion
    }
}
