using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Vocup.ViewModels;

public class AboutViewModel : ViewModelBase
{
    protected const string DefaultCopyright = "Copyright © 2011 Florian Amstutz, © 2018-present Daniel Lerch.";
    protected const string DefaultAppVersion = "0.0.0";

    public AboutViewModel(string deployment)
    {
        string prefix = Lang.Resources.AboutView_Version;
        string architecture = RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant();

        Version = $"{prefix} {AppVersion} ({architecture}, {deployment})";

        Copyright = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright
            ?? DefaultCopyright;

        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240))
            MicrosoftStoreLink = "ms-windows-store://pdp/?productid=9N6W2H3QJQMM";
        else
            MicrosoftStoreLink = "https://www.microsoft.com/store/apps/9N6W2H3QJQMM";
    }

    public string Version { get; }
    public virtual string Copyright { get; }
    public string MicrosoftStoreLink { get; }
    public LicensesViewModel Licenses { get; } = new();

    protected virtual string AppVersion => Assembly.GetEntryAssembly()?
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
        .InformationalVersion?
        .Split('+')[0]
        ?? DefaultAppVersion;
}

public class DesignAboutViewModel : AboutViewModel
{
    public DesignAboutViewModel() : base("Design Mode")
    {
    }

    public override string Copyright => DefaultCopyright;
    protected override string AppVersion => DefaultAppVersion;
}
