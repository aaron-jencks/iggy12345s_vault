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
using FireworkToolkit.Graphics.FormsComponents;

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

        public bool previewMode { get; private set; } = false;

        #endregion

        #region Constructors

        public FireworksFrame()
        {

            InitializeComponent();

        }

        public FireworksFrame(Rectangle Bounds)
        {
            InitializeComponent();

            this.Bounds = Bounds;
        }

        public FireworksFrame(IntPtr PreviewWndHandle)
        {
            InitializeComponent();

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

        private void asXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fireworksSimControl1.Simulation.SaveAssets();
        }

        private void fromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fireworksSimControl1.Simulation.LoadAssets(false);
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fireworksSimControl1.Simulation.ClearAssets();
        }

        private void FireworksFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            fireworksSimControl1.Simulation.Stop();
        }

        private void allSpritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpriteManager wizard = new SpriteManager(fireworksSimControl1.Simulation.GetAllSprites());
            wizard.ShowDialog();
        }

        #endregion
    }
}
