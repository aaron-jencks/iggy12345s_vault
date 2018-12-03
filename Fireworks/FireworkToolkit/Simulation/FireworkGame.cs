using FireworkToolkit._2D;
using FireworkToolkit.Gaming;
using FireworkToolkit.Interfaces;
using FireworkToolkit.SpriteGraphics;
using FireworkToolkit.Templates;
using FireworkToolkit.Vectors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FireworkToolkit.Simulation
{
    public class FireworkGame : AGame, ISimulation, IFilable
    {
        #region Properties

        /// <summary>
        /// The simulation used by this game.
        /// </summary>
        public FireworksSim Simulation { get; private set; } = new FireworksSim();

        /// <summary>
        /// Boolean indicator indicating if the game has ended.
        /// </summary>
        protected bool isOver { get; set; } = false;

        /// <summary>
        /// Boolean indicator indicating if the game is currently being played.
        /// </summary>
        protected bool isRunning { get; set; } = false;

        /// <summary>
        /// Flags if the game speed previously decreased
        /// </summary>
        private bool prevDecreasedSpeed { get; set; } = false;

        /// <summary>
        /// Flags if the game speed previously increased
        /// </summary>
        private bool prevIncreasedSpeed { get; set; } = true;

        /// <summary>
        /// Flags if the game probability previously decreased
        /// </summary>
        private bool prevDecreasedProb { get; set; } = false;

        /// <summary>
        /// Flags if the game probability previously increased
        /// </summary>
        private bool prevIncreasedProb { get; set; } = true;

        #endregion

        #region Methods

        /// <summary>
        /// Simulates a click by the user onto the screen, causes a firework to explode if the firework is within a certain tolerance
        /// </summary>
        /// <param name="p">The point in space that was clicked</param>
        /// <returns>Returns the number of fireworks that exploded</returns>
        public int Click(Point p)
        {
            if (isRunning)
            {
                int sum = 0;

                AFirework[] list = (AFirework[])Simulation.GetAllFireworks().ToArray().Clone();

                Vector2D v = (Vector2D)list[0].Position;

                foreach (AFirework f in list)
                    if (!f.Exploded && Math.Abs((int)f.Position.AllComponents()['X'] - p.X) < 10 && Math.Abs((int)f.Position.AllComponents()['Y'] - p.Y) < 10)
                    {
                        f.Explode();
                        sum++;
                    }

                return sum;
            }
            return 0;
        }

        public void AddFirework(AFirework firework)
        {
            Simulation.AddFirework(firework);
        }

        public void AddFireworkRange(ICollection<AFirework> fireworks)
        {
            Simulation.AddFireworkRange(fireworks);
        }

        public void AddSprite(Sprite sprite)
        {
            Simulation.AddSprite(sprite);
        }

        public void AddSpriteRange(ICollection<Sprite> sprites)
        {
            Simulation.AddSpriteRange(sprites);
        }

        public void ClearAssets()
        {
            Simulation.ClearAssets();
        }

        public ICollection<IFilable> GetAllAssets()
        {
            return Simulation.GetAllAssets();
        }

        public ICollection<Sprite> GetSprites()
        {
            return Simulation.GetAllSprites();
        }

        public override string GetPlayerName()
        {
            UserInputTextBox wizard = new UserInputTextBox("Please enter your name!");
            wizard.ShowDialog();
            string result = wizard.Value;
            wizard.Dispose();
            return result;
        }

        public override bool IsOver()
        {
            return isOver;
        }

        public override bool IsRunning()
        {
            return isRunning;
        }

        public void LoadAssets(string filename, bool clearOld = true)
        {
            Simulation.LoadAssets(filename, clearOld);
        }

        public void LoadAssets(bool clearOld = true)
        {
            Simulation.LoadAssets(clearOld);
        }

        public void Pause()
        {
            Simulation.Pause();
        }

        public void Resume()
        {
            Simulation.Resume();
        }

        public void SaveAssets(string filename)
        {
            Simulation.SaveAssets(filename);
        }

        public void SaveAssets()
        {
            Simulation.SaveAssets();
        }

        public void Show(System.Drawing.Graphics g)
        {
            Simulation.Show(g);
        }

        public void Simulate(int numSteps = 1)
        {
            Simulation.Simulate(numSteps);
        }

        public void Start()
        {
            isRunning = true;
            Simulation.RefreshRate = 50;
            Simulation.Start();
        }

        public void Stop()
        {
            isRunning = false;
            Simulation.Stop();
        }

        /// <summary>
        /// Increases the score, if the score becomes a multiple of 10, then it decreases the refresh rate
        /// If the score becomes a multiple of 5 then the fireworks spawn probability increases by 1%
        /// </summary>
        /// <param name="amt">The amount to decrease the score by</param>
        /// <returns>Returns the new score</returns>
        public override int IncreaseScore(int amt = 1)
        {
            int prev = Score;
            Score += amt;

            if (!prevIncreasedSpeed && Score % 10 == 0 || prev + 10 - (prev % 10) <= Score)
            {
                Simulation.RefreshRate = (int)(Simulation.RefreshRate * 0.9);
                prevIncreasedSpeed = true;
            }

            if (!prevIncreasedProb && Score % 5 == 0 || prev + 5 - (prev % 5) <= Score)
            {
                Simulation.LaunchProb += (Simulation.LaunchProb + 0.01 > 1) ? 1 : 0.01;
                prevIncreasedProb = true;
            }
            return Score;
        }

        /// <summary>
        /// Decreases the score, if the score becomes a multiple of 10, then it decreases the refresh rate
        /// If the score becomes a multiple of 5 then the fireworks spawn probability decreases by 1%
        /// </summary>
        /// <param name="amt">The amount to decrease the score by</param>
        /// <returns>Returns the new score</returns>
        public override int DecreaseScore(int amt = 1)
        {
            int prev = Score;
            Score -= amt;

            if (prevIncreasedSpeed && Score % 10 == 0 || prev - (prev % 10) >= Score)
            {
                Simulation.RefreshRate = (int)(Simulation.RefreshRate / 0.9);
                prevIncreasedSpeed = false;
            }

            if (prevIncreasedProb && Score % 5 == 0 || prev + 5 - (prev % 5) <= Score)
            {
                Simulation.LaunchProb -= (Simulation.LaunchProb - 0.01 < 0) ? 0 : 0.01;
                prevIncreasedProb = false;
            }

            return Score;
        }

        public XElement GetElement()
        {
            return Simulation.GetElement();
        }

        public void FromElement(XElement e)
        {
            Stop();
            isOver = false;
            ResetScore();
            Simulation.FromElement(e);
        }

        #endregion
    }
}
