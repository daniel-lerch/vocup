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
            // main page
            LbOS.Text = await Task.Run(() => Util.SystemInfo.GetOSName());
            LbNetFramwork.Text = Util.SystemInfo.GetNetFrameworkVersion();
            // tab info
            LbVersion.Text = string.Format(LbVersion.Text, Util.AppInfo.GetVersion(3));
            LbCopyright.Text = Util.AppInfo.CopyrightInfo;
            LlbProjectWebsite.LinkClicked += (s, args) => Process.Start("https://github.com/daniel-lerch/vocup");
            LlbProjectEMail.LinkClicked += (s, args) => Process.Start("mailto:" + LlbProjectEMail.Text);
            LlbProjectLicense.LinkClicked += (s, args) => Process.Start("https://github.com/daniel-lerch/vocup/blob/master/LICENSE");
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}