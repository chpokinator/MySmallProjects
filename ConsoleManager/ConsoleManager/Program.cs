using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleManager
{
    class Program
    {


        static void Main(string[] args)
        {
           
            CommandLine commandline = new CommandLine();
            FileCreatorReader files = new FileCreatorReader();

            Dictionary<string, Delegate> commands = new Dictionary<string, Delegate>
            {
                {"mf", new Action<string>(commandline.MoveFile)},
                {"cd", new Action<string>(commandline.MoveToDirectory)},
                {"ls", new Action<string>(commandline.ShowAllInCurrentDirectory)},
                {"md", new Action<string>(commandline.MoveFolder)},
                {"cl", new Action(commandline.ClearConsole) },
                {"crtxt", new Action<string>(files.CreateTxtFile) },
                {"optxt", new Action<string>(files.ReadTxtFile) },
                {"cpd", new Action<string>(commandline.CopyDirectoryFirstPart) },
                {"cpf", new Action<string>(commandline.CopyFile) },
                {"dd", new Action<string>(commandline.DeleteDirectory) },
                {"crdir", new Action<string>(commandline.CreateDirectory) },
                {"sf", new Action<string>(commandline.SearchFile) },
                {"history", new Action<List<string>>(commandline.ShowHistory) },
                {"gr", new Action<string>(commandline.GroupFileRename) },
                {"sha", new Action<string>(commandline.GetFileAttributes) },
                {"help", new Action<string>(files.ReadHelpFile) }
            };

            string pathByDefault = Directory.GetCurrentDirectory();
            List<string> history = new List<string>();
            PseudoGraphicManager graphics = new PseudoGraphicManager(commands);
            commands.Add("graphics", new Action(graphics.ChooseTwoDirectories));

            

            string[] delimeters = { ">>" };

            while(true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"\n{Directory.GetCurrentDirectory()} >>> ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    string workerString = Convert.ToString(Console.ReadLine());
                    Console.WriteLine();
                    Console.ResetColor();
                    string[] str = workerString.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                    string commandsHistory = str[0];

                    if (str.Length == 1)
                    {
                        history.Add(commandsHistory);
                    }
                    else
                    {
                        commandsHistory += $">>{str[1]}";
                        history.Add(commandsHistory);
                    }

                    if (str[0] == "ls")
                        commands[str[0]]?.DynamicInvoke(Directory.GetCurrentDirectory());
                    else if (str[0] == "cl")
                    {
                        history.Clear();
                        commands[str[0]]?.DynamicInvoke();
                    }
                    else if (str[0] == "history")
                        commands[str[0]]?.DynamicInvoke(history);
                    else if (str[0] == "help")
                        commands[str[0]]?.DynamicInvoke(pathByDefault);
                    else if (str[0] == "graphics")
                        commands[str[0]]?.DynamicInvoke();


                    else
                    {
                        commands[str[0]]?.DynamicInvoke(str[1]);
                    }


                    
                

                }
                catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
            
            
            

        }
    }
}
