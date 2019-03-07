using FireworkToolkit.Interfaces;
using FireworkToolkit.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Vectors
{
    public class UnlimitedVector : AVector
    {
        public UnlimitedVector(IDictionary<char, double> components = null)
        {
            if(components != null)
                foreach (KeyValuePair<char, double> k in components)
                    AddComponent(k);
        }

        public override double Angle()
        {
            return 0;
        }

        public override IVector Clone()
        {
            return new UnlimitedVector(AllComponents());
        }
    }
}
