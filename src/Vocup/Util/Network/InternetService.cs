using Octokit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vocup.Util.Network
{
    public class InternetService
    {
        public async Task Start()
        {
            var github = new GitHubClient(new ProductHeaderValue(AppInfo.ProductName, AppInfo.GetVersion(3)));
            IReadOnlyList<Release> releases = await github.Repository.Release.GetAll("daniel-lerch", "vocup");
        }
    }
}
