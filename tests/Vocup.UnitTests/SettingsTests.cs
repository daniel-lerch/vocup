using System.IO;
using System.Threading.Tasks;
using Vocup.Settings;
using Vocup.Settings.Core;
using Xunit;

namespace Vocup.UnitTests;

public class SettingsTests
{
    [Fact]
    public async Task TestSettings()
    {
        DirectoryInfo directory = new(Path.GetTempPath());
        string basename = "vocup_settings";

        VersionedSettingsLoader<VocupSettings> loader = new(directory, basename);
        VersionedSettings<VocupSettings> settings = await loader.LoadAsync();

        settings.Value.StartupCounter++;
        await settings.DisposeAsync();
    }
}
