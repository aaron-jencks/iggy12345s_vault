using FireworkToolkit.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FireworkToolkit.SpriteGraphics
{
    /// <summary>
    /// A class used to load images and mask them to get a set of coordinates used for the fireworks to draw pictures
    /// </summary>
    public class Sprite : IFilable
    {

        #region Properties

        /// <summary>
        /// The bitmap of the image that was loaded into the sprite
        /// </summary>
        protected Bitmap image { get; set; }

        /// <summary>
        /// The mask color used by the sprite to generate coordinate lists
        /// </summary>
        public Color maskColor { get; set; } = Color.Black;

        /// <summary>
        /// Used to determine if the coordinate list need to be calculated yet, or not
        /// </summary>
        private bool isConverted { get; set; } = false;

        /// <summary>
        /// Flags when the sprite is converting the image, so it doesn't happen multiple times
        /// </summary>
        private bool isConverting { get; set; } = false;

        /// <summary>
        /// The list of coordinates that this sprite has generated
        /// </summary>
        private List<Tuple<int, int>> coordinates { get; set; } = new List<Tuple<int, int>>();

        /// <summary>
        /// The list of coordinates that this sprite has generated
        /// </summary>
        public List<Tuple<int, int>> Coordinates { get => getCoordinates(); }

        /// <summary>
        /// How far the pixels in the coordinates list should be multiplied by when creating the image
        /// </summary>
        public double Zoom { get; set; } = 1;

        /// <summary>
        /// Name of the file that this sprite was initialized from
        /// </summary>
        public string Name { get; protected set; } = "";

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an empty sprite, shouldn't be used unless loading attributes from a file
        /// </summary>
        public Sprite()
        {

        }

        /// <summary>
        /// Creates a new sprite from the given filename and the default mask color of black
        /// </summary>
        /// <param name="filename">The file to load in</param>
        /// <param name="convert">[optional] Specifies if the sprite should auto-convert upon initialization (true)</param>
        public Sprite(string filename, double zoom = 1, bool convert = true)
        {
            Name = filename;
            Zoom = zoom;
            image = (Bitmap)Image.FromFile(filename);
            if (convert)
                Convert();
        }

        /// <summary>
        /// Creates a new sprite from the given filename and uses the given mask color
        /// </summary>
        /// <param name="filename">The file to load in</param>
        /// <param name="maskColor">The mask color to use</param>
        /// <param name="convert">[optional] Specifies if the sprite should auto-convert upon initialization (true)</param>
        public Sprite(string filename, Color maskColor, double zoom = 1, bool convert = true)
        {
            Name = filename;
            Zoom = zoom;
            image = (Bitmap)Image.FromFile(filename);
            this.maskColor = maskColor;
            if (convert)
                Convert();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the coordinates used by this sprite and returns them, calculating them if necessary
        /// </summary>
        /// <returns>Returns a list of X,Y coordinate pairs representing the colored pixels of the image</returns>
        protected List<Tuple<int, int>> getCoordinates()
        {
            if (!isConverted && !isConverting)
                Convert();

            return coordinates;
        }

        protected void Convert()
        {
            if (isConverting)
                return;

            isConverting = true;

            List<Thread> threads;
            int height, adjustedPortion;

            Queue<Tuple<int, int>> tempQueue = new Queue<Tuple<int, int>>();

            lock (image)
            {
                threads = new List<Thread>(Environment.ProcessorCount);
                height = image.Height;
                adjustedPortion = height / Environment.ProcessorCount;
            }

            // Splits the image into equal parts to be processed by each processor to avoid overloading the cpu and being inefficient
            for (int processor = 1; processor <= Environment.ProcessorCount; processor++)
            {
                Thread t = new Thread((object row) =>
                {
                    int p = (int)row;
                    for (int i = (p - 1) * adjustedPortion; i < p * adjustedPortion; i++)
                    {
                        Bitmap b;

                        lock (image)
                        {
                            b = (Bitmap)image.Clone();
                        }

                        for (int j = 0; j < b.Width; j++)
                        {
                            Color c = b.GetPixel(j, i);
                            if (c.R == maskColor.R && c.G == maskColor.G && c.B == maskColor.B)
                            {
                                Tuple<int, int> tuple = new Tuple<int, int>(j - (b.Width / 2), -1 * (i - (b.Height / 2)));
                                lock (tempQueue)
                                    tempQueue.Enqueue(tuple);
                            }
                        }

                        b.Dispose();
                    }
                });

                t.Start(processor);

                threads.Add(t);
                
            }

            // Waits until all of the threads have finished
            foreach (Thread t in threads)
                while (t.IsAlive) ;

            // Moves the queue items into the coordinates list
            coordinates.Clear();
            coordinates.Capacity = tempQueue.Count; // Resizes it to the number of coordinates
            while (tempQueue.Count > 0)
                coordinates.Add(tempQueue.Dequeue());
            tempQueue.Clear();

            List<Tuple<int, int>> nullIndices = new List<Tuple<int, int>>();
            foreach (Tuple<int, int> t in coordinates)
                if (t == null)
                {
                    Console.WriteLine("There's something wrong here, removing null coordinate...");
                    nullIndices.Add(t);
                }

            foreach (Tuple<int, int> t in nullIndices)
                coordinates.Remove(t);

            isConverted = true;
            isConverting = false;
        }

        #region File IO

        public XElement GetElement()
        {
            XElement result = new XElement("Sprite", new XAttribute("Zoom", Zoom), new XAttribute("Name", Name));

            XElement data = new XElement("Data");

            foreach (Tuple<int, int> coord in getCoordinates())
                if (coord != null)
                    data.Add(new XElement("Coordinate", 
                        new XAttribute("X", coord.Item1), 
                        new XAttribute("Y", coord.Item2)));

            result.Add(data);

            return result;
        }

        public void FromElement(XElement e)
        {
            coordinates.Clear();
            Zoom = (double)e.Attribute("Zoom");
            Name = (string)e.Attribute("Name");
            foreach (XElement c in e.Element("Data").Elements())
                if (c.Name == "Coordinate")
                    coordinates.Add(new Tuple<int, int>((int)c.Attribute("X"), (int)c.Attribute("Y")));
            isConverted = true;
        }

        #endregion

        public override string ToString()
        {
            return Name.Split(new char[]{'/', '\\'}).Last();
        }

        /// <summary>
        /// Clones the current sprite
        /// </summary>
        /// <returns>Returns a new sprite with the same attributes as this one</returns>
        public Sprite Clone()
        {
            while (isConverting) ;

            Sprite result = new Sprite();
            result.Name = Name;
            result.Zoom = Zoom;
            result.coordinates = Coordinates;
            result.isConverted = isConverted;

            return result;
        }

        #endregion
    }
}
