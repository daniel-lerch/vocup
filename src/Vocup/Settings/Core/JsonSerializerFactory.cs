using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using LostTech.App;

namespace Vocup.Settings.Core;

public class JsonSerializerFactory : ISerializerFactory, IDeserializerFactory
{
    public static JsonSerializerFactory Instance { get; } = new JsonSerializerFactory();

    private readonly JsonSerializerOptions options;

    public JsonSerializerFactory()
    {
        options = new JsonSerializerOptions { WriteIndented = true };
    }

    public Func<Stream, T, Task> MakeSerializer<T>() => Serialize;

    private Task Serialize<T>(Stream output, T value)
    {
        return JsonSerializer.SerializeAsync(output, value, options);
    }

    public Func<Stream, Task<T>> MakeDeserializer<T>() => Deserialize<T>;

    private async Task<T> Deserialize<T>(Stream input)
    {
        return await JsonSerializer.DeserializeAsync<T>(input).ConfigureAwait(false) 
            ?? throw new InvalidDataException("JSON deserialization returned null");
    }
}
