using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Vocup.Settings2;
using Xunit;

namespace Vocup.UnitTests
{
    public class SettingsTests
    {
        [Fact]
        public async Task TestWriteSettings()
        {
            string path = Path.Combine(Path.GetTempPath(), "vocup_settings.1.json");
            VocupSettings settings = new();
            using (FileStream stream = new(path, FileMode.Create, FileAccess.Write))
                await JsonSerializer.SerializeAsync(stream, settings);
            File.Delete(path);
        }

        [Fact]
        public async Task TestSettings()
        {
            var settings = LostTech.App.XmlSettings.Create(new DirectoryInfo(Path.GetTempPath()));
            var vocupSettings = await settings.LoadOrCreate<VocupSettings>("vocup.xml");
            vocupSettings.Value.StartupCounter++;
            await Task.Delay(10);
            vocupSettings.ScheduleSave();
            settings.ScheduleSave();
            await Task.Delay(10);
            await vocupSettings.DisposeAsync();
            await settings.DisposeAsync();
        }
    }
}
