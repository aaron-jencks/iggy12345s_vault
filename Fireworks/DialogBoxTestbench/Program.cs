using FireworkToolkit.Gaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogBoxTestbench
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading dialog box");
            /*
            UserInputTextBox test = new UserInputTextBox("Please enter your name!\n\n\n\n\nHello World!");
            test.ShowDialog();
            Console.WriteLine("Your name is: " + test.Value);
            */

            Form1 form = new Form1();
            form.ShowDialog();
            Console.ReadKey();
        }
    }
}
