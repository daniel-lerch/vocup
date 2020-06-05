using Octokit;
using System;
using System.Threading.Tasks;

namespace Vocup.Util
{
    public static class UpdateService
    {
        public static async Task<string> GetUpdateUrl()
        {
            Release release;
            try
            {
                var github = new GitHubClient(new ProductHeaderValue(AppInfo.ProductName, AppInfo.GetVersion(3)));
                release = await github.Repository.Release.GetLatest("daniel-lerch", "vocup").ConfigureAwait(false);
            }
            catch
            {
                return null;
            }

            string tagName = release.TagName.StartsWith("v") ?
                release.TagName.Substring(1) :
                release.TagName;

            if (Version.TryParse(tagName, out Version releaseVersion))
            {
                // Add zero as revision because otherwise 1.7.1.0 would be greater than 1.7.1
                if (releaseVersion.Revision == -1)
                    releaseVersion = new Version(releaseVersion.Major, releaseVersion.Minor, releaseVersion.Build, 0);

                if (AppInfo.Version >= releaseVersion)
                    return null;

                foreach (ReleaseAsset asset in release.Assets)
                {
                    if (AppInfo.IsWindowsInstallation && asset.Name.EndsWith("_Setup.exe", StringComparison.OrdinalIgnoreCase))
                        return asset.BrowserDownloadUrl;
                    if (AppInfo.IsWindows && !AppInfo.IsWindowsInstallation && asset.Name.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                        return asset.BrowserDownloadUrl;
                    if (AppInfo.IsMono && asset.Name.EndsWith("_Linux.tar.gz", StringComparison.OrdinalIgnoreCase))
                        return asset.BrowserDownloadUrl;
                }
            }

            return release.HtmlUrl;
        }
    }
}
