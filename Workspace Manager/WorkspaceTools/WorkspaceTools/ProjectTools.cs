using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace WorkspaceTools
{
    public static class ProjectTools
    {
        /// <summary>
        /// Retrieves the default project file layout for the given project type
        /// </summary>
        /// <param name="name">Name of the assembly to make</param>
        /// <param name="type">The programming language that the assembly is in</param>
        /// <param name="subtype">The type of executable to make, program (Exe) or Library (Library)</param>
        /// <returns>Returns a .proj file with the bare contents able to compile your code.</returns>
        public static XElement GetMinimalProject(string name, string type = "C#", string subtype = "Exe")
        {
            switch(type)
            {
                case "C#":

                    XElement root = XElement.Load("MinimalistProjectFile.xml");
                    XNamespace ns = root.Name.Namespace;
                    XElement props = root.Elements().ElementAt(1);
                    props.Add(new XElement(ns + "OutputType", subtype));
                    props.Add(new XElement(ns + "AssemblyName", name));
                    if (subtype == "Library")
                        props.Add(new XElement("RootNamespace", name));

                    return root;
                default:
                    throw new NotImplementedException();
            }
            
        }

        /// <summary>
        /// Reads the AssemblyInfoTemplate.txt file and returns it as a list of string lines
        /// </summary>
        /// <returns>Returns a list of string representing the lines of the text file</returns>
        public static List<string> GetAssemblyInfoTemplate()
        {
            List<string> lines = new List<string>();
            StreamReader r = new StreamReader(File.OpenRead("AssemblyInfoTemplate.txt"));
            while (!r.EndOfStream)
                lines.Add(r.ReadLine());
            return lines;
        }

        /// <summary>
        /// Reads the AppConfigTemplate.txt file and returns it as a list of string lines
        /// </summary>
        /// <returns>Returns a list of string representing the lines of the text file</returns>
        public static List<string> GetAppConfigTemplate()
        {
            List<string> lines = new List<string>();
            StreamReader r = new StreamReader(File.OpenRead("AppConfigTemplate.txt"));
            while (!r.EndOfStream)
                lines.Add(r.ReadLine());
            return lines;
        }

        /// <summary>
        /// Returns all the valid languages for this manager
        /// If the language doesn't already have a specific class made for it,
        /// it will use the generic project, but will not use a descriptor
        /// </summary>
        /// <returns></returns>
        public static List<string> GetValidLanguages()
        {
            List<string> lines = new List<string>();
            StreamReader r = new StreamReader(File.OpenRead("ValidLanguages.txt"));
            while (!r.EndOfStream)
                lines.Add(r.ReadLine());
            return lines;
        }
    }
}
