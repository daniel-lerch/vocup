using Microsoft.Win32;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Windows.Forms;
using Windows.Foundation.Metadata;
using Windows.Management.Deployment;
using Windows.Win32;
using Windows.Win32.Foundation;

namespace Vocup.Util;

/// <summary>
/// Provides util methods for application metadata.
/// </summary>
public static class AppInfo
{
    /// <summary>
    /// Defines invalid characters for a path and thereby many strings in Vocup.
    /// </summary>
    public const string InvalidPathChars = "#=:\\/|<>*?\"";

    /// <summary>
    /// Gets the directory where custom special char files are stored.
    /// </summary>
    public static string SpecialCharDirectory => Path.Combine(Program.Settings.VhrPath, "specialchar");

    private static readonly Lazy<Version> version = new(() =>
    {
        string? versionString = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
            ?? throw new ApplicationException("Assembly version is undefined");
        string versionPart = versionString.Split('+')[0];
        return Version.Parse(versionPart);
    });
    public static Version Version => version.Value;

    public static Version FileVersion { get; } = new Version(1, 0);

    public static string ProductName { get; }
        = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>()?.Product
        ?? throw new ApplicationException("Assemly product name is undefined");

    public static string CopyrightInfo { get; }
        = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright
        ?? throw new ApplicationException("Assembly copyright information is undefined");

    private static readonly Lazy<bool> isUwp = new(() =>
    {
        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240))
        {
            uint length = 0;
            WIN32_ERROR status = PInvoke.GetCurrentPackageFullName(ref length, null);
            return status != WIN32_ERROR.APPMODEL_ERROR_NO_PACKAGE;
        }
        else return false;
    });
    public static bool IsUwp => isUwp.Value;

    private static readonly Lazy<bool> isInstallation = new(() =>
    {
        return TryGetVocupInstallation(out _, out string? installLocation, out _)
            && Application.StartupPath.TrimEnd('\\').Equals(installLocation.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase);
    });
    public static bool IsWindowsInstallation => isInstallation.Value;

    private static readonly Lazy<bool> isWine = new(() =>
    {
        using RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
        using RegistryKey? wine = hklm.OpenSubKey(@"SOFTWARE\Wine", writable: false);
        return wine is not null;
    });
    public static bool IsWine => isWine.Value;


    public static string GetDeployment()
    {
        return (IsUwp, IsWindowsInstallation) switch
        {
            (true, _) => "UWP",
            (false, true) => "Installer",
            (false, false) => "Portable"
        };
    }

    public static bool TryGetVocupInstallation(
        [NotNullWhen(true)] out Version? version,
        [NotNullWhen(true)] out string? installLocation,
        [NotNullWhen(true)] out string? uninstallString)
    {
        version = null;
        installLocation = null;
        uninstallString = null;

        // Vocup is installed as 32bit application
        using RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
        using RegistryKey? vocup = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Vocup_is1", writable: false);

        if (vocup == null) return false;
        if (!Version.TryParse((string?)vocup.GetValue("DisplayVersion"), out Version? temp)) return false;
        version = temp.Revision == -1 ? new Version(temp.Major, temp.Minor, temp.Build, 0) : temp;
        installLocation = (string?)vocup.GetValue("InstallLocation");
        uninstallString = (string?)vocup.GetValue("UninstallString");
        return installLocation is not null && uninstallString is not null;
    }

    [SupportedOSPlatform("windows10.0.10240.0")]
    public static bool TryGetVocupUwpApp([NotNullWhen(true)] out Version? version)
    {
        var packageManager = new PackageManager();
        string? userSecurityId = WindowsIdentity.GetCurrent().User?.Value;
        if (userSecurityId is not null)
        {
            foreach (var package in packageManager.FindPackagesForUser(userSecurityId, "9961VectorData.Vocup_ffrs9s78t67f2"))
            {
                if (!package.IsResourcePackage)
                {
                    version = new Version(package.Id.Version.Major, package.Id.Version.Minor, package.Id.Version.Build, package.Id.Version.Revision);
                    return true;
                }
            }
        }
        version = null;
        return false;
    }

    public static int GetCHUAPlatformVersion()
    {
        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240))
        {
            for (int version = 0; ; version++)
            {
                if (!ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", (ushort)(version + 1)))
                    return version;
            }
        }
        else return 0;
    }
}
