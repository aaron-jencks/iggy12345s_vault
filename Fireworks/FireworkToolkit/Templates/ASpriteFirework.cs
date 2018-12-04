using FireworkToolkit.Interfaces;
using FireworkToolkit.SpriteGraphics;
using FireworkToolkit.Vectors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Templates
{
    public abstract class ASpriteFirework : AFirework
    {
        #region Properties

        //public double Zoom { get; set; } = 1;

        protected Sprite sprite { get; private set; }

        #endregion

        #region Constructors

        public ASpriteFirework(Vector2D pos, Vector2D vel, Sprite sprite) : base(pos, vel)
        {
            ParticleDiminishRate = 15;
            //sprite.Zoom = Zoom;
            this.sprite = sprite;
            ExplosionPlacementRadius = 1;
            ExplosionMag = 5;
        }

        #endregion
    }
}
