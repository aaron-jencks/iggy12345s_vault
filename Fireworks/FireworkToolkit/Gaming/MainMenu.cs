using FireworkToolkit.Graphics.FormsComponents;
using FireworkToolkit.Simulation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FireworkToolkit.Gaming
{
    public partial class MainMenu : Form
    {
        private FireworkGame Game { get; set; }

        public MainMenu()
        {
            InitializeComponent();
        }

        public MainMenu(FireworkGame game)
        {
            InitializeComponent();

            Game = game;
        }

        /// <summary>
        /// Creates a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Game.ResetScore();
            Game.Start();
            Dispose();
        }

        /// <summary>
        /// Launches the sprite editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            SpriteManager wizard = new SpriteManager(Game.GetSprites());
            wizard.ShowDialog();
            Game.ClearSprites();
            wizard.Dispose();
        }

        /// <summary>
        /// Quits the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
