using FireworkToolkit._2D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using FireworkToolkit.Vectors;
using FireworkToolkit.Templates;
using FireworkToolkit.SpriteGraphics;
using FireworkToolkit;
using FireworkToolkit.Simulation;

namespace FireworksFormsApp
{
    public partial class FireworksFrame : Form
    {
        #region Windows API Functions

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        #endregion

        #region Properties

        /// <summary>
        /// The simulation housed within this form
        /// </summary>
        public FireworksSim Simulation { get; private set; } = new FireworksSim();

        private Random rng = new Random();

        private bool canDraw = false;
        private bool isDrawing = false;

        private Point mouseLocation;
        private bool previewMode = false;

        #endregion

        #region Constructors

        public FireworksFrame()
        {

            InitializeComponent();

            Setup();
        }

        public FireworksFrame(Rectangle Bounds)
        {
            InitializeComponent();

            Setup();

            this.Bounds = Bounds;
        }

        public FireworksFrame(IntPtr PreviewWndHandle)
        {
            InitializeComponent();

            Setup();

            // Set the preview window as the parent of this window
            SetParent(this.Handle, PreviewWndHandle);

            // Make this a child window so it will close when the parent dialog closes
            // GWL_STYLE = -16, WS_CHILD = 0x40000000
            SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

            // Place our window inside the parent
            Rectangle ParentRect;
            GetClientRect(PreviewWndHandle, out ParentRect);
            Size = ParentRect.Size;
            Location = new Point(0, 0);

            previewMode = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets up the canvasbox
        /// </summary>
        private void Setup()
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
        private void Simulation_UpdateEvent(object sender, EventArgs e)
        {
            canvasBox.Invalidate();
        }

        /// <summary>
        /// Draws all of the particle onto the screen
        /// </summary>
        private void DrawParticles()
        {
            if (canDraw)
            {
                isDrawing = true;
                lock (canvasBox.Image)
                {
                    using (Graphics g = Graphics.FromImage(canvasBox.Image))
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
        private void FireworksFrame_Load(object sender, EventArgs e)
        {
            //Cursor.Hide();
            TopMost = false;

            Simulation.Width = Width;
            Simulation.Height = Height;

            while (isDrawing) ;

            canDraw = false;
            // Resizes the drawing canvas to fill the window.
            canvasBox.Left = 0;
            canvasBox.Top = 0;
            canvasBox.Width = this.Size.Width;
            canvasBox.Height = this.Size.Height;

            lock (canvasBox.Image)
                canvasBox.Image = new Bitmap(Width, Height);

            canvasBox.Invalidate();

            canDraw = true;
        }

        #endregion

        private void FireworksFrame_FormClosed(object sender, FormClosedEventArgs e)
        {
            Simulation.Stop();
        }

        private void canvasBox_Paint(object sender, PaintEventArgs e)
        {
            DrawParticles();
        }
    }
}
