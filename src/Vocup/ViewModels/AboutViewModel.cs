using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Vocup.ViewModels;

public class AboutViewModel : ViewModelBase
{
    public AboutViewModel(string deployment)
    {
        var versionAttribute = GetType().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        var appVersionString = versionAttribute?.InformationalVersion?.Split('+')[0] ?? "Development";

        var copyrightAttribute = GetType().Assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
        var copyrightString = copyrightAttribute?.Copyright ?? "Copyright © 2011 Florian Amstutz, © 2018-present Daniel Lerch.";

        string prefix = Lang.Resources.AboutView_Version;
        string architecture = RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant();
        Version = $"{prefix} {appVersionString} ({architecture}, {deployment})";

        Copyright = copyrightString;

        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240))
            MicrosoftStoreLink = "ms-windows-store://pdp/?productid=9N6W2H3QJQMM";
        else
            MicrosoftStoreLink = "https://www.microsoft.com/store/apps/9N6W2H3QJQMM";
    }

    public string Version { get; }
    public string Copyright { get; }
    public string MicrosoftStoreLink { get; }
    public LicensesViewModel Licenses { get; } = new();
}

public class AboutDesignViewModel : AboutViewModel
{
    public AboutDesignViewModel()
    : base("Development") { }
}
