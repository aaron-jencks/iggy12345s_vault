using FireworkToolkit.Interfaces;
using System;
using System.Collections.Generic;
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
        protected ICollection<IParticle> particles { get; private set; } = new List<IParticle>();

        /// <summary>
        /// A flag specifying if the firework has exploded yet, or not
        /// </summary>
        protected bool Exploded { get; set; } = false;

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

        #region Methods

        public virtual bool Done()
        {
            return (Exploded && particles.Count == 0);
        }

        public virtual void Explode(int qty = 100)
        {
            Exploded = true;
        }

        #endregion
    }
}
