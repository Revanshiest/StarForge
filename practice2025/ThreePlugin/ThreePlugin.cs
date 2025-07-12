using PluginInterface;

namespace ThreePlugin;

[PluginLoad("TwoPlugin")]
public class ThreePlugin : IPlugin
{    
    public void Execute()
    {
        Console.WriteLine("Выполняется третий плагин");
    }
} 
