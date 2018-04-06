using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Vocup
{
    public partial class donation : Form
    {

        int seconds = 15;

        public donation()
        {
            InitializeComponent();
        }

        private void donation_Load(object sender, EventArgs e)
        {
            close_button.Enabled = false;

            //Währung anzeigen

            waehrung.SelectedItem = "CHF";

            
        }

        private void donation_Shown(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Interval = 1000;
            
            timer.Start();

        }

        private void donation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close_button.Enabled == false)
            {
                e.Cancel = true;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            close_button.Text = seconds + " sek";

            if (seconds == 0)
            {
                close_button.Enabled = true;
                close_button.Text = "schliessen";

                timer.Stop();
            }
            else
            {
                seconds--;
            }

        }

        private void close_button_Click(object sender, EventArgs e)
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