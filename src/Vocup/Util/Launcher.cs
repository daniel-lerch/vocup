using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Vocup.Util;

public static class Launcher
{
    public static async ValueTask LaunchUriAsync(string uri)
    {
        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240))
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(uri));
        }
        else
        {
            Process.Start(new ProcessStartInfo(uri) { UseShellExecute = true });
        }
    }
}
