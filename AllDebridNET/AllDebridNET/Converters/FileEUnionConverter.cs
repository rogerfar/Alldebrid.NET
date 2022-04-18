using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AllDebridNET.Converters;

public class FileEUnionConverter : JsonConverter
{
    public override Boolean CanConvert(Type t)
    {
        return t == typeof(FileEUnion) || t == typeof(FileEUnion?);
    }

    public override Object ReadJson(JsonReader reader, Type t, Object existingValue, JsonSerializer serializer)
    {
        switch (reader.TokenType)
        {
            case JsonToken.StartObject:
                var objectValue = serializer.Deserialize<FileE1>(reader);

                return new FileEUnion
                {
                    FileE1 = objectValue
                };
            case JsonToken.StartArray:
                var arrayValue = serializer.Deserialize<List<FileE1>>(reader);

                return new FileEUnion
                {
                    PurpleEArray = arrayValue
                };
        }

        throw new Exception("Cannot unmarshal type EUnion");
    }

    public override void WriteJson(JsonWriter writer, Object untypedValue, JsonSerializer serializer)
    {
        var value = (FileEUnion)untypedValue;

        if (value.PurpleEArray != null)
        {
            serializer.Serialize(writer, value.PurpleEArray);

            return;
        }

        if (value.FileE1 != null)
        {
            serializer.Serialize(writer, value.FileE1);

            return;
        }

        throw new Exception("Cannot marshal type EUnion");
    }
}