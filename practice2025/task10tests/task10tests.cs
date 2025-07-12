using System.IO;
using PluginLoader;

namespace PluginLoaderTests;

public class PluginLoaderTests
{        
    [Fact]
    public void PluginLoader_ShouldExecuteAllPlugins_InCorrectOrder()
    {
        var pluginDirectory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Plugins");
        var pluginLoader = new PluginLoader.PluginLoader();

        var consoleOutput = new StringWriter();
        var originalOutput = Console.Out;
        Console.SetOut(consoleOutput);

        pluginLoader.LoadAndExecutePlugins(pluginDirectory);

        var output = consoleOutput.ToString();

        int idx1 = output.IndexOf("первый");
        int idx2 = output.IndexOf("второй");
        int idx3 = output.IndexOf("третий");
        int idx4 = output.IndexOf("четвертый");

        Assert.True(idx1 >= 0, "Не найден первый плагина");
        Assert.True(idx2 > idx1, "Второй не после первого");
        Assert.True(idx3 > idx2, "Третий не после второго");
        Assert.True(idx4 > idx3, "Четвертый не после третьего");

        Console.SetOut(originalOutput);
    }
    
    [Fact]
    public void PluginLoader_ShouldNotOutputAnything_WhenNoPluginsInDirectory()
    {
        var emptyDir = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "EmptyDir");
        var pluginLoader = new PluginLoader.PluginLoader();

        var consoleOutput = new StringWriter();
        var originalOutput = Console.Out;
        Console.SetOut(consoleOutput);

        pluginLoader.LoadAndExecutePlugins(emptyDir);

        var output = consoleOutput.ToString();
        Assert.True(string.IsNullOrWhiteSpace(output));

        Console.SetOut(originalOutput);
    }
}
