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

namespace Vocup.Forms
{
    public partial class PracticeDialog : Form
    {
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

        public PracticeDialog(VocabularyBook book, List<VocabularyWordPractice> practiceList)
        {
            InitializeComponent();

            Icon = Icon.FromHandle(Icons.LightningBolt.GetHicon());

            evaluator = new Evaluator
            {
                OptionalExpressions = Settings.Default.EvaluateOptionalExpressions,
                TolerateArticle = Settings.Default.EvaluateTolerateArticle,
                TolerateNoSynonym = Settings.Default.EvaluateTolerateNoSynonym,
                ToleratePunctuationMark = Settings.Default.EvaluateToleratePunctuationMark,
                TolerateSpecialChar = Settings.Default.EvaluateTolerateSpecialChar,
                TolerateWhiteSpace = Settings.Default.EvaluateTolerateWhiteSpace
            };

            player = new SoundPlayer();

            this.book = book;
            this.practiceList = practiceList;
            index = 0;
            currentPractice = practiceList[0];
            currentWord = currentPractice.VocabularyWord;

            specialCharDialog = new SpecialCharKeyboard();
            specialCharDialog.Initialize(this, BtnSpecialChar);

            if (Settings.Default.UserEvaluates)
            {
                Height = LogicalToDeviceUnits(380); // All other controls are adjusted by anchor
            }

            LbMotherTongue.Text = book.MotherTongue;
            LbForeignLang.Text = book.ForeignLang;

            if (book.PracticeMode == PracticeMode.AskForForeignLang)
                GroupPractice.Text = string.Format(Words.TranslateFromTo, book.MotherTongue, book.ForeignLang);
            else
                GroupPractice.Text = string.Format(Words.TranslateFromTo, book.ForeignLang, book.MotherTongue);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            PbPracticeProgress.Maximum = practiceList.Count;
            TbPracticeCount.Text = practiceList.Count.ToString();

            RefreshStatistics();

            if (Settings.Default.UserEvaluates)
            {
                GroupUserEvaluation.Visible = true;
                GroupUserEvaluation.Enabled = false;
            }

            LoadNextWord(); // Load first word
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            if (index == practiceList.Count)
            {
                Close();
                return;
            }

            if (Settings.Default.UserEvaluates)
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
            else if (Settings.Default.PracticeFastContinue && !Settings.Default.UserEvaluates)
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
            if (Settings.Default.UserEvaluates)
            {
                GroupUserEvaluation.Enabled = false;
            }

            if (!Settings.Default.PracticeFastContinue || Settings.Default.UserEvaluates)
            {
                TbCorrectAnswer.Text = "";
                TbCorrectAnswer.BackColor = SystemColors.Control;
            }

            if (book.PracticeMode == PracticeMode.AskForForeignLang)
            {
                TbMotherTongue.Text = currentWord.MotherTongue;
                TbMotherTongue.ReadOnly = true;
                TbForeignLang.Text = "";
                TbForeignLang.ReadOnly = false;
                TbForeignLang.BackColor = Settings.Default.PracticeInputBackColor;
                TbForeignLangSynonym.Text = "";
                if (string.IsNullOrWhiteSpace(currentWord.ForeignLangSynonym))
                {
                    TbForeignLangSynonym.BackColor = DefaultBackColor;
                    TbForeignLangSynonym.ReadOnly = true;
                }
                else
                {
                    TbForeignLangSynonym.ReadOnly = false;
                    TbForeignLangSynonym.BackColor = Settings.Default.PracticeInputBackColor;
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
                TbForeignLangSynonym.ReadOnly = true;
                TbMotherTongue.BackColor = Settings.Default.PracticeInputBackColor;
                TbMotherTongue.Select();
            }

            check = true;
            solution = true;
        }

        private PracticeResult EvaluateInput()
        {
            if (Settings.Default.UserEvaluates)
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

            if (book.PracticeMode == PracticeMode.AskForForeignLang)
            {
                if (string.IsNullOrWhiteSpace(currentWord.ForeignLangSynonym))
                {
                    inputs = new[] { TbForeignLang.Text.Trim() };
                    results = new[] { currentWord.ForeignLang };
                }
                else
                {
                    inputs = new[] { TbForeignLang.Text.Trim(), TbForeignLangSynonym.Text.Trim() };
                    results = new[] { currentWord.ForeignLang, currentWord.ForeignLangSynonym };
                }
            }
            else // PracticeMode.AskForMotherTongue
            {
                inputs = new[] { TbMotherTongue.Text.Trim() };
                results = new[] { currentWord.MotherTongue };
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

            Stream sound = null;

            // Show result
            if (currentPractice.PracticeResult == PracticeResult.Correct)
            {
                if (currentWord.PracticeStateNumber == 0)
                    currentWord.PracticeStateNumber = 2;
                else
                    currentWord.PracticeStateNumber++;

                if (!Settings.Default.UserEvaluates)
                {
                    TbCorrectAnswer.Text = Words.Correct + "!";
                    TbCorrectAnswer.BackColor = Color.FromArgb(144, 238, 144);
                    sound = Sounds.sound_correct;
                }
            }
            else if (currentPractice.PracticeResult == PracticeResult.PartlyCorrect)
            {
                currentPractice.WrongInput = GetWrongInput();
                // Do not change practice state number in this case

                if (!Settings.Default.UserEvaluates)
                {
                    TbCorrectAnswer.Text = $"{Words.PartlyCorrect}! ({GetEvaluationAnswer()})";
                    TbCorrectAnswer.BackColor = Color.FromArgb(255, 215, 0);
                    sound = Sounds.sound_correct;
                }
            }
            else // if (currentPractice.PracticeResult == PracticeResult.Wrong)
            {
                currentPractice.WrongInput = GetWrongInput();
                currentWord.PracticeStateNumber = 1;

                if (!Settings.Default.UserEvaluates)
                {
                    TbCorrectAnswer.Text = $"{Words.Wrong}! ({GetEvaluationAnswer()})";
                    TbCorrectAnswer.BackColor = Color.FromArgb(255, 192, 203);
                    sound = Sounds.sound_wrong;
                }
            }

            // Sound playback
            player.Stop();
            if (Settings.Default.PracticeSoundFeedback && sound != null)
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
            if (book.PracticeMode == PracticeMode.AskForForeignLang)
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
            if (book.PracticeMode == PracticeMode.AskForForeignLang)
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
            if (book.PracticeMode == PracticeMode.AskForForeignLang)
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
            specialCharDialog.RegisterTextBox((TextBox)sender);
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            player.Stop();
        }
    }
}
