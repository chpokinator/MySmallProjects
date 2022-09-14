using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleManager
{
    class CommandLine
    {
        

        
        
        
        public void ShowAllInCurrentDirectory(string tmp = "")
        {
           

            foreach (var i in Directory.GetDirectories(tmp))
            {
                DirectoryInfo info = new DirectoryInfo(i);
                Console.WriteLine(info.Name);
            }
            foreach(var i in Directory.GetFiles(tmp))
            {
                string resName = Path.GetFileName(i);
                
                Console.WriteLine($"{resName}");
            }
        }

        

        public void MoveFile(string str)
        {
            char[] delimeters = { '>' };
            string[] tmp = str.Split(delimeters, System.StringSplitOptions.RemoveEmptyEntries);
            string path = tmp[0];
            string fileName = tmp[0].Remove(0,tmp[0].LastIndexOf('\\')+1);
            string pathTo = tmp[1];


            if (File.Exists(path))
            {
                try
                {


                    if (Directory.Exists(tmp[1]))
                    {
                        if (File.Exists(pathTo))
                        {
                            File.Delete(pathTo + "\\" + fileName);
                        }
                        File.Move(path, pathTo + "\\" + fileName);
                    }
                    else
                    {
                        throw new IOException();
                    }
                }

                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }

                
            }
            
           
        }

        public void MoveFolder(string str)
        {
            char[] delimeters = { '>' };
            string[] tmp = str.Split(delimeters, System.StringSplitOptions.RemoveEmptyEntries);
            string path = Directory.GetCurrentDirectory();
            string pathTo = tmp[1];


            if (Directory.Exists(path + "\\" + tmp[0]))

            {
                try
                {


                    if (!(Directory.Exists(tmp[1])))
                    {

                        Directory.Move(path + "\\" + tmp[0], tmp[1]);
                        
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {e.Message}");
                    Console.ResetColor();
                }


            }


        }

        public void CreateDirectory(string str)
        {
            char[] delimeters = { '>' };
            string[] tmp = str.Split(delimeters, System.StringSplitOptions.RemoveEmptyEntries);
            string pathTo = tmp[1];

            if(!(Directory.Exists(pathTo + "\\" + tmp[0])))
            {
                Directory.CreateDirectory(tmp[1] + "\\" + tmp[0]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This directory already exists");
                Console.ResetColor();
            }

        }

        public void DeleteDirectory(string str)
        {
            
            string path = Directory.GetCurrentDirectory();
            

            if (Directory.Exists(path + "\\" + str))
            {
                Directory.Delete(path + "\\" + str, true);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This directory already exists");
                Console.ResetColor();
            }
        }

        public void MoveToDirectory(string moveTo)
        {
            if (Directory.Exists(moveTo))
            {
                Directory.SetCurrentDirectory(moveTo);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error this directory doesn't exist");
                Console.ResetColor();
            }
        }

       

        public void ClearConsole()
        {

            Console.Clear();
        }

        public void GetFileAttributes(string str)
        {
            string path = Directory.GetCurrentDirectory() + @"\" + str;
            FileAttributes attrs = File.GetAttributes(path);
            Console.WriteLine(attrs.ToString() + " - attribute");
            
        }

        public void CopyFile(string str)
        {
            char[] delimeters = { '>' };
            string[] tmp = str.Split(delimeters, System.StringSplitOptions.RemoveEmptyEntries);
            string path = tmp[0];
            string filename = tmp[0].Remove(0, tmp[0].LastIndexOf('\\') + 1);
            string pathTo = tmp[1];
            File.Copy(path, tmp[1] + "\\" + filename,true);
        }

        public void CopyDirectoryFirstPart(string str)
        {
            char[] delimeters = { '>' };
            string[] tmp = str.Split(delimeters, System.StringSplitOptions.RemoveEmptyEntries);

            DirectoryInfo source = new DirectoryInfo(tmp[0]);
            DirectoryInfo destination = new DirectoryInfo(tmp[1] + @"\" + source.Name);
            Directory.CreateDirectory(tmp[1] + @"\" + source.Name);
            CopyDirectorySecondPart(source, destination);

        }

        private void CopyDirectorySecondPart(DirectoryInfo source, DirectoryInfo destination)
        {
            
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(destination.FullName, file.Name), true);
            foreach(DirectoryInfo directory in source.GetDirectories())
            {
                DirectoryInfo target = destination.CreateSubdirectory(directory.Name);
                CopyDirectorySecondPart(directory, target);
            }
        }

        public void SearchFile(string str)
        {
            char[] delimeters = { '>' };
            string[] tmp = str.Split(delimeters, System.StringSplitOptions.RemoveEmptyEntries);
            string path = Directory.GetCurrentDirectory() + "\\" + tmp[0];
            if (File.Exists(path))
            {
                File.Delete(path);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success: there's a file with such name");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Failure: there isn't any files with such name");
                Console.ResetColor();
            }
        }

        public void ShowHistory(List<string> history)
        {
            foreach(var a in history)
                Console.WriteLine(a);
        }

        public void GroupFileRename(string str)
        {
            if (str == "true")
            {
                Console.Clear();
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
                int index;
                for(int i = 0; i<files.Length; i++)
                {
                    FileAttributes attributes = File.GetAttributes(files[i]);
                    if(attributes == FileAttributes.System || attributes == FileAttributes.Hidden)
                    {
                        continue;
                    }
                    else
                    {
                        index = files[i].LastIndexOf('\\');
                        Console.Write($"insert a new name for {files[i].Remove(0, index + 1)} --> ");
                        string newName = Convert.ToString(Console.ReadLine());
                        File.Move(files[i], files[i].Remove(index) + "\\" + newName);
                        Console.WriteLine();
                    }
                }
            }
            else
                return;
        }

    }
}
