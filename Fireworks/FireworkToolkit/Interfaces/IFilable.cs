using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FireworkToolkit.Interfaces
{
    /// <summary>
    /// Represents the functions necessary to save an item to an xml file
    /// </summary>
    public interface IFilable
    {
        /// <summary>
        /// Converts this object into an xml element so that it can be appended to a file
        /// </summary>
        /// <returns>Returns an xml element that represents this object</returns>
        XElement GetElement();

        /// <summary>
        /// Loads an object from an xml element
        /// </summary>
        /// <param name="e">Element to load this item from</param>
        void FromElement(XElement e);
    }
}
