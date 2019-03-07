using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceTools
{
    public static class ConsoleTools
    {
        public static string AskQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }
    }
}
