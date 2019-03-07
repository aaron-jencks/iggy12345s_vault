using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using WorkspaceTools.Interfaces;

namespace WorkspaceTools.Abstracts
{
    public abstract class ADescriptor : IDescriptor
    {
        #region Properties

        /// <summary>
        /// A list of project references that this project needs to be maintained for.
        /// </summary>
        public List<string> References { get; private set; } = new List<string>();

        /// <summary>
        /// The assembly name of the project
        /// </summary>
        protected string Name { get; private set; }

        /// <summary>
        /// The output type of the project (executable)
        /// </summary>
        public string Subtype { get; set; } = "Exe";

        #endregion

        #region Methods

        public virtual void Setup(string name)
        {
            Name = name;
            Console.WriteLine("Creating Descriptor information");
            FileStream s = File.Create("../Projects/" + name + "/descriptor.xml");
            Console.WriteLine("Generating contents...");
            XElement root = new XElement("Project", new XAttribute("Name", name), new XAttribute("Language", "C#"));
            root.Save(s);
            s.Close();
            Console.WriteLine("Descriptor information finished");
            CreateFiles();
        }

        public virtual void CreateFiles()
        {
            CreateGenericFiles();
            CreateAppConfig();
        }

        /// <summary>
        /// Creates the generic project files for this project
        /// </summary>
        protected abstract void CreateGenericFiles();

        /// <summary>
        /// This is only used if the project is an executable type
        /// </summary>
        protected abstract void CreateAppConfig();

        #region File IO

        public abstract XElement ToXml();

        public abstract void FromXml(XElement element);

        #endregion

        #endregion
    }
}
