using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vocup.Models;

namespace Vocup.IO.Vhf2;

internal class PracticeResultConverter : JsonConverter<PracticeResult2>
{
    public override PracticeResult2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return (PracticeResult2)Enum.Parse(typeToConvert, reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, PracticeResult2 value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
