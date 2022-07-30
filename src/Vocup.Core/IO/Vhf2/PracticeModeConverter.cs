﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vocup.Models;

namespace Vocup.IO.Vhf2;

internal class PracticeModeConverter : JsonConverter<PracticeMode>
{
    public override PracticeMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return (PracticeMode)Enum.Parse(typeToConvert, reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, PracticeMode value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
