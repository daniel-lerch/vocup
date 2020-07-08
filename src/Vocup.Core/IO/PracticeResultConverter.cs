using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vocup.Models;

namespace Vocup.IO
{
    internal class PracticeResultConverter : JsonConverter<PracticeResult>
    {
        public override PracticeResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return (PracticeResult)Enum.Parse(typeToConvert, reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, PracticeResult value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
