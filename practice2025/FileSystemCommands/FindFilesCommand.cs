using System;
using System.IO;
using System.Linq;
using CommandLib;
using CommonAttributes;

namespace FileSystemCommands
{
    [DisplayName("Команда поиска файлов")]
    [Version("1.0")]
    public class FindFilesCommand : ICommand
    {
        private readonly string _directoryPath;
        private readonly string _searchPattern;

        public FindFilesCommand(string directoryPath, string searchPattern)
        {
            _directoryPath = directoryPath;
            _searchPattern = searchPattern;
        }

        [DisplayName("Выполнить поиск")]
        public void Execute()
        {
            Directory.GetFiles(_directoryPath, _searchPattern)
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
} 
