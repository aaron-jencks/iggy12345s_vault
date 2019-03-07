using FireworkToolkit.Interfaces;
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
            lock(g)
                g.FillEllipse(Brush,
                    new Rectangle((int)Math.Round(((Vector2D)Position).X), (int)Math.Round(((Vector2D)Position).Y),
                    4, 4));
        }

        public override void Show(Bitmap img)
        {
            int radius = Diameter / 2;
            Enumerable.Range(-1 * radius, radius).ToList().ForEach((i) =>
            {
                Enumerable.Range(-1 * radius, radius).ToList().ForEach((j) =>
                {
                    if(i*i + j*j <= radius * radius)
                    {
                        img.SetPixel((int)((Vector2D)Position).X + i, (int)((Vector2D)Position).Y + j, Color);
                    }
                });
            });
        }

        public override IParticle Clone()
        {
            Particle2D temp = new Particle2D(Color.FromArgb(Color.ToArgb()), (Vector2D)Position.Clone(), (Vector2D)Velocity.Clone(), (Vector2D)Acceleration.Clone())
            {
                Diameter = Diameter,
                Mass = Mass
            };
            return temp;
        }
    }
}
