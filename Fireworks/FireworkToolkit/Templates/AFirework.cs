using FireworkToolkit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Templates
{
    public abstract class AFirework : IFirework
    {
        public abstract bool Done();
        public abstract void Explode(int qty = 100);
    }
}
