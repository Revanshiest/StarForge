using PluginInterface;

namespace FourPlugin;

[PluginLoad("OnePlugin", "ThreePlugin")]
public class FourPlugin : IPlugin
{    
    public void Execute()
    {
        Console.WriteLine("Выполняется четвертый плагин");
    }
} 
