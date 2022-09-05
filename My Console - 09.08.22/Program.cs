using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MyConsole

namespace Homework_9._08._22_MyConsole_
{
    class Program
    {
        static void Main(string[] args)
        {
            string server = null;
            List<string> history = new List<string>(20);
            DirectoryInfo curPath = new DirectoryInfo(Directory.GetCurrentDirectory());
            Console.WriteLine("\tMy console\n");

            while (server != "exit")
            {
                Console.Write($"{curPath} $ ");
                server = Console.ReadLine();
                history.Add($"{DateTime.Now}\t-> {server}");

                string[] subSer = server.Split(' ');
                if (subSer.Length == 2 && subSer[0] == "help")
                    server = "help key";
                else if (subSer.Length == 2 && subSer[0] == "dir")
                    server = "dir key";
                else if (subSer.Length == 3 && subSer[0] == "move")
                    server = "move to";
                else if (subSer.Length == 2 && subSer[0] == "cd")
                    server = "cd ";
                else if (subSer.Length == 2 && subSer[0] == "del")
                    server = "del to";
                else if (subSer.Length == 2 && subSer[0] == "mkdir")
                    server = "mkdir to";
                else if (subSer.Length == 3 && subSer[0] == "copy" && subSer[1] != "con")
                    server = "copy to";
                else if (subSer.Length == 3 && subSer[0] == "copy" && subSer[1] == "con")
                    server = "create file";
                else if (subSer.Length == 3 && subSer[0] == "find") // find путь что ищем?
                    server = "find to";
                else if (subSer.Length == 2 && subSer[0] == "type")
                    server = "type to";
                else if (subSer.Length == 3 && subSer[0] == "rename")
                    server = "rename to to";

                Console.WriteLine();
                switch (server)
                {
                    case "help":
                        Console.WriteLine("help\t - show all commands");
                        Console.WriteLine("cls\t - clear");
                        Console.WriteLine("move\t - move file or folder");
                        Console.WriteLine("dir\t - listing files and folders");
                        Console.WriteLine("cd\t - change current folder"); // исправил
                        Console.WriteLine("copy\t - copy files");
                        Console.WriteLine("copy con\t - create *txt file and write in");// copy con name.txt
                        Console.WriteLine("del\t - delete files");
                        Console.WriteLine("mkdir\t - create folder");
                        Console.WriteLine("find\t - find files or directories");// find where what
                        Console.WriteLine("type\t - read .txt file");// type name.txt
                        Console.WriteLine("rename\t - change name .txt file one or all");// rename test1.txt test2.txt / rename *.txt test.txt
                        Console.WriteLine("history\t - history of entered command");
                        break;
                    case "help key":
                        try
                        {
                            switch (subSer[1])
                            {
                                case "help":
                                    Console.WriteLine("Output of reference information about commands of Windows");
                                    Console.WriteLine("help [<command>]");
                                    break;
                                case "cls":
                                    Console.WriteLine("Clears the contents of the screen\n\nCLS");
                                    break;
                                case "dir":
                                    Console.WriteLine("dir /B\t - output only name files and folders");
                                    Console.WriteLine("dir /A\t - output info all files and folders");
                                    Console.WriteLine("dir /C\t - sort files and folders by creation time");
                                    break;
                                case "move":
                                    Console.WriteLine("Move file from <path1> in <path2");
                                    break;
                                case "rename":
                                    Console.WriteLine("rename name_folder1 name_folder2\t - change one .txt file");
                                    Console.WriteLine("rename *.txt name_folder\t - change names of group .txt files");
                                    break;
                                default:
                                    Console.WriteLine("Excuse me!Non command!!!");
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    case "cls":
                        Console.Clear();
                        break;
                    case "dir":
                        int directory = 0;
                        int files = 0;
                        if (curPath.GetDirectories().Length == 0)
                            Console.WriteLine("Non directories!");
                        else
                        {
                            foreach (var item in curPath.GetDirectories())
                            {
                                Console.WriteLine(item);
                                directory++;
                            }
                        }
                        if (curPath.GetFiles().Length == 0)
                            Console.WriteLine("Non files!");
                        else
                        {
                            foreach (var item in curPath.GetFiles())
                            {
                                Console.WriteLine("{0}:\t , {1}kb", item.Name, item.Length);
                                files++;
                            }
                        }
                        Console.WriteLine($"{directory}\t - quantity of folders");
                        Console.WriteLine($"{files}\t - quantity of files");
                        break;
                    case "dir key":
                        switch (subSer[1])
                        {
                            case "/a":
                                foreach (var item in curPath.GetFiles())
                                {
                                    Console.WriteLine($"{item.Attributes} - {item.Name.Substring(0, item.Name.IndexOf(".")),-20} {item.Extension,5}\t{item.Length}b\tcreation time - {item.CreationTime}");
                                }
                                foreach (var item in curPath.GetDirectories())
                                {
                                    Console.WriteLine($"{item.Attributes} - {item.Name}\t\t{item.Root.Name}\tcreation time - {item.CreationTime}");
                                }
                                break;
                            case "/b":
                                foreach (var item in curPath.GetFiles())
                                {
                                    Console.WriteLine(item.Name);
                                }
                                foreach (var item in curPath.GetDirectories())
                                {
                                    Console.WriteLine(item.Name);
                                }
                                break;
                            case "/c":
                                string tmp;
                                string[] sortFi = Directory.GetFiles(curPath.FullName);
                                Console.WriteLine($"\n\t\tFiles = {sortFi.Length}");
                                for (int i = 0; i < sortFi.Length; i++)
                                {
                                    for (int j = 0; j < sortFi.Length - 1; j++)
                                    {
                                        if (File.GetCreationTime(sortFi[j]) > File.GetCreationTime(sortFi[j + 1]))
                                        {
                                            tmp = sortFi[j];
                                            sortFi[j] = sortFi[j + 1];
                                            sortFi[j + 1] = tmp;
                                        }
                                    }
                                }
                                foreach (var item in sortFi)
                                {
                                    Console.WriteLine($"{item} - ");
                                    Console.WriteLine($"\t\t{File.GetCreationTime(item)}");
                                }

                                string[] sortDi = Directory.GetDirectories(curPath.FullName);
                                Console.WriteLine($"\n\t\tDirectories = {sortDi.Length}");
                                for (int i = 0; i < sortDi.Length; i++)
                                {
                                    for (int j = 0; j < sortDi.Length - 1; j++)
                                    {
                                        if (File.GetCreationTime(sortDi[j]) > File.GetCreationTime(sortDi[j + 1]))
                                        {
                                            tmp = sortDi[j];
                                            sortDi[j] = sortDi[j + 1];
                                            sortDi[j + 1] = tmp;
                                        }
                                    }
                                }
                                foreach (var item in sortDi)
                                {
                                    Console.WriteLine($"{item} - ");
                                    Console.WriteLine($"\t\t{File.GetCreationTime(item)}");
                                }
                                break;
                            default:
                                Console.WriteLine("Command is not exsisted");
                                break;
                        }
                        break;
                    case "move to":
                        try
                        {
                            Directory.Move(subSer[1], subSer[2]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    case "cd":
                        Console.WriteLine(curPath);
                        break;
                    case "cd..":
                        try
                        {
                            curPath = new DirectoryInfo(curPath.Parent.FullName);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    case "cd ":
                        try
                        {
                            bool flag = false;
                            if (subSer[1] == "")
                            {
                                Console.WriteLine("I don`t know this command!!!");
                                break;
                            }
                            foreach (var item in curPath.GetDirectories())
                            {
                                if (item.Name == subSer[1])
                                {
                                    curPath = new DirectoryInfo($"{curPath}\\{item}");
                                    flag = true;
                                    break;
                                }
                            }
                            if (flag == false)
                            {
                                bool flag2 = false;
                                string mon = null;
                                string[] splitCur = curPath.FullName.Split('\\');
                                foreach (var item in splitCur)
                                {
                                    mon += $"{item}\\";
                                    if (item == subSer[1])
                                    {
                                        flag2 = true;
                                        break;
                                    }
                                }
                                if (flag2 == true)
                                    curPath = new DirectoryInfo(mon);
                                else
                                    Console.WriteLine("\t\tI couldn`t find this directory!!!");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    case "copy to":
                        try
                        {
                            string sourseFile = Path.Combine(curPath.FullName, subSer[1]);
                            string destFile = Path.Combine(curPath.FullName + "\\" + subSer[2], subSer[1]);
                            Directory.CreateDirectory(curPath.FullName + "\\" + subSer[2]);
                            File.Copy(sourseFile, destFile, true);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    case "create file":
                        Console.WriteLine("Hello");
                        Console.WriteLine(subSer[2]);
                        try
                        {
                            FileInfo newfi = new FileInfo(subSer[2]);
                            Console.WriteLine();
                            string str = Console.ReadLine();
                            File.WriteAllText($"{newfi.Name}", str);
                            Console.WriteLine("\t\tSuccessful create .txt file!!!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }

                        break;
                    case "type to":
                        try
                        {
                            if (File.Exists($"{curPath.FullName}\\{subSer[1]}") == true)
                                Console.WriteLine(File.ReadAllText(subSer[1]));
                            else
                                Console.WriteLine("\t\tNon file!!!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    case "del to":
                        if (File.Exists(curPath.FullName + "\\" + subSer[1]))
                        {
                            try
                            {
                                File.Delete(curPath.FullName + "\\" + subSer[1]);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else
                            Console.WriteLine("I can`t find!!!");
                        break;
                    case "mkdir to":
                        try
                        {
                            curPath.CreateSubdirectory(subSer[1]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    case "find to":
                        try
                        {
                            string searchPattern = $"{subSer[2]}*";

                            DirectoryInfo di = new DirectoryInfo(subSer[1]);
                            DirectoryInfo[] directories =
                                di.GetDirectories(searchPattern, SearchOption.TopDirectoryOnly);

                            FileInfo[] fi =
                                di.GetFiles(searchPattern, SearchOption.TopDirectoryOnly);

                            foreach (var item in directories)
                            {
                                Console.WriteLine(
                                    "{0,-25} {1,25}", item.FullName, item.LastWriteTime);
                            }

                            Console.WriteLine();

                            foreach (var item in fi)
                            {
                                Console.WriteLine(
                                    "{0,-25} {1,25}", item.Name, item.LastWriteTime);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }

                        break;
                    case "rename to to":
                        if (subSer[1] == "*.txt")
                        {
                            int count = 0;
                            foreach (var item in curPath.GetFiles("*.txt"))
                            {
                                if (count == 0)
                                {
                                    try
                                    {
                                        using (var sr = new StreamReader(item.Name, Encoding.UTF8))
                                        {
                                            using (var sw = new StreamWriter(subSer[2], false, Encoding.UTF8))
                                            {
                                                while (!sr.EndOfStream)
                                                {
                                                    sw.Write(sr.ReadLine());
                                                }
                                            }
                                        }
                                        count++;
                                        File.Delete(item.Name);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error: {ex.Message}");
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        string[] str = subSer[2].Split('.');
                                        string newStr = $"{str[0]}{count}.{str[1]}";
                                        using (var sr = new StreamReader(item.Name, Encoding.UTF8))
                                        {
                                            using (var sw = new StreamWriter(newStr, false, Encoding.UTF8))
                                            {
                                                while (!sr.EndOfStream)
                                                {
                                                    sw.Write(sr.ReadLine());
                                                }
                                            }
                                        }
                                        count++;
                                        File.Delete(item.Name);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error: {ex.Message}");
                                    }
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                using (var sr = new StreamReader(subSer[1], Encoding.UTF8))
                                {
                                    using (var sw = new StreamWriter(subSer[2], false, Encoding.UTF8))
                                    {
                                        while (!sr.EndOfStream)
                                        {
                                            sw.Write(sr.ReadLine());
                                        }
                                    }
                                }
                                File.Delete(subSer[1]);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        break;
                    case "history":
                        try
                        {
                            int start = 1;
                            foreach (var item in history)
                            {
                                Console.WriteLine($"{start}\t-> {item}");
                                start++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    default:
                        Console.WriteLine("Non command!!!");
                        break;
                    case "exit":
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
