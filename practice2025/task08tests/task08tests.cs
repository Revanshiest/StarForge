using System;
using System.IO;
using System.Diagnostics;
using Xunit;
using FileSystemCommands;

public class FileSystemCommandsTests
{
    [Fact]
    public void DirectorySizeCommand_ShouldCalculateSize()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World");

        var command = new DirectorySizeCommand(testDir);
        command.Execute();

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void FindFilesCommand_ShouldFindMatchingFiles()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

        var command = new FindFilesCommand(testDir, "*.txt");
        command.Execute();

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void CommandRunner_ShouldPrintDirectorySize_And_FoundFiles()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDirForIntegration");
        Directory.CreateDirectory(testDir);
        var file1 = Path.Combine(testDir, "test1.txt");
        var file2 = Path.Combine(testDir, "test2.txt");
        File.WriteAllText(file1, "Hello");
        File.WriteAllText(file2, "World");

        var currentDir = Directory.GetCurrentDirectory();
        var solutionRoot = Directory.GetParent(currentDir).Parent.Parent.Parent.FullName;

        var runnerDir = Path.Combine(solutionRoot, "CommandRunner", "bin", "Debug", "net8.0");

        string runnerPath;
        
        runnerPath = Path.Combine(runnerDir, "CommandRunner.dll");

        var process = new Process();

        process.StartInfo.FileName = "dotnet";
        process.StartInfo.Arguments = $"\"{runnerPath}\" \"{testDir}\"";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        Assert.Contains("10", output);
        Assert.Contains(file1, output);
        Assert.Contains(file2, output);

        Directory.Delete(testDir, true);
    }
}
