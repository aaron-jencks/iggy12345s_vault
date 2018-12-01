using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireworkToolkit.Interfaces;
using FireworkToolkit.Simulation;
using FireworkToolkit.SpriteGraphics;

namespace AssetControlTestbench
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            FireworksSim sim = new FireworksSim();
            Console.WriteLine("Loading Assets");
            sim.AddSprite(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pikachu Outline.bmp", 1));
            sim.AddSprite(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pachirisu Outline.bmp", 4));
            sim.AddSprite(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\LaurenText.bmp", 2));
            sim.AddSprite(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\BongoCat.bmp", 2));
            Console.WriteLine("Saving Assets");
            Console.WriteLine("Assets in the simulation: ");
            foreach (IFilable f in sim.GetAllAssets())
                Console.WriteLine(f);
            sim.SaveAssets();
            Console.WriteLine("Opening Assets");
            sim.LoadAssets();
            Console.WriteLine("Assets in the simulation: ");
            foreach (IFilable f in sim.GetAllAssets())
                Console.WriteLine(f);
            Console.ReadKey();
        }
    }
}
