using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<AF> fs = new List<AF>()
            {
                new FD(),
                new sFD()
            };
            foreach (AF f in fs)
                f.MyMethod();
            Console.ReadKey();
        }
    }

    interface P
    {
        void MyMethod();
    }

    interface F : P
    {

    }

    abstract class AP : P
    {
        public virtual void MyMethod()
        {
            Console.WriteLine("This is P's Method!");
        }
    }

    abstract class AF : AP, F
    {
        public override void MyMethod()
        {
            Console.WriteLine("This is F's Method!");
        }
    }

    class PD : AP
    {

    }

    class FD : AF
    {
        public override void MyMethod()
        {
            Console.WriteLine("This is FD's method");
        }
    }

    class sFD : AF
    {
        public override void MyMethod()
        {
            Console.WriteLine("This is sFD's method");
        }
    }
}
