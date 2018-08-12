using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vocup.Forms;
using Vocup.Models;
using Vocup.Properties;

namespace Vocup.Forms
{
    public partial class PracticeDialog : Form
    {
        public string[,] practise_list;

        private VocabularyBook book;
        private List<VocabularyWordPractice> practiceList;
        private VocabularyWordPractice currentPractice;
        private VocabularyWord currentWord;
        private int index;
        private SpecialCharKeyboard specialCharDialog;

        private bool check;
        private bool solution;

        // TODO: Use System.Media.SoundPlayer instead of MCIPlayback
        private SimpleAudioVideoPlayback.MCIPlayback mci = new SimpleAudioVideoPlayback.MCIPlayback();

        public PracticeDialog(VocabularyBook book, List<VocabularyWordPractice> practiceList)
        {
            InitializeComponent();

            Icon = Icon.FromHandle(Icons.practise.GetHicon());

            this.book = book;
            this.practiceList = practiceList;
            index = 0;
            currentPractice = practiceList[0];
            currentWord = currentPractice.VocabularyWord;

            specialCharDialog = new SpecialCharKeyboard();
            specialCharDialog.Initialize(this);
            specialCharDialog.VisibleChanged += (a0, a1) => BtnSpecialChar.Enabled = !specialCharDialog.Visible;

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
                mci.Close();
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
                    check_vokabel();

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
                check_vokabel();
                if (index < practiceList.Count)
                {
                    LoadNextWord();
                }
            }
            else if (check) //Falls die Eingabe überprüft werden soll
            {
                check_vokabel();
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

        //Eingabe kontrollieren
        private void check_vokabel()
        {
            TbForeignLang.ReadOnly = true;
            TbMotherTongue.ReadOnly = true;
            TbForeignLangSynonym.ReadOnly = true;

            GroupUserEvaluation.Enabled = true;

            //Eingabe auf Richtigkeit überprüfen

            bool[] correct = new bool[2];

            if (Properties.Settings.Default.UserEvaluates == false)
            {
                if (book.PracticeMode == PracticeMode.AskForMotherTongue)
                {

                    string vokabel_own_language_komp = prepare_text(TbMotherTongue.Text);
                    string vokabel_own_language_komp_richtig = prepare_text(practise_list[index, 2]);

                    //Richtig
                    if (TbMotherTongue.Text == practise_list[index, 2] || TbMotherTongue.Text == practise_list[index, 2] + " ")
                    {
                        correct[0] = true;
                        correct[1] = true;
                    }

                    //Teilweise richtig
                    else if (vokabel_own_language_komp == vokabel_own_language_komp_richtig)
                    {
                        correct[0] = true;
                        correct[1] = false;
                    }

                    //Richtig wenn zwei mit , oder ; getrennte Wörter vertauscht worden sind
                    else if (practise_list[index, 2].Contains(",") || practise_list[index, 2].Contains(";"))
                    {
                        string[] keywords = practise_list[index, 2].Replace(" ", "").Replace(";", ",").Split(',');

                        for (int i = 0; i < keywords.Length; i++)
                        {
                            if (TbMotherTongue.Text.Contains(keywords[i]) == true)
                            {
                                correct[0] = true;
                                correct[1] = true;
                            }
                            else
                            {
                                correct[0] = false;
                                correct[1] = false;
                                break;
                            }
                        }
                    }
                    else //Falsch
                    {
                        correct[0] = false;
                        correct[1] = false;
                    }
                }
                else
                {
                    //Falls kein Synonym vorhanden ist
                    if (practise_list[index, 4] == "" || practise_list[index, 4] == null)
                    {

                        string vokabel_foreign_language_komp_richtig = prepare_text(practise_list[index, 3]);
                        string vokabel_foreign_language_komp = prepare_text(TbForeignLang.Text);


                        //Richtig
                        if (TbForeignLang.Text == practise_list[index, 3] || TbForeignLang.Text == practise_list[index, 3] + " ")
                        {
                            correct[0] = true;
                            correct[1] = true;
                        }

                        //Teilweise richtig
                        else if (vokabel_foreign_language_komp == vokabel_foreign_language_komp_richtig)
                        {
                            correct[0] = true;
                            correct[1] = false;
                        }
                        //Richtig wenn zwei mit , oder ; getrennte Wörter vertauscht worden sind

                        else if (practise_list[index, 3].Contains(",") || practise_list[index, 3].Contains(";"))
                        {
                            string[] keywords = practise_list[index, 3].Replace(" ", "").Replace(";", ",").Split(',');

                            for (int i = 0; i < keywords.Length; i++)
                            {
                                if (TbForeignLang.Text.Contains(keywords[i]) == true)
                                {
                                    correct[0] = true;
                                    correct[1] = true;
                                }
                                else
                                {
                                    correct[0] = false;
                                    correct[1] = false;
                                    break;
                                }
                            }
                        }
                        //Falsch
                        else
                        {
                            correct[0] = false;
                            correct[1] = false;
                        }

                    }
                    //Falls ein Synonym vorhanden ist
                    else
                    {
                        string vokabel_foreign_language_komp_richtig = prepare_text(practise_list[index, 3]);
                        string vokabel_foreign_language_komp = prepare_text(TbForeignLang.Text);
                        string vokabel_synonym_komp = prepare_text(TbForeignLang.Text);
                        string vokabel_synonym_komp_richtig = prepare_text(practise_list[index, 4]);

                        //Richtig
                        if (TbForeignLang.Text == practise_list[index, 3] && TbForeignLangSynonym.Text == practise_list[index, 4])
                        {
                            correct[0] = true;
                            correct[1] = true;
                        }
                        else if (TbForeignLang.Text == practise_list[index, 4] && TbForeignLangSynonym.Text == practise_list[index, 3])
                        {
                            correct[0] = true;
                            correct[1] = true;
                        }
                        else if (Properties.Settings.Default.nearly_correct_synonym == true) //Teilweise richtig
                        {

                            if (TbForeignLang.Text == practise_list[index, 3] && TbForeignLangSynonym.Text != practise_list[index, 4])
                            {
                                correct[0] = true;
                                correct[1] = false;
                            }
                            else if (TbForeignLang.Text == practise_list[index, 4] && TbForeignLangSynonym.Text != practise_list[index, 3])
                            {
                                correct[0] = true;
                                correct[1] = false;
                            }
                            else if (TbForeignLang.Text != practise_list[index, 3] && TbForeignLangSynonym.Text == practise_list[index, 4])
                            {
                                correct[0] = true;
                                correct[1] = false;
                            }
                            else if (TbForeignLang.Text != practise_list[index, 4] && TbForeignLangSynonym.Text == practise_list[index, 3])
                            {
                                correct[0] = true;
                                correct[1] = false;
                            }
                        }
                        else if (vokabel_foreign_language_komp == vokabel_foreign_language_komp_richtig && vokabel_synonym_komp == vokabel_synonym_komp_richtig)
                        {
                            correct[0] = true;
                            correct[1] = false;
                        }
                        else if (vokabel_foreign_language_komp == vokabel_synonym_komp_richtig && vokabel_synonym_komp == vokabel_foreign_language_komp_richtig)
                        {
                            correct[0] = true;
                            correct[1] = false;
                        }

                        //Richtig wenn zwei mit , oder ; getrennte Wörter vertauscht worden sind
                        else if (practise_list[index, 3].Contains(",") == true || practise_list[index, 3].Contains(";") == true)
                        {
                            string[] keywords = practise_list[index, 3].Replace(" ", "").Replace(";", ",").Split(',');

                            for (int i = 0; i < keywords.Length; i++)
                            {
                                if (TbForeignLang.Text.Contains(keywords[i]) == true)
                                {
                                    correct[0] = true;
                                    correct[1] = true;
                                }
                                else
                                {
                                    correct[0] = false;
                                    correct[1] = false;
                                    break;
                                }
                            }
                        }
                        else //falsch
                        {
                            correct[0] = false;
                            correct[1] = false;
                        }
                    }
                }
            }
            else //Falls selber bewertet werden soll
            {
                if (RbCorrect.Checked == true)
                {
                    correct[0] = true;
                    correct[1] = true;
                }
                else if (RbPartlyCorrect.Checked == true)
                {
                    correct[0] = true;
                    correct[1] = false;
                }
                else
                {
                    correct[0] = false;
                    correct[1] = false;
                }
            }

            //Ergebnis bekannt geben
            if (correct[0] == true && correct[1] == true)
            {
                if (!Settings.Default.UserEvaluates)
                {
                    TbCorrectAnswer.Text = Words.Correct + "!";
                    TbCorrectAnswer.BackColor = Color.FromArgb(144, 238, 144);
                }

                //Ergebnisse speichern
                if (Convert.ToInt32(practise_list[index, 1]) == 0)
                {
                    practise_list[index, 1] = Convert.ToString(2);
                }
                else
                {
                    practise_list[index, 1] = Convert.ToString(Convert.ToInt32(practise_list[index, 1]) + 1);
                }

                practise_list[index, 5] = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

                practise_list[index, 6] = "right";

                if (Properties.Settings.Default.UserEvaluates == false)
                {
                    //Sound abspielen

                    mci.Close();

                    if (Properties.Settings.Default.sound == true)
                    {
                        FileInfo info = new FileInfo(Application.StartupPath + @"\" + "sound_correct.wav");

                        if (info.Exists == true)
                        {
                            mci.Open(Application.StartupPath + @"\" + "sound_correct.wav", "mpegvideo");
                            mci.SetTimeFormat("ms");

                            mci.Play();
                        }
                    }
                }
            }
            else if (correct[0] == true && correct[1] == false)
            {
                if (book.PracticeMode == PracticeMode.AskForMotherTongue)
                {
                    if (!Settings.Default.UserEvaluates)
                    {
                        TbCorrectAnswer.Text = $"{Words.PartlyCorrect}! ({string.Format(Words.CorrectWasX, practise_list[index, 2])})";
                    }

                    //Ergebnisse speichern
                    practise_list[index, 5] = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                    practise_list[index, 6] = "nearly right";
                    practise_list[index, 7] = TbMotherTongue.Text;
                }
                else
                {
                    if (!Settings.Default.UserEvaluates)
                    {
                        if (string.IsNullOrWhiteSpace(practise_list[index, 4]))
                        {
                            TbCorrectAnswer.Text = $"{Words.PartlyCorrect}! ({string.Format(Words.CorrectWasX, practise_list[index, 3])})";
                        }
                        else
                        {
                            TbCorrectAnswer.Text = $"{Words.PartlyCorrect}! ({string.Format(Words.CorrectWasXAndY, practise_list[index, 3], practise_list[index, 4])})";
                        }
                    }

                    //Ergebnisse speichern
                    practise_list[index, 5] = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                    practise_list[index, 6] = "nearly right";
                    practise_list[index, 7] = TbForeignLang.Text;
                    if (practise_list[index, 4] != "" && practise_list[index, 4] != null && TbForeignLangSynonym.Text != "")
                    {
                        practise_list[index, 7] = practise_list[index, 7] + ", " + TbForeignLangSynonym.Text;
                    }
                }

                if (Properties.Settings.Default.UserEvaluates == false)
                {
                    TbCorrectAnswer.BackColor = Color.FromArgb(255, 215, 0);

                    //Sound abspielen
                    mci.Close();

                    if (Properties.Settings.Default.sound == true)
                    {
                        FileInfo info_correct = new FileInfo(Application.StartupPath + @"\" + "sound_correct.wav");
                        FileInfo info_nearly_correct = new FileInfo(Application.StartupPath + @"\" + "sound_nearly_correct.wav");

                        if (info_nearly_correct.Exists == true)
                        {
                            mci.Open(Application.StartupPath + @"\" + "sound_nearly_correct.wav", "mpegvideo");
                            mci.SetTimeFormat("ms");

                            mci.Play();
                        }
                        else if (info_correct.Exists == true)
                        {
                            mci.Open(Application.StartupPath + @"\" + "sound_correct.wav", "mpegvideo");
                            mci.SetTimeFormat("ms");

                            mci.Play();
                        }
                    }
                }
            }
            else
            {
                if (book.PracticeMode == PracticeMode.AskForMotherTongue)
                {
                    if (!Settings.Default.UserEvaluates)
                    {
                        TbCorrectAnswer.Text = $"{Words.Wrong}! ({string.Format(Words.CorrectWasX, practise_list[index, 2])})";
                    }
                    //Ergebnisse speichern

                    practise_list[index, 1] = "1";
                    practise_list[index, 5] = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                    practise_list[index, 6] = "false";
                    practise_list[index, 7] = TbMotherTongue.Text;
                }
                else
                {
                    if (!Settings.Default.UserEvaluates)
                    {
                        if (string.IsNullOrEmpty(practise_list[index, 4]))
                        {
                            TbCorrectAnswer.Text = $"{Words.Wrong}! ({string.Format(Words.CorrectWasX, practise_list[index, 3])})";
                        }
                        else
                        {
                            TbCorrectAnswer.Text = $"{Words.Wrong}! ({string.Format(Words.CorrectWasXAndY, practise_list[index, 3], practise_list[index, 4])})";
                        }
                    }

                    //Ergebnisse speichern
                    practise_list[index, 1] = "1";
                    practise_list[index, 5] = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                    practise_list[index, 6] = "false";
                    practise_list[index, 7] = TbForeignLang.Text;
                    if (practise_list[index, 4] != "" && practise_list[index, 4] != null & TbForeignLangSynonym.Text != "")
                    {
                        practise_list[index, 7] = practise_list[index, 7] + ", " + TbForeignLangSynonym.Text;
                    }
                }

                if (Properties.Settings.Default.UserEvaluates == false)
                {
                    TbCorrectAnswer.BackColor = Color.FromArgb(255, 192, 203);

                    //Sound abspielen

                    mci.Close();

                    if (Properties.Settings.Default.sound == true)
                    {
                        FileInfo info = new FileInfo(Application.StartupPath + @"\" + "sound_wrong.wav");

                        if (info.Exists == true)
                        {
                            mci.Open(Application.StartupPath + @"\" + "sound_wrong.wav", "mpegvideo");
                            mci.SetTimeFormat("ms");

                            mci.Play();
                        }
                    }
                }
            }

            //Nächste Vokabel vorbereiten
            if (index < practiceList.Count)
            {
                index++;
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

        //Sonderzeichen
        private void TextBox_Enter(object sender, EventArgs e)
        {
            specialCharDialog.RegisterTextBox((TextBox)sender);
        }

        private void BtnSpecialChar_Click(object sender, EventArgs e)
        {
            specialCharDialog.Show();
        }

        //Teilweise richtig vorbereiten

        private string prepare_text(string text)
        {
            //Text bearbeiten
            //Leerschläge
            if (Properties.Settings.Default.nearly_correct_blank_char == true)
            {
                text = text.Replace(" ", "");
            }

            //Satzzeichen
            if (Properties.Settings.Default.nearly_correct_punctuation_char == true)
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

            if (Properties.Settings.Default.nearly_correct_special_char == true)
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

            if (Properties.Settings.Default.nearly_correct_artical == true)
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
            mci.Close();
        }
    }
}