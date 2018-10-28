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
        private readonly SoundPlayer player;

        private bool check;
        private bool solution;

        public PracticeDialog(VocabularyBook book, List<VocabularyWordPractice> practiceList)
        {
            InitializeComponent();

            Icon = Icon.FromHandle(Icons.LightningBolt.GetHicon());
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
                Height = 370; // All other controls are adjusted by anchor
            }

            LbMotherTongue.Text = book.MotherTongue;
            LbForeignLang.Text = book.ForeignLang;

            if (book.PracticeMode == PracticeMode.AskForForeignLang)
                GroupPractice.Text = string.Format(GroupPractice.Text, book.MotherTongue, book.ForeignLang);
            else
                GroupPractice.Text = string.Format(GroupPractice.Text, book.ForeignLang, book.MotherTongue);
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

            LoadNextWord(); //Erste Vokabel einlesen
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
            else if (check) //Falls die Eingabe überprüft werden soll
            {
                EvaluateWord();
            }
            else //Falls die nächste vokabel eingelesen werden soll
            {
                LoadNextWord();
            }
        }

        //Vokabel einlesen
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

            if (book.PracticeMode == PracticeMode.AskForForeignLang)
            {
                string[] inputs;
                string[] results;

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

                int missed = 0;

                foreach (string result in results)
                {
                    if (!inputs.Contains(result))
                        missed++;
                }

                if (missed == 0)
                    return PracticeResult.Correct;
                else if (missed < results.Length && Settings.Default.EvaluateTolerateNoSynonym)
                    return PracticeResult.PartlyCorrect;

                string[] simplifiedInputs = inputs.Select(x => SimplifyText(x)).ToArray();
                string[] simplifiedResults = results.Select(x => SimplifyText(x)).ToArray();

                missed = 0;

                foreach (string result in simplifiedResults)
                {
                    if (!simplifiedInputs.Contains(result))
                        missed++;
                }

                if (missed == 0)
                    return PracticeResult.PartlyCorrect;

                int correctCount = 0;

                foreach (string result in results)
                {
                    if (result.ContainsAny(",;"))
                    {
                        bool found = false;

                        foreach (string input in inputs)
                        {
                            if (GetPartitialResult(input, result) == PracticeResult.Correct)
                                found = true;
                        }

                        if (found) correctCount++;
                    }
                }

                if (correctCount == results.Length)
                    return PracticeResult.Correct;
                else
                    return PracticeResult.Wrong;
            }
            else // PracticeMode.AskForMotherTongue
            {
                if (TbMotherTongue.Text.Trim() == currentWord.MotherTongue)
                    return PracticeResult.Correct;

                string simplifiedInput = SimplifyText(TbMotherTongue.Text);
                string simplifiedResult = SimplifyText(currentWord.MotherTongue);

                if (simplifiedInput == simplifiedResult)
                    return PracticeResult.PartlyCorrect;

                if (currentWord.MotherTongue.ContainsAny(",;"))
                    return GetPartitialResult(TbMotherTongue.Text, currentWord.MotherTongue);
                else
                    return PracticeResult.Wrong;
            }
        }

        private PracticeResult GetPartitialResult(string input, string result)
        {
            string[] keywords = currentWord.MotherTongue.Replace(";", ",")
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in keywords)
            {
                if (!TbMotherTongue.Text.Contains(item))
                    return PracticeResult.Wrong;
            }

            return PracticeResult.Correct;
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

            //Ergebnis bekannt geben
            if (currentPractice.PracticeResult == PracticeResult.Correct)
            {
                //Ergebnisse speichern
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
                    TbCorrectAnswer.Text = $"{Words.PartlyCorrect}! ({GetEvaluationAnswer()}";
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
                    TbCorrectAnswer.Text = $"{Words.Wrong}! ({GetEvaluationAnswer()}";
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

            //Nächste Vokabel vorbereiten

            index++;

            if (index < practiceList.Count)
            {
                currentPractice = practiceList[index];
                currentWord = currentPractice.VocabularyWord;
            }

            //Zahlen aktualisieren

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

            //Radio-Buttons aktivieren
            GroupUserEvaluation.Enabled = true;

            check = true;
            solution = false;
        }

        //Zahlen aktualisieren

        private void RefreshStatistics()
        {
            //Progress-Bar
            PbPracticeProgress.Value = index;

            //Zahlen aktualiseren
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

        //Sonderzeichen
        private void TextBox_Enter(object sender, EventArgs e)
        {
            specialCharDialog.RegisterTextBox((TextBox)sender);
        }

        /// <summary>
        /// Returns the uppercase input simplified with all acitvated rules.
        /// </summary>
        private string SimplifyText(string text)
        {
            //Leerschläge
            if (Settings.Default.EvaluateTolerateWhiteSpace)
            {
                text = text.Replace(" ", "");
            }

            //Satzzeichen
            if (Settings.Default.EvaluateToleratePunctuationMark)
            {
                text = text.Replace(",", "");
                text = text.Replace(".", "");
                text = text.Replace(";", "");
                text = text.Replace("-", "");
                text = text.Replace("!", "");
                text = text.Replace("?", "");
                text = text.Replace("'", "");
                text = text.Replace("\\", "");
                text = text.Replace("/", "");
                text = text.Replace("(", "");
                text = text.Replace(")", "");
            }

            //Sonderzeichen
            if (Settings.Default.EvaluateTolerateSpecialChar)
            {
                text = text.Replace("ä", "a");
                text = text.Replace("ö", "o");
                text = text.Replace("ü", "u");
                text = text.Replace("ß", "ss");

                text = text.Replace("à", "a");
                text = text.Replace("â", "a");
                text = text.Replace("ă", "a");
                text = text.Replace("æ", "oe");
                text = text.Replace("ç", "c");
                text = text.Replace("é", "e");
                text = text.Replace("è", "e");
                text = text.Replace("ê", "e");
                text = text.Replace("ë", "e");
                text = text.Replace("ï", "i");
                text = text.Replace("î", "i");
                text = text.Replace("ì", "i");
                text = text.Replace("í", "i");
                text = text.Replace("ñ", "n");
                text = text.Replace("ô", "o");
                text = text.Replace("ò", "o");
                text = text.Replace("ó", "o");
                text = text.Replace("œ", "oe");
                text = text.Replace("ş", "s");
                text = text.Replace("ţ", "t");
                text = text.Replace("ù", "u");
                text = text.Replace("ú", "u");
                text = text.Replace("û", "u");
                text = text.Replace("ÿ", "y");

                text = text.Replace("ª", "");
                text = text.Replace("º", "");
                text = text.Replace("¡", "");
                text = text.Replace("¿", "");

            }

            //Artikel bearbeiten
            if (Settings.Default.EvaluateTolerateArticle)
            {
                //Deutsch
                text = text.Replace("der", "");
                text = text.Replace("die", "");
                text = text.Replace("das", "");
                text = text.Replace("des", "");
                text = text.Replace("dem", "");
                text = text.Replace("den", "");
                text = text.Replace("ein", "");
                text = text.Replace("eine", "");
                text = text.Replace("eines", "");
                text = text.Replace("einer", "");
                text = text.Replace("einem", "");
                text = text.Replace("einen", "");

                //Französisch
                text = text.Replace("un", "");
                text = text.Replace("une", "");
                text = text.Replace("le", "");
                text = text.Replace("la", "");
                text = text.Replace("les", "");
                text = text.Replace("l'", "");

                //Englisch
                text = text.Replace("a", "");
                text = text.Replace("an", "");
                text = text.Replace("that", "");
                text = text.Replace("the", "");

                //Italienisch
                text = text.Replace("il", "");
                text = text.Replace("i", "");
                text = text.Replace("lo", "");
                text = text.Replace("gli", "");
                text = text.Replace("uno", "");
                text = text.Replace("una", "");
                text = text.Replace("un'", "");

                //Spanisch
                text = text.Replace("el", "");
                text = text.Replace("los", "");
                text = text.Replace("las", "");
                text = text.Replace("unos", "");
                text = text.Replace("unas", "");
                text = text.Replace("lo", "");
                text = text.Replace("otro", "");
                text = text.Replace("medio", "");
            }

            text = text.ToUpper();
            return text;
        }


        //Schliessen
        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            player.Stop();
        }
    }
}