using FireworkToolkit.Templates;
using FireworkToolkit.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit
{
    public static class PhysicsLib
    {
        public static Vector2D Gravity2D = new Vector2D(0, 0.2);

        public static void Collision(ref AParticle a, ref AParticle b, int time = 1)
        {
            AVector bu = (AVector)b.Velocity.Clone();

            b.Velocity = 2 * a.Mass * a.Velocity / (a.Mass + b.Mass) + (b.Mass - a.Mass) / (a.Mass + b.Mass) * b.Velocity;
            a.Velocity = 2 * b.Mass * bu / (a.Mass + b.Mass) - (b.Mass - a.Mass) / (a.Mass + b.Mass) * a.Velocity;
        }

        public static AVector GetZeroVector(char[] components)
        {
            Dictionary<char, double> pairs = new Dictionary<char, double>(components.Length);
            foreach (char c in components)
                pairs.Add(c, 0);
            return new UnlimitedVector(pairs); 
        }

        public static int GetLargestStartingVelocity(int windowHeight, int numSteps = 100)
        {
            double g = Gravity2D.Y;
            return (int)((windowHeight - sumPrev(numSteps) * g) / numSteps);

            int sumPrev(int interval)
            {
                int sum = 0;
                for (int i = 0; i <= interval; i++)
                    sum += i;
                return sum;
            }
        }
    }
}
