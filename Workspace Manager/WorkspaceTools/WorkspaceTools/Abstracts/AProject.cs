using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorkspaceTools.Interfaces;

namespace WorkspaceTools.Abstracts
{
    public abstract class AProject : IProject
    {
        #region Properties

        /// <summary>
        /// The name of the project, matches the name of the folder it's stored in
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The descriptor file used for this project
        /// </summary>
        public IDescriptor Descriptor { get; set; }

        #endregion

        /// <summary>
        /// Creates a new project object with the given name and project properties
        /// </summary>
        /// <param name="name">Name for the project</param>
        /// <param name="descriptor">Descriptor for the project</param>
        public AProject(string name, IDescriptor descriptor = null)
        {
            Name = name;
            Descriptor = descriptor;
        }

        #region Methods

        public void Setup()
        {
            CreateFolders();
            Descriptor?.Setup(Name);
        }

        public void CreateFolders()
        {
            Console.WriteLine("Creating a new project with name " + Name + " at Projects/" + Name + "/code/");
            Directory.CreateDirectory("../Projects/" + Name + "/code");
            Console.WriteLine("Created Project Directory");
            Directory.CreateDirectory("../Libraries/" + Name + "/Debug");
            Directory.CreateDirectory("../Libraries/" + Name + "/Release");
            Console.WriteLine("Created Project Release Directories");
            Console.WriteLine("Creating project folder");
        }

        #region File IO

        public virtual XElement ToXml()
        {
            return new XElement("Project", new XAttribute("Name", Name));
        }

        public virtual void FromXml(XElement element)
        {

        }

        #endregion

        #endregion
    }
}
