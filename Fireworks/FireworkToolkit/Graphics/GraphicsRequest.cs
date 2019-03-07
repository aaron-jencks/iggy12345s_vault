using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Graphics
{
    public class GraphicsRequest
    {
        public Point Coordinate { get; set; }

        public Image Image { get; set; }

        /// <summary>
        /// Creates a new GraphicsRequest object for use in the GraphicsLib
        /// </summary>
        /// <param name="coord">Coordinate to draw the image at</param>
        /// <param name="image">The image to draw</param>
        public GraphicsRequest(Point coord, Image image)
        {
            Coordinate = coord;
            Image = image;
            
        }

        public void Show(System.Drawing.Graphics g)
        {
            g.DrawImage(Image, Coordinate);
        }
    }
}
