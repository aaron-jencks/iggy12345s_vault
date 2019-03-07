using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorkspaceTools.Abstracts;

namespace WorkspaceTools.Templates
{
    public class CSDescriptor : ADescriptor
    { 
        public override void Setup(string name)
        {
            Directory.CreateDirectory("../Projects/" + name + "/code/Properties");
            base.Setup(name);
        }

        protected override void CreateGenericFiles()
        {
            XElement root = ProjectTools.GetMinimalProject(Name);
            root.Save("../Projects/" + Name + "/code/" + Name + ".csproj");
            Console.WriteLine("Created " + Name + ".csproj");
            StreamWriter w = File.CreateText("../Projects/" + Name + "/code/Properties/AssemblyInfo.cs");
            List<string> assemblyInfo = ProjectTools.GetAssemblyInfoTemplate();
            int titleIndex = assemblyInfo.FindIndex(new Predicate<string>((s) => { return s.Contains("[assembly: AssemblyTitle("); }));
            int productIndex = assemblyInfo.FindIndex(new Predicate<string>((s) => { return s.Contains("[assembly: AssemblyProduct("); }));
            assemblyInfo[titleIndex] = assemblyInfo[titleIndex].Replace("TemplateName", Name);
            assemblyInfo[productIndex] = assemblyInfo[productIndex].Replace("TemplateName", Name);
            foreach (string s in assemblyInfo)
                w.WriteLine(s);
            w.Close();
            Console.WriteLine("Created AssemblyInfo.cs");
        }

        protected override void CreateAppConfig()
        {
            StreamWriter w = File.CreateText("../Projects/" + Name + "/code/App.config");
            List<string> appConfig = ProjectTools.GetAppConfigTemplate();
            foreach (string s in appConfig)
                w.WriteLine(s);
            Console.WriteLine("Created App.config");
            w.Close();
        }

        public override void FromXml(XElement element)
        {
            throw new NotImplementedException();
        }

        public override XElement ToXml()
        {
            throw new NotImplementedException();
        }
    }
}
