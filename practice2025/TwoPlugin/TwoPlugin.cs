using PluginInterface;

namespace TwoPlugin;

[PluginLoad("OnePlugin")]
public class TwoPlugin : IPlugin
{    
    public void Execute()
    {
        Console.WriteLine("Выполняется второй плагин");
    }
} 
