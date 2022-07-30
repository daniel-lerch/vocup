using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Models.Legacy;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup.Forms;

public partial class PracticeCountDialog : Form
{
    private readonly VocabularyBook book;
    private int _count;

    public PracticeCountDialog(VocabularyBook book)
    {
        InitializeComponent();
        this.book = book;
        PracticeList = new List<VocabularyWordPractice>();
        Count = book.Statistics.NotFullyPracticed;
    }

    public List<VocabularyWordPractice> PracticeList { get; }

    private int Count
    {
        get => _count;
        set
        {
            _count = value;
            NudCount.Maximum = value;

            BtnCount20.Enabled = value >= 20;
            BtnCount30.Enabled = value >= 30;
            BtnCount40.Enabled = value >= 40;
            BtnCountAll.Text = $"{Words.AllWords} ({value})";
        }
    }

    private void Form_Load(object sender, EventArgs e)
    {
        RbUnpracticed.Enabled = book.Statistics.Unpracticed > 0;
        RbWronglyPracticed.Enabled = book.Statistics.WronglyPracticed > 0;
        RbCorrectlyPracticed.Enabled = book.Statistics.CorrectlyPracticed > 0;
    }

    private void BtnCount20_Click(object sender, EventArgs e) => Finish(20);

    private void BtnCount30_Click(object sender, EventArgs e) => Finish(30);

    private void BtnCount40_Click(object sender, EventArgs e) => Finish(40);

    private void BtnCountAll_Click(object sender, EventArgs e) => Finish(Count);

    private void BtnCountCustom_Click(object sender, EventArgs e) => Finish((int)NudCount.Value);

    private void RbAllStates_CheckedChanged(object sender, EventArgs e)
    {
        if (RbAllStates.Checked)
            Count = book.Statistics.NotFullyPracticed;
    }

    private void RbUnpracticed_CheckedChanged(object sender, EventArgs e)
    {
        if (RbUnpracticed.Checked)
            Count = book.Statistics.Unpracticed;
    }

    private void RbWronglyPracticed_CheckedChanged(object sender, EventArgs e)
    {
        if (RbWronglyPracticed.Checked)
            Count = book.Statistics.WronglyPracticed;
    }

    private void RbCorrectlyPracticed_CheckedChanged(object sender, EventArgs e)
    {
        if (RbCorrectlyPracticed.Checked)
            Count = book.Statistics.CorrectlyPracticed;
    }

    private void Finish(int count)
    {
        ListCompositor<IVocabularyWord> compositor = new ListCompositor<IVocabularyWord>();

        IEnumerable<IVocabularyWord> unpracticedItems = book.Words
            .Where(x => x.PracticeState == PracticeState.Unpracticed);
        IEnumerable<IVocabularyWord> wronglyPracticedItems = book.Words
            .Where(x => x.PracticeState == PracticeState.WronglyPracticed);
        IEnumerable<IVocabularyWord> correctlyPracticedItems = book.Words
            .Where(x => x.PracticeState == PracticeState.CorrectlyPracticed);

        if (RbAllDates.Checked)
        {
            // No sorting wanted here, so we shuffle
            unpracticedItems = unpracticedItems.Shuffle();
            wronglyPracticedItems = wronglyPracticedItems.Shuffle();
            correctlyPracticedItems = correctlyPracticedItems.Shuffle();
        }
        if (RbEarlierPracticed.Checked)
        {
            unpracticedItems = unpracticedItems.OrderBy(x => x.PracticeDate);
            wronglyPracticedItems = wronglyPracticedItems.OrderBy(x => x.PracticeDate);
            correctlyPracticedItems = correctlyPracticedItems.OrderBy(x => x.PracticeDate);
        }
        else if (RbLaterPracticed.Checked)
        {
            unpracticedItems = unpracticedItems.OrderByDescending(x => x.PracticeDate);
            wronglyPracticedItems = wronglyPracticedItems.OrderByDescending(x => x.PracticeDate);
            correctlyPracticedItems = correctlyPracticedItems.OrderByDescending(x => x.PracticeDate);
        }

        if (RbAllStates.Checked)
        {
            compositor.AddSource(unpracticedItems.ToList(),
                count * Program.Settings.PracticePercentageUnpracticed / 100d);

            compositor.AddSource(wronglyPracticedItems.ToList(),
                count * Program.Settings.PracticePercentageWrong / 100d);

            compositor.AddSource(correctlyPracticedItems.ToList(),
                count * Program.Settings.PracticePercentageCorrect / 100d);
        }
        else if (RbUnpracticed.Checked)
        {
            compositor.AddSource(unpracticedItems.ToList(), 1d);
        }
        else if (RbWronglyPracticed.Checked)
        {
            compositor.AddSource(wronglyPracticedItems.ToList(), 1d);
        }
        else if (RbCorrectlyPracticed.Checked)
        {
            compositor.AddSource(correctlyPracticedItems.ToList(), 1d);
        }

        List<IVocabularyWord> resultList = compositor.ToList(count);

        foreach (IVocabularyWord item in resultList)
        {
            PracticeList.Add(new VocabularyWordPractice(item));
        }

        DialogResult = DialogResult.OK;
        Close();
    }
}