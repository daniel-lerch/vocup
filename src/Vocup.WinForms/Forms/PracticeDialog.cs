using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup.Forms;

public partial class PracticeDialog : Form
{
    private const int userEvaluationHeight = 38;
    private readonly Color InputHighlightBackColor;
    private readonly Color CorrectFeedbackBackColor;
    private readonly Color PartlyCorrectFeedbackBackColor;
    private readonly Color WrongFeedbackBackColor;

    private readonly VocabularyBook book;
    private readonly List<VocabularyWordPractice> practiceList;
    private VocabularyWordPractice currentPractice;
    private VocabularyWord currentWord;
    private int index;
    private readonly SpecialCharKeyboard specialCharDialog;
    private readonly Evaluator evaluator;
    private readonly SoundPlayer player;

    private bool check;
    private bool solution;
    private PracticeDirection practiceDirection;
	
    public PracticeDialog(VocabularyBook book, List<VocabularyWordPractice> practiceList)
    {
        InitializeComponent();

        Icon = Icon.FromHandle(Icons.LightningBolt.GetHicon());

        InputHighlightBackColor = Color.FromArgb(255, 255, 150);
        CorrectFeedbackBackColor = Color.FromArgb(144, 238, 144);
        PartlyCorrectFeedbackBackColor = Color.FromArgb(255, 215, 0);
        WrongFeedbackBackColor = Color.FromArgb(255, 192, 203);

#pragma warning disable WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        if (Application.ColorMode == SystemColorMode.Dark || (Application.ColorMode == SystemColorMode.System && Application.SystemColorMode == SystemColorMode.Dark))
        {
            InputHighlightBackColor = Color.FromArgb(127, 127, 75);
            CorrectFeedbackBackColor = Color.FromArgb(0, 100, 0);
            PartlyCorrectFeedbackBackColor = Color.FromArgb(127, 106, 0);
            WrongFeedbackBackColor = Color.FromArgb(127, 0, 0);
            TbCorrectCount.BackColor = Color.DarkGreen;
            TbPartlyCorrectCount.BackColor = Color.DarkGoldenrod;
            TbWrongCount.BackColor = Color.DarkRed;
        }
#pragma warning restore WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

        evaluator = new Evaluator
        {
            OptionalExpressions = Program.Settings.EvaluateOptionalExpressions,
            TolerateArticle = Program.Settings.EvaluateTolerateArticle,
            TolerateNoSynonym = Program.Settings.EvaluateTolerateNoSynonym,
            ToleratePunctuationMark = Program.Settings.EvaluateToleratePunctuationMark,
            TolerateSpecialChar = Program.Settings.EvaluateTolerateSpecialChar,
            TolerateWhiteSpace = Program.Settings.EvaluateTolerateWhiteSpace
        };

        player = new SoundPlayer();

        this.book = book;
        this.practiceList = practiceList;
        index = 0;
        currentPractice = practiceList[0];
        currentWord = currentPractice.VocabularyWord;

        specialCharDialog = new SpecialCharKeyboard();
        specialCharDialog.Initialize(this, BtnSpecialChar);

        if (Program.Settings.UserEvaluates)
        {
            int userEvaluationDeviceHeight = LogicalToDeviceUnits(userEvaluationHeight);

            MinimumSize = new Size(MinimumSize.Width, MinimumSize.Height + userEvaluationDeviceHeight);
            TableLayout.Height -= userEvaluationDeviceHeight;
            GroupUserEvaluation.Top -= userEvaluationDeviceHeight;
        }

        LbMotherTongue.Text = book.MotherTongue;
        LbForeignLang.Text = book.ForeignLang;
    }

    private void Form_Load(object sender, EventArgs e)
    {
        Size size = Program.Settings.PracticeDialogSize;
        if (size.Width >= MinimumSize.Width && size.Height >= MinimumSize.Height)
        {
            Size = size;
        }

        PbPracticeProgress.Maximum = practiceList.Count;
        TbPracticeCount.Text = practiceList.Count.ToString();

        RefreshStatistics();

        if (Program.Settings.UserEvaluates)
        {
            GroupUserEvaluation.Visible = true;
            GroupUserEvaluation.Enabled = false;
        }

        LoadNextWord(); // Load first word

        Program.TrackingService.Page("/practice");
    }

    private void BtnContinue_Click(object sender, EventArgs e)
    {
        if (index == practiceList.Count)
        {
            Close();
            return;
        }

        if (Program.Settings.UserEvaluates)
        {
            if (check == false)
            {
                LoadNextWord();
            }
            else if (check == true && solution == true)
            {
                ShowSolution();
            }
            else if (check == true && solution == false)
            {
                EvaluateWord();

                if (index < practiceList.Count)
                {
                    LoadNextWord();
                }
                else
                {
                    Close();
                }
            }
        }
        else if (Program.Settings.PracticeFastContinue && !Program.Settings.UserEvaluates)
        {
            EvaluateWord();
            if (index < practiceList.Count)
            {
                LoadNextWord();
            }
        }
        else if (check)
        {
            EvaluateWord();
        }
        else
        {
            LoadNextWord();
        }
    }

    public void LoadNextWord()
    {
        if (Program.Settings.UserEvaluates)
        {
            GroupUserEvaluation.Enabled = false;
        }

        if (!Program.Settings.PracticeFastContinue || Program.Settings.UserEvaluates)
        {
            TbCorrectAnswer.Text = "";
            TbCorrectAnswer.BackColor = SystemColors.Control;
        }

        switch (book.PracticeMode)
        {
            case PracticeMode.AskForForeignLang:
                practiceDirection = PracticeDirection.MotherTongueToForeignLang;
                break;
            case PracticeMode.AskForMotherTongue:
                practiceDirection = PracticeDirection.ForeignLangToMotherTongue;
                break;
            case PracticeMode.AskForBothMixed:
                if (Random.Shared.Next(2) == 0)
                {
                    practiceDirection = PracticeDirection.MotherTongueToForeignLang;
                }
                else
                {
                    practiceDirection = PracticeDirection.ForeignLangToMotherTongue;
                }
                break;
        }

        if (practiceDirection == PracticeDirection.MotherTongueToForeignLang)
        {
            TbMotherTongue.Text = currentWord.MotherTongue;
            TbMotherTongue.ReadOnly = true;
            TbMotherTongue.BackColor = DefaultBackColor;
            TbForeignLang.Text = "";
            TbForeignLang.ReadOnly = false;
            TbForeignLang.BackColor = InputHighlightBackColor;
            TbForeignLangSynonym.Text = "";
            if (string.IsNullOrWhiteSpace(currentWord.ForeignLangSynonym))
            {
                TbForeignLangSynonym.BackColor = DefaultBackColor;
                TbForeignLangSynonym.ReadOnly = true;
            }
            else
            {
                TbForeignLangSynonym.ReadOnly = false;
                TbForeignLangSynonym.BackColor = InputHighlightBackColor;
            }
            TbForeignLang.Select();
        }
        else
        {
            TbForeignLang.Text = currentWord.ForeignLang;
            if (!string.IsNullOrWhiteSpace(currentWord.ForeignLangSynonym))
                TbForeignLangSynonym.Text = currentWord.ForeignLangSynonym;
            else
                TbForeignLangSynonym.Text = "";
            TbMotherTongue.Text = "";
            TbMotherTongue.ReadOnly = false;
            TbForeignLang.ReadOnly = true;
            TbForeignLang.BackColor = DefaultBackColor;
            TbForeignLangSynonym.ReadOnly = true;
            TbMotherTongue.BackColor = InputHighlightBackColor;
            TbMotherTongue.Select();
        }

        check = true;
        solution = true;
    }

    private PracticeResult EvaluateInput()
    {
        if (Program.Settings.UserEvaluates)
        {
            if (RbCorrect.Checked)
                return PracticeResult.Correct;
            else if (RbPartlyCorrect.Checked)
                return PracticeResult.PartlyCorrect;
            else
                return PracticeResult.Wrong;
        }

        string[] inputs;
        string[] results;

        if (practiceDirection == PracticeDirection.MotherTongueToForeignLang)
        {
            if (string.IsNullOrWhiteSpace(currentWord.ForeignLangSynonym))
            {
                inputs = [TbForeignLang.Text.Trim()];
                results = [currentWord.ForeignLang];
            }
            else
            {
                inputs = [TbForeignLang.Text.Trim(), TbForeignLangSynonym.Text.Trim()];
                results = [currentWord.ForeignLang, currentWord.ForeignLangSynonym];
            }
        }
        else // PracticeMode.AskForMotherTongue
        {
            inputs = [TbMotherTongue.Text.Trim()];
            results = [currentWord.MotherTongue];
        }

        return evaluator.GetResult(results, inputs);
    }


    private void EvaluateWord()
    {
        TbForeignLang.ReadOnly = true;
        TbMotherTongue.ReadOnly = true;
        TbForeignLangSynonym.ReadOnly = true;

        GroupUserEvaluation.Enabled = true;

        currentPractice.PracticeResult = EvaluateInput();
        currentWord.PracticeDate = DateTime.Now;

        Stream? sound = null;

        // Show result
        if (currentPractice.PracticeResult == PracticeResult.Correct)
        {
            if (currentWord.PracticeStateNumber == 0)
                currentWord.PracticeStateNumber = 2;
            else
                currentWord.PracticeStateNumber++;

            if (!Program.Settings.UserEvaluates)
            {
                TbCorrectAnswer.Text = Words.Correct + "!";
                TbCorrectAnswer.BackColor = CorrectFeedbackBackColor;
                sound = Sounds.sound_correct;
            }
        }
        else if (currentPractice.PracticeResult == PracticeResult.PartlyCorrect)
        {
            currentPractice.WrongInput = GetWrongInput();
            // Do not change practice state number in this case

            if (!Program.Settings.UserEvaluates)
            {
                TbCorrectAnswer.Text = $"{Words.PartlyCorrect}! ({GetEvaluationAnswer()})";
                TbCorrectAnswer.BackColor = PartlyCorrectFeedbackBackColor;
                sound = Sounds.sound_correct;
            }
        }
        else // if (currentPractice.PracticeResult == PracticeResult.Wrong)
        {
            currentPractice.WrongInput = GetWrongInput();
            currentWord.PracticeStateNumber = 1;

            if (!Program.Settings.UserEvaluates)
            {
                TbCorrectAnswer.Text = $"{Words.Wrong}! ({GetEvaluationAnswer()})";
                TbCorrectAnswer.BackColor = WrongFeedbackBackColor;
                sound = Sounds.sound_wrong;
            }
        }

        // Sound playback
        player.Stop();
        if (Program.Settings.PracticeSoundFeedback && sound != null)
        {
            player.Stream = sound;
            player.Play();
        }

        // Prepare next word

        index++;

        if (index < practiceList.Count)
        {
            currentPractice = practiceList[index];
            currentWord = currentPractice.VocabularyWord;
        }

        RefreshStatistics();

        check = false;
        solution = false;

        if (index == practiceList.Count)
        {
            BtnContinue.Text = Words.Finish;
        }
    }

    private void ShowSolution()
    {
        if (practiceDirection == PracticeDirection.MotherTongueToForeignLang)
        {
            if (string.IsNullOrWhiteSpace(currentWord.ForeignLangSynonym))
                TbCorrectAnswer.Text = string.Format(Words.CorrectWasX, currentWord.ForeignLang);
            else
                TbCorrectAnswer.Text = string.Format(Words.CorrectWasXAndY, currentWord.ForeignLang, currentWord.ForeignLangSynonym);
        }
        else
        {
            TbCorrectAnswer.Text = string.Format(Words.CorrectWasX, currentWord.MotherTongue);
        }

        TbCorrectAnswer.BackColor = Color.White;

        TbForeignLang.ReadOnly = true;
        TbMotherTongue.ReadOnly = true;
        TbForeignLangSynonym.ReadOnly = true;

        // Show radio buttons for manual evaluation
        GroupUserEvaluation.Enabled = true;

        check = true;
        solution = false;
    }

    private void RefreshStatistics()
    {
        PbPracticeProgress.Value = index;

        TbPracticedCount.Text = index.ToString();
        TbUnpracticedCount.Text = (practiceList.Count - index).ToString();

        IEnumerable<PracticeResult> results = practiceList.Take(index).Select(x => x.PracticeResult);
        TbCorrectCount.Text = results.Where(x => x == PracticeResult.Correct).Count().ToString();
        TbPartlyCorrectCount.Text = results.Where(x => x == PracticeResult.PartlyCorrect).Count().ToString();
        TbWrongCount.Text = results.Where(x => x == PracticeResult.Wrong).Count().ToString();
    }

    private string GetEvaluationAnswer()
    {
        if (practiceDirection == PracticeDirection.MotherTongueToForeignLang)
        {
            if (string.IsNullOrWhiteSpace(currentWord.ForeignLangSynonym))
                return string.Format(Words.CorrectWasX, currentWord.ForeignLang);
            else
                return string.Format(Words.CorrectWasXAndY, currentWord.ForeignLang, currentWord.ForeignLangSynonym);
        }
        else
        {
            return string.Format(Words.CorrectWasX, currentWord.MotherTongue);
        }
    }

    private string GetWrongInput()
    {
        if (practiceDirection == PracticeDirection.MotherTongueToForeignLang)
        {
            if (string.IsNullOrWhiteSpace(currentWord.ForeignLangSynonym))
                return TbForeignLang.Text;
            else
                return $"{TbForeignLang.Text}, {TbForeignLangSynonym.Text}";
        }
        else
        {
            return TbMotherTongue.Text;
        }
    }

    private void TextBox_Enter(object sender, EventArgs e)
    {
        // PracticeDialog is a special form because textboxes might be readonly and must not be registered in that case

        if (sender is TextBox textBox && textBox.Enabled && !textBox.ReadOnly)
        {
            specialCharDialog.RegisterTextBox(textBox);
        }
    }

    private void Form_FormClosing(object sender, FormClosingEventArgs e)
    {
        Program.Settings.PracticeDialogSize = Size;
    }

    private void Form_FormClosed(object sender, FormClosedEventArgs e)
    {
        player.Stop();
    }
}
