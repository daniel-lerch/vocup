using Octokit;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vocup.Util
{
    public static class UpdateService
    {
        public static async Task<string?> GetUpdateUrl()
        {
            Release release;
            try
            {
                var github = new GitHubClient(new ProductHeaderValue(AppInfo.ProductName, AppInfo.Version.ToString()));
                release = await github.Repository.Release.GetLatest("daniel-lerch", "vocup").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to search for updates: " + ex.ToString());
                return null;
            }

            string tagName = release.TagName.StartsWith("v") ?
                release.TagName.Substring(1) :
                release.TagName;

            if (Version.TryParse(tagName, out Version? releaseVersion))
            {
                if (AppInfo.Version >= releaseVersion)
                    return null;

                string pattern = AppInfo.IsWindowsInstallation ? @"^Vocup_\d+\.\d+\.\d+\.exe$" : @"^Vocup_\d+\.\d+\.\d+\.zip$";

                foreach (ReleaseAsset asset in release.Assets)
                {
                    if (Regex.IsMatch(asset.Name, pattern))
                        return asset.BrowserDownloadUrl;
                }
            }

            return release.HtmlUrl;
        }
    }
}
