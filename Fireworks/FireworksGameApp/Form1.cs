using FireworkToolkit.Gaming;
using FireworkToolkit.Graphics;
using FireworkToolkit.Simulation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FireworksGameApp
{
    public partial class Form1 : Form
    {
        #region Properties

        public FireworkGame Game { get; private set; } = new FireworkGame();
        public bool DrawFade { get; set; } = false;

        /// <summary>
        /// Random number generator used by this form
        /// </summary>
        protected Random rng { get; private set; } = new Random();

        private GraphicsLib graphicsLib { get; set; }

        private bool canDraw = false;
        private bool isDrawing = false;

        #endregion

        public Form1()
        {
            InitializeComponent();

            Setup();

            this.Disposed += Form1_Disposed;

            if (!Game.IsRunning())
            {
                // Tries to load in a previous environment if one exists
                if (File.Exists(Environment.CurrentDirectory + "\\Environment.xml"))
                {
                    Console.WriteLine("Found an environment file, trying to load.");
                    XElement doc = XElement.Load(Environment.CurrentDirectory + "\\Environment.xml");
                    if (doc.HasElements)
                        if (doc.Name == "FireworkGameConfig" && doc.Attribute("Ver").Value == "1.0.1")
                            Game.FromElement(doc.Element("FireworkGame"));
                }
                else
                    Console.WriteLine("Did not find an environment file, will create one this time.");

                FireworkToolkit.Gaming.MainMenu menu = new FireworkToolkit.Gaming.MainMenu(Game);
                menu.ShowDialog();
                if (!Game.IsRunning())
                    Dispose();
            }
        }

        #region Methods

        private void Form1_Load(object sender, EventArgs e)
        {

            Game.Simulation.Width = Width;
            Game.Simulation.Height = Height;

            graphicsLib = new GraphicsLib(Width, Height);

            while (isDrawing) ;

            canDraw = false;

            canvasBox.Width = Width;
            canvasBox.Height = Height;

            lock (canvasBox.Image)
                canvasBox.Image = graphicsLib.Image; //new Bitmap(Width, Height);

            canvasBox.Invalidate();

            canDraw = true;
        }

        /// <summary>
        /// Sets up the canvasbox
        /// </summary>
        protected void Setup()
        {
            // Sets up the background color of the canvas.
            canvasBox.Image = new Bitmap(Width, Height);
            canvasBox.BackColor = Color.Black;
            canvasBox.Invalidate();

            Game.Simulation.UpdateEvent += Simulation_UpdateEvent;

            //ExecutionModule();
        }

        /// <summary>
        /// Triggered when the simulation updates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Simulation_UpdateEvent(object sender, EventArgs e)
        {
            canvasBox.Invalidate();
        }

        /// <summary>
        /// Draws all of the particle onto the screen
        /// </summary>
        protected void DrawParticles()
        {
            if (canDraw && !Game.Simulation.isPaused)
            {
                isDrawing = true;
                /*
                lock (canvasBox.Image)
                {
                    //Image i = (Image)canvasBox.Image.Clone();
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(canvasBox.Image))
                    {
                        g.Clear(Color.Black);
                        //g.FillRectangle(new SolidBrush(Color.Black), g.ClipBounds);

                        if (DrawFade)
                        {
                            Image image = (Image)canvasBox.Image.Clone();

                            Bitmap bmp = new Bitmap(image.Width, image.Height);

                            //create a color matrix object  
                            ColorMatrix matrix = new ColorMatrix();

                            //set the opacity  
                            matrix.Matrix33 *= 0.95f;
                            if (matrix.Matrix33 < 0.75)
                                matrix.Matrix33 = 0;

                            //create image attributes  
                            ImageAttributes attributes = new ImageAttributes();

                            //set the color(opacity) of the image  
                            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            //now draw the image  
                            g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                        }

                        g.DrawString("Score: " + Game.Score + " Lives: " + Game.Lives, SystemFonts.DefaultFont, new SolidBrush(Color.White), new Point(0, 0));


                        Game.Simulation.Show(g);

                        g.Dispose();
                    }
                    //canvasBox.Image = i;
                    canvasBox.Invalidate();
                }
                */
                Game.Show(graphicsLib.requests);
            }
            isDrawing = false;
        }

        private void canvasBox_Paint(object sender, PaintEventArgs e)
        {
            DrawParticles();
        }

        #endregion

        private void Form1_Disposed(object sender, EventArgs e)
        {
            Game.Stop();
            graphicsLib.Dispose();

            // Tries to save the environment to an xml document, creating the file if it doesn't exist
            Console.WriteLine("Writing environment to the file");
            FileStream fs = new FileStream(Environment.CurrentDirectory + "\\Environment.xml", FileMode.Create);
            fs.Dispose();
            XElement doc = new XElement("FireworkGameConfig", new XAttribute("Ver", "1.0.1"));
            doc.Add(Game.GetElement());
            doc.Save(Environment.CurrentDirectory + "\\Environment.xml");
        }

        /// <summary>
        /// Triggered whenever the player clicks on the simulation to interact with the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvasBox_Click(object sender, EventArgs e)
        {
            if (Game.IsRunning())
            {
                int count = Game.Click(PointToClient(Cursor.Position));
                if (count > 0)
                    Game.IncreaseScore(count);
                else //if (Game.Score == 0)
                {
                    Game.DecreaseLifeCount();
                    if(Game.IsOver())
                    {
                        Game.Stop();
                        if(!Game.CheckScore(Game.Score))
                            MessageBox.Show("Better luck next time!");
                        else
                        {
                            UserInputTextBox wizard = new UserInputTextBox("Please enter your name!", "You got a high score!");
                            wizard.ShowDialog();
                            Game.SaveScore(new HighScore(wizard.Value, Game.Score));
                        }

                        FireworkToolkit.Gaming.MainMenu menu = new FireworkToolkit.Gaming.MainMenu(Game);
                        menu.ShowDialog();
                        if (!Game.IsRunning())
                            Dispose();
                    }
                }
                /*
                else
                    Game.DecreaseScore();
                    */
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == ' ')
            {
                GameMenu menu = new GameMenu(Game);
                menu.ShowDialog();

                // If the player decided to quit
                if (!Game.IsRunning())
                    Dispose();
            }
        }
    }
}
