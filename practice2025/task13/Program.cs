using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using libraryClass;

namespace task13
{
    public class JsonDateTimeConverter : JsonConverter<DateTime>
    {
        private const string Format = "yyyy-MM-dd";
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => DateTime.ParseExact(reader.GetString(), Format, null);
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(Format));
    }

    public static class JsonStudentService
    {
        public static JsonSerializerOptions GetOptions()
        {
            return new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true,
                Converters = { new JsonDateTimeConverter() }
            };
        }

        public static string Serialize<T>(T student)
        {
            return JsonSerializer.Serialize(student, GetOptions());
        }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, GetOptions());
        }

        public static void SaveToFile(string json, string filePath)
        {
            File.WriteAllText(filePath, json);
        }

        public static string LoadFromFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
