using Microsoft.Extensions.DependencyInjection;
using Vocup.Settings;
using Vocup.Settings.Core;

namespace Vocup.Desktop;

public class App : Vocup.App
{
    protected override void ConfigureSettings(IServiceCollection services)
    {
        base.ConfigureSettings(services);

        services.AddSingleton<SettingsLoaderBase<VocupSettings>, DesktopSettingsLoader>();
    }
}
