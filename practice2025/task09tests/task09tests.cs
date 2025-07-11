using System;
using System.IO;
using System.Diagnostics;
using Xunit;

public class InspectorTests
{
    private readonly string _dllPath;
    private readonly string _inspectorPath;

    public InspectorTests()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var solutionRoot = Directory.GetParent(currentDir).Parent.Parent.Parent.FullName;

        _dllPath = Path.Combine(solutionRoot, "FileSystemCommands", "bin", "Debug", "net8.0", "FileSystemCommands.dll");
        _inspectorPath = Path.Combine(solutionRoot, "Inspector", "bin", "Debug", "net8.0", "Inspector.dll");
    }

    private string RunInspector()
    {
        var process = new Process();
        process.StartInfo.FileName = "dotnet";
        process.StartInfo.Arguments = $"\"{_inspectorPath}\" \"{_dllPath}\"";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        return output;
    }

    [Fact]
    public void ShouldPrintClassNames()
    {
        var output = RunInspector();
        Assert.Contains("FindFilesCommand", output);
        Assert.Contains("DirectorySizeCommand", output);
    }

    [Fact]
    public void ShouldPrintAttributeNames()
    {
        var output = RunInspector();
        Assert.Contains("Команда поиска файлов", output);
        Assert.Contains("1.0", output);
    }

    [Fact]
    public void ShouldPrintMethodNames()
    {
        var output = RunInspector();
        Assert.Contains("Execute", output);
    }

    [Fact]
    public void ShouldPrintConstructorNames()
    {
        var output = RunInspector();
        Assert.Contains("FindFilesCommand", output);
        Assert.Contains("DirectorySizeCommand", output);
    }

    [Fact]
    public void ShouldPrintParameterInformation()
    {
        var output = RunInspector();
        Assert.Contains("String", output);
        Assert.Contains("directoryPath", output);
        Assert.Contains("searchPattern", output);
    }

    [Fact]
    public void ShouldHandleMethodsWithoutParameters()
    {
        var output = RunInspector();
        Assert.Contains("Execute", output);
    }
}
