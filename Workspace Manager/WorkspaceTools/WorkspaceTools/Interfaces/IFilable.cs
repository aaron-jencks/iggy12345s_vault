using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WorkspaceTools.Interfaces
{
    public interface IFilable
    {
        /// <summary>
        /// Converts the given object into an Xml Element
        /// </summary>
        /// <returns>Returns an XElement that represents the object</returns>
        XElement ToXml();

        /// <summary>
        /// Loads the object from a given Xml element
        /// </summary>
        /// <param name="element">The XElement to load the object from</param>
        void FromXml(XElement element);
    }
}
