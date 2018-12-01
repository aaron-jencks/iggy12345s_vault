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

        private Color pcolor;
        /// <summary>
        /// The color that the particle uses when the Show() method is invoked
        /// </summary>
        public Color Color { get => pcolor; set => setColor(value); }

        /// <summary>
        /// The brush used when the Show() method is invoked
        /// </summary>
        public Brush Brush { get; protected set; }

        /// <summary>
        /// The pen used when the Show() method is invoked
        /// </summary>
        public Pen Pen { get; protected set; }

        /// <summary>
        /// The random number generator used by this instance
        /// </summary>
        protected Random rng { get; private set; } = new Random();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new particle with the given parameters
        /// </summary>
        /// <param name="degree">the degree of the vector to use</param>
        public AParticle(int degree = 2)
        {
            List<char> components = new List<char>(degree);
            for (int i = 1; i <= degree; i++)
                components.Add((char)i);

            Position = PhysicsLib.GetZeroVector(components.ToArray());
            Velocity = PhysicsLib.GetZeroVector(components.ToArray());
            Acceleration = PhysicsLib.GetZeroVector(components.ToArray());

            Color = Color.FromArgb(255, rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));

            Brush = new SolidBrush(Color);
            Pen = new Pen((SolidBrush)(Brush.Clone()), 1);
        }

        /// <summary>
        /// Creates a new particle with the given parameters
        /// </summary>
        /// <param name="color">color to use</param>
        /// <param name="degree">the degree of the vector to use</param>
        public AParticle(Color color, int degree = 2)
        {
            List<char> components = new List<char>(degree);
            for (int i = 1; i <= degree; i++)
                components.Add((char)i);

            Position = PhysicsLib.GetZeroVector(components.ToArray());
            Velocity = PhysicsLib.GetZeroVector(components.ToArray());
            Acceleration = PhysicsLib.GetZeroVector(components.ToArray());
            Color = color;
            Brush = new SolidBrush(Color);
            Pen = new Pen((SolidBrush)(Brush.Clone()), 1);
        }

        /// <summary>
        /// Creates a new particle with the given parameters
        /// </summary>
        /// <param name="pos">Position vector to use</param>
        /// <param name="vel">Velocity vector to use</param>
        /// <param name="acc">Acceleration vector to use</param>
        public AParticle(AVector pos, AVector vel = null, AVector acc = null)
        {
            Position = pos;
            Velocity = vel ?? PhysicsLib.GetZeroVector(pos.AllComponents().Keys.ToArray());
            Acceleration = acc ?? PhysicsLib.GetZeroVector(pos.AllComponents().Keys.ToArray());

            Color = Color.FromArgb(255, rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));

            Brush = new SolidBrush(Color);
            Pen = new Pen((SolidBrush)(Brush.Clone()), 1);
        }

        /// <summary>
        /// Creates a new particle with the given parameters
        /// </summary>
        /// <param name="pos">Position vector to use</param>
        /// <param name="vel">Velocity vector to use</param>
        /// <param name="acc">Acceleration vector to use</param>
        /// <param name="color">Color to use</param>
        public AParticle(Color color, AVector pos, AVector vel = null, AVector acc = null)
        {
            Position = pos;
            Velocity = vel ?? PhysicsLib.GetZeroVector(pos.AllComponents().Keys.ToArray());
            Acceleration = acc ?? PhysicsLib.GetZeroVector(pos.AllComponents().Keys.ToArray());
            Color = color;

            Brush = new SolidBrush(Color);
            Pen = new Pen((SolidBrush)(Brush.Clone()), 1);
        }

        #endregion

        #region Methods

        private void setColor(Color c)
        {
            pcolor = c;
            Brush = new SolidBrush(c);
            Pen = new Pen(Brush);
        }

        public virtual void ApplyForce(AVector force)
        {
            AVector temp = force; /// Mass;
            Acceleration += temp;
        }

        public abstract void Show(Graphics g);

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

        public override string ToString()
        {
            return base.ToString() + " Position: " + Position + " Velocity: " + Velocity + " Acceleration: " + Acceleration;
        }

        #endregion
    }
}
