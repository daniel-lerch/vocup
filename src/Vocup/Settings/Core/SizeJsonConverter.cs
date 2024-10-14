using Avalonia;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vocup.Settings.Core;

public class SizeJsonConverter : JsonConverter<Size>
{
    public override Size Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        double width = 0, height = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();

                if (propertyName == "Width")
                {
                    reader.Read();
                    width = reader.GetDouble();
                }
                else if (propertyName == "Height")
                {
                    reader.Read();
                    height = reader.GetDouble();
                }
                else
                {
                    reader.Skip();
                }
            }
            else if (reader.TokenType == JsonTokenType.EndObject)
            {
                return new Size(width, height);
            }
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Size value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("Width");
        writer.WriteNumberValue(value.Width);
        writer.WritePropertyName("Height");
        writer.WriteNumberValue(value.Height);
        writer.WriteEndObject();
    }
}
