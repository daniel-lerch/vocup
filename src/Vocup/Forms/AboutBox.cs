using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup.Forms;

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

        string versionText = string.Format(LbVersion.Text, AppInfo.Version, architecture, AppInfo.GetDeployment());

        LbVersion.Text = versionText;
        LbCopyright.Text = AppInfo.CopyrightInfo;
    }

    private void LlbProjectWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Process.Start("https://github.com/daniel-lerch/vocup");
    }

    private void LlbDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240))
            Process.Start("ms-windows-store://pdp/?productid=9N6W2H3QJQMM");
        else
            Process.Start("https://www.microsoft.com/store/apps/9N6W2H3QJQMM");
    }

    private void LlbProjectMail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Process.Start("mailto:" + LlbProjectMail.Text);
    }

    private void LlbProjectLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Process.Start("https://github.com/daniel-lerch/vocup/blob/master/LICENSE");
    }

    private void LwComponents_DoubleClick(object sender, EventArgs e)
    {
        if (LwComponents.SelectedItems.Count > 0)
        {
            Process.Start(LwComponents.SelectedItems[0].SubItems[2].Text);
        }
    }
}