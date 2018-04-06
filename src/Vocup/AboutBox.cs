using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;
using System.Net;
using System.IO;

using System.Security.Permissions;


namespace Vocup
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            version.Text = String.Format("Version {0}", Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Build.ToString());
            copyright.Text = "© Copyright 2011 Florian Amstutz. All Rights Reserved.";
            
            //OS ermitteln

            int[] os_version = new int[2];
            os_version[0] = Environment.OSVersion.Version.Major;
            os_version[1] = Environment.OSVersion.Version.Minor;
         

            if (os_version[0] == 4)
            {
                if (os_version[1] == 1)
                {
                    os.Text = "Windows 98";
                }
                else
                {
                    os.Text = "Windows ME";
                }
            }
            
            else if (os_version[0] == 5)
            {
                if (os_version[1] == 0)
                {
                    os.Text = "Windows 2000";
                }
                else if (os_version[1] == 1)
                {
                    os.Text = "Windows XP";
                }
                else if (os_version[1] == 2)
                {
                    os.Text = "Server 2003";
                }
            }

            else if (os_version[0] == 6)
            {
                if (os_version[1] == 0)
                {
                    os.Text = "Windows Vista";
                }
                else if (os_version[1] == 1)
                {
                    os.Text = "Windows 7";
                }
            }
            else
            {
                os.Text = "Unbekannt";
            }

            os.Text = os.Text + " (Build " + Environment.OSVersion.Version.Build.ToString() + ")";
            if (Environment.OSVersion.ServicePack.ToString() != "")
            {
                os.Text = os.Text + " " + Environment.OSVersion.ServicePack.ToString();
            }

            //.net Framework ermitteln
            //Registry auslesen

            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\NET Framework Setup\\NDP", false);
            string[] subkeys = key.GetSubKeyNames();
            decimal net_version = 0;

            for (int i = 0; i < subkeys.Length; i++)
            {
                if (subkeys[i].StartsWith("v") == true)
                {
                    try
                    {
                        string next = subkeys[i].Remove(0, 1);
                        string[] splitted = next.Split('.');
                        next = splitted[0] + "." + splitted[1];
                        decimal y = Convert.ToDecimal(next);
                        decimal get_version = y;

                        if (get_version > net_version)
                        {
                            net_version = get_version;
                        }
                    }
                    catch
                    {
                    }
                }
            }

            


            //framework.Text = ".NET Framework " + Environment.Version.Major.ToString() + "." + Environment.Version.Minor.ToString();
            framework.Text = ".NET Framework " + net_version.ToString();
            
            //Währung anzeigen

            waehrung.SelectedItem = "CHF";
            
            AcceptButton = button;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.vocup.ch");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:info@vocup.ch");
        }
        
        private void button_Click(object sender, EventArgs e)
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