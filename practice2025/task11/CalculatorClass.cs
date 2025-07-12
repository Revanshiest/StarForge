using System.Reflection;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.Loader;

namespace task11;

public interface ICalculator
{
    int Add(int a, int b);
    int Minus(int a, int b);
    int Mul(int a, int b);
    int Div(int a, int b);
}

public static class CalculatorClass
{
    public static ICalculator CreateCalculator()
    {
        string code = @"
                        using System;
                        public class Calculator : task11.ICalculator
                        {
                            public int Add(int a, int b) => a + b;
                            public int Minus(int a, int b) => a - b;
                            public int Mul(int a, int b) => a * b;
                            public int Div(int a, int b) => a / b;
                        }";

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
            .Select(a => MetadataReference.CreateFromFile(a.Location))
            .Cast<MetadataReference>();

        var compilation = CSharpCompilation.Create(
            "DynamicCalculator",
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using var ms = new MemoryStream();
        compilation.Emit(ms);
        ms.Seek(0, SeekOrigin.Begin);
        
        var loadContext = new AssemblyLoadContext("CalculatorContext", isCollectible: true);
        var assembly = loadContext.LoadFromStream(ms);
        var type = assembly.GetType("Calculator");
        
        if (type == null)
        {
            throw new InvalidOperationException("Calculator type not found in assembly");
        }
        
        var instance = Activator.CreateInstance(type);

        if (instance == null)
        {
            throw new InvalidOperationException("Failed to create Calculator instance");
        }
        
        return (ICalculator)instance;
    }
}
