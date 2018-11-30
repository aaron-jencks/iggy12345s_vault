using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fireworks
{
    public class SpriteFirework : Firework
    {
        #region Properties

        //public double Zoom { get; set; } = 1;

        protected Sprite sprite { get; private set; }

        #endregion

        #region Constructors

        public SpriteFirework(Vector2D pos, Vector2D vel, Sprite sprite) : base(pos, vel)
        {
            particleDiminishRate = 15;
            //sprite.Zoom = Zoom;
            this.sprite = sprite;
            explosionPlacementRadius = 1;
            explosionMag = 5;
        }

        #endregion

        public override void Explode(int qty = 2000)
        {
            Task.Factory.StartNew(() =>
            {
                exploded = true;

                while (busy) ;
                busy = true;
                lock (particles)
                {
                    
                    List<Tuple<int, int>> coords = sprite.Coordinates;  // Can take a while the first time that it executes
                    qty = (int)(coords.Count * 0.4);
                    particles = new List<Particle>(qty);

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
                        double targetY = explosionPlacementRadius *
                            coord.Item2 * sprite.Zoom;


                        double angle = rng.NextDouble() * 2 * Math.PI;
                        double xVel = (rng.NextDouble() * explosionMag) * Math.Cos(angle);
                        double yVel = (rng.NextDouble() * explosionMag) * Math.Sin(angle);

                        particles.Add(new Particle(new Vector2D(firework.Pos.X + targetX, firework.Pos.Y - targetY),
                            new Vector2D(xVel, yVel),
                            new Vector2D(), firework.Color));
                    }
                }
                busy = false;
            });
        }
    }
}
