namespace PluginInterface
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginLoad : Attribute
    {
        public List<string> Dependencies { get; private set; }

        public PluginLoad()
            => Dependencies = [];

        public PluginLoad(params string[] dependencies)
            => Dependencies = new List<string>(dependencies);
    }
}
