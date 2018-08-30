using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;

namespace Vocup.Forms
{
    public partial class PracticeResultList : Form
    {
        private VocabularyBook book;
        private List<VocabularyWordPractice> practiceList;

        private int notPracticed;
        private int wrong;
        private int partlyCorrect;
        private int correct;

        public PracticeResultList(VocabularyBook book, List<VocabularyWordPractice> practiceList)
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.BarChart.GetHicon());

            this.book = book;
            this.practiceList = practiceList;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            motherTongueColumn.Text = book.MotherTongue;
            foreignLangColumn.Text = book.ForeignLang;

            if (practiceList.Count == 1)
                GroupStatistics.Text = Words.OverallOneWord + ":";
            else
                GroupStatistics.Text = string.Format(Words.OverallXWords, practiceList.Count) + ":";


            ListView.BeginUpdate();
            ListView.GridLines = Settings.Default.GridLines;

            foreach (VocabularyWordPractice practice in practiceList)
            {
                VocabularyWord word = practice.VocabularyWord;
                ListView.Items.Add(new ListViewItem(new[] { "", word.MotherTongue, word.ForeignLangText, practice.WrongInput }, (int)practice.PracticeResult));
            }

            ListView.EndUpdate();
            ListView.Enabled = true;


            //Zahlen aktualiseren
            IEnumerable<PracticeResult> results = practiceList.Select(x => x.PracticeResult);

            notPracticed = results.Where(x => x == PracticeResult.NotPracticed).Count();
            wrong = results.Where(x => x == PracticeResult.Wrong).Count();
            partlyCorrect = results.Where(x => x == PracticeResult.PartlyCorrect).Count();
            correct = results.Where(x => x == PracticeResult.Correct).Count();

            TbNotPracticedCount.Text = notPracticed.ToString();
            TbWrongCount.Text = wrong.ToString();
            TbPartlyCorrectCount.Text = partlyCorrect.ToString();
            TbCorrectCount.Text = correct.ToString();

            CalculateGrade();
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            if (book.Statistics.NotFullyPracticed == 0)
            {
                MessageBox.Show(Messages.BookPracticeFinished, Messages.BookPracticeFinishedT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Prevents the user from changing these columns
        private void ListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Cancel = true;
                e.NewWidth = 20;
            }
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            if (CbDoNotShowAgain.Checked)
            {
                Settings.Default.show_practise_result_list = false;
                Settings.Default.Save();
            }

            Close();
        }

        private void CalculateGrade()
        {
            if (notPracticed == practiceList.Count)
            {
                TbGrade.Text = "-";
                TbPercentage.Text = "-";
            }
            else // Calculate percentage and grade
            {
                decimal grade;
                if (Settings.Default.notensystem == "de")
                {
                    grade = 7M - Math.Round(((correct + partlyCorrect / 2M) * 5M / (practiceList.Count - notPracticed)) + 1, 1);

                    //Hintergrundfarbe bestimmen
                    if (grade > 3)
                        TbGrade.BackColor = Color.FromArgb(255, 192, 203);
                    else if (grade > 2)
                        TbGrade.BackColor = Color.FromArgb(255, 215, 0);
                    else
                        TbGrade.BackColor = Color.FromArgb(144, 238, 144);
                }
                else
                {
                    grade = Math.Round(((correct + partlyCorrect / 2M) * 5M / (practiceList.Count - notPracticed)) + 1, 1);

                    //Hintergrundfarbe bestimmen
                    if (grade < 4)
                        TbGrade.BackColor = Color.FromArgb(255, 192, 203);
                    else if (grade < 5)
                        TbGrade.BackColor = Color.FromArgb(255, 215, 0);
                    else
                        TbGrade.BackColor = Color.FromArgb(144, 238, 144);
                }

                //Prozent berechnen
                TbPercentage.Text = Math.Round((correct + partlyCorrect / 2M) / (practiceList.Count - notPracticed) * 100M) + "%";

                //Note anzeigen
                TbGrade.Text = grade.ToString();
            }
        }
    }
}