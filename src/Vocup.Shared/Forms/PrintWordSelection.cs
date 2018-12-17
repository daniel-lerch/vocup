using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vocup.Properties;

namespace Vocup.Forms
{
    public partial class PrintWordSelection : Form
    {
        public PrintWordSelection()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.Print.GetHicon());
        }

        //Array, das die Status-Informationen der Vokabeln enthält
        public int[] vocable_state;

        private void BtnCheckAll_Click(object sender, EventArgs e) //Falls auf alle Auswählen geklickt wurde
        {
            ListBox.BeginUpdate();

            for (int i = 0; i < ListBox.Items.Count; i++)
            {
                ListBox.SetItemChecked(i, true);
            }

            ListBox.EndUpdate();

            BtnContinue.Enabled = true;
            BtnContinue.Focus(); //Fokus auf weiter-Button
        }

        private void BtnUncheckAll_Click(object sender, EventArgs e) //Falls auf alle Abwählen geklickt wurde
        {
            ListBox.BeginUpdate();

            for (int i = 0; i < ListBox.Items.Count; i++)
            {
                ListBox.SetItemChecked(i, false);
            }

            ListBox.EndUpdate();

            BtnContinue.Enabled = false;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            if (ListBox.CheckedItems.Count == 0)
            {
                BtnContinue.Enabled = false;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void RbList_CheckedChanged(object sender, EventArgs e)
        {
            //Schaut ob das GroupBox-Element aktiviert oder deaktiviert werden soll

            if (RbList.Checked == true)
            {
                GroupPracticeMode.Enabled = true;
            }
            else
            {
                GroupPracticeMode.Enabled = false;
            }
        }

        private void ListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //Schaut, ob alle Elemente abgewählt worden sind

            BtnContinue.Enabled = ListBox.CheckedItems.Count > 0;
        }

        //Nur Vokabeln üben die, noch nie geübt wurden
        private void CbUnpracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnCheckAll.Enabled)
            {
                //Falls auf alle Abwählen geklickt wurde

                int anzahl = ListBox.Items.Count;

                for (int i = 0; i < anzahl; i++)
                {
                    ListBox.SetItemChecked(i, false);
                }

                BtnCheckAll.Enabled = false;
                BtnUncheckAll.Enabled = false;
            }

            if (CbUnpracticed.Checked)
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] == 0)
                    {
                        ListBox.SetItemChecked(i, true);
                    }
                }

                BtnContinue.Enabled = true;
            }
            else
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] == 0)
                    {
                        ListBox.SetItemChecked(i, false);
                    }
                }
            }

            if (CbWronglyPracticed.Checked == false && CbCorrectlyPracticed.Checked == false && CbUnpracticed.Checked == false && CbFullyPracticed.Checked == false)
            {
                BtnCheckAll.Enabled = true;
                BtnUncheckAll.Enabled = true;

                BtnCheckAll_Click(sender, e);
            }
        }

        //Falsch übersetzt wurden
        private void CbWronglyPracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnCheckAll.Enabled == true)
            {
                //Falls auf alle Abwählen geklickt wurde

                int anzahl = ListBox.Items.Count;

                for (int i = 0; i < anzahl; i++)
                {
                    ListBox.SetItemChecked(i, false);
                }

                BtnCheckAll.Enabled = false;
                BtnUncheckAll.Enabled = false;
            }

            if (CbWronglyPracticed.Checked == true)
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] == 1)
                    {
                        ListBox.SetItemChecked(i, true);
                    }
                }

                BtnContinue.Enabled = true;
            }
            else
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] == 1)
                    {
                        ListBox.SetItemChecked(i, false);
                    }
                }
            }

            if (CbWronglyPracticed.Checked == false && CbCorrectlyPracticed.Checked == false && CbUnpracticed.Checked == false && CbFullyPracticed.Checked == false)
            {
                BtnCheckAll.Enabled = true;
                BtnUncheckAll.Enabled = true;

                BtnCheckAll_Click(sender, e);
            }
        }

        //mindestens einmal richtig geübt wurden
        private void CbCorrectlyPracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnCheckAll.Enabled == true)
            {
                //Falls auf alle Abwählen geklickt wurde

                int anzahl = ListBox.Items.Count;

                for (int i = 0; i < anzahl; i++)
                {
                    ListBox.SetItemChecked(i, false);
                }

                BtnCheckAll.Enabled = false;
                BtnUncheckAll.Enabled = false;
            }

            if (CbCorrectlyPracticed.Checked == true)
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] > 1 && vocable_state[i] <= Properties.Settings.Default.MaxPracticeCount)
                    {
                        ListBox.SetItemChecked(i, true);
                    }
                }
                BtnContinue.Enabled = true;
            }
            else
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] > 1 && vocable_state[i] <= Properties.Settings.Default.MaxPracticeCount)
                    {
                        ListBox.SetItemChecked(i, false);
                    }
                }
            }


            if (CbWronglyPracticed.Checked == false && CbCorrectlyPracticed.Checked == false && CbUnpracticed.Checked == false && CbFullyPracticed.Checked == false)
            {
                BtnCheckAll.Enabled = true;
                BtnUncheckAll.Enabled = true;

                BtnCheckAll_Click(sender, e);
            }
        }

        //als gelernt markiert wurden
        private void CbFullyPracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnCheckAll.Enabled == true)
            {
                //Falls auf alle Abwählen geklickt wurde

                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    ListBox.SetItemChecked(i, false);
                }

                BtnCheckAll.Enabled = false;
                BtnUncheckAll.Enabled = false;
            }

            if (CbFullyPracticed.Checked == true)
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] > Settings.Default.MaxPracticeCount)
                    {
                        ListBox.SetItemChecked(i, true);
                    }
                }

                BtnContinue.Enabled = true;
            }
            else
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] > Settings.Default.MaxPracticeCount)
                    {
                        ListBox.SetItemChecked(i, false);
                    }
                }
            }

            if (CbWronglyPracticed.Checked == false && CbCorrectlyPracticed.Checked == false && CbUnpracticed.Checked == false && CbFullyPracticed.Checked == false)
            {
                BtnCheckAll.Enabled = true;
                BtnUncheckAll.Enabled = true;

                BtnCheckAll_Click(sender, e);
            }
        }
    }
}