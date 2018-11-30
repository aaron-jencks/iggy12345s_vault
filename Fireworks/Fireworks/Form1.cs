using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fireworks
{
    public partial class Form1 : Form
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

        private List<Firework> firework;
        private List<Sprite> sprites = new List<Sprite>();
        private static int refresh = 10;   // refresh delay in ms
        private static double launchProb = 0.05;
        private static int minVel = -5;
        private static int maxVel = -20;

        #endregion

        private Thread painter;
        private Random rng = new Random();

        private bool canDraw = false;
        private bool isExitting = false;
        private bool isDrawing = false;

        private Point mouseLocation;
        private bool previewMode = false;

        public Form1()
        {
            
            InitializeComponent();

            // Sets up the background color of the canvas.
            canvasBox.Image = new Bitmap(Width, Height);
            canvasBox.BackColor = Color.Black;
            canvasBox.Invalidate();

            firework = new List<Firework>();

            //sprites.Add(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pikachu Outline.bmp", 1));
            sprites.Add(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pachirisu Outline.bmp", 4));
            //sprites.Add(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\LaurenText.bmp", 2));
            sprites.Add(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\BongoCat.bmp", 2));

            painter = new Thread(ExecutionModule);
            painter.Start();
        }

        public Form1(Rectangle Bounds)
        {
            InitializeComponent();

            // Sets up the background color of the canvas.
            canvasBox.Image = new Bitmap(Width, Height);
            canvasBox.BackColor = Color.Black;
            canvasBox.Invalidate();

            firework = new List<Firework>();

            //sprites.Add(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pikachu Outline.bmp", 1));
            sprites.Add(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pachirisu Outline.bmp", 4));
            //sprites.Add(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\LaurenText.bmp", 2));
            sprites.Add(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\BongoCat.bmp", 2));

            painter = new Thread(ExecutionModule);
            painter.Start();

            this.Bounds = Bounds;
        }

        public Form1(IntPtr PreviewWndHandle)
        {
            InitializeComponent();

            // Sets up the background color of the canvas.
            canvasBox.Image = new Bitmap(Width, Height);
            canvasBox.BackColor = Color.Black;
            canvasBox.Invalidate();

            firework = new List<Firework>();

            //sprites.Add(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pikachu Outline.bmp", 1));
            sprites.Add(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pachirisu Outline.bmp", 4));
            //sprites.Add(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\LaurenText.bmp", 2));
            sprites.Add(new Sprite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\BongoCat.bmp", 2));

            painter = new Thread(ExecutionModule);
            painter.Start();

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

        #region Execution Engine

        private void UpdateParticles()
        {
            lock (firework)
            {
                for(int i = firework.Count - 1; i >= 0; i--)
                {
                    if (firework[i].Done())
                        firework.RemoveAt(i);
                    else
                        firework[i].Update();
                }
            }
        }

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

                        
                        lock (firework)
                            foreach(Firework f in firework)
                                f.Show(g);

                        g.Dispose();
                    }
                    canvasBox.Invalidate();
                }
            }
            isDrawing = false;
        }

        /// <summary>
        /// Controls painting the particles on the screen, time updates, etc...
        /// Causes frames to happen.
        /// </summary>
        private void ExecutionModule()
        {
            while(!isExitting)
            {
                Thread.Sleep(refresh);

                if (rng.NextDouble() <= launchProb)
                    lock (firework)
                    {
                        /*
                        firework.Add(new SpriteFirework(
                                new Vector2D(rng.Next(Width), Height),
                                new Vector2D(0, rng.Next(maxVel, minVel)),
                                sprites[rng.Next(0, sprites.Count - 1)], 0.5));
                                */

                        
                        double r = rng.NextDouble();

                        if (r > 0.75)
                            firework.Add(new Firework(new Vector2D(
                                rng.Next(Width), Height),
                                new Vector2D(0, rng.Next(maxVel, minVel))));
                        else if (r > 0.25)
                            firework.Add(new Heart_Firework(new Vector2D(
                                rng.Next(Width), Height),
                                new Vector2D(0, rng.Next(maxVel, minVel))));
                        else if (r > 0.1)
                            firework.Add(new SpriteFirework(
                                new Vector2D(rng.Next(Width), Height),
                                new Vector2D(0, rng.Next(maxVel, minVel)),
                                sprites[rng.Next(0, sprites.Count)]));
                        /*else
                            firework.Add(new Pikachu_Firework(new Vector2D(
                                rng.Next(Width), Height),
                                new Vector2D(0, rng.Next(maxVel, minVel))));*/

                    }


                UpdateParticles();
                canvasBox.Invalidate();
            }
        }

        #endregion



        private void Form1_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
            TopMost = true;

            while (isDrawing) ;

            canDraw = false;
            // Resizes the drawing canvas to fill the window.
            canvasBox.Left = 0;
            canvasBox.Top = 0;
            canvasBox.Width = this.Size.Width;
            canvasBox.Height = this.Size.Height;

            lock(canvasBox.Image)
                canvasBox.Image = new Bitmap(Width, Height);

            canvasBox.Invalidate();

            canDraw = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isExitting = true;
        }

        private void canvasBox_Paint(object sender, PaintEventArgs e)
        {
            DrawParticles();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!previewMode)
                Application.Exit();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseLocation.IsEmpty)
            {
                // Terminate if mouse is moved a significant distance
                if (Math.Abs(mouseLocation.X - e.X) > 5 ||
                    Math.Abs(mouseLocation.Y - e.Y) > 5)
                    Application.Exit();
            }

            // Update current mouse location
            mouseLocation = e.Location;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }
    }
}
