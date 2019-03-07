using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceTools.Interfaces
{
    public interface IDescriptor : IFilable
    {
        /// <summary>
        /// Creates a new workspace project directory with this descriptor's setup
        /// </summary>
        /// <param name="name">Name of the directory to use</param>
        void Setup(string name);

        /// <summary>
        /// Creates the files for this project
        /// </summary>
        void CreateFiles();
    }
}
