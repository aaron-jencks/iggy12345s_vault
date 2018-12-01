using FireworkToolkit.Templates;
using FireworkToolkit.Vectors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit._2D
{
    public class Particle2D : AParticle
    {
        /// <summary>
        /// Creates a new particle with the given parameters
        /// </summary>
        /// <param name="pos">Position vector to use</param>
        /// <param name="vel">Velocity vector to use</param>
        /// <param name="acc">Acceleration vector to use</param>
        public Particle2D(Vector2D pos, Vector2D vel = null, Vector2D acc = null)
        {
            Position = pos;
            Velocity = vel ?? new Vector2D();
            Acceleration = acc ?? new Vector2D();

            Color = Color.FromArgb(255, rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));

            Brush = new SolidBrush(Color);
            Pen = new Pen((Brush)Brush.Clone(), 1);
        }

        /// <summary>
        /// Creates a new particle with the given parameters
        /// </summary>
        /// <param name="pos">Position vector to use</param>
        /// <param name="vel">Velocity vector to use</param>
        /// <param name="acc">Acceleration vector to use</param>
        /// <param name="color">Color to use</param>
        public Particle2D(Color color, Vector2D pos, Vector2D vel = null, Vector2D acc = null)
        {
            Position = pos;
            Velocity = vel ?? new Vector2D();
            Acceleration = acc ?? new Vector2D();
            Color = color;

            Brush = new SolidBrush(Color);
            Pen = new Pen((Brush)Brush.Clone(), 1);
        }

        public override void Show(System.Drawing.Graphics g)
        {
            g.FillEllipse(Brush,
                new Rectangle((int)Math.Round(((Vector2D)Position).X), (int)Math.Round(((Vector2D)Position).Y),
                4, 4));
        }
    }
}
