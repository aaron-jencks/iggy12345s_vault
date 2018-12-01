using FireworkToolkit.SpriteGraphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FireworkToolkit.Graphics.FormsComponents
{
    public partial class SpriteManager : Form
    {
        #region Properties

        /// <summary>
        /// The list of sprites currently in use by the manager
        /// </summary>
        public ICollection<Sprite> Sprites { get; private set; } = new List<Sprite>();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new sprite manager with an empty list of sprites
        /// </summary>
        public SpriteManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new sprite manager with the given initial collection of sprites
        /// </summary>
        /// <param name="sprites"></param>
        public SpriteManager(ICollection<Sprite> sprites)
        {
            InitializeComponent();
            Sprites = sprites;
        }

        #endregion
    }
}
