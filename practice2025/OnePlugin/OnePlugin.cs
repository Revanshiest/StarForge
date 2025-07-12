using PluginInterface;

namespace OnePlugin;

[PluginLoad]
public class OnePlugin : IPlugin
{
    public void Execute()
    {
        Console.WriteLine("Выполняется первый плагин");
    }
} 
