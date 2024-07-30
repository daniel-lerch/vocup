using System.Runtime.InteropServices;

namespace Vocup.ViewModels;

public class AboutViewModel : ViewModelBase
{
    public AboutViewModel()
    {

    }

    public AboutViewModel(string appVersion, string copyright)
    {
        AppVersion = appVersion;
        Copyright = copyright;
    }

    public string AppVersion { get; } = "0.0.0";
    public string Version
    {
        get
        {
            string prefix = Lang.Resources.AboutView_Version;
            string architecture = RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant();
            return $"{prefix} {AppVersion} ({architecture})";
        }
    }
    public string Copyright { get; } = "Copyright © 2011 Florian Amstutz, © 2018-present Daniel Lerch.";
    public LicensesViewModel Licenses { get; } = new();
}
