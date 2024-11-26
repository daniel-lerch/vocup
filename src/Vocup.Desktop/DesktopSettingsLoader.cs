using System;
using System.IO;
using Vocup.Settings;
using Vocup.Settings.Core;

namespace Vocup.Desktop;

public class DesktopSettingsLoader : SettingsLoaderBase<VocupSettings>
{
    public DesktopSettingsLoader()
        : base(
            new DirectoryInfo(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Vocup")),
            "settings.2.json")
    { }
}
