using FireworkToolkit.Gaming;
using FireworkToolkit.Simulation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighScoreTestbench
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rng = new Random();
            FireworkGame game = new FireworkGame();

            Enumerable.Range(0, 100).ToList().ForEach((o) =>
            {
                game.SaveScore(new HighScore("Aaron", rng.Next()));
            });

            game.GetAllAssets().ToList().ForEach((a) => { Console.WriteLine(a); });

            game.GetHighScores().ForEach((a) =>
            {
                Console.WriteLine(a);
            });

            Console.ReadKey();

        }
    }
}
