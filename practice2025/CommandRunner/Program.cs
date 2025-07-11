using System;
using System.IO;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string dirPath = args[0];

            string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileSystemCommands.dll");
            if (!File.Exists(dllPath))
            {
                Console.WriteLine("Ошибка");
                return;
            }
            var asm = Assembly.LoadFrom(dllPath);

            var sizeType = asm.GetTypes().FirstOrDefault(t => t.Name == "DirectorySizeCommand");
            if (sizeType != null)
            {
                var sizeCmd = Activator.CreateInstance(sizeType, dirPath);
                sizeType.GetMethod("Execute")?.Invoke(sizeCmd, null);
            }

            var findType = asm.GetTypes().FirstOrDefault(t => t.Name == "FindFilesCommand");
            if (findType != null)
            {
                var findCmd = Activator.CreateInstance(findType, dirPath, "*.txt");
                findType.GetMethod("Execute")?.Invoke(findCmd, null);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("EXCEPTION: " + ex);
        }
    }
} 
