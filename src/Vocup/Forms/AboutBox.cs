using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vocup.Forms
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
        }

        private async void AboutBox_Load(object sender, EventArgs e)
        {
            // main page
            LbOS.Text = await Task.Run(() => Util.SystemInfo.GetOSName());
            LbNetFramwork.Text = Util.SystemInfo.GetNetFrameworkVersion();
            // tab info
            LbVersion.Text = "Version " + Util.AppInfo.GetVersion(3);
            LbCopyright.Text = AppInfo.Copyright;
            LlbProjectWebsite.Text = AppInfo.ProjectWebsite;
            LlbProjectWebsite.LinkClicked += (o1, o2) => Process.Start(AppInfo.ProjectWebsiteUrl);
            LlbProjectEMail.Text = AppInfo.ProjectEMail;
            LlbProjectEMail.LinkClicked += (o3, o4) => Process.Start("mailto:" + AppInfo.ProjectEMail);
            LbProjectLicense.Text = AppInfo.ProjectLicense;
            // tab spenden
            waehrung.SelectedItem = "CHF";

            AcceptButton = BtnOK;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void spende_button_Click(object sender, EventArgs e)
        {
            string url = "https://www.paypal.com/cgi-bin/webscr&cmd=_donations&business=donation@vocup.ch&item_name=Vocup+Spende&no_shipping=1&tax=0&lc=CH&bn=PP-DonationsBF&currency_code=";

            if (waehrung.SelectedItem.ToString() == "CHF")
            {
                url = url + "CHF";
            }
            else if (waehrung.SelectedItem.ToString() == "EUR")
            {
                url = url + "EUR";
            }
            else if (waehrung.SelectedItem.ToString() == "USD")
            {
                url = url + "USD";
            }

            Process.Start(url);
        }

        private void spender_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.vocup.ch/spender");
        }
    }
}