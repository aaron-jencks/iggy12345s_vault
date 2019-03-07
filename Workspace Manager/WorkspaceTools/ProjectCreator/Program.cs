using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceTools;

namespace ProjectCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            ProjectManager.CreateProject("Hello_World");
            Console.ReadKey();
        }
    }
}
