using FireworkToolkit.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Templates
{
    public abstract class AParticle : IParticle
    {
        public abstract void ApplyForce(IVector force);
        public abstract void Show(Graphics g);
        public abstract void Update(int steps = 1);
    }
}
