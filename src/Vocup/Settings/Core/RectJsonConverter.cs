using Avalonia;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vocup.Settings.Core;

public class RectJsonConverter : JsonConverter<Rect>
{
    public override Rect Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        double x = 0, y = 0, width = 0, height = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();

                if (propertyName == "X")
                {
                    reader.Read();
                    x = reader.GetDouble();
                }
                else if (propertyName == "Y")
                {
                    reader.Read();
                    y = reader.GetDouble();
                }
                else if (propertyName == "Width")
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
                return new Rect(x, y, width, height);
            }
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Rect value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("X");
        writer.WriteNumberValue(value.X);

        writer.WritePropertyName("Y");
        writer.WriteNumberValue(value.Y);

        writer.WritePropertyName("Width");
        writer.WriteNumberValue(value.Width);

        writer.WritePropertyName("Height");
        writer.WriteNumberValue(value.Height);

        writer.WriteEndObject();
    }
}
