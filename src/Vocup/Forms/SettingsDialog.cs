using System;
using System.Drawing;
using System.IO;
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
            RbRecentFile.Checked = settings.StartScreen == (int)StartScreen.LastFile || settings.StartScreen == (int)StartScreen.AboutBox;
            CbAutoSave.Checked = settings.AutoSave;

            if (AppInfo.IsUwp)
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

            // Paths
            TbVhfPath.Text = settings.VhfPath;
            TbVhrPath.Text = settings.VhrPath;

            switch (Settings.Default.OverrideCulture)
            {
                case "en-US": CbLanguage.SelectedIndex = 1; break;
                case "de-DE": CbLanguage.SelectedIndex = 2; break;
                default: CbLanguage.SelectedIndex = 0; break; // System language
            }

            // Evaluation
            CbManualCheck.Checked = settings.UserEvaluates;
            CbPracticeResult.Checked = settings.PracticeShowResultList;
            CbOptionalExpressions.Checked = settings.EvaluateOptionalExpressions;
            switch (settings.PracticeGradeCulture)
            {
                case "de-CH": CbEvaluationSystem.SelectedIndex = 1; break;
                default: CbEvaluationSystem.SelectedIndex = 0; break; // de-DE
            }

            // Partly correct configuration
            CbTolerateWhiteSpace.Checked = settings.EvaluateTolerateWhiteSpace;
            CbToleratePunctuationMark.Checked = settings.EvaluateToleratePunctuationMark;
            CbTolerateSpecialChar.Checked = settings.EvaluateTolerateSpecialChar;
            CbTolerateArticle.Checked = settings.EvaluateTolerateArticle;
            CbTolerateNoSynonym.Checked = settings.EvaluateTolerateNoSynonym;

            // User interface
            CbColoredTextfield.Checked = settings.PracticeInputBackColor != SystemColors.Window;
            CbAcousticFeedback.Checked = settings.PracticeSoundFeedback;
            CbSingleContinueButton.Checked = settings.PracticeFastContinue;

            // Practice composition
            TrbRepetitions.Value = settings.MaxPracticeCount;

            LbUnpracticed.Text = settings.PracticePercentageUnpracticed + "%";
            TrbUnknown.Value = settings.PracticePercentageUnpracticed / 10;

            LbWronglyPracticed.Text = settings.PracticePercentageWrong + "%";
            LbCorrectlyPracticed.Text = settings.PracticePercentageCorrect + "%";
            TrbWrongRight.Maximum = 10 - TrbUnknown.Value - 1;
            TrbWrongRight.Value = settings.PracticePercentageCorrect / 10;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            settings.StartScreen = RbRecentFile.Checked ? (int)StartScreen.LastFile : (int)StartScreen.None;
            settings.AutoSave = CbAutoSave.Checked;
            settings.DisableInternetServices = CbDisableInternetServices.Checked;

            // ListView
            settings.GridLines = CbGridLines.Checked;
            settings.ColumnResize = CbColumnResize.Checked;

            // Paths
            settings.VhfPath = TbVhfPath.Text;
            settings.VhrPath = TbVhrPath.Text;

            string oldCulture = settings.OverrideCulture;
            switch (CbLanguage.SelectedIndex)
            {
                case 1: settings.OverrideCulture = "en-US"; break;
                case 2: settings.OverrideCulture = "de-DE"; break;
                default: settings.OverrideCulture = ""; break; // System language
            }
            if (settings.OverrideCulture != oldCulture)
                MessageBox.Show(Messages.SettingsRestartRequired, Messages.SettingsRestartRequiredT, MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Evaluation
            settings.UserEvaluates = CbManualCheck.Checked;
            settings.PracticeShowResultList = CbPracticeResult.Checked;
            settings.EvaluateOptionalExpressions = CbOptionalExpressions.Checked;
            switch (CbEvaluationSystem.SelectedIndex)
            {
                case 1: settings.PracticeGradeCulture = "de-CH"; break;
                default: settings.PracticeGradeCulture = "de-DE"; break;
            }

            // Partly correct configuration
            settings.EvaluateTolerateWhiteSpace = CbTolerateWhiteSpace.Checked;
            settings.EvaluateToleratePunctuationMark = CbToleratePunctuationMark.Checked;
            settings.EvaluateTolerateSpecialChar = CbTolerateSpecialChar.Checked;
            settings.EvaluateTolerateArticle = CbTolerateArticle.Checked;
            settings.EvaluateTolerateNoSynonym = CbTolerateNoSynonym.Checked;

            // User interface
            settings.PracticeInputBackColor = CbColoredTextfield.Checked ? Color.FromArgb(250, 250, 150) : SystemColors.Window;
            settings.PracticeSoundFeedback = CbAcousticFeedback.Checked;
            settings.PracticeFastContinue = CbSingleContinueButton.Checked;

            // Practice composition
            settings.MaxPracticeCount = TrbRepetitions.Value;
            settings.PracticePercentageUnpracticed = TrbUnknown.Value * 10;
            settings.PracticePercentageCorrect = TrbWrongRight.Value * 10;
            settings.PracticePercentageWrong = (10 - TrbUnknown.Value - TrbWrongRight.Value) * 10;

            settings.Save();
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
            TrbWrongRight.Value = 2;
        }

        private void TrbUnknown_ValueChanged(object sender, EventArgs e)
        {
            LbUnpracticed.Text = TrbUnknown.Value * 10 + "%";
            TrbWrongRight.Maximum = 10 - TrbUnknown.Value - 1;

            if (TrbUnknown.Value == 8)
            {
                TrbWrongRight.Enabled = false;
                LbWronglyPracticed.Text = "10%";
                LbCorrectlyPracticed.Text = "10%";
            }
            else
            {
                TrbWrongRight.Enabled = true;
                LbCorrectlyPracticed.Text = TrbWrongRight.Value * 10 + "%";
                LbWronglyPracticed.Text = (10 - TrbUnknown.Value - TrbWrongRight.Value) * 10 + "%";
            }
        }

        private void TrbWrongRight_ValueChanged(object sender, EventArgs e)
        {
            LbCorrectlyPracticed.Text = TrbWrongRight.Value * 10 + "%";
            LbWronglyPracticed.Text = (10 - TrbUnknown.Value - TrbWrongRight.Value) * 10 + "%";
        }

        private void CbPracticeResult_CheckedChanged(object sender, EventArgs e)
        {
            CbEvaluationSystem.Enabled = CbPracticeResult.Checked;
        }

        private void BtnVhfPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = Messages.BrowseVhfPath,
                SelectedPath = settings.VhfPath
            })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // This call fails for inaccessible paths like optical disk drives
                        _ = Directory.GetFiles(fbd.SelectedPath);

                        TbVhfPath.Text = fbd.SelectedPath;
                    }
                    catch (IOException)
                    {
                        MessageBox.Show(Messages.VhfPathInvalid, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnVhrPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog { SelectedPath = settings.VhrPath })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // This call fails for inaccessible paths like optical disk drives
                        _ = Directory.GetFiles(fbd.SelectedPath);

                        TbVhrPath.Text = fbd.SelectedPath;
                    }
                    catch (IOException)
                    {
                        MessageBox.Show(Messages.VhrPathInvalid, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
