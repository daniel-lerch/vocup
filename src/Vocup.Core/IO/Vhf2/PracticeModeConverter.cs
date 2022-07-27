using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vocup.Models;

namespace Vocup.IO.Vhf2;

internal class PracticeModeConverter : JsonConverter<PracticeMode2>
{
    public override PracticeMode2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return (PracticeMode2)Enum.Parse(typeToConvert, reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, PracticeMode2 value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
