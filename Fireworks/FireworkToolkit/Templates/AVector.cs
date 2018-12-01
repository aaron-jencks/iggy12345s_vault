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

        /// <summary>
        /// Creates a new vector with no components
        /// </summary>
        public AVector() { }

        /// <summary>
        /// Creates a new vector with the given vector components
        /// </summary>
        /// <param name="comps">Components to use</param>
        public AVector(Dictionary<char, double> comps)
        {
            components = comps;
        }

        #region Methods

        public virtual Dictionary<char, double> AllComponents()
        {
            /*
            Dictionary<char, double> result = new Dictionary<char, double>(components.Count);

            foreach (KeyValuePair<char, double> p in components)
                result.Add(p.Key, p.Value);
            */

            return components;
        }

        public virtual void AddComponent(KeyValuePair<char, double> comp)
        {
            components.Add(comp.Key, comp.Value);
        }

        public virtual KeyValuePair<char, double> RemoveComponent(char c)
        {
            if (components.ContainsKey(c))
            {
                KeyValuePair<char, double> k = new KeyValuePair<char, double>(c, components[c]);
                components.Remove(c);
                return k;
            }
            return new KeyValuePair<char, double>(c, 0);
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

        public override string ToString()
        {
            string result = "";

            foreach (KeyValuePair<char, double> k in AllComponents())
                result += k.Key + ": " + k.Value + " ";

            return base.ToString() + " " + result;
        }

        #region Operators

        public static AVector operator +(AVector a, AVector b)
        {
            AVector v = (AVector)a.Clone();

            for (int i = 0; i < b.AllComponents().Count; i++)
            {
                char c = b.AllComponents().ElementAt(i).Key;
                if (v.AllComponents().ContainsKey(c))
                    v.AllComponents()[c] += b.AllComponents()[c];
                else
                    v.AllComponents().Add(c, b.AllComponents()[c]);
            }

            return v;
        }

        public static AVector operator -(AVector a, AVector b)
        {
            AVector v = (AVector)a.Clone();

            for (int i = 0; i < b.AllComponents().Count; i++)
            {
                char c = b.AllComponents().ElementAt(i).Key;
                if (v.AllComponents().ContainsKey(c))
                    v.AllComponents()[c] -= b.AllComponents()[c];
                else
                    v.AllComponents().Add(c, -1 * b.AllComponents()[c]);
            }

            return v;
        }

        public static AVector operator *(AVector a, AVector b)
        {
            AVector v = (AVector)a.Clone();

            for (int i = 0; i < b.AllComponents().Count; i++)
            {
                char c = b.AllComponents().ElementAt(i).Key;
                if (v.AllComponents().ContainsKey(c))
                    v.AllComponents()[c] *= b.AllComponents()[c];
                else
                    v.AllComponents().Add(c, 0);
            }

            return v;
        }

        public static AVector operator /(AVector a, AVector b)
        {
            AVector v = (AVector)a.Clone();

            for (int i = 0; i < b.AllComponents().Count; i++)
            {
                char c = b.AllComponents().ElementAt(i).Key;
                if (v.AllComponents().ContainsKey(c))
                    v.AllComponents()[c] /= b.AllComponents()[c];
                else
                    v.AllComponents().Add(c, 0);
            }

            return v;
        }

        public static AVector operator +(AVector a, double b)
        {
            AVector v = (AVector)a.Clone();

            for (int i = 0; i < v.AllComponents().Count; i++)
            {
                char c = v.AllComponents().ElementAt(i).Key;
                v.AllComponents()[c] += b;
            }

            return v;
        }

        public static AVector operator -(AVector a, double b)
        {
            AVector v = (AVector)a.Clone();

            for (int i = 0; i < v.AllComponents().Count; i++)
            {
                char c = v.AllComponents().ElementAt(i).Key;
                v.AllComponents()[c] -= b;
            }

            return v;
        }

        public static AVector operator -(double a, AVector b)
        {
            AVector v = (AVector)b.Clone();

            for (int i = 0; i < v.AllComponents().Count; i++)
            {
                char c = v.AllComponents().ElementAt(i).Key;
                v.AllComponents()[c] = a - v.AllComponents()[c];
            }

            return v;
        }

        public static AVector operator *(AVector a, double b)
        {
            AVector v = (AVector)a.Clone();

            for (int i = 0; i < v.AllComponents().Count; i++)
            {
                char c = v.AllComponents().ElementAt(i).Key;
                v.AllComponents()[c] *= b;
            }

            return v;
        }

        public static AVector operator *(double b, AVector a)
        {
            AVector v = (AVector)a.Clone();

            for (int i = 0; i < v.AllComponents().Count; i++)
            {
                char c = v.AllComponents().ElementAt(i).Key;
                v.AllComponents()[c] *= b;
            }

            return v;
        }

        public static AVector operator /(AVector a, double b)
        {
            AVector v = (AVector)a.Clone();

            for (int i = 0; i < v.AllComponents().Count; i++)
            {
                char c = v.AllComponents().ElementAt(i).Key;
                v.AllComponents()[c] /= b;
            }

            return v;
        }

        public static AVector operator /(double a, AVector b)
        {
            AVector v = (AVector)b.Clone();

            for(int i = 0; i < v.AllComponents().Count; i++)
            {
                char c = v.AllComponents().ElementAt(i).Key;
                v.AllComponents()[c] = a / v.AllComponents()[c];
            }

            return v;
        }

        #endregion

        #endregion
    }
}
