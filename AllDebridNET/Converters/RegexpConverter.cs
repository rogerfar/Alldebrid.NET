using System.Text.Json;
using System.Text.Json.Serialization;

namespace AllDebridNET;

public class RegexpConverter : JsonConverter<List<String>>
{
    public override List<String> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                return [reader.GetString() ?? String.Empty];
            case JsonTokenType.StartArray:
            {
                var list = new List<String>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                    {
                        break;
                    }

                    if (reader.TokenType == JsonTokenType.String)
                    {
                        list.Add(reader.GetString() ?? String.Empty);
                    }
                }
                return list;
            }
            case JsonTokenType.None:
            case JsonTokenType.StartObject:
            case JsonTokenType.EndObject:
            case JsonTokenType.EndArray:
            case JsonTokenType.PropertyName:
            case JsonTokenType.Comment:
            case JsonTokenType.Number:
            case JsonTokenType.True:
            case JsonTokenType.False:
            case JsonTokenType.Null:
            default:
                throw new JsonException($"Unexpected token {reader.TokenType} when parsing regexp");
        }
    }

    public override void Write(Utf8JsonWriter writer, List<String>? value, JsonSerializerOptions options)
    {
        if (value == null || value.Count == 0)
        {
            writer.WriteNullValue();
            return;
        }

        if (value.Count == 1)
        {
            // If there's only one item, write it as a single string
            writer.WriteStringValue(value[0]);
        }
        else
        {
            // If there are multiple items, write as array
            writer.WriteStartArray();
            foreach (var item in value)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();
        }
    }
}