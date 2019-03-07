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
    public class Firework2D : AFirework
    {
        #region Properties

        //public new Vector2D Position { get; set; } = new Vector2D();

        //public new Vector2D Velocity { get; set; } = new Vector2D();

        //public new Vector2D Acceleration { get; set; } = new Vector2D();

        #endregion

        #region Constructors

        public Firework2D(Vector2D pos, Vector2D vel) : base(pos, vel)
        {
            Position = pos;
            Velocity = vel;
            Acceleration = new Vector2D();
        }

        #endregion

        #region Methods

        /*
        public virtual void ApplyForce(Vector2D force)
        {
            Vector2D temp = force; /// Mass;
            Acceleration += temp;
        }
        */

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

        public override void Show(System.Drawing.Graphics g)
        {
            if (!Exploded)
                lock(g)
                    g.FillEllipse(Brush,
                    new Rectangle((int)Math.Round(((Vector2D)Position).X), (int)Math.Round(((Vector2D)Position).Y),
                    8, 8));
            else
            {
                base.Show(g);
            }
        }

        public override void Show(Bitmap img)
        {
            if (!Exploded)
            {
                int radius = Diameter >> 1;
                Enumerable.Range(-1 * radius, radius).ToList().ForEach((i) =>
                {
                    Enumerable.Range(-1 * radius, radius).ToList().ForEach((j) =>
                    {
                        if (i * i + j * j <= radius * radius)
                        {
                            img.SetPixel(i + radius, j + radius, Color);
                        }
                    });
                });
            }
            else
            {
                throw new InvalidOperationException("Can't create a bitmap for a firework when you don't know how big the cloud will be.");
            }
        }

        public override void Explode(int qty = 100)
        {
            base.Explode();

            
            while (Busy) ;
            Busy = true;
            lock (Particles)
            {
                Particles.Clear();

                for (int i = 0; i < qty; i++)
                {
                    double angle = rng.NextDouble() * 2 * Math.PI;
                    double xVel = (rng.NextDouble() * ExplosionMag) * Math.Cos(angle);
                    double yVel = (rng.NextDouble() * ExplosionMag) * Math.Sin(angle);

                    Particles.Add(new Particle2D(Color,
                        (Vector2D)Position.Clone(),
                        new Vector2D(xVel, yVel)));
                    //Console.WriteLine("Added a particle, now there are " + Particles.Count);
                }
            }
            Busy = false;
            
            /*
            Task.Factory.StartNew(() =>
            {
                while (Busy) ;
                Busy = true;
                lock (Particles)
                {
                    Particles.Clear();

                    for (int i = 0; i < qty; i++)
                    {
                        double angle = rng.NextDouble() * 2 * Math.PI;
                        double xVel = (rng.NextDouble() * ExplosionMag) * Math.Cos(angle);
                        double yVel = (rng.NextDouble() * ExplosionMag) * Math.Sin(angle);

                        Particles.Add(new Particle2D(Color, 
                            ((Vector2D)Position.Clone()),
                            new Vector2D(xVel, yVel),
                            ((Vector2D)Acceleration.Clone())));
                        //Console.WriteLine("Added a particle, now there are " + Particles.Count);
                    }
                }
                Busy = false;
            });
            */
        }

        public override IParticle Clone()
        {
            Firework2D temp = new Firework2D((Vector2D)Position.Clone(), (Vector2D)Velocity.Clone());
            temp.Particles.Clear();
            lock(Particles)
                Particles.ToList().ForEach((p) =>
                {
                    temp.Particles.Add((AParticle)p.Clone());
                });
            temp.Mass = Mass;
            temp.ParticleDiminishRate = ParticleDiminishRate;
            temp.Exploded = Exploded;
            temp.Color = Color;
            temp.ExplosionPlacementRadius = ExplosionPlacementRadius;
            temp.ExplosionMag = ExplosionMag;
            temp.Diameter = Diameter;
            temp.ExplosionAlpha = ExplosionAlpha;
            return temp;
        }

        #endregion
    }
}
