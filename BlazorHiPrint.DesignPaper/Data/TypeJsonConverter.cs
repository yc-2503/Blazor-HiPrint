using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorHiPrint.DesignPaper.Data
{
    public class TypeJsonConverter : JsonConverter<Type>
    {
        public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var typeName = reader.GetString();
            return Type.GetType(typeName) ?? throw new JsonException($"Could not parse type: {typeName}");
        }

        public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.AssemblyQualifiedName);
        }
    }
}
