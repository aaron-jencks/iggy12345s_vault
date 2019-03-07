using FireworkToolkit;
using FireworkToolkit._2D;
using FireworkToolkit.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkInheritanceTestbench
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Random rng = new Random();
                Firework2D fire = new Firework2D(
                    new FireworkToolkit.Vectors.Vector2D(rng.Next(), rng.Next()),
                    new FireworkToolkit.Vectors.Vector2D(rng.Next(), rng.Next(-20, -5)));
                Console.WriteLine(fire);
                while (!fire.Done())
                {
                    fire.Update();
                    //((Vector2D)fire.Acceleration).Y = 0.2;
                    Console.WriteLine(fire);

                }
                Console.WriteLine("There are " + fire.Particles.Count + " Particles to write");
                foreach (Particle2D p in fire.Particles)
                    Console.WriteLine(p);
                Console.ReadKey();
            }
        }
    }
}
