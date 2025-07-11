using System;
using System.Reflection;
using System.Linq;

namespace task07;

// Атрибут для отображаемого имени (может применяться к классам, методам, свойствам)
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class DisplayNameAttribute : Attribute
{
    // Свойство для хранения отображаемого имени
    public string DisplayName { get; }
    public DisplayNameAttribute(string displayName)
    {
        DisplayName = displayName;
    }
}

// Атрибут для версии класса (только для классов)
[AttributeUsage(AttributeTargets.Class)]
public class VersionAttribute : Attribute
{
    // Свойства для хранения мажорной и минорной версии
    public int Major { get; }
    public int Minor { get; }
    public VersionAttribute(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }
}

// Пример класса с атрибутами
[DisplayName("Пример класса")]
[Version(1, 0)]
public class SampleClass
{
    // Свойство с атрибутом DisplayName
    [DisplayName("Числовое свойство")]
    public int Number { get; set; }

    // Метод с атрибутом DisplayName
    [DisplayName("Тестовый метод")]
    public void TestMethod() { }
}

// Вспомогательный класс для анализа атрибутов через рефлексию
public static class ReflectionHelper
{
    // Метод выводит информацию о типе, его атрибутах, методах и свойствах
    public static void PrintTypeInfo(Type type)
    {
        // Получаем и выводим DisplayName класса через Attribute.GetCustomAttribute
        var displayNameAttr = (DisplayNameAttribute)Attribute.GetCustomAttribute(type, typeof(DisplayNameAttribute));
        if (displayNameAttr != null)
            Console.WriteLine($"DisplayName: {displayNameAttr.DisplayName}");
        else
            Console.WriteLine("DisplayName: <нет>");

        // Получаем и выводим версию класса через Attribute.GetCustomAttribute
        var versionAttr = (VersionAttribute)Attribute.GetCustomAttribute(type, typeof(VersionAttribute));
        if (versionAttr != null)
            Console.WriteLine($"Version: {versionAttr.Major}.{versionAttr.Minor}");
        else
            Console.WriteLine("Version: <нет>");

        // Используем LINQ для поиска методов с атрибутом DisplayName и вывода информации о них
        type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(m => new { Method = m, Attr = (DisplayNameAttribute)Attribute.GetCustomAttribute(m, typeof(DisplayNameAttribute)) })
            .Where(x => x.Attr != null)
            .ToList()
            .ForEach(x => Console.WriteLine($"Method: {x.Method.Name}, DisplayName: {x.Attr.DisplayName}"));

        // Используем LINQ для поиска свойств с атрибутом DisplayName и вывода информации о них
        type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => new { Property = p, Attr = (DisplayNameAttribute)Attribute.GetCustomAttribute(p, typeof(DisplayNameAttribute)) })
            .Where(x => x.Attr != null)
            .ToList()
            .ForEach(x => Console.WriteLine($"Property: {x.Property.Name}, DisplayName: {x.Attr.DisplayName}"));
    }
}
