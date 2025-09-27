using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;

namespace Vocup.Forms;

public partial class PracticeResultList : Form
{
    private readonly Color CorrectFeedbackBackColor;
    private readonly Color PartlyCorrectFeedbackBackColor;
    private readonly Color WrongFeedbackBackColor;

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

        CorrectFeedbackBackColor = Color.FromArgb(144, 238, 144);
        PartlyCorrectFeedbackBackColor = Color.FromArgb(255, 215, 0);
        WrongFeedbackBackColor = Color.FromArgb(255, 192, 203);

#pragma warning disable WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        if (Application.ColorMode == SystemColorMode.Dark || (Application.ColorMode == SystemColorMode.System && Application.SystemColorMode == SystemColorMode.Dark))
        {
            CorrectFeedbackBackColor = Color.FromArgb(0, 100, 0);
            PartlyCorrectFeedbackBackColor = Color.FromArgb(127, 106, 0);
            WrongFeedbackBackColor = Color.FromArgb(127, 0, 0);
        }
#pragma warning restore WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.


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

        Program.TrackingService.Page("/practice/result");
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

    private void ListView_Resize(object sender, EventArgs e)
    {
        if (Program.Settings.ColumnResize)
        {
            int include = SystemInformation.VerticalScrollBarWidth + ListView.Columns.Count;
            int width = (ListView.Width - imageColumn.Width - include) / 3;
            motherTongueColumn.Width = width;
            foreignLangColumn.Width = width;
            wrongInputColumn.Width = width;
        }
    }

    private void BtnContinue_Click(object sender, EventArgs e)
    {
        if (CbDoNotShowAgain.Checked)
        {
            Program.Settings.PracticeShowResultList = false;
        }

        Close();
    }

    private void CalculateGrade()
    {
        if (notPracticed == practiceList.Count)
        {
            TbPercentage.Text = "-";
        }
        else // Calculate percentage and grade
        {
            double correctRatio = (correct + 0.5 * partlyCorrect) / (practiceList.Count - notPracticed);

            TbPercentage.BackColor = correctRatio switch
            {
                // Steps taken from https://de.wikipedia.org/wiki/Vorlage:Punktesystem_der_gymnasialen_Oberstufe
                >= 0.70 => CorrectFeedbackBackColor, // at least 70% -> green background
                >= 0.45 => PartlyCorrectFeedbackBackColor, // at least 45% -> yellow background
                _ => WrongFeedbackBackColor // less than 45% -> red background
            };

            TbPercentage.Text = Math.Round(correctRatio * 100) + "%";
        }
    }
}