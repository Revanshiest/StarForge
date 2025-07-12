using System.Reflection;
using PluginInterface;

namespace PluginLoader
{
    public class PluginLoader
    {
        public void LoadAndExecutePlugins(string path)
        {
            var pluginDlls = Directory.EnumerateFiles(path, "*.dll", SearchOption.TopDirectoryOnly)
                .Where(f => Path.GetFileName(f) != "PluginInterface.dll" && Path.GetFileName(f) != "task10.dll")
                .ToList();

            var assemblies = pluginDlls.Select(Assembly.LoadFrom).ToList();

            var pluginTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract && t.GetCustomAttributes(typeof(PluginLoad), false).Any())
                .ToList();

            var pluginInfos = pluginTypes
                .Select(t => new
                {
                    Type = t,
                    Name = t.Name,
                    Dependencies = ((PluginLoad)t.GetCustomAttributes(typeof(PluginLoad), false).First()).Dependencies
                })
                .ToList();

            var nameToInfo = pluginInfos.ToDictionary(p => p.Name);

            IEnumerable<string> BFS(IEnumerable<string> front, HashSet<string> visited)
            {
                if (!front.Any()) return Enumerable.Empty<string>();
                foreach (var name in front)
                    visited.Add(name);
                var nextFront = pluginInfos
                    .Where(p => !visited.Contains(p.Name) && p.Dependencies.All(d => visited.Contains(d)))
                    .Select(p => p.Name)
                    .Except(front)
                    .ToList();
                return front.Concat(BFS(nextFront, visited));
            }

            var initialFront = pluginInfos.Where(p => !p.Dependencies.Any()).Select(p => p.Name).ToList();
            var order = BFS(initialFront, new HashSet<string>()).ToList();

            File.WriteAllText("order.txt", string.Join(", ", order));

            order
                .Select(name => nameToInfo[name].Type)
                .Select(t => (IPlugin)Activator.CreateInstance(t)!)
                .ToList()
                .ForEach(plugin => plugin.Execute());

        }
    }
}
