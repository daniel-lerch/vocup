using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Settings;

namespace Vocup.Forms;

public partial class SettingsDialog : Form
{
    private readonly VocupSettings settings;

    public SettingsDialog(VocupSettings settings)
    {
        InitializeComponent();
        Icon = Icon.FromHandle(Icons.Settings.GetHicon());
        this.settings = settings;
    }

    private void SettingsDialog_Load(object sender, EventArgs e)
    {
        RbRecentFile.Checked = settings.StartScreen == (int)StartScreen.LastFile || settings.StartScreen == (int)StartScreen.AboutBox;
        CbAutoSave.Checked = settings.AutoSave;

        CbDisableInternetServices.Checked = settings.DisableInternetServices;

        // ListView
        CbColumnResize.Checked = settings.ColumnResize;

        // Paths
        TbVhfPath.Text = settings.VhfPath;
        TbVhrPath.Text = settings.VhrPath;

        switch (settings.OverrideCulture)
        {
            case "en-US": CbLanguage.SelectedIndex = 1; break;
            case "de-DE": CbLanguage.SelectedIndex = 2; break;
            default: CbLanguage.SelectedIndex = 0; break; // System language
        }

        // Evaluation
        CbManualCheck.Checked = settings.UserEvaluates;
        CbShowPracticeResult.Checked = settings.PracticeShowResultList;
        CbOptionalExpressions.Checked = settings.EvaluateOptionalExpressions;

        // Partly correct configuration
        CbTolerateWhiteSpace.Checked = settings.EvaluateTolerateWhiteSpace;
        CbToleratePunctuationMark.Checked = settings.EvaluateToleratePunctuationMark;
        CbTolerateSpecialChar.Checked = settings.EvaluateTolerateSpecialChar;
        CbTolerateArticle.Checked = settings.EvaluateTolerateArticle;
        CbTolerateNoSynonym.Checked = settings.EvaluateTolerateNoSynonym;

        // User interface
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

        Program.TrackingService.Page("/settings/general");
    }

    private void BtnOk_Click(object sender, EventArgs e)
    {
        settings.StartScreen = RbRecentFile.Checked ? (int)StartScreen.LastFile : (int)StartScreen.None;
        settings.AutoSave = CbAutoSave.Checked;
        settings.DisableInternetServices = CbDisableInternetServices.Checked;

        // ListView
        settings.ColumnResize = CbColumnResize.Checked;

        // Paths
        settings.VhfPath = TbVhfPath.Text;
        settings.VhrPath = TbVhrPath.Text;

        string? oldCulture = settings.OverrideCulture;
        settings.OverrideCulture = CbLanguage.SelectedIndex switch
        {
            1 => "en-US",
            2 => "de-DE",
            _ => null, // System language
        };
        if (settings.OverrideCulture != oldCulture)
            MessageBox.Show(Messages.SettingsRestartRequired, Messages.SettingsRestartRequiredT, MessageBoxButtons.OK, MessageBoxIcon.Information);

        // Evaluation
        settings.UserEvaluates = CbManualCheck.Checked;
        settings.PracticeShowResultList = CbShowPracticeResult.Checked;
        settings.EvaluateOptionalExpressions = CbOptionalExpressions.Checked;

        // Partly correct configuration
        settings.EvaluateTolerateWhiteSpace = CbTolerateWhiteSpace.Checked;
        settings.EvaluateToleratePunctuationMark = CbToleratePunctuationMark.Checked;
        settings.EvaluateTolerateSpecialChar = CbTolerateSpecialChar.Checked;
        settings.EvaluateTolerateArticle = CbTolerateArticle.Checked;
        settings.EvaluateTolerateNoSynonym = CbTolerateNoSynonym.Checked;

        // User interface
        settings.PracticeSoundFeedback = CbAcousticFeedback.Checked;
        settings.PracticeFastContinue = CbSingleContinueButton.Checked;

        // Practice composition
        settings.MaxPracticeCount = TrbRepetitions.Value;
        settings.PracticePercentageUnpracticed = TrbUnknown.Value * 10;
        settings.PracticePercentageCorrect = TrbWrongRight.Value * 10;
        settings.PracticePercentageWrong = (10 - TrbUnknown.Value - TrbWrongRight.Value) * 10;

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

    private void TabControlMain_Selected(object sender, TabControlEventArgs e)
    {
        string pageName = e.TabPageIndex switch
        {
            0 => "/general",
            1 => "/practice",
            2 => "/practice/composition",
            _ => string.Empty
        };

        Program.TrackingService.Page("/settings" + pageName);
    }
}
