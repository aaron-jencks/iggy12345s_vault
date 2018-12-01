using FireworkToolkit.SpriteGraphics;
using FireworkToolkit.Templates;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireworkToolkit.Interfaces
{
    /// <summary>
    /// Represents an interface for a simulation of fireworks
    /// </summary>
    public interface ISimulation
    {
        #region Flow control

        /// <summary>
        /// Causes simulation of a number of seconds
        /// </summary>
        /// <param name="numSteps">The number of seconds to simulate</param>
        void Simulate(int numSteps = 1);

        /// <summary>
        /// Starts the simulation autonomously
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the simulation
        /// </summary>
        void Stop();

        /// <summary>
        /// Pauses the simulation momentarily
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes the simulation
        /// </summary>
        void Resume();

        #endregion

        #region Asset Control

        /// <summary>
        /// Finds all assets within this simulation
        /// </summary>
        /// <returns>Returns a collection of all assets in the simulation</returns>
        ICollection<IFilable> GetAllAssets();

        /// <summary>
        /// Wipes the assets from the current simulation
        /// </summary>
        void ClearAssets();

        /// <summary>
        /// Adds a firework manually to the list of fireworks in the simulation
        /// </summary>
        /// <param name="firework">firework to add</param>
        void AddFirework(AFirework firework);

        /// <summary>
        /// Adds a collection of fireworks manually to the list of fireworks in the simulation
        /// </summary>
        /// <param name="fireworks">fireworks to add</param>
        void AddFireworkRange(ICollection<AFirework> fireworks);

        // There is no control for removing fireworks, they disappear after they explode

        /// <summary>
        /// Adds a sprite to the list of sprites in the simulation
        /// </summary>
        /// <param name="sprite">Sprite to add</param>
        void AddSprite(Sprite sprite);

        /// <summary>
        /// Adds a collection of sprites to the list of sprites in the simulation
        /// </summary>
        /// <param name="sprites">Sprites to add</param>
        void AddSpriteRange(ICollection<Sprite> sprites);

        /// <summary>
        /// Saves the assets to the given filename
        /// (Must be xml)
        /// </summary>
        /// <param name="filename">file to save the assets to</param>
        void SaveAssets(string filename);

        /// <summary>
        /// Launches a SaveFileDialog to aid in finding a file to save the assets to
        /// </summary>
        void SaveAssets();

        /// <summary>
        /// Opens the assets from the given filename
        /// (Must be xml)
        /// </summary>
        /// <param name="filename">file to open the assets from</param>
        void LoadAssets(string filename, bool clearOld = true);

        /// <summary>
        /// Launches an OpenFileDialog to aid in finding a file to open the assets from
        /// </summary>
        void LoadAssets(bool clearOld = true);

        #endregion

        /// <summary>
        /// Draws all of the fireworks given a graphics object
        /// </summary>
        /// <param name="g">Graphics object to use</param>
        void Show(Graphics g);
    }
}
