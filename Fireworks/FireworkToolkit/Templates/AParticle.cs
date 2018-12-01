using FireworkToolkit.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Templates
{
    public abstract class AParticle : IParticle
    {
        #region Properties

        #region Physics

        /// <summary>
        /// The particle's current coordinate position
        /// </summary>
        public AVector Position { get; set; }

        /// <summary>
        /// The particle's current velocity vector
        /// </summary>
        public AVector Velocity { get; set; }

        /// <summary>
        /// The particle's current acceleration vector
        /// </summary>
        public AVector Acceleration { get; set; }

        /// <summary>
        /// The mass of the particle in Kg, default is 5 grams.
        /// Helps to determine how long acceleration lasts
        /// </summary>
        public double Mass { get; set; } = 0.005;

        #endregion

        /// <summary>
        /// The color that the particle uses when the Show() method is invoked
        /// </summary>
        public Color color { get; set; } = new Color();

        /// <summary>
        /// The brush used when the Show() method is invoked
        /// </summary>
        public Brush brush { get; protected set; }

        /// <summary>
        /// The pen used when the Show() method is invoked
        /// </summary>
        public Pen pen { get; protected set; }

        /// <summary>
        /// The random number generator used by this instance
        /// </summary>
        protected Random rng { get; private set; } = new Random();

        #endregion

        #region Methods

        public virtual void ApplyForce(AVector force)
        {
            Acceleration += force / Mass;
        }

        /// <summary>
        /// Draws the particle on the canvas supplied
        /// </summary>
        /// <param name="g">Graphics object to use</param>
        public virtual void Show(Graphics g)
        {
            g.FillEllipse(brush,
                new Rectangle((int)Math.Round(Position.X), (int)Math.Round(Position.Y),
                4, 4));
        }

        /// <summary>
        /// Steps the particle through n seconds of movement, resets the acceleration after 1 second.
        /// </summary>
        /// <param name="steps">Number of seconds to do.</param>
        public virtual void Update(int steps = 1)
        {
            for (int i = 0; i < steps; i++)
            {
                Velocity += Acceleration;
                Position += Velocity;
                Acceleration *= 0;
            }
        }

        #endregion
    }
}
