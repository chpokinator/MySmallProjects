using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleManager
{
    class PseudoGraphicManager
    {
        const char border = '║';
        char borderDirectory = '|';
        char horizonBorder = '-';

        string directoryLeftSide;
        string directoryRightSide;

        int currentDirectory = 0;
        int cursorPosition = 0;

        DriveInfo[] drives = DriveInfo.GetDrives();

        Dictionary<string, Delegate> commands = new Dictionary<string, Delegate>();
        
        public PseudoGraphicManager(Dictionary<string, Delegate> commands)
        {
            this.commands = commands;
        }

        public void ChooseTwoDirectories()
        {
            int i = 0;
            int setDirectories = 0;
            

            while (true)
            {
                
                Console.Clear();
               
                for(int j = 0;j<drives.Length;j++)
                {
                    
                    
                    if (j == i)
                    {
                        
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"{drives[j].Name}");
                        Console.ResetColor();
                    }
                    else
                        Console.WriteLine($"{drives[j].Name}");

                }

                
                
                switch(Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        {
                            if (setDirectories == 0)
                            {
                                directoryLeftSide = drives[i].Name;
                                setDirectories++;
                            }
                            else
                            {
                                directoryRightSide = drives[i].Name;
                                setDirectories++;
                            }
                           
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (i - 1 < 0)
                            {
                                i = 0;
                                break;
                            }
                            else
                                i--;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (i + 1 > drives.Length - 1)
                            {
                                i = drives.Length - 1;
                                break;
                            }
                            else
                                i++;
                            break;
                        }
                }

                if (setDirectories == 2)
                {
                    Console.Clear();
                    Graphics();
                    break;
                }
            }
            
        }
        
        public void Graphics()
        {
            bool check = true;
            
            while (check)
            {
              
                try
                {


                    Console.Clear();
                    DrawGraphics();

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Enter:
                            {
                                if (currentDirectory == 0)
                                {
                                    CheckDirectoryAndChangeIt(getLeftSide()[cursorPosition].FullName, currentDirectory);
                                }
                                else
                                {
                                    CheckDirectoryAndChangeIt(getRightSide()[cursorPosition].FullName, currentDirectory);
                                }
                                break;
                            }
                        case ConsoleKey.Escape:
                            {
                                if (currentDirectory == 0)
                                {
                                    CheckDirectoryAndChangeIt("..", currentDirectory);

                                }
                                else
                                {
                                    CheckDirectoryAndChangeIt("..", currentDirectory);
                                }
                                break;
                            }
                        case ConsoleKey.UpArrow:
                            {

                                if (cursorPosition - 1 < 0)
                                    cursorPosition = 0;
                                else
                                    cursorPosition--;


                                break;
                            }
                        case ConsoleKey.DownArrow:
                            {
                                if (currentDirectory == 0)
                                {
                                    if (cursorPosition + 1 == getLeftSide().Length)
                                        cursorPosition = getLeftSide().Length - 1;
                                    else
                                        cursorPosition++;
                                }
                                else
                                {
                                    if (cursorPosition + 1 == getRightSide().Length)
                                        cursorPosition = getRightSide().Length - 1;
                                    else
                                        cursorPosition++;
                                }

                                break;
                            }
                        case ConsoleKey.Tab:
                            {
                                if(currentDirectory==0)
                                {
                                    currentDirectory++;
                                    cursorPosition = 0;
                                }
                                else
                                {
                                    currentDirectory = 0;
                                    cursorPosition = 0;
                                }
                                break;
                            }
                        case ConsoleKey.F1:
                            {
                                ChooseTwoDirectories();
                                break;
                            }
                        case ConsoleKey.F5:
                            {
                                if(currentDirectory ==0)
                                {
                                    string str = getLeftSide()[cursorPosition].FullName + ">" + directoryRightSide;
                                    commands["mf"]?.DynamicInvoke(str);
                                }
                                else
                                {
                                    string str = getRightSide()[cursorPosition].FullName + ">" + directoryLeftSide;
                                    commands["mf"]?.DynamicInvoke(str);
                                }
                                break;
                            }
                        case ConsoleKey.F4:
                            {
                                if (currentDirectory == 0)
                                {
                                    string str = getLeftSide()[cursorPosition].FullName + ">" + directoryRightSide;
                                    commands["cpf"]?.DynamicInvoke(str);
                                }
                                else
                                {
                                    string str = getRightSide()[cursorPosition].FullName + ">" + directoryLeftSide;
                                    commands["cpf"]?.DynamicInvoke(str);
                                }
                                break;
                            }
                        case ConsoleKey.F7:
                            {
                                check = false;
                                break;
                            }
                    }
                }
                catch(Exception ex)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: you tried to move to system or hidden location or tried to complete command with wrong arguments");
                    Console.ResetColor();
                    Console.ReadKey();
                    ChooseTwoDirectories();
                }
            }

            Console.Clear();

        }
        
        public void DrawGraphics()
        {
            DirectoryInfo infoLeft = new DirectoryInfo(directoryLeftSide);
            DirectoryInfo infoRight = new DirectoryInfo(directoryRightSide);
            FileSystemInfo[] leftDraw = infoLeft.GetFileSystemInfos();
            FileSystemInfo[] rightDraw = infoRight.GetFileSystemInfos();


            Console.SetCursorPosition(3, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{borderDirectory}{infoLeft.FullName}{borderDirectory}");
            Console.ResetColor();
            Console.SetCursorPosition(0, 2);

            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(horizonBorder);

            for (int j = 0; j < leftDraw.Length + rightDraw.Length; j++)
            {
                
                if (j >= leftDraw.Length)
                {
                    Console.WriteLine(border);
                }
                else
                {
                    Console.Write(border + " ");
                    if (currentDirectory == 0)
                    {
                        if (cursorPosition == j)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"{leftDraw[j].Name,-57}");
                            Console.ResetColor();
                        }
                        else
                            Console.WriteLine(leftDraw[j].Name);

                    }
                    else
                        Console.WriteLine(leftDraw[j].Name);

                }



            }

            Console.SetCursorPosition(Console.WindowWidth/2 + 3, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{borderDirectory}{infoRight.FullName}{borderDirectory}");
            Console.ResetColor();

            for (int y = 0; y < rightDraw.Length + leftDraw.Length; y++)
            {
                if (y >= rightDraw.Length)
                {
                    Console.SetCursorPosition(Console.WindowWidth - 1, y + 3);
                    Console.Write(border);
                    Console.SetCursorPosition((Console.WindowWidth / 2), y + 3);
                    Console.Write(border);
                }
                else
                {
                    Console.SetCursorPosition((Console.WindowWidth / 2), y + 3);
                    Console.Write(border + " ");
                    if (currentDirectory == 1)
                    {
                        if (cursorPosition == y)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write($"{rightDraw[y].Name,-57}");
                            Console.ResetColor();
                            Console.SetCursorPosition(Console.WindowWidth - 1, y + 3);
                            Console.Write(border);
                        }
                        else
                        {
                            Console.Write(rightDraw[y].Name);
                            Console.SetCursorPosition(Console.WindowWidth - 1, y + 3);
                            Console.Write(border);
                        }
                    }
                    else
                    {
                        Console.Write(rightDraw[y].Name);
                        Console.SetCursorPosition(Console.WindowWidth - 1, y + 3);
                        Console.Write(border);
                    }

                }



            }
            Console.WriteLine();
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(horizonBorder);
            Console.WriteLine("f5 to cut file | f4 to copy file | f1 to change discs | esc to move upper | tab to change window | f7 to return to console");
            Console.SetCursorPosition(0, cursorPosition);
        }

        private FileSystemInfo[] getLeftSide()
        {
            DirectoryInfo infoLeft = new DirectoryInfo(directoryLeftSide);
            FileSystemInfo[] leftDraw = infoLeft.GetFileSystemInfos();

            return leftDraw;
        }

        private FileSystemInfo[] getRightSide()
        {
            DirectoryInfo infoRight = new DirectoryInfo(directoryRightSide);
            FileSystemInfo[] rightDraw = infoRight.GetFileSystemInfos();

            return rightDraw;
        }
        public void CheckDirectoryAndChangeIt(string path, int whichDirectory)
        {
            try
            {
                if (path == "..")
                {
                    if (whichDirectory == 0)
                    {
                        string newPath = directoryLeftSide.Remove(directoryLeftSide.LastIndexOf('\\'));
                        if (Directory.Exists(newPath))
                            directoryLeftSide = newPath;

                    }
                    else
                    {
                        string newPath = directoryRightSide.Remove(directoryRightSide.LastIndexOf('\\'));
                        if (Directory.Exists(newPath))
                            directoryRightSide = newPath;

                    }
                }
                else
                {
                    if (Directory.Exists(path))
                    {
                        if (whichDirectory == 0)
                        {
                            directoryLeftSide = path;
                        }
                        else
                        {
                            directoryRightSide = path;
                        }

                        cursorPosition = 0;
                    }
                    else
                        return;
                }
            }
            catch(Exception ex)
            {
                ChooseTwoDirectories();
            }

            
        }

    }
}
