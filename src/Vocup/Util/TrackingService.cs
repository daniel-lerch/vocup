using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Vocup.Properties;

namespace Vocup.Util
{
    public static class TrackingService
    {
        public static async void Action(string actionName)
        {
            if (Settings.Default.DisableInternetServices) return;

            try
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"Vocup/{AppInfo.GetVersion(3)} (Windows NT {Environment.OSVersion.Version})");
                var query = new Dictionary<string, string>()
                {
                    ["idsite"] = "1",
                    ["rec"] = "1",
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
