using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
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

        private async void AboutBox_Load(object sender, EventArgs e)
        {
            // tab info
            if (AppInfo.IsUwp())
                LbVersion.Text = string.Format(LbVersion.Text, AppInfo.GetVersion(3) + " (UWP)");
            else
                LbVersion.Text = string.Format(LbVersion.Text, AppInfo.GetVersion(3));
            LbCopyright.Text = AppInfo.CopyrightInfo;

            // main page
            LbOS.Text = await Task.Run(() => SystemInfo.GetOSName());
            LbNetFramwork.Text = await Task.Run(() => SystemInfo.GetNetFrameworkVersion());
        }

        private void LlbProjectWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/daniel-lerch/vocup");
        }

        private void LlbDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (SystemInfo.IsWindows10())
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
                Process.Start(LwComponents.SelectedItems[0].SubItems[3].Text);
            }
        }
    }
}