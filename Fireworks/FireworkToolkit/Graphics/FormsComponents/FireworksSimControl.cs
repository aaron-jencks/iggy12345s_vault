using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireworkToolkit.Simulation;
using System.Drawing.Imaging;

namespace FireworkToolkit.Graphics.FormsComponents
{
    public partial class FireworksSimControl : UserControl
    {
        #region Properties

        /// <summary>
        /// The simulation housed within this form
        /// </summary>
        public FireworksSim Simulation { get; private set; } = new FireworksSim();

        /// <summary>
        /// Random number generator used by this form
        /// </summary>
        protected Random rng { get; private set; } = new Random();

        private bool canDraw = false;
        private bool isDrawing = false;

        private Point mouseLocation;
        private bool previewMode = false;

        #endregion

        #region Constructors

        public FireworksSimControl()
        {
            InitializeComponent();
            Setup();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets up the canvasbox
        /// </summary>
        protected void Setup()
        {
            // Sets up the background color of the canvas.
            canvasBox.Image = new Bitmap(Width, Height);
            canvasBox.BackColor = Color.Black;
            canvasBox.Invalidate();

            Simulation.UpdateEvent += Simulation_UpdateEvent;

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
            if (canDraw && !Simulation.isPaused)
            {
                isDrawing = true;
                lock (canvasBox.Image)
                {
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(canvasBox.Image))
                    {
                        Image image = (Image)canvasBox.Image.Clone();

                        g.Clear(Color.Black);


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


                        Simulation.Show(g);

                        g.Dispose();
                    }
                    canvasBox.Invalidate();
                }
            }
            isDrawing = false;
        }

        /// <summary>
        /// Triggered when the form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FireworksFrame_Load(object sender, EventArgs e)
        {

            Simulation.Width = Width;
            Simulation.Height = Height;

            while (isDrawing) ;

            canDraw = false;

            canvasBox.Width = Width;
            canvasBox.Height = Height;

            lock (canvasBox.Image)
                canvasBox.Image = new Bitmap(Width, Height);

            canvasBox.Invalidate();

            canDraw = true;
        }

        #endregion

        private void canvasBox_Paint(object sender, PaintEventArgs e)
        {
            DrawParticles();
        }
    }
}
