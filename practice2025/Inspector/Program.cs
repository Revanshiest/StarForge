using System;
using System.IO;
using System.Linq;
using System.Reflection;
using CommonAttributes;

class Inspector
{
    static void Main(string[] args)
    {
        if (args.Length == 0 || !File.Exists(args[0]))
            return;

        var asm = Assembly.LoadFrom(args[0]);
        asm.GetTypes()
            .Where(t => t.IsClass)
            .ToList()
            .ForEach(type =>
            {
                Console.WriteLine(type.Name);

                type.GetCustomAttributes(false)
                    .ToList()
                    .ForEach(attr =>
                    {
                        if (attr is DisplayNameAttribute displayName)
                            Console.WriteLine(displayName.Name);
                        else if (attr is VersionAttribute version)
                            Console.WriteLine(version.Version);
                        else
                            Console.WriteLine(attr.GetType().Name);
                    });

                type.GetConstructors()
                    .ToList()
                    .ForEach(ctor =>
                    {
                        Console.WriteLine(type.Name);
                        ctor.GetParameters()
                            .ToList()
                            .ForEach(p => Console.WriteLine($"{p.ParameterType.Name} {p.Name}"));
                    });

                type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                    .ToList()
                    .ForEach(method =>
                    {
                        Console.WriteLine(method.Name);
                        method.GetCustomAttributes(false)
                            .Select(attr => attr.GetType().Name)
                            .ToList()
                            .ForEach(Console.WriteLine);
                        method.GetParameters()
                            .ToList()
                            .ForEach(p => Console.WriteLine($"{p.ParameterType.Name} {p.Name}"));
                    });
            });
    }
}
