using FireworkToolkit.Graphics.FormsComponents;
using FireworkToolkit.Simulation;
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
            Game.ResetLives();
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
            ICollection<Sprite> copyList = new List<Sprite>();
            foreach (Sprite s in Game.GetSprites())
                copyList.Add(s.Clone());
            SpriteManager wizard = new SpriteManager(copyList);
            wizard.ShowDialog();
            Game.ClearSprites();
            Game.AddSpriteRange(wizard.Sprites);
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

        private void button4_Click(object sender, EventArgs e)
        {
            HighScoreTable table = new HighScoreTable(Game.GetHighScores(Game.NumberOfHighScoresToKeep));
            table.ShowDialog();
            table.Dispose();
        }
    }
}
