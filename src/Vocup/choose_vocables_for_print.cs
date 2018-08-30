using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vocup.Properties;

namespace Vocup
{
    public partial class choose_vocables_for_print : Form
    {
        public choose_vocables_for_print()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.Print.GetHicon());
        }

        //Array, das die Status-Informationen der Vokabeln enthält

        public int[] vocable_state;


        private void check_all_Click(object sender, EventArgs e) //Falls auf alle Auswählen geklickt wurde
        {
            for (int i = 0; i < listbox.Items.Count; i++)
            {
                listbox.SetItemChecked(i, true);
            }

            print_button.Enabled = true;
            print_button.Focus(); //Fokus auf weiter-Button
        }

        private void discheck_all_Click(object sender, EventArgs e) //Falls auf alle Abwählen geklickt wurde
        {
            for (int i = 0; i < listbox.Items.Count; i++)
            {
                listbox.SetItemChecked(i, false);
            }

            print_button.Enabled = false;
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void print_button_Click(object sender, EventArgs e)
        {
            if (listbox.CheckedItems.Count == 0)
            {
                print_button.Enabled = false;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void print_button_MouseEnter(object sender, EventArgs e)
        {
            if (listbox.CheckedItems.Count == 0)
            {
                print_button.Enabled = false;
            }
        }


        private void radioButton_liste_CheckedChanged(object sender, EventArgs e)
        {

            //Schaut ob das GroupBox-Element aktiviert oder deaktiviert werden soll

            if (radioButton_liste.Checked == true)
            {
                groupBox3.Enabled = true;
            }
            else
            {
                groupBox3.Enabled = false;
            }
        }

        private void listbox_SelectedValueChanged(object sender, EventArgs e)
        {
            //Schaut, ob alle Elemente abgewählt worden sind

            if (listbox.CheckedItems.Count == 0)
            {
                print_button.Enabled = false;
            }
            else if (listbox.CheckedItems.Count > 0)
            {
                print_button.Enabled = true;
            }
        }

        //Nur Vokabeln üben die,

        //Noch nie geübt wurden
        private void checkBox_noch_nie_CheckedChanged(object sender, EventArgs e)
        {
            if (check_all.Enabled == true)
            {
                //Falls auf alle Abwählen geklickt wurde

                int anzahl = listbox.Items.Count;

                for (int i = 0; i < anzahl; i++)
                {
                    listbox.SetItemChecked(i, false);
                }

                check_all.Enabled = false;
                discheck_all.Enabled = false;
            }


            if (checkBox_noch_nie.Checked == true)
            {

                for (int i = 0; i < listbox.Items.Count; i++)
                {
                    if (vocable_state[i] == 0)
                    {
                        listbox.SetItemChecked(i, true);
                    }

                }

                print_button.Enabled = true;
            }
            else
            {
                for (int i = 0; i < listbox.Items.Count; i++)
                {
                    if (vocable_state[i] == 0)
                    {
                        listbox.SetItemChecked(i, false);
                    }
                }
            }


            if (checkBox_falsch.Checked == false && checkBox_mindestens_einmal.Checked == false && checkBox_noch_nie.Checked == false && checkBox_fertig.Checked == false)
            {
                check_all.Enabled = true;
                discheck_all.Enabled = true;

                check_all_Click(sender, e);
            }
        }

        //Falsch übersetzt wurden
        private void checkBox_falsch_CheckedChanged(object sender, EventArgs e)
        {
            if (check_all.Enabled == true)
            {
                //Falls auf alle Abwählen geklickt wurde

                int anzahl = listbox.Items.Count;

                for (int i = 0; i < anzahl; i++)
                {
                    listbox.SetItemChecked(i, false);
                }

                check_all.Enabled = false;
                discheck_all.Enabled = false;


            }

            if (checkBox_falsch.Checked == true)
            {

                for (int i = 0; i < listbox.Items.Count; i++)
                {
                    if (vocable_state[i] == 1)
                    {
                        listbox.SetItemChecked(i, true);
                    }

                }

                print_button.Enabled = true;
            }
            else
            {
                for (int i = 0; i < listbox.Items.Count; i++)
                {
                    if (vocable_state[i] == 1)
                    {
                        listbox.SetItemChecked(i, false);
                    }
                }
            }

            if (checkBox_falsch.Checked == false && checkBox_mindestens_einmal.Checked == false && checkBox_noch_nie.Checked == false && checkBox_fertig.Checked == false)
            {
                check_all.Enabled = true;
                discheck_all.Enabled = true;

                check_all_Click(sender, e);
            }
        }

        //mindestens einmal richtig geübt wurden
        private void checkBox_mindestens_einmal_CheckedChanged(object sender, EventArgs e)
        {
            if (check_all.Enabled == true)
            {
                //Falls auf alle Abwählen geklickt wurde

                int anzahl = listbox.Items.Count;

                for (int i = 0; i < anzahl; i++)
                {
                    listbox.SetItemChecked(i, false);
                }

                check_all.Enabled = false;
                discheck_all.Enabled = false;
            }

            if (checkBox_mindestens_einmal.Checked == true)
            {
                for (int i = 0; i < listbox.Items.Count; i++)
                {
                    if (vocable_state[i] > 1 && vocable_state[i] <= Properties.Settings.Default.MaxPracticeCount)
                    {
                        listbox.SetItemChecked(i, true);
                    }
                }
                print_button.Enabled = true;
            }
            else
            {
                for (int i = 0; i < listbox.Items.Count; i++)
                {
                    if (vocable_state[i] > 1 && vocable_state[i] <= Properties.Settings.Default.MaxPracticeCount)
                    {
                        listbox.SetItemChecked(i, false);
                    }
                }
            }


            if (checkBox_falsch.Checked == false && checkBox_mindestens_einmal.Checked == false && checkBox_noch_nie.Checked == false && checkBox_fertig.Checked == false)
            {
                check_all.Enabled = true;
                discheck_all.Enabled = true;

                check_all_Click(sender, e);
            }
        }

        //als gelernt markiert wurden
        private void checkBox_fertig_CheckedChanged(object sender, EventArgs e)
        {
            if (check_all.Enabled == true)
            {
                //Falls auf alle Abwählen geklickt wurde

                for (int i = 0; i < listbox.Items.Count; i++)
                {
                    listbox.SetItemChecked(i, false);
                }

                check_all.Enabled = false;
                discheck_all.Enabled = false;
            }

            if (checkBox_fertig.Checked == true)
            {
                for (int i = 0; i < listbox.Items.Count; i++)
                {
                    if (vocable_state[i] >= Properties.Settings.Default.MaxPracticeCount + 1)
                    {
                        listbox.SetItemChecked(i, true);
                    }
                }

                print_button.Enabled = true;
            }
            else
            {
                for (int i = 0; i < listbox.Items.Count; i++)
                {
                    if (vocable_state[i] >= Properties.Settings.Default.MaxPracticeCount + 1)
                    {
                        listbox.SetItemChecked(i, false);
                    }
                }
            }

            if (checkBox_falsch.Checked == false && checkBox_mindestens_einmal.Checked == false && checkBox_noch_nie.Checked == false && checkBox_fertig.Checked == false)
            {
                check_all.Enabled = true;
                discheck_all.Enabled = true;

                check_all_Click(sender, e);
            }
        }
    }
}