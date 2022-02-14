using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using Vocup.Properties;

namespace Vocup.Util
{
    public static class TrackingService
    {
        private static string userAgent;

        static TrackingService()
        {
            string osArch = RuntimeInformation.OSArchitecture.ToString().ToLowerInvariant();
            string processArch = RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.X86 => "Win32",
                Architecture.X64 => "Win64",
                Architecture.Arm64 => "WoA64",
                _ => "Unknown Runtime"
            };
            userAgent = $"Vocup/{AppInfo.GetVersion(3)} (Windows NT {Environment.OSVersion.Version}; {processArch}; {osArch}; {AppInfo.GetDeployment()})";
        }

        public static async void Action(string actionName)
        {
            if (Settings.Default.DisableInternetServices) return;

            try
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
                var query = new Dictionary<string, string>()
                {
                    ["idsite"] = "1",
#if DEBUG
                    ["rec"] = "0",
#else
                    ["rec"] = "1",
#endif
                    ["action_name"] = actionName,
                    ["apiv"] = "1"
                };
                var uriBuilder = new UriBuilder("https://vocup.org/api/analytics/record");
                uriBuilder.Query = await new FormUrlEncodedContent(query).ReadAsStringAsync();
                HttpResponseMessage response = await httpClient.PostAsync(uriBuilder.Uri, new ByteArrayContent(Array.Empty<byte>()));
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Server replied with {0} {1}", response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
