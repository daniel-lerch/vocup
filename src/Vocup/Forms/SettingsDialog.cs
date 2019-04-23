using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup
{
    public partial class SettingsDialog : Form
    {
        private readonly Settings settings;

        public SettingsDialog()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.Settings.GetHicon());
            settings = Settings.Default;
        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            // Startbild   
            RbRecentFile.Checked = settings.StartScreen == (int)StartScreen.LastFile || settings.StartScreen == (int)StartScreen.AboutBox;

            // Vokabelheft automatisch speichern
            CbAutoSave.Checked = settings.AutoSave;

            // Automatisches Update
            if (AppInfo.IsUwp())
            {
                GroupUpdate.Enabled = false;
                CbDisableInternetServices.Checked = true;
            }
            else
            {
                CbDisableInternetServices.Text += $" ({Words.NotRecommended})";
                CbDisableInternetServices.Checked = settings.DisableInternetServices;
            }

            // ListView
            CbGridLines.Checked = settings.GridLines;
            CbColumnResize.Checked = settings.ColumnResize;

            // Pfad Vokabelhefte
            TbVhfPath.Text = settings.VhfPath;

            // Pfad Ergebnisse
            TbVhrPath.Text = settings.VhrPath;

            // Selber bewerten
            CbManualCheck.Checked = settings.UserEvaluates;

            // Eingabefelder mit Farbe hervorheben
            CbColoredTextfield.Checked = settings.PracticeInputBackColor != SystemColors.Window;

            // Teilweise richtig
            checkbox_leerschläge.Checked = settings.EvaluateTolerateWhiteSpace;
            checkbox_satzzeichen.Checked = settings.EvaluateToleratePunctuationMark;
            checkbox_sonderzeichen.Checked = settings.EvaluateTolerateSpecialChar;
            checkbox_artikel.Checked = settings.EvaluateTolerateArticle;
            checkbox_synonyme.Checked = settings.EvaluateTolerateNoSynonym;

            // Fortfahren-Button
            CbSingleContinueButton.Checked = settings.PracticeFastContinue;

            // Klänge
            CbAcousticFeedback.Checked = settings.PracticeSoundFeedback;

            // Auswertung
            CbPracticeResult.Checked = settings.PracticeShowResultList;

            // Notensystem
            if (settings.PracticeGradeCulture == "de-DE")
                CbEvaluationSystem.SelectedItem = "Deutschland";
            else
                CbEvaluationSystem.SelectedItem = "Schweiz";


            // Anzahl richtig
            TrbRepetitions.Value = settings.MaxPracticeCount;

            // Max von trackbar-anzahl_richtig_falsch ermitteln
            TrbWrongRigtht.Maximum = 10 - TrbUnknown.Value;

            // Prozentualer Anteil an noch nicht geübten Vokabeln
            LbUnpracticed.Text = settings.PracticePercentageUnpracticed + "%";
            TrbUnknown.Value = settings.PracticePercentageUnpracticed / 10;

            // Prozentualer Anteil an falsch geübten Vokabeln
            LbWronglyPracticed.Text = settings.PracticePercentageWrong + "%";

            // Prozentualer Anteil an richtig geübten Vokabeln
            LbCorrectlyPracticed.Text = settings.PracticePercentageCorrect + "%";

            // Trackbar anzahl_falsch_richtig
            TrbWrongRigtht.Maximum = (settings.PracticePercentageCorrect + settings.PracticePercentageWrong) / 10 - 1;
            TrbWrongRigtht.Value = settings.PracticePercentageCorrect / 10;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            // Startbild
            settings.StartScreen = RbRecentFile.Checked ? (int)StartScreen.LastFile : (int)StartScreen.None;

            // Vokabelheft automatisch speichern
            settings.AutoSave = CbAutoSave.Checked;

            // Automatisches Update
            settings.DisableInternetServices = CbDisableInternetServices.Checked;

            // ListView
            settings.GridLines = CbGridLines.Checked;
            settings.ColumnResize = CbColumnResize.Checked;

            // Pfad Vokabelhefte
            settings.VhfPath = TbVhfPath.Text;

            // Pfad Ergebnisse
            settings.VhrPath = TbVhrPath.Text;

            // Auswertung
            settings.PracticeShowResultList = CbPracticeResult.Checked;

            // Notensystem
            if (CbEvaluationSystem.SelectedItem.ToString() == "Deutschland")
            {
                settings.PracticeGradeCulture = "de-DE";
            }
            else
            {
                settings.PracticeGradeCulture = "de-CH";
            }

            // Übersetzungen selber bewerten
            settings.UserEvaluates = CbManualCheck.Checked;

            // Eingabefelder mit Farbe hervorheben
            settings.PracticeInputBackColor = CbColoredTextfield.Checked ? Color.FromArgb(250, 250, 150) : SystemColors.Window;

            // Teilweise richtig
            settings.EvaluateTolerateWhiteSpace = checkbox_leerschläge.Checked;
            settings.EvaluateToleratePunctuationMark = checkbox_satzzeichen.Checked;
            settings.EvaluateTolerateSpecialChar = checkbox_sonderzeichen.Checked;
            settings.EvaluateTolerateArticle = checkbox_artikel.Checked;
            settings.EvaluateTolerateNoSynonym = checkbox_synonyme.Checked;

            // Fortfahren-Button
            settings.PracticeFastContinue = CbSingleContinueButton.Checked;

            // Akustische Rückmeldung
            settings.PracticeSoundFeedback = CbAcousticFeedback.Checked;

            // Anzahl richtig
            settings.MaxPracticeCount = TrbRepetitions.Value;

            // Prozentuale Anteile
            settings.PracticePercentageUnpracticed = TrbUnknown.Value * 10;
            settings.PracticePercentageCorrect = TrbWrongRigtht.Value * 10;
            settings.PracticePercentageWrong = (10 - TrbUnknown.Value - TrbWrongRigtht.Value) * 10;

            // Einstellungen speichern
            settings.Save();

            // Dialogfenster beenden
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnResetStartScreen_Click(object sender, EventArgs e)
        {
            settings.StartScreen = (int)StartScreen.AboutBox;
            RbRecentFile.Checked = true;
        }

        private void BtnResetPractice_Click(object sender, EventArgs e)
        {
            // Reset practice settings

            TrbRepetitions.Value = 3;

            TrbUnknown.Value = 5;
            TrbWrongRigtht.Value = 2;
        }

        private void TrbUnknown_ValueChanged(object sender, EventArgs e)
        {
            LbUnpracticed.Text = TrbUnknown.Value * 10 + "%";
            TrbWrongRigtht.Maximum = 10 - TrbUnknown.Value - 1;

            if (TrbUnknown.Value == 8)
            {
                TrbWrongRigtht.Enabled = false;
                LbWronglyPracticed.Text = "10%";
                LbCorrectlyPracticed.Text = "10%";
            }
            else
            {
                TrbWrongRigtht.Enabled = true;
                LbCorrectlyPracticed.Text = TrbWrongRigtht.Value * 10 + "%";
                LbWronglyPracticed.Text = (10 - TrbUnknown.Value - TrbWrongRigtht.Value) * 10 + "%";
            }
        }

        private void TrbWrongRight_ValueChanged(object sender, EventArgs e)
        {
            LbCorrectlyPracticed.Text = TrbWrongRigtht.Value * 10 + "%";
            LbWronglyPracticed.Text = (10 - TrbUnknown.Value - TrbWrongRigtht.Value) * 10 + "%";
        }

        private void CbPracticeResult_CheckedChanged(object sender, EventArgs e)
        {
            CbEvaluationSystem.Enabled = CbPracticeResult.Checked;
        }

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