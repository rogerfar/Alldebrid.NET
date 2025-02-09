using System.Text.Json.Serialization;
using System.Text.Json;

namespace AllDebridNET.Converters;

public class StringToInt64Converter : JsonConverter<Int64>
{
    public override Int64 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.String when Int64.TryParse(reader.GetString(), out var result) => result,
            JsonTokenType.String => throw new JsonException("Could not parse string value to Int64."),
            JsonTokenType.Number => reader.GetInt64(),
            _ => throw new JsonException($"Unexpected token {reader.TokenType} when parsing Int64.")
        };
    }

    public override void Write(Utf8JsonWriter writer, Int64 value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}