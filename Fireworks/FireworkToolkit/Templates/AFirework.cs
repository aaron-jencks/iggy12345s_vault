using FireworkToolkit.Graphics;
using FireworkToolkit.Interfaces;
using FireworkToolkit.Vectors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Templates
{
    public abstract class AFirework : AParticle, IFirework
    {
        #region Properties

        /// <summary>
        /// A collection of particles currently in use by the firework
        /// </summary>
        public ICollection<AParticle> Particles { get; private set; } = new List<AParticle>();

        /// <summary>
        /// A flag specifying if the firework has exploded yet, or not
        /// </summary>
        public bool Exploded { get; protected set; } = false;

        /// <summary>
        /// The current alpha of the particles
        /// </summary>
        protected int ExplosionAlpha { get; set; } = 255;

        /// <summary>
        /// Flags whether this object is currently using it's collection, or drawing, etc...
        /// It's doing stuff
        /// </summary>
        public bool Busy { get; protected set; } = false;

        /// <summary>
        /// The magnitude of force that occurs when this firework explodes
        /// </summary>
        public int ExplosionMag { get; set; } = 10;

        /// <summary>
        /// How far from the center of the explosion that the particles are placed when drawing
        /// </summary>
        public int ExplosionPlacementRadius { get; set; } = 5;

        /// <summary>
        /// How quickly the particles diminish per step
        /// </summary>
        public int ParticleDiminishRate { get; set; } = 4;

        #endregion

        #region Constructors

        public AFirework(AVector pos, AVector vel) : base(pos, vel)
        {
            Position = pos;
            Velocity = vel;
        }

        #endregion

        #region Methods

        public virtual bool Done()
        {
            return (Exploded && Particles.Count == 0 && !Busy);
        }

        public virtual void Explode(int qty = 100)
        {
            Exploded = true;
        }

        /// <summary>
        /// Draws the particles of this firework, or just the singular particle for this if
        /// the firework has yet to explode.
        /// </summary>
        /// <param name="g">The graphics object to use to draw</param>
        public override void Show(System.Drawing.Graphics g)
        { 
            while (Busy) ;
            Busy = true;
            lock (Particles)
            {
                foreach (AParticle p in Particles)
                {
                    if (ExplosionAlpha >= 0)
                        p.Color = Color.FromArgb(ExplosionAlpha, p.Color.R, p.Color.G, p.Color.B);
                    
                    p.Show(g);
                }
            }
            Busy = false;
        }

        /// <summary>
        /// Draws the particles of this firework, or just the singular particle for this if
        /// the firework has yet to explode.
        /// </summary>
        /// <param name="g">The graphics object to use to draw</param>
        public async void Show(Queue<GraphicsRequest> requestQueue)
        {
            if (!Exploded)
            {
                Bitmap b = new Bitmap(Diameter, Diameter);
                Show(b);
                requestQueue.Enqueue(new GraphicsRequest(
                    new Point((int)Position.AllComponents()['X'], (int)Position.AllComponents()['Y']), b));
            }
            else
            {
                while (Busy) ;
                Busy = true;
                List<AParticle> particlesClone = null;

                // Clones particles so that we can make this method asynchronous
                await Task.Run(() =>
                {
                    lock (Particles)
                    {
                        particlesClone = new List<AParticle>(Particles.Count);

                        Particles.ToList().ForEach((p) =>
                        {
                            particlesClone.Add((AParticle)p.Clone());
                        });
                    }
                });

                // Represents the top-left and bottom-right coordinates of the particles generated
                Point top = new Point(), bottom = new Point();

                // Finds the top and bottom coordinates
                await Task.Run(() =>
                {
                    int Ttop = 2147483647, Tleft = 2147483647, Tbottom = -2147483648, Tright = -2147483648;

                    particlesClone.ForEach((p) =>
                    {
                        int x = (int)p.Position.AllComponents()['X'], y = (int)p.Position.AllComponents()['Y'];
                        if (y < Ttop)
                            Ttop = y;
                        if (y > Tbottom)
                            Tbottom = y;
                        if (x < Tleft)
                            Tleft = x;
                        if (x > Tright)
                            Tright = x;
                    });

                    top = new Point(Tleft, Ttop);
                    bottom = new Point(Tright, Tbottom);
                });

                // Creates a new bitmap image to be drawn on
                Bitmap sketch = new Bitmap(Math.Abs(bottom.X - top.X), Math.Abs(bottom.Y - top.Y));

                await Task.Run(() =>
                {
                    particlesClone.ForEach((p) =>
                    {
                        if (ExplosionAlpha >= 0)
                            p.Color = Color.FromArgb(ExplosionAlpha, p.Color.R, p.Color.G, p.Color.B);

                        p.Position.AllComponents()['X'] -= top.X;
                        p.Position.AllComponents()['Y'] -= top.Y;

                        p.Show(sketch);
                    });
                });

                requestQueue.Enqueue(new GraphicsRequest(top, sketch));

                Busy = false;
            }
        }

        #endregion
    }
}
