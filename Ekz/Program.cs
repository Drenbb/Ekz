using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dll;
using Microsoft.Win32;

namespace Ekz
{
   
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string s="";
            using (StreamReader sr = new StreamReader(File.Open("Users.csv", FileMode.OpenOrCreate)))
            {
                while (sr.Peek() > -1)
                {
                    s = sr.ReadToEnd();
                }
            }
            Class1.read(s);
            Class1.Second();
            Class1.Third();
            Class1.fourth();
            RegistryKey registryKey = Registry.CurrentUser;
            MessageBox.Show(registryKey.GetValue("AverageAge") + " " + registryKey.GetValue("MaxLen"));
            Console.ReadKey();
        }
    }
}
