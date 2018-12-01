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

        /// <summary>
        /// Adds a component to the vector unless it's 'X' or 'Y'
        /// </summary>
        /// <param name="comp">component to add</param>
        public override void AddComponent(KeyValuePair<char, double> comp)
        {
            if(comp.Key != 'X' && comp.Key != 'Y')
                base.AddComponent(comp);
        }

        /// <summary>
        /// Removes a component from the vector, unless the component is 'X' or 'Y'
        /// </summary>
        /// <param name="c">Component to add</param>
        /// <returns>Returns the removed component</returns>
        public override KeyValuePair<char, double> RemoveComponent(char c)
        {
            if(c != 'X' && c != 'Y')
                return base.RemoveComponent(c);
            return new KeyValuePair<char, double>(c, 0);
        }

        #region Operators

        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            Vector2D v = (Vector2D)a.Clone();

            v.X += b.X;
            v.Y += b.Y;

            return v;
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            Vector2D v = (Vector2D)a.Clone();

            v.X -= b.X;
            v.Y -= b.Y;

            return v;
        }

        public static Vector2D operator *(Vector2D a, Vector2D b)
        {
            Vector2D v = (Vector2D)a.Clone();

            v.X *= b.X;
            v.Y *= b.Y;

            return v;
        }

        public static Vector2D operator /(Vector2D a, Vector2D b)
        {
            Vector2D v = (Vector2D)a.Clone();

            v.X /= b.X;
            v.Y /= b.Y;

            return v;
        }

        public static Vector2D operator +(Vector2D a, double b)
        {
            Vector2D v = (Vector2D)a.Clone();

            foreach (char c in v.AllComponents().Keys)
            {
                v.AllComponents()[c] += b;
            }

            return v;
        }

        public static Vector2D operator -(Vector2D a, double b)
        {
            Vector2D v = (Vector2D)a.Clone();

            foreach (char c in v.AllComponents().Keys)
            {
                v.AllComponents()[c] -= b;
            }

            return v;
        }

        public static Vector2D operator -(double a, Vector2D b)
        {
            Vector2D v = (Vector2D)b.Clone();

            foreach (char c in v.AllComponents().Keys)
            {
                v.AllComponents()[c] = a - v.AllComponents()[c];
            }

            return v;
        }

        public static Vector2D operator *(Vector2D a, double b)
        {
            Vector2D v = (Vector2D)a.Clone();

            foreach (char c in v.AllComponents().Keys)
            {
                v.AllComponents()[c] *= b;
            }

            return v;
        }

        public static Vector2D operator *(double b, Vector2D a)
        {
            Vector2D v = (Vector2D)a.Clone();

            foreach (char c in v.AllComponents().Keys)
            {
                v.AllComponents()[c] *= b;
            }

            return v;
        }

        public static Vector2D operator /(Vector2D a, double b)
        {
            Vector2D v = (Vector2D)a.Clone();

            foreach (char c in v.AllComponents().Keys)
            {
                v.AllComponents()[c] /= b;
            }

            return v;
        }

        public static Vector2D operator /(double a, Vector2D b)
        {
            Vector2D v = (Vector2D)b.Clone();

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
