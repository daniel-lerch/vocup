using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Vocup.UnitTests
{
    public class SettingsTests
    {
        [Fact]
        public async Task TestWriteSettings()
        {
            string path = Path.Combine(Path.GetTempPath(), "vocup_settings.1.json");
            Settings2.VocupSettings settings = new();
            using (FileStream stream = new(path, FileMode.Create, FileAccess.Write))
                await JsonSerializer.SerializeAsync(stream, settings);
            //File.Delete(path);
        }
    }
}
