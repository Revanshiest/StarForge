using System;
using System.IO;
using System.Linq;
using CommandLib;
using CommonAttributes;

namespace FileSystemCommands
{
    [DisplayName("Команда подсчёта размера директории")]
    [Version("1.0")]
    public class DirectorySizeCommand : ICommand
    {
        private readonly string _directoryPath;

        public DirectorySizeCommand(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public void Execute()
        {
            var totalSize = Directory.GetFiles(_directoryPath)
                .Select(f => new FileInfo(f).Length)
                .Sum();
            Console.WriteLine(totalSize);
        }
    }
} 
