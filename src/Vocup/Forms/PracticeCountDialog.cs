using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Vocup.Forms
{
    public partial class PracticeCountDialog : Form
    {
        public PracticeCountDialog()
        {
            InitializeComponent();
        }

        public string button;
        public int anzahl_noch_nicht;
        public int anzahl_falsch;
        public int anzahl_richtig;
        public int anzahl_gesamt;

        private void BtnCount20_Click(object sender, EventArgs e)
        {
            button = "20";
            Close();
        }

        private void BtnCount30_Click(object sender, EventArgs e)
        {
            button = "30";
            Close();
        }

        private void BtnCount40_Click(object sender, EventArgs e)
        {
            button = "40";
            Close();
        }

        private void BtnCountAll_Click(object sender, EventArgs e)
        {
            button = "alle";
            Close();
        }

        private void BtnCountCustom_Click(object sender, EventArgs e)
        {
            button = "individuell";
            Close();
        }


        private void RbAllStates_CheckedChanged(object sender, EventArgs e)
        {
            if (RbAllStates.Checked)
            {
                int count;

                if (RbAllDates.Checked)
                    count = anzahl_gesamt;
                else
                    count = anzahl_gesamt - anzahl_noch_nicht;

                anzahl.Maximum = count;

                BtnCount20.Enabled = count >= 20;
                BtnCount30.Enabled = count >= 30;
                BtnCount40.Enabled = count >= 40;
            }
        }

        private void RbUnpracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (RbUnpracticed.Checked)
            {
                anzahl.Maximum = anzahl_noch_nicht;

                BtnCount20.Enabled = anzahl_noch_nicht >= 20;
                BtnCount30.Enabled = anzahl_noch_nicht >= 30;
                BtnCount40.Enabled = anzahl_noch_nicht >= 40;

                //Zeitlich abschalten
                RbLaterPracticed.Enabled = false;
                RbEarlierPracticed.Enabled = false;
                RbAllDates.Checked = true;
            }
            else
            {
                //Zeitlich einschalten
                RbLaterPracticed.Enabled = true;
                RbEarlierPracticed.Enabled = true;
            }
        }

        private void RbWronglyPracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (RbWronglyPracticed.Checked)
            {
                anzahl.Maximum = anzahl_falsch;

                BtnCount20.Enabled = anzahl_falsch >= 20;
                BtnCount30.Enabled = anzahl_falsch >= 30;
                BtnCount40.Enabled = anzahl_falsch >= 40;
            }
        }

        private void RbCorrectlyPracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (RbCorrectlyPracticed.Checked)
            {
                anzahl.Maximum = anzahl_richtig;

                BtnCount20.Enabled = anzahl_richtig >= 20;
                BtnCount30.Enabled = anzahl_richtig >= 30;
                BtnCount40.Enabled = anzahl_richtig >= 40;
            }
        }


        //Welche Vokabeln haben priorität

        private void zeitlich_alle_CheckedChanged(object sender, EventArgs e)
        {
            RbAllStates_CheckedChanged(sender, e);
        }

        private void zeitlich_laengst_CheckedChanged(object sender, EventArgs e)
        {
            if (RbEarlierPracticed.Checked == true)
            {
                if (RbAllStates.Checked == true)
                {
                    RbAllStates_CheckedChanged(sender, e);
                }
                else if (RbWronglyPracticed.Checked == true)
                {
                    RbWronglyPracticed_CheckedChanged(sender, e);
                }
                else if (RbCorrectlyPracticed.Checked == true)
                {
                    RbCorrectlyPracticed_CheckedChanged(sender, e);
                }
            }
        }

        private void zeitlich_kuerzlich_CheckedChanged(object sender, EventArgs e)
        {
            if (RbLaterPracticed.Checked == true)
            {
                if (RbAllStates.Checked == true)
                {
                    RbAllStates_CheckedChanged(sender, e);
                }
                else if (RbWronglyPracticed.Checked == true)
                {
                    RbWronglyPracticed_CheckedChanged(sender, e);
                }
                else if (RbCorrectlyPracticed.Checked == true)
                {
                    RbCorrectlyPracticed_CheckedChanged(sender, e);
                }
            }
        }
    }
}