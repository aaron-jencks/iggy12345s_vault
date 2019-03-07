using FireworkToolkit._2D;
using FireworkToolkit.Interfaces;
using FireworkToolkit.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkUpdateInheritanceTestbench
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rng = new Random();
            List<IFirework> firework = new List<IFirework>()
            {
                new Firework2D(new FireworkToolkit.Vectors.Vector2D(),
                new FireworkToolkit.Vectors.Vector2D(0, rng.Next(-20, -5))),
                new SpriteFirework2D(new FireworkToolkit.Vectors.Vector2D(),
                new FireworkToolkit.Vectors.Vector2D(0, rng.Next(-20, -5)),
                new FireworkToolkit.SpriteGraphics.Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\BongoCat.bmp", 2))
            };
            foreach (IFirework f in firework)
                f.Update();
        }
    }
}
