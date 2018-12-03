using FireworkToolkit.SpriteGraphics;
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
    public class SpriteFirework2D : ASpriteFirework
    {
        #region Constructors

        public SpriteFirework2D(Vector2D pos, Vector2D vel, Sprite sprite) : base(pos, vel, sprite) { }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the firework by applying the gravity, and then determining if it needs to explode.
        /// </summary>
        public override void Update(int steps = 1)
        {
            if (!Exploded)
            {
                ApplyForce(PhysicsLib.Gravity2D);

                Velocity += Acceleration;
                Position += Velocity;
                Acceleration *= 0;

                if (((Vector2D)Velocity).Y >= 0)
                    Explode();
            }
            else
            {
                while (Busy) ;
                Busy = true;
                lock (Particles)
                {
                    ExplosionAlpha -= ParticleDiminishRate;
                    foreach (AParticle p in Particles)
                    {
                        //Console.WriteLine(p);
                        p.ApplyForce(PhysicsLib.Gravity2D);
                        p.Velocity *= 0.95;
                        p.Update();
                    }
                    if (ExplosionAlpha <= 0)
                        Particles.Clear();
                }
                Busy = false;
            }
        }

        public override void Explode(int qty = 2000)
        {
            Task.Factory.StartNew(() =>
            {
                Exploded = true;

                while (Busy) ;
                Busy = true;
                lock (Particles)
                {

                    List<Tuple<int, int>> coords = sprite.Coordinates;  // Can take a while the first time that it executes
                    qty = (int)(coords.Count * (rng.NextDouble() * 0.2 + 0.1));
                    Particles.Clear();

                    for (int i = 0; i < qty; i++)
                    {
                        // Gets coordinate out of the list of coordinates supplied by the sprite
                        Tuple<int, int> coord;
                        do
                        {
                            int randomT = rng.Next(0, (coords.Count > 0) ? coords.Count - 1 : 0);
                            coord = coords[randomT];
                            if (coord == null)
                                coords.RemoveAt(randomT);

                        } while (coord == null);

                        double targetX = coord.Item1 * sprite.Zoom;
                        double targetY = ExplosionPlacementRadius *
                            coord.Item2 * sprite.Zoom;


                        double angle = (targetX == 0) ? (targetY > 0) ? Math.PI / 2 : (targetY == 0) ? 0 : -1 * Math.PI / 2 : // Coordinate is on the Y-Axis
                            (targetY == 0) ? (targetX < 0) ? Math.PI : 0 : // coordinate is on the X-axis
                            (targetY > 0) ? (targetX > 0) ? Math.Atan(targetY / targetX) : -1 * (Math.PI - Math.Atan(targetY / targetX)) : // Coordinate is above the X-axis
                            (targetX > 0) ? Math.Atan(targetY / targetX) : -1 * (Math.PI - Math.Atan(targetY / targetX));  // Coordinate is below the X-axis
                        double xVel = (rng.NextDouble() * ExplosionMag) * Math.Cos(angle);
                        double yVel = -1 * (rng.NextDouble() * ExplosionMag) * Math.Sin(angle);

                        Particles.Add(new Particle2D(Color,
                        new Vector2D(((Vector2D)Position).X + targetX, ((Vector2D)Position).Y - targetY),
                        new Vector2D(xVel, yVel)));
                    }
                }
                Busy = false;
            });
        }

        public override void Show(System.Drawing.Graphics g)
        {
            if (!Exploded)
                lock(g)
                    g.FillEllipse(Brush,
                        new Rectangle((int)Math.Round(((Vector2D)Position).X), (int)Math.Round(((Vector2D)Position).Y),
                        4, 4));
            else
            {
                base.Show(g);
            }
        }

        #endregion
    }
}
