using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleManager
{
    class FileCreatorReader
    {
        public void CreateTxtFile(string name)
        {
          
            string path = Directory.GetCurrentDirectory()+"\\"+name+ ".txt";
            using (File.Create(path)) {; }
            
        }

        public void ReadTxtFile(string name)
        {
            string path = Directory.GetCurrentDirectory() + "\\" + name; 
            if(File.Exists(path))
            {
                string txt = File.ReadAllText(path);
                Console.WriteLine(txt);
            }
        }

        public void ReadHelpFile(string str)
        {
            
                string txt = File.ReadAllText(str + "\\help.txt");
                Console.WriteLine(txt);
            
        }
    }
}
