using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Graphics
{
    /// <summary>
    /// This class will be used to multi-thread drawing techniques
    /// </summary>
    public class GraphicsLib
    {
        #region Properties

        /// <summary>
        /// Contains all drawing requests for the object
        /// </summary>
        public Queue<GraphicsRequest> requests { get; private set; } = new Queue<GraphicsRequest>();

        /// <summary>
        /// Contains the image currently being drawn by this object
        /// </summary>
        public Image Image { get; private set; } = null;

        /// <summary>
        /// Flags when this object is exitting
        /// </summary>
        public bool IsExitting { get; set; } = false;

        /// <summary>
        /// Flags when this object has been disposed and is no longer usable
        /// </summary>
        public bool IsDisposed { get; private set; } = false;

        /// <summary>
        /// Task used by the painter to paint the image on
        /// </summary>
        protected Task painterTask { get; private set; } = null;

        #endregion

        public GraphicsLib(int Width, int Height)
        {
            Image = new Bitmap(Width, Height);
        }

        #region Methods

        private void Painter()
        {
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(Image))
            {
                while (!IsExitting)
                {
                    if (requests.Count > 0)
                    {
                        Enumerable.Range(0, requests.Count).ToList().ForEach((i) =>
                        {
                            requests.Dequeue().Show(g);
                        });
                    }
                }

                g.Dispose();
            }
        }

        /// <summary>
        /// Releases the painter thread being reserved by this object
        /// </summary>
        public void Dispose()
        {
            IsExitting = true;
            while (!painterTask.IsCompleted) ;
            requests.Clear();
            IsDisposed = true;
        }

        #endregion
    }
}
