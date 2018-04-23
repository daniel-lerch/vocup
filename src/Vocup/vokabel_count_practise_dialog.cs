using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Vocup
{
    public partial class vokabel_count_practise_dialog : Form
    {
        public vokabel_count_practise_dialog()
        {
            InitializeComponent();
        }

        public string button;
        public int anzahl_noch_nicht;
        public int anzahl_falsch;
        public int anzahl_richtig;
        public int anzahl_gesamt;


        //Variabel "Button" wird geschrieben

        private void vokabeln_20_Click(object sender, EventArgs e)
        {
            button = "20";
            Close();
        }

        private void vokabeln_30_Click(object sender, EventArgs e)
        {
            button = "30";
            Close();
        }

        private void vokabeln_40_Click(object sender, EventArgs e)
        {
            button = "40";
            Close();
        }

        private void vokabeln_alle_Click(object sender, EventArgs e)
        {
            button = "alle";
            Close();
        }

        private void vokabeln_anzahl_Click(object sender, EventArgs e)
        {
            button = "individuell";
            Close();
        }



        //Buttons ausschalten, falls eine andere Art von Vokabeln gewählt wurde
        private void art_alle_CheckedChanged(object sender, EventArgs e)
        {
            if (art_alle.Checked == true)
            {
                if (zeitlich_alle.Checked == true)
                {
                    anzahl.Maximum = anzahl_gesamt;

                    if (anzahl_gesamt >= 40)
                    {
                        vokabeln_20.Enabled = true;
                        vokabeln_30.Enabled = true;
                        vokabeln_40.Enabled = true;
                    }
                    else if (anzahl_gesamt >= 30 && anzahl_gesamt < 40)
                    {
                        vokabeln_20.Enabled = true;
                        vokabeln_30.Enabled = true;
                        vokabeln_40.Enabled = false;
                    }
                    else if (anzahl_gesamt >= 20 && anzahl_gesamt < 30)
                    {
                        vokabeln_20.Enabled = true;
                        vokabeln_30.Enabled = false;
                        vokabeln_40.Enabled = false;
                    }
                    else if (anzahl_gesamt < 20)
                    {
                        vokabeln_20.Enabled = false;
                        vokabeln_30.Enabled = false;
                        vokabeln_40.Enabled = false;
                    }
                }
                else
                {
                    anzahl.Maximum = anzahl_gesamt - anzahl_noch_nicht;

                    if (anzahl_gesamt - anzahl_noch_nicht >= 40)
                    {
                        vokabeln_20.Enabled = true;
                        vokabeln_30.Enabled = true;
                        vokabeln_40.Enabled = true;
                    }
                    else if (anzahl_gesamt - anzahl_noch_nicht >= 30 && anzahl_gesamt - anzahl_noch_nicht < 40)
                    {
                        vokabeln_20.Enabled = true;
                        vokabeln_30.Enabled = true;
                        vokabeln_40.Enabled = false;
                    }
                    else if (anzahl_gesamt - anzahl_noch_nicht >= 20 && anzahl_gesamt - anzahl_noch_nicht < 30)
                    {
                        vokabeln_20.Enabled = true;
                        vokabeln_30.Enabled = false;
                        vokabeln_40.Enabled = false;
                    }
                    else if (anzahl_gesamt - anzahl_noch_nicht < 20)
                    {
                        vokabeln_20.Enabled = false;
                        vokabeln_30.Enabled = false;
                        vokabeln_40.Enabled = false;
                    }
                }
            }
        }

        private void art_noch_nicht_CheckedChanged(object sender, EventArgs e)
        {
            if (art_noch_nicht.Checked == true)
            {
                anzahl.Maximum = anzahl_noch_nicht;

                if (anzahl_noch_nicht >= 40)
                {
                    vokabeln_20.Enabled = true;
                    vokabeln_30.Enabled = true;
                    vokabeln_40.Enabled = true;
                }
                else if (anzahl_noch_nicht >= 30 && anzahl_noch_nicht < 40)
                {
                    vokabeln_20.Enabled = true;
                    vokabeln_30.Enabled = true;
                    vokabeln_40.Enabled = false;
                }
                else if (anzahl_noch_nicht >= 20 && anzahl_noch_nicht < 30)
                {
                    vokabeln_20.Enabled = true;
                    vokabeln_30.Enabled = false;
                    vokabeln_40.Enabled = false;
                }
                else if (anzahl_noch_nicht < 20)
                {
                    vokabeln_20.Enabled = false;
                    vokabeln_30.Enabled = false;
                    vokabeln_40.Enabled = false;
                }

                //Zeitlich abschalten

                zeitlich_kuerzlich.Enabled = false;
                zeitlich_laengst.Enabled = false;

                zeitlich_alle.Checked = true;
            }
            else
            {
                //Zeitlich einschalten

                zeitlich_kuerzlich.Enabled = true;
                zeitlich_laengst.Enabled = true;
            }
        }

        private void art_falsch_CheckedChanged(object sender, EventArgs e)
        {
            if (art_falsch.Checked == true)
            {
                anzahl.Maximum = anzahl_falsch;

                if (anzahl_falsch >= 40)
                {
                    vokabeln_20.Enabled = true;
                    vokabeln_30.Enabled = true;
                    vokabeln_40.Enabled = true;
                }
                else if (anzahl_falsch >= 30 && anzahl_falsch < 40)
                {
                    vokabeln_20.Enabled = true;
                    vokabeln_30.Enabled = true;
                    vokabeln_40.Enabled = false;
                }
                else if (anzahl_falsch >= 20 && anzahl_falsch < 30)
                {
                    vokabeln_20.Enabled = true;
                    vokabeln_30.Enabled = false;
                    vokabeln_40.Enabled = false;
                }
                else if (anzahl_falsch < 20)
                {
                    vokabeln_20.Enabled = false;
                    vokabeln_30.Enabled = false;
                    vokabeln_40.Enabled = false;
                }
            }
        }

        private void art_richtig_CheckedChanged(object sender, EventArgs e)
        {
            if (art_richtig.Checked == true)
            {
                anzahl.Maximum = anzahl_richtig;

                if (anzahl_richtig >= 40)
                {
                    vokabeln_20.Enabled = true;
                    vokabeln_30.Enabled = true;
                    vokabeln_40.Enabled = true;
                }
                else if (anzahl_richtig >= 30 && anzahl_richtig < 40)
                {
                    vokabeln_20.Enabled = true;
                    vokabeln_30.Enabled = true;
                    vokabeln_40.Enabled = false;
                }
                else if (anzahl_richtig >= 20 && anzahl_richtig < 30)
                {
                    vokabeln_20.Enabled = true;
                    vokabeln_30.Enabled = false;
                    vokabeln_40.Enabled = false;
                }
                else if (anzahl_richtig < 20)
                {
                    vokabeln_20.Enabled = false;
                    vokabeln_30.Enabled = false;
                    vokabeln_40.Enabled = false;
                }
            }
        }


        //Welche Vokabeln haben priorität

        private void zeitlich_alle_CheckedChanged(object sender, EventArgs e)
        {
            art_alle_CheckedChanged(sender, e);
        }

        private void zeitlich_laengst_CheckedChanged(object sender, EventArgs e)
        {
            if (zeitlich_laengst.Checked == true)
            {
                if (art_alle.Checked == true)
                {
                    art_alle_CheckedChanged(sender, e);
                }
                else if (art_falsch.Checked == true)
                {
                    art_falsch_CheckedChanged(sender, e);
                }
                else if (art_richtig.Checked == true)
                {
                    art_richtig_CheckedChanged(sender, e);
                }
            }
        }

        private void zeitlich_kuerzlich_CheckedChanged(object sender, EventArgs e)
        {

            if (zeitlich_kuerzlich.Checked == true)
            {
                if (art_alle.Checked == true)
                {
                    art_alle_CheckedChanged(sender, e);
                }
                else if (art_falsch.Checked == true)
                {
                    art_falsch_CheckedChanged(sender, e);
                }
                else if (art_richtig.Checked == true)
                {
                    art_richtig_CheckedChanged(sender, e);
                }
            }
        }
    }
}