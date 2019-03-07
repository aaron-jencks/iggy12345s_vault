using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireworkToolkit.Gaming;
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
            FireworkGame sim = new FireworkGame();
            Console.WriteLine("Loading Assets");
            sim.AddSprite(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pachirisu Outline.bmp", 4));
            sim.AddSprite(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\LaurenText.bmp", 2));
            sim.AddSprite(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\BongoCat.bmp", 2));
            sim.SaveScoreRange(new List<HighScore>()
            {
                new HighScore("Aaron", 100),
                new HighScore("Fuck you", 200),
                new HighScore("Master Lauren", 2000)
            });
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
