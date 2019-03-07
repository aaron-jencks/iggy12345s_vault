using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceTools.Abstracts;
using WorkspaceTools.Templates;

namespace WorkspaceTools
{
    public static class ProjectManager
    {
        public static void CreateProject(string name, ADescriptor desc = null)
        {
            if (desc == null)
            {
                string lang = ConsoleTools.AskQuestion("What is the language of the project? (Leave blank for C#)");
                string type = ConsoleTools.AskQuestion("What kind of output file? (Leave blank for Exe)");
                type = (type == "") ? "Exe" : type;
                switch (lang)
                {
                    default:
                        desc = new CSDescriptor()
                        {
                            Subtype = type
                        };
                        break;
                }
            }

            GenericProject proj = new GenericProject(name, desc);
            proj.Setup();
        }

        public static void UpdateProject(string name)
        {

        }

        public static void RemoveProject(string name)
        {

        }
    }
}
