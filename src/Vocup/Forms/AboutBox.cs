using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vocup.Properties;

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
            LbVersion.Text = string.Format(LbVersion.Text, Util.AppInfo.GetVersion(3));
            LbCopyright.Text = Util.AppInfo.CopyrightInfo;

            // main page
            LbOS.Text = await Task.Run(() => Util.SystemInfo.GetOSName());
            LbNetFramwork.Text = await Task.Run(() => Util.SystemInfo.GetNetFrameworkVersion());
        }

        private void LlbProjectWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/daniel-lerch/vocup");
        }

        private void LlbProjectEMail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:" + LlbProjectEMail.Text);
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