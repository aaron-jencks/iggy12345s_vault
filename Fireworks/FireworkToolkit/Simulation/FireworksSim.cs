using FireworkToolkit._2D;
using FireworkToolkit.SpriteGraphics;
using FireworkToolkit.Templates;
using FireworkToolkit.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FireworkToolkit.Simulation
{
    /// <summary>
    /// A class used to embody a fireworks simulation
    /// </summary>
    public class FireworksSim
    {
        #region Properties

        #region Tuning

        /// <summary>
        /// The collection of fireworks that are currently in rotation
        /// </summary>
        protected List<AFirework> Fireworks { get; private set; } = new List<AFirework>();

        /// <summary>
        /// The list of sprites currently loaded into the simulation
        /// </summary>
        protected List<Sprite> Sprites { get; private set; } = new List<Sprite>();

        /// <summary>
        /// How often the updating thread will update all of the fireworks in ms
        /// </summary>
        public static int RefreshRate { get; set; } = 10;   // refresh delay in ms

        /// <summary>
        /// The probability in % (0-1) of how likely that a firework will be generated is.
        /// </summary>
        public static double LaunchProb { get; set; } = 0.05;

        /// <summary>
        /// The minimum velocity that a firework can have initially
        /// This is set automatically
        /// In units of pixels per update
        /// </summary>
        public static int MinVel { get; protected set; } = -5;

        /// <summary>
        /// The maximum velocity that a firework can have initially
        /// This is set automatically
        /// In units of pixels per update
        /// </summary>
        public static int MaxVel { get; protected set; } = -20;

        /// <summary>
        /// Pauses the simulation
        /// </summary>
        public bool isPaused { get; set; } = false;

        /// <summary>
        /// Determines how long the metaphorical box for the simulation is
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Determines how high the metaphorical ceiling is for the simulation
        /// </summary>
        public int Height { get; set; }

        #endregion

        /// <summary>
        /// Thread that the painter will run on
        /// </summary>
        private Thread painter;

        /// <summary>
        /// Random number generator used in generating the fireworks
        /// </summary>
        private Random rng = new Random();

        /// <summary>
        /// Flags if the system can draw particles
        /// </summary>
        private bool canDraw = false;

        /// <summary>
        /// Flags when the system needs to shutdown
        /// </summary>
        private bool isExitting = false;

        /// <summary>
        /// Flags when the system is currently drawing
        /// </summary>
        private bool isDrawing = false;

        #endregion

        #region Constructors

        public FireworksSim(int width, int height)
        {
            painter = new Thread(ExecutionModule);
            painter.Start();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates all of the particles in the collection
        /// </summary>
        private void UpdateParticles()
        {
            lock (Fireworks)
            {
                for (int i = Fireworks.Count - 1; i >= 0; i--)
                {
                    if (Fireworks[i].Done())
                        Fireworks.RemoveAt(i);
                    else
                        Fireworks[i].Update();
                }
            }
        }

        /// <summary>
        /// Controls painting the particles on the screen, time updates, etc...
        /// Causes frames to happen.
        /// </summary>
        private void ExecutionModule()
        {
            while (!isExitting)
            {

                Thread.Sleep(RefreshRate);

                if (rng.NextDouble() <= LaunchProb)
                    lock (Fireworks)
                    {
                        double r = rng.NextDouble();

                        if (r > 0.75)
                            Fireworks.Add(new Firework2D(
                                new Vector2D(rng.Next(Width), Height),
                                new Vector2D(0, rng.Next(MaxVel, MinVel))));
                        else if (r > 0.1)
                            Fireworks.Add(new SpriteFirework2D(
                                new Vector2D(rng.Next(Width), Height),
                                new Vector2D(0, rng.Next(MaxVel, MinVel)),
                                Sprites[rng.Next(0, Sprites.Count)]));

                    }


                UpdateParticles();
            }
        }

        #endregion
    }
}
