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

        #endregion
    }
}
