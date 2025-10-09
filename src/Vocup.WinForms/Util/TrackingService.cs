using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Vocup.Util;

public class TrackingService : IAsyncDisposable
{
    private readonly HttpClient httpClient;
    private Func<Task>? defer;

    public TrackingService()
    {
        string osArch = RuntimeInformation.OSArchitecture.ToString().ToLowerInvariant();
        string processArch = RuntimeInformation.ProcessArchitecture switch
        {
            Architecture.X86 => "Win32",
            Architecture.X64 => "Win64",
            Architecture.Arm64 => "WoA64",
            _ => "Unknown Runtime"
        };
        string userAgent = AppInfo.IsWine ?
            $"Vocup/{AppInfo.Version} (Wine; Linux; {processArch}; {osArch}; {AppInfo.GetDeployment()})" :
            $"Vocup/{AppInfo.Version} (Windows NT {Environment.OSVersion.Version}; {processArch}; {osArch}; {AppInfo.GetDeployment()})";

        string chUAPlatformVersion = AppInfo.GetCHUAPlatformVersion().ToString();

        httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
        httpClient.DefaultRequestHeaders.Add("Sec-CH-UA-Platform", "Windows");
        httpClient.DefaultRequestHeaders.Add("Sec-CH-UA-Platform-Version", chUAPlatformVersion);
        httpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    public void Page(string url)
    {
        if (Program.Settings.DisableInternetServices) return;

        _ = SendAction(url, string.Empty);
    }

    public void Action(string url, string actionName)
    {
        if (Program.Settings.DisableInternetServices) return;

        _ = SendAction(url, actionName);
    }

    /// <summary>
    /// Registers a single tracking request to be performed on <see cref="DisposeAsync"/>.
    /// This is required because all Tasks scheduled before <see cref="System.Windows.Forms.Application.Run"/> exits will be aborted.
    /// </summary>
    public void DeferAction(string url, string actionName)
    {
        if (Program.Settings.DisableInternetServices) return;

        defer = () => SendAction(url, actionName);
    }

    private async Task SendAction(string url, string actionName)
    {
        try
        {
            var query = new Dictionary<string, string>()
            {
                ["idsite"] = "1",
#if DEBUG
                ["rec"] = "0",
#else
                ["rec"] = "1",
#endif
                ["url"] = "https://app.vocup.org" + url,
                ["action_name"] = actionName,
                ["apiv"] = "1",
                ["dimension1"] = AppInfo.Version.ToString(),
                ["dimension2"] = RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant(),
                ["dimension3"] = AppInfo.GetDeployment(),
                ["dimension4"] = Environment.OSVersion.Version.ToString(),
                ["dimension6"] = RuntimeInformation.OSArchitecture.ToString().ToLowerInvariant(),
            };
            var uriBuilder = new UriBuilder("https://vocup.org/api/analytics/record");
            uriBuilder.Query = await new FormUrlEncodedContent(query).ReadAsStringAsync().ConfigureAwait(false);
            HttpResponseMessage response = await httpClient.PostAsync(uriBuilder.Uri, new ByteArrayContent([])).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Server replied with {0} {1}", response.StatusCode, response.ReasonPhrase);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (defer != null)
            await defer().ConfigureAwait(false);

        httpClient.Dispose();
    }
}
