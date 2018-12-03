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
    public partial class GameMenu : Form
    {
        private FireworkGame Game { get; set; }

        public GameMenu()
        {
            InitializeComponent();
        }

        public GameMenu(FireworkGame game)
        {
            InitializeComponent();

            Game = game;
            Game.Pause();
        }

        /// <summary>
        /// Resumes the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Game.Resume();
            Dispose();
        }

        /// <summary>
        /// Launches a sprite editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            SpriteManager wizard = new SpriteManager(Game.GetSprites());
            wizard.ShowDialog();
            wizard.Dispose();
        }

        /// <summary>
        /// Launches options menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Quits the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            Game.Stop();
            Dispose();
        }

        private void GameMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.Resume();
        }
    }
}
