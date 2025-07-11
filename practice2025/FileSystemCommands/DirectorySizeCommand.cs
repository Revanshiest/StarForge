using System;
using System.IO;
using System.Linq;
using CommandLib;

namespace FileSystemCommands
{
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
