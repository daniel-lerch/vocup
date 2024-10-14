using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Vocup.Settings;
using Vocup.Settings.Core;
using Xunit;

namespace Vocup.UnitTests;

public class SettingsTests : IDisposable
{
    private static readonly string directory = Path.GetTempPath();
    private readonly string basename;
    private readonly JsonSerializerOptions options;

    public SettingsTests()
    {
        basename = $"vocup_{Guid.NewGuid()}.json";
        options = new JsonSerializerOptions { WriteIndented = true };
        options.Converters.Add(new RectJsonConverter());
        options.Converters.Add(new SizeJsonConverter());
    }

    [Fact]
    public async Task TestCreateSettings()
    {
        SettingsLoaderBase<VocupSettings> loader = new(new(directory), basename);
        SettingsContext<VocupSettings> settings = await loader.LoadAsync();

        settings.Value.StartupCounter = 1;

        await settings.DisposeAsync();

        Assert.True(File.Exists(Path.Combine(directory, basename)));

        using FileStream file = new(Path.Combine(directory, basename), FileMode.Open, FileAccess.Read, FileShare.Read);
        var settingsValue = await JsonSerializer.DeserializeAsync<VocupSettings>(file, options);

        Assert.NotNull(settingsValue);
        Assert.Equal(1, settingsValue.StartupCounter);
    }

    [Fact]
    public void TestSerialize()
    {
        VocupSettings settings = new();
        string json = JsonSerializer.Serialize(settings, options);

        Assert.False(string.IsNullOrWhiteSpace(json));
    }

    public void Dispose()
    {
        try
        {
            File.Delete(Path.Combine(directory, basename));
        }
        catch { }
    }
}
