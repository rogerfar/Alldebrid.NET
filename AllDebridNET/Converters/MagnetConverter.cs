using System.Text.Json;
using System.Text.Json.Serialization;

namespace AllDebridNET;

public class MagnetListConverter : JsonConverter<List<Magnet>>
{
    public override List<Magnet> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.StartArray:
                // If it's already an array, deserialize it directly as a list
                return JsonSerializer.Deserialize<List<Magnet>>(ref reader, options) ?? [];
            case JsonTokenType.StartObject:
            {
                // If it's a single object, create a list with just that object
                var magnet = JsonSerializer.Deserialize<Magnet>(ref reader, options)!;
                return [magnet];
            }
            case JsonTokenType.Null:
                // Handle null case
                return [];
            case JsonTokenType.None:
            case JsonTokenType.EndObject:
            case JsonTokenType.EndArray:
            case JsonTokenType.PropertyName:
            case JsonTokenType.Comment:
            case JsonTokenType.String:
            case JsonTokenType.Number:
            case JsonTokenType.True:
            case JsonTokenType.False:
            default:
                throw new JsonException($"Unexpected token {reader.TokenType} when parsing Magnet list");
        }
    }

    public override void Write(Utf8JsonWriter writer, List<Magnet>? value, JsonSerializerOptions options)
    {
        if (value == null || value.Count == 0)
        {
            writer.WriteNullValue();
            return;
        }

        if (value.Count == 1)
        {
            // If there's only one item, write it as a single object
            JsonSerializer.Serialize(writer, value[0], options);
        }
        else
        {
            // If there are multiple items, write as array
            writer.WriteStartArray();
            foreach (var magnet in value)
            {
                JsonSerializer.Serialize(writer, magnet, options);
            }
            writer.WriteEndArray();
        }
    }
}
