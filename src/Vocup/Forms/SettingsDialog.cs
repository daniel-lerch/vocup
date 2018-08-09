using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vocup.Properties;

namespace Vocup
{
    public partial class SettingsDialog : Form
    {
        private Settings settings;

        public SettingsDialog()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.settings.GetHicon());
            settings = Settings.Default;
        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            // Startbild   
            RbRecentFile.Checked = settings.startscreen == "zuletzt" || settings.startscreen == "willkommensbild";

            // Vokabelheft automatisch speichern
            CbAutoSave.Checked = settings.auto_save;

            // Automatisches Update
            CbAutoUpdate.Checked = settings.auto_update;

            // list view grid lines
            CbGridLines.Checked = settings.GridLines;

            // Pfad Vokabelhefte
            TbVhfPath.Text = settings.VhfPath;

            // Pfad Ergebnisse
            TbVhrPath.Text = settings.VhrPath;

            // Selber bewerten
            CbManualCheck.Checked = settings.UserEvaluates;

            // Eingabefelder mit Farbe hervorheben
            CbColoredTextfield.Checked = settings.PracticeInputBackColor != SystemColors.Window;

            // Teilweise richtig
            checkbox_leerschläge.Checked = settings.nearly_correct_blank_char;
            checkbox_satzzeichen.Checked = settings.nearly_correct_punctuation_char;
            checkbox_sonderzeichen.Checked = settings.nearly_correct_special_char;
            checkbox_artikel.Checked = settings.nearly_correct_artical;
            checkbox_synonyme.Checked = settings.nearly_correct_synonym;

            // Fortfahren-Button
            CbSingleContinueButton.Checked = settings.only_one_click;

            // Klänge
            CbAcousticFeedback.Checked = settings.sound;

            // Auswertung
            CbPracticeResult.Checked = settings.show_practise_result_list;

            // Notensystem
            if (settings.notensystem == "de")
                CbEvaluationSystem.SelectedItem = "Deutschland";
            else
                CbEvaluationSystem.SelectedItem = "Schweiz";


            // Anzahl richtig
            TrbRepetitions.Value = settings.MaxPracticeCount;

            // Max von trackbar-anzahl_richtig_falsch ermitteln
            TrbWrongRigtht.Maximum = 10 - TrbUnknown.Value;

            // Prozentualer Anteil an noch nicht geübten Vokabeln
            anzahl_noch_nicht_label.Text = settings.prozent_noch_nicht + "%";
            TrbUnknown.Value = settings.prozent_noch_nicht / 10;

            // Prozentualer Anteil an falsch geübten Vokabeln
            anzahl_falsch_label.Text = settings.prozent_falsch + "%";

            // Prozentualer Anteil an richtig geübten Vokabeln
            anzahl_richtig_label.Text = settings.prozent_richtig + "%";

            // Trackbar anzahl_falsch_richtig
            TrbWrongRigtht.Maximum = (settings.prozent_richtig + settings.prozent_falsch) / 10 - 1;
            TrbWrongRigtht.Value = settings.prozent_richtig / 10;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            // Einstellungen speichern

            // Startbild
            settings.startscreen = RbRecentFile.Checked ? "zuletzt" : "nichts";

            // Vokabelheft automatisch speichern
            settings.auto_save = CbAutoSave.Checked;

            // Automatisches Update
            settings.auto_update = CbAutoUpdate.Checked;

            // list view grid lines
            settings.GridLines = CbGridLines.Checked;

            // Pfad Vokabelhefte
            settings.VhfPath = TbVhfPath.Text;

            // Pfad Ergebnisse
            settings.VhrPath = TbVhrPath.Text;

            // Auswertung
            settings.show_practise_result_list = CbPracticeResult.Checked;

            // Notensystem
            if (CbEvaluationSystem.SelectedItem.ToString() == "Deutschland")
            {
                settings.notensystem = "de";
            }
            else
            {
                settings.notensystem = "ch";
            }

            // Übersetzungen selber bewerten
            settings.UserEvaluates = CbManualCheck.Checked;

            // Eingabefelder mit Farbe hervorheben
            settings.PracticeInputBackColor = CbColoredTextfield.Checked ? Color.FromArgb(250, 250, 150) : SystemColors.Window;

            // Teilweise richtig
            settings.nearly_correct_blank_char = checkbox_leerschläge.Checked;
            settings.nearly_correct_punctuation_char = checkbox_satzzeichen.Checked;
            settings.nearly_correct_special_char = checkbox_sonderzeichen.Checked;
            settings.nearly_correct_artical = checkbox_artikel.Checked;
            settings.nearly_correct_synonym = checkbox_synonyme.Checked;

            // Fortfahren-Button
            settings.only_one_click = CbSingleContinueButton.Checked;

            // Akustische Rückmeldung
            settings.sound = CbAcousticFeedback.Checked;

            // Anzahl richtig
            settings.MaxPracticeCount = TrbRepetitions.Value;

            // Prozentuale Anteile
            settings.prozent_noch_nicht = TrbUnknown.Value * 10;
            settings.prozent_richtig = TrbWrongRigtht.Value * 10;
            settings.prozent_falsch = (10 - TrbUnknown.Value - TrbWrongRigtht.Value) * 10;

            // Einstellungen speichern
            settings.Save();

            // Dialogfenster beenden
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnResetStartScreen_Click(object sender, EventArgs e)
        {
            settings.startscreen = "willkommensbild";
            RbRecentFile.Checked = true;
        }

        private void BtnResetPractice_Click(object sender, EventArgs e)
        {
            // Einstellungen zurücksetzen

            TrbRepetitions.Value = 3;

            TrbUnknown.Value = 5;
            TrbWrongRigtht.Value = 2;
        }

        private void TrbUnknown_ValueChanged(object sender, EventArgs e)
        {
            anzahl_noch_nicht_label.Text = TrbUnknown.Value * 10 + "%";
            TrbWrongRigtht.Maximum = 10 - TrbUnknown.Value - 1;

            if (TrbUnknown.Value == 8)
            {
                TrbWrongRigtht.Enabled = false;
                anzahl_falsch_label.Text = "10%";
                anzahl_richtig_label.Text = "10%";
            }
            else
            {
                TrbWrongRigtht.Enabled = true;
                anzahl_richtig_label.Text = TrbWrongRigtht.Value * 10 + "%";
                anzahl_falsch_label.Text = (10 - TrbUnknown.Value - TrbWrongRigtht.Value) * 10 + "%";
            }
        }

        private void TrbWrongRight_ValueChanged(object sender, EventArgs e)
        {
            anzahl_richtig_label.Text = TrbWrongRigtht.Value * 10 + "%";
            anzahl_falsch_label.Text = (10 - TrbUnknown.Value - TrbWrongRigtht.Value) * 10 + "%";
        }

        private void CbPracticeResult_CheckedChanged(object sender, EventArgs e)
        {
            CbEvaluationSystem.Enabled = CbPracticeResult.Checked;
        }

        // Verzeichnis für VHF-Dateien auswählen
        private void BtnVhfPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = settings.VhfPath;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    TbVhfPath.Text = fbd.SelectedPath;
                }
            }
        }

        // Verzeichnis für VHR-Dateien auswählen
        private void BtnVhrPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = settings.VhrPath;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    TbVhrPath.Text = fbd.SelectedPath;
                }
            }
        }
    }
}