using FireworkToolkit.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit
{
    public static class PhysicsLib
    {
        public static void Collision(ref AParticle a, ref AParticle b, int time = 1)
        {
            AVector bu = (AVector)b.Velocity.Clone();

            b.Velocity = 2 * a.Mass * a.Velocity / (a.Mass + b.Mass) + (b.Mass - a.Mass) / (a.Mass + b.Mass) * b.Velocity;
            a.Velocity = 2 * b.Mass * bu / (a.Mass + b.Mass) - (b.Mass - a.Mass) / (a.Mass + b.Mass) * a.Velocity;
        }
    }
}
