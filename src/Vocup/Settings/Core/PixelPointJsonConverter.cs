using Avalonia;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vocup.Settings.Core;

public class PixelPointJsonConverter : JsonConverter<PixelPoint>
{
    public override PixelPoint Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        int x = 0, y = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();

                if (propertyName == "X")
                {
                    reader.Read();
                    x = reader.GetInt32();
                }
                else if (propertyName == "Y")
                {
                    reader.Read();
                    y = reader.GetInt32();
                }
                else
                {
                    reader.Skip();
                }
            }
            else if (reader.TokenType == JsonTokenType.EndObject)
            {
                return new PixelPoint(x, y);
            }
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, PixelPoint value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("X");
        writer.WriteNumberValue(value.X);
        writer.WritePropertyName("Y");
        writer.WriteNumberValue(value.Y);
        writer.WriteEndObject();
    }
}
