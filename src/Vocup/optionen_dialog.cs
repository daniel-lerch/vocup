using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Vocup
{
    public partial class optionen_dialog : Form
    {
        public optionen_dialog()
        {
            InitializeComponent();
        }

       

        private void optionen_dialog_Load(object sender, EventArgs e)
        {
            //Einstellungen ins Dialogfenster laden

            //Startbild   

            if (Properties.Settings.Default.startscreen == "zuletzt" || Properties.Settings.Default.startscreen == "willkommensbild")
            {
                zuletzt_geöffnet.Checked = true;
            }
            else
            {
                nichts_anzeigen.Checked = true;
            }
            

            //Vokabelheft automatisch speichern

            if (Properties.Settings.Default.auto_save == true)
            {
                auto_save_option.Checked = true;
            }
            else
            {
                auto_save_option.Checked = false;
            }

           //Automatisches Update

            if (Properties.Settings.Default.auto_update == true)
            {
                auto_update_option.Checked = true;
            }
            else
            {
                auto_update_option.Checked = false;
            }

            //Auswertung

            if (Properties.Settings.Default.show_practise_result_list == true)
            {
                practise_result_list_option.Checked = true;
            }
            else
            {
                practise_result_list_option.Checked = false;
            }

            //Pfad Vokabelhefte

            textbox_path_vhf.Text = Properties.Settings.Default.path_vhf;

            //Pfad Ergebnisse

            textbox_path_vhr.Text = Properties.Settings.Default.path_vhr;

            //Notensystem

            if (Properties.Settings.Default.notensystem == "de")
            {
                comboBox.SelectedItem = "Deutschland";
            }
            else
            {
                comboBox.SelectedItem = "Schweiz";
            }

            //Selber bewerten

            if (Properties.Settings.Default.selber_bewerten == true)
            {
                selber_bewerten_checkbox.Checked = true;
            }
            else
            {
                selber_bewerten_checkbox.Checked = false;
            }

            //Eingabefelder mit Farbe hervorheben

            if (Properties.Settings.Default.colored_textfields == true)
            {
                colored_textfield.Checked = true;
            }
            else
            {
                colored_textfield.Checked = false;
            }

            //Teilweise richtig

            if (Properties.Settings.Default.nearly_correct_blank_char == true)
            {
                checkbox_leerschläge.Checked = true;
            }
            else
            {
                checkbox_leerschläge.Checked = false;
            }

            if (Properties.Settings.Default.nearly_correct_punctuation_char == true)
            {
                checkbox_satzzeichen.Checked = true;
            }
            else
            {
                checkbox_satzzeichen.Checked = false;
            }

            if (Properties.Settings.Default.nearly_correct_special_char == true)
            {
                checkbox_sonderzeichen.Checked = true;
            }
            else
            {
                checkbox_sonderzeichen.Checked = false;
            }

            if (Properties.Settings.Default.nearly_correct_artical == true)
            {
                checkbox_artikel.Checked = true;
            }
            else
            {
                checkbox_artikel.Checked = false;
            }

            if (Properties.Settings.Default.nearly_correct_synonym == true)
            {
                checkbox_synonyme.Checked = true;
            }
            else
            {
                checkbox_synonyme.Checked = false;
            }
            //Fortfahren-Button
            if (Properties.Settings.Default.only_one_click == true)
            {
                continue_button.Checked = true;
            }
            else
            {
                continue_button.Checked = false;
            }

            //Klänge

            if (Properties.Settings.Default.sound == false)
            {
                sound.Checked = false;
            }
            else
            {
                sound.Checked = true;
            }

            //Anzahl richtig

            max_richtig.Value = Properties.Settings.Default.max;


            //Max von trackbar-anzahl_richtig_falsch ermitteln

            trackBar_anzahl_falsch_richtig.Maximum = 10 - trackBar_anzahl_noch_nicht.Value;

            //Prozentualer Anteil an noch nicht geübten Vokabeln

            anzahl_noch_nicht_label.Text = Properties.Settings.Default.prozent_noch_nicht + "%";

            trackBar_anzahl_noch_nicht.Value = Properties.Settings.Default.prozent_noch_nicht / 10;

            //Prozentualer Anteil an falsch geübten Vokabeln

            anzahl_falsch_label.Text = Properties.Settings.Default.prozent_falsch + "%";

            //Prozentualer Anteil an richtig geübten Vokabeln

            anzahl_richtig_label.Text = Properties.Settings.Default.prozent_richtig + "%";

            //Trackbar anzahl_falsch_richtig

            trackBar_anzahl_falsch_richtig.Maximum = (Properties.Settings.Default.prozent_richtig + Properties.Settings.Default.prozent_falsch) / 10 - 1;
            trackBar_anzahl_falsch_richtig.Value = Properties.Settings.Default.prozent_richtig / 10;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            //Einstellungen speichern

            // Startbild

            if (zuletzt_geöffnet.Checked == true)
            {
                Properties.Settings.Default.startscreen = "zuletzt";
            }

            else
            {
                Properties.Settings.Default.startscreen = "nichts";
            }


            //Vokabelheft automatisch speichern

            if (auto_save_option.Checked == true)
            {
                Properties.Settings.Default.auto_save = true;
            }

            else
            {
                Properties.Settings.Default.auto_save = false;
            }

            //Automatisches Update

            if (auto_update_option.Checked == true)
            {
                Properties.Settings.Default.auto_update = true;
            }

            else
            {
                Properties.Settings.Default.auto_update = false;
            }

            //Auswertung

            if (practise_result_list_option.Checked == true)
            {
                Properties.Settings.Default.show_practise_result_list = true;
            }
            else
            {
                Properties.Settings.Default.show_practise_result_list = false;
            }

            //Pfad Vokabelhefte

            if (textbox_path_vhf.Text != Properties.Settings.Default.path_vhf)
            {
                Properties.Settings.Default.path_vhf = textbox_path_vhf.Text;
            }

            //Pfad Ergebnisse

            if (textbox_path_vhr.Text != Properties.Settings.Default.path_vhr)
            {
                Properties.Settings.Default.path_vhr = textbox_path_vhr.Text;
            }
            
            //Notensystem

            if (comboBox.SelectedItem.ToString() == "Deutschland")
            {
                Properties.Settings.Default.notensystem = "de";
            }

            else
            {
                Properties.Settings.Default.notensystem = "ch";
            }
            
            //Übersetzungen selber bewerten

            if (selber_bewerten_checkbox.Checked == true)
            {
                Properties.Settings.Default.selber_bewerten = true;
            }
            else
            {
                Properties.Settings.Default.selber_bewerten = false;
            }

            //Eingabefelder mit Farbe hervorheben

            if (colored_textfield.Checked == true)
            {
                Properties.Settings.Default.colored_textfields = true;
            }
            else
            {
                Properties.Settings.Default.colored_textfields = false;
            }

            //Teilweise richtig

            if (checkbox_leerschläge.Checked == true)
            {
                Properties.Settings.Default.nearly_correct_blank_char = true;
            }
            else
            {
                Properties.Settings.Default.nearly_correct_blank_char = false;
            }

            if (checkbox_satzzeichen.Checked == true)
            {
                Properties.Settings.Default.nearly_correct_punctuation_char = true;
            }
            else
            {
                Properties.Settings.Default.nearly_correct_punctuation_char = false;
            }

            if ( checkbox_sonderzeichen.Checked == true)
            {
                Properties.Settings.Default.nearly_correct_special_char = true;
            }
            else
            {
                Properties.Settings.Default.nearly_correct_special_char = false;
            }

            if (checkbox_artikel.Checked == true)
            {
                
                Properties.Settings.Default.nearly_correct_artical = true;
            }
            else
            {
                Properties.Settings.Default.nearly_correct_artical = false;
            }

            if (checkbox_synonyme.Checked == true)
            {
                Properties.Settings.Default.nearly_correct_synonym = true;
            }
            else
            {
                Properties.Settings.Default.nearly_correct_synonym = false;
            }
            
            //Forfahren-Button

            if (continue_button.Checked == true)
            {
                Properties.Settings.Default.only_one_click = true;
            }
            else
            {
                Properties.Settings.Default.only_one_click = false;
            }

            //Akustische Klänge

            if (sound.Checked == true)
            {
                Properties.Settings.Default.sound = true;
            }
            else
            {
                Properties.Settings.Default.sound = false;
            }

            //Anzahl richtig

            Properties.Settings.Default.max = max_richtig.Value;

            //Prozentuale Anteile

            Properties.Settings.Default.prozent_noch_nicht = trackBar_anzahl_noch_nicht.Value * 10;
            Properties.Settings.Default.prozent_richtig = trackBar_anzahl_falsch_richtig.Value * 10;
            Properties.Settings.Default.prozent_falsch = (10 - trackBar_anzahl_noch_nicht.Value - trackBar_anzahl_falsch_richtig.Value) * 10;

            //Einstellungen speichern

            Properties.Settings.Default.Save();

            //Dialogfenster beenden

            Close();
        }


        //Werte zurücksetzen

        private void zurücksetzen_button_Click(object sender, EventArgs e)
        {
            //Einstellungen zurücksetzen

            max_richtig.Value = 3;

            trackBar_anzahl_noch_nicht.Value = 5;
            trackBar_anzahl_falsch_richtig.Value = 2;
        }

        //Falls der Wert von trackBar_anzahl_noch_nicht verändert wurde

        private void trackBar_anzahl_noch_nicht_ValueChanged(object sender, EventArgs e)
        {
            anzahl_noch_nicht_label.Text = trackBar_anzahl_noch_nicht.Value * 10 + "%";
            trackBar_anzahl_falsch_richtig.Maximum = 10 - trackBar_anzahl_noch_nicht.Value - 1;

            if (trackBar_anzahl_noch_nicht.Value == 8)
            {
                trackBar_anzahl_falsch_richtig.Enabled = false;
                anzahl_falsch_label.Text = "10%";
                anzahl_richtig_label.Text = "10%";
            }
            else
            {
                trackBar_anzahl_falsch_richtig.Enabled = true;
                anzahl_richtig_label.Text = trackBar_anzahl_falsch_richtig.Value * 10 + "%";
                anzahl_falsch_label.Text = (10 - trackBar_anzahl_noch_nicht.Value - trackBar_anzahl_falsch_richtig.Value) * 10 + "%";
            }
        }

        private void trackBar_anzahl_falsch_richtig_ValueChanged(object sender, EventArgs e)
        {
            anzahl_richtig_label.Text = trackBar_anzahl_falsch_richtig.Value * 10 + "%";
            anzahl_falsch_label.Text = (10 - trackBar_anzahl_noch_nicht.Value - trackBar_anzahl_falsch_richtig.Value) * 10 + "%";
        }

        private void practise_result_list_option_CheckedChanged(object sender, EventArgs e)
        {
            if (practise_result_list_option.Checked == false)
            {
                comboBox.Enabled = false;
            }
            else
            {
                comboBox.Enabled = true;
            }
        }

        //Verzeichnis für VHF-Dateien auswählen

        private void button_path_vhf_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder_dialog = new FolderBrowserDialog();

            folder_dialog.SelectedPath = Properties.Settings.Default.path_vhf;
            
            
            if (folder_dialog.ShowDialog() == DialogResult.OK)
            {
                textbox_path_vhf.Text = folder_dialog.SelectedPath;

            }
        }

        //Verzeichnis für VHR-Dateien auswählen

        private void button_path_vhr_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder_dialog = new FolderBrowserDialog();

            folder_dialog.SelectedPath = Properties.Settings.Default.path_vhr;


            if (folder_dialog.ShowDialog() == DialogResult.OK)
            {
                textbox_path_vhr.Text = folder_dialog.SelectedPath;

            }
        }

    }
}