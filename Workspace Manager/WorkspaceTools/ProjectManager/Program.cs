using System;
using System.Collections.Generic;
using WorkspaceTools;
using WorkspaceTools.Abstracts;
using WorkspaceTools.Templates;

namespace ProjectManager
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                sendError("Invalid number of arguments supplied");
                return;
            }

            List<string> validArguments = new List<string>();
            string language = "C#";
            string type = "Exe";

            switch (args[0])
            {
                case "-create":
                    validArguments = new List<string>()
                    {
                        "-language",
                        "-type"
                    };

                    int i = 1;
                    while(validArguments.Contains(args[i]))
                    {
                        switch(args[i])
                        {
                            case "-language":
                                if (args.Length < i + 3)
                                {
                                    sendError("Invalid number of arguments supplied");
                                    return;
                                }

                                List<string> languages = ProjectTools.GetValidLanguages();
                                if (languages.Contains(args[i + 1]))
                                    language = args[i + 1];

                                i += 2;
                                break;
                            case "-type":
                                if (args.Length < i + 3)
                                {
                                    sendError("Invalid number of arguments supplied");
                                    return;
                                }

                                if (new List<string>() { "Exe", "Library" }.Contains(args[i + 1]))
                                    type = args[i + 1];

                                i += 2;
                                break;
                        }
                    }

                    if (i != args.Length - 1)
                    {
                        sendError("Invalid number of arguments supplied");
                        return;
                    }

                    ADescriptor desc = (language == "C#") ?
                        new CSDescriptor() { Subtype = (type == "") ? "Exe" : type } :
                        null;

                    WorkspaceTools.ProjectManager.CreateProject(args[i], desc);

                    break;
                case "-update":
                    break;
                case "-remove":
                    break;
                default:
                    sendError("Argument " + args[0] + " not recognized");
                    return;
            }
        }

        static void sendError(string msg = "")
        {
            Console.WriteLine("Error: " + msg);
            //Console.ReadKey();
        }
    }
}
