﻿using System;
using System.Runtime.InteropServices;

namespace Vocup.ViewModels;

public class AboutViewModel : ViewModelBase
{
    public AboutViewModel() 
        : this("0.0.0", "Copyright © 2011 Florian Amstutz, © 2018-present Daniel Lerch.") { }

    public AboutViewModel(string appVersion, string copyright)
    {
        string prefix = Lang.Resources.AboutView_Version;
        string architecture = RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant();
        Version = $"{prefix} {appVersion} ({architecture})";

        Copyright = copyright;

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
