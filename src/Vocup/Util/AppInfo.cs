using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Windows.Management.Deployment;

namespace Vocup.Util
{
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
        public static string SpecialCharDirectory { get; } = Path.Combine(Properties.Settings.Default.VhrPath, "specialchar");

        public static Version Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;

        public static Version FileVersion { get; } = new Version(1, 0);

        public static string ProductName { get; }
            = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>()?.Product;

        public static string CopyrightInfo { get; }
            = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;

        private static readonly Lazy<bool> isUwp = new Lazy<bool>(() =>
        {
            if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240))
            {
                int length = 0;
                StringBuilder sb = new StringBuilder(0);
                GetCurrentPackageFullName(ref length, sb);
                sb = new StringBuilder(length);
                int result = GetCurrentPackageFullName(ref length, sb);
                return result != APPMODEL_ERROR_NO_PACKAGE;
            }
            else return false;
        });
        public static bool IsUwp => isUwp.Value;

        private static readonly Lazy<bool> isInstallation = new Lazy<bool>(() =>
        {
            return TryGetVocupInstallation(out _, out string installLocation, out _)
                && Application.StartupPath.TrimEnd('\\').Equals(installLocation.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase);
        });
        public static bool IsWindowsInstallation => isInstallation.Value;


        public static string GetDeployment()
        {
            return (IsUwp, IsWindowsInstallation) switch
            {
                (true, _) => "UWP",
                (false, true) => "Installer",
                (false, false) => "Portable"
            };
        }

        /// <summary>
        /// Returns the product version of the currently running instance.
        /// </summary>
        /// <param name="length">Count of version numbers. Range from 1 to 4.</param>
        public static string GetVersion(int length)
        {
            if (length < 1 || length > 4)
                throw new ArgumentOutOfRangeException(nameof(length));
            string version = Application.ProductVersion;
            for (int i = 0; i < 4 - length; i++)
                version = version.Remove(version.LastIndexOf('.'));
            return version;
        }

        public static bool TryGetVocupInstallation(out Version version, out string installLocation, out string uninstallString)
        {
            version = null;
            installLocation = null;
            uninstallString = null;

            // Vocup is installed as 32bit application
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            using (RegistryKey vocup = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Vocup_is1", writable: false))
            {
                if (vocup == null) return false;
                if (!Version.TryParse((string)vocup.GetValue("DisplayVersion"), out Version temp)) return false;
                version = temp.Revision == -1 ? new Version(temp.Major, temp.Minor, temp.Build, 0) : temp;
                installLocation = (string)vocup.GetValue("InstallLocation");
                uninstallString = (string)vocup.GetValue("UninstallString");
                return true;
            }
        }

        [SupportedOSPlatform("windows10.0.10240.0")]
        public static bool TryGetVocupUwpApp(out Version version)
        {
            var packageManager = new PackageManager();
            foreach (var package in packageManager.FindPackagesForUser(WindowsIdentity.GetCurrent().User.Value, "9961VectorData.Vocup_ffrs9s78t67f2"))
            {
                if (!package.IsResourcePackage)
                {
                    version = new Version(package.Id.Version.Major, package.Id.Version.Minor, package.Id.Version.Build, package.Id.Version.Revision);
                    return true;
                }
            }
            version = null;
            return false;
        }

        private const int APPMODEL_ERROR_NO_PACKAGE = 15700;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder packageFullName);
    }
}
