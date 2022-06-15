using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup.Forms
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.Info.GetHicon());
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            string architecture = RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant();

            string versionText = string.Format(LbVersion.Text, AppInfo.GetVersion(3), architecture, AppInfo.GetDeployment());

            LbVersion.Text = versionText;
            LbCopyright.Text = AppInfo.CopyrightInfo;
        }

        private async void LlbProjectWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            await Launcher.LaunchUriAsync("https://github.com/daniel-lerch/vocup");
        }

        private async void LlbDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240))
                await Launcher.LaunchUriAsync("ms-windows-store://pdp/?productid=9N6W2H3QJQMM");
            else
                await Launcher.LaunchUriAsync("https://www.microsoft.com/store/apps/9N6W2H3QJQMM");
        }

        private async void LlbProjectMail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            await Launcher.LaunchUriAsync("mailto:" + LlbProjectMail.Text);
        }

        private async void LlbProjectLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            await Launcher.LaunchUriAsync("https://github.com/daniel-lerch/vocup/blob/master/LICENSE");
        }

        private async void LwComponents_DoubleClick(object sender, EventArgs e)
        {
            if (LwComponents.SelectedItems.Count > 0)
            {
                await Launcher.LaunchUriAsync(LwComponents.SelectedItems[0].SubItems[2].Text);
            }
        }
    }
}
