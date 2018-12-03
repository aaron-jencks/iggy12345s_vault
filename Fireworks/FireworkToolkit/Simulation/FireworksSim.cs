using FireworkToolkit._2D;
using FireworkToolkit.Interfaces;
using FireworkToolkit.SpriteGraphics;
using FireworkToolkit.Templates;
using FireworkToolkit.Vectors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace FireworkToolkit.Simulation
{
    /// <summary>
    /// A class used to embody a fireworks simulation
    /// </summary>
    public class FireworksSim : ISimulation, IFilable
    {
        #region Events

        #region Update Event

        public delegate void UpdateEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Triggered every time that the Simulate method is called
        /// </summary>
        public event UpdateEventHandler UpdateEvent;
        protected void OnUpdateEvent()
        {
            UpdateEvent?.Invoke(this, new EventArgs());
        }

        #endregion

        #endregion

        #region Properties

        #region Tuning

        /// <summary>
        /// The collection of fireworks that are currently in rotation
        /// </summary>
        protected List<AFirework> Fireworks { get; private set; } = new List<AFirework>();

        /// <summary>
        /// The list of sprites currently loaded into the simulation
        /// </summary>
        protected List<Sprite> Sprites { get; private set; } = new List<Sprite>();

        /// <summary>
        /// How often the updating thread will update all of the fireworks in ms
        /// </summary>
        public int RefreshRate { get; set; } = 33;   // refresh delay in ms

        /// <summary>
        /// The probability in % (0-1) of how likely that a firework will be generated is.
        /// </summary>
        public double LaunchProb { get; set; } = 0.05;

        /// <summary>
        /// The minimum velocity that a firework can have initially
        /// This is set automatically
        /// In units of pixels per update
        /// </summary>
        public int MinVel { get; protected set; } = -5;

        /// <summary>
        /// The maximum velocity that a firework can have initially
        /// This is set automatically
        /// In units of pixels per update
        /// </summary>
        public int MaxVel { get; protected set; } = -20;

        /// <summary>
        /// Pauses the simulation
        /// </summary>
        public bool isPaused { get; set; } = false;

        /// <summary>
        /// Determines how long the metaphorical box for the simulation is
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Determines how high the metaphorical ceiling is for the simulation
        /// </summary>
        public int Height { get; set; }

        #endregion

        /// <summary>
        /// Thread that the painter will run on
        /// </summary>
        private Thread painter;

        /// <summary>
        /// Random number generator used in generating the fireworks
        /// </summary>
        private Random rng = new Random();

        /// <summary>
        /// Flags if the system can draw particles
        /// </summary>
        private bool canDraw = false;

        /// <summary>
        /// Flags when the system needs to shutdown
        /// </summary>
        private bool isExitting = false;

        /// <summary>
        /// Flags when the system is currently drawing
        /// </summary>
        private bool isDrawing = false;

        #endregion

        #region Methods

        #region Flow Control

        public virtual void Simulate(int numSteps = 1)
        {
            lock (Fireworks)
            {
                for (int j = 0; j < numSteps; j++)
                {
                    if (rng.NextDouble() <= LaunchProb)
                        lock (Fireworks)
                        {
                            double r = rng.NextDouble();

                            if (r > 0.75)
                                Fireworks.Add(new Firework2D(
                                    new Vector2D(rng.Next(Width), Height),
                                    new Vector2D(0, rng.Next(MaxVel, MinVel))));
                            else if (r > 0.1)
                            {
                                if (Sprites.Count > 0)
                                    Fireworks.Add(new SpriteFirework2D(
                                        new Vector2D(rng.Next(Width), Height),
                                        new Vector2D(0, rng.Next(MaxVel, MinVel)),
                                        Sprites[rng.Next(0, Sprites.Count)]));
                                else
                                    Fireworks.Add(new Firework2D(
                                        new Vector2D(rng.Next(Width), Height),
                                        new Vector2D(0, rng.Next(MaxVel, MinVel))));
                            }

                        }

                    for (int i = Fireworks.Count - 1; i >= 0; i--)
                    {
                        if (Fireworks[i].Done())
                            Fireworks.RemoveAt(i);
                        else
                            Fireworks[i].Update();
                    }
                }
            }

            OnUpdateEvent();
        }

        public virtual void Start()
        {
            if (painter == null || !painter.IsAlive)
            {
                isExitting = false;
                painter = new Thread(ExecutionModule);
                painter.Start();
            }
        }

        public virtual void Stop()
        {
            if (painter != null && painter.IsAlive)
            {
                isExitting = true;
                while (painter.IsAlive) ;
            }
        }

        public virtual void Pause()
        {
            isPaused = true;
        }

        public virtual void Resume()
        {
            isPaused = false;
        }

        #endregion

        #region Asset Control

        public virtual ICollection<IFilable> GetAllAssets()
        {
            return Sprites.Cast<IFilable>().ToList();
        }

        public virtual ICollection<Sprite> GetAllSprites()
        {
            return Sprites;
        }

        public virtual ICollection<AFirework> GetAllFireworks()
        {
            return Fireworks;
        }

        public virtual void ClearAssets()
        {
            Sprites.Clear();
        }

        public virtual void AddFirework(AFirework firework)
        {
            Fireworks.Add(firework);
        }

        public virtual void AddFireworkRange(ICollection<AFirework> fireworks)
        {
            Fireworks.AddRange(fireworks);
        }

        public virtual void ClearSprites()
        {
            Sprites.Clear();
        }

        public virtual void AddSprite(Sprite sprite)
        {
            Sprites.Add(sprite);
        }

        public virtual void AddSpriteRange(ICollection<Sprite> sprites)
        {
            Sprites.AddRange(sprites);
        }

        #region File IO

        public virtual void SaveAssets(string filename)
        {
            XElement doc = new XElement("root");
            foreach (IFilable f in GetAllAssets())
                doc.Add(f.GetElement());
            doc.Save(filename);
        }

        [STAThread]
        public virtual void SaveAssets()
        {
            SaveFileDialog wizard = new SaveFileDialog();
            wizard.AddExtension = true;
            wizard.Filter = "Xml | *.xml";
            wizard.DefaultExt = "xml";
            wizard.Title = "Select a file to save to";
            wizard.ShowDialog();

            if(wizard.FileName != "")
                SaveAssets(wizard.FileName);

            wizard.Dispose();
        }

        public virtual void LoadAssets(string filename, bool clearOld = true)
        {
            if (clearOld)
                ClearAssets();

            XElement doc = XElement.Load(filename);
            foreach(XElement child in doc.Elements())
                switch(child.Name.ToString())
                {
                    case "Sprite":
                        Sprite s = new Sprite();
                        s.FromElement(child);
                        AddSprite(s);
                        break;
                }
        }

        [STAThread]
        public virtual void LoadAssets(bool clearOld = true)
        {
            OpenFileDialog wizard = new OpenFileDialog();
            wizard.Filter = "Xml | *.xml";
            wizard.Title = "Select an attribute file to open";
            wizard.RestoreDirectory = true;
            wizard.ShowDialog();

            if (wizard.FileName != "")
                LoadAssets(wizard.FileName, clearOld);

            wizard.Dispose();
        }

        public virtual XElement GetElement()
        {
            XElement result = new XElement("FireworksSim",
                new XAttribute("RefreshRate", RefreshRate),
                new XAttribute("LaunchProb", LaunchProb),
                new XAttribute("MinVel", MinVel),
                new XAttribute("MaxVel", MaxVel),
                new XAttribute("Width", Width),
                new XAttribute("Height", Height));

            foreach (IFilable f in GetAllAssets())
                result.Add(f.GetElement());

            return result;
        }

        public virtual void FromElement(XElement e)
        {
            // Wipes this object if it has already begun being used.
            Stop();
            ClearAssets();

            RefreshRate = (int)e.Attribute("RefreshRate");
            LaunchProb = (double)e.Attribute("LaunchProb");
            MinVel = (int)e.Attribute("MinVel");
            MaxVel = (int)e.Attribute("MaxVel");
            Width = (int)e.Attribute("Width");
            Height = (int)e.Attribute("Height");
            foreach (XElement c in e.Elements())
                if (c.Name == "Sprite")
                {
                    Sprite s = new Sprite();
                    s.FromElement(c);
                    AddSprite(s);
                }
            
        }

        #endregion

        #endregion

        public virtual void Show(System.Drawing.Graphics g)
        {
            lock (Fireworks)
            {
                int fCount = Fireworks.Count, pCount = Environment.ProcessorCount;

                List<Thread> tasks = new List<Thread>((fCount < pCount) ? fCount : pCount);

                int adjustedIncrement = (fCount < pCount) ? 1 : fCount / pCount;

                for (int i = 0; i < ((fCount < pCount) ? fCount : pCount); i++)
                {
                    Thread t = new Thread((object p) =>
                    {
                        int f = (int)p;
                        for (int j = f * adjustedIncrement; j < (f + 1) * adjustedIncrement; j++)
                        {
                            Fireworks[j].Show(g);
                        }
                    });

                    t.Start(i);

                    tasks.Add(t);
                }

                foreach (Thread t in tasks)
                    while (t.IsAlive) ;
            }
        }

        /// <summary>
        /// Controls painting the particles on the screen, time updates, etc...
        /// Causes frames to happen.
        /// </summary>
        private void ExecutionModule()
        {
            while (!isExitting)
            {
                Thread.Sleep(RefreshRate);
                if (!isPaused)
                    Simulate();
            }
        }

        public override string ToString()
        {
            string result = base.ToString() + "\nAssets:\n";
            foreach (IFilable f in GetAllAssets())
                result += f.ToString() + "\n";
            return result;
        }

        #endregion
    }
}
