using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceTools.Interfaces
{
    public interface IProject : IFilable
    {
        /// <summary>
        /// Sets up the project, initializing it
        /// creates required directories
        /// sets up documents
        /// creates code files
        /// etc...
        /// </summary>
        void Setup();

        /// <summary>
        /// Creates the directories for this project
        /// </summary>
        void CreateFolders();
    }
}
