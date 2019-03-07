using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceTools.Abstracts;
using WorkspaceTools.Interfaces;

namespace WorkspaceTools.Templates
{
    public class GenericProject : AProject
    {
        public GenericProject(string name, IDescriptor descriptor = null) : base(name, descriptor)
        {

        }
    }
}
