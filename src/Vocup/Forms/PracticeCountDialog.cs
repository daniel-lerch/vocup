using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Util;

namespace Vocup.Forms
{
    public partial class PracticeCountDialog : Form
    {
        private VocabularyBook book;

        public PracticeCountDialog(VocabularyBook book)
        {
            InitializeComponent();
            this.book = book;
            PracticeList = new List<VocabularyWordPractice>();
        }

        public List<VocabularyWordPractice> PracticeList { get; }

        private void Form_Load(object sender, EventArgs e)
        {
            RbUnpracticed.Enabled = book.Statistics.Unpracticed > 0;
            RbWronglyPracticed.Enabled = book.Statistics.WronglyPracticed > 0;
            RbCorrectlyPracticed.Enabled = book.Statistics.CorrectlyPracticed > 0;
        }

        private void BtnCount20_Click(object sender, EventArgs e) => Finish(20);

        private void BtnCount30_Click(object sender, EventArgs e) => Finish(30);

        private void BtnCount40_Click(object sender, EventArgs e) => Finish(40);

        private void BtnCountAll_Click(object sender, EventArgs e) => Finish(book.Statistics.NotFullyPracticed);

        private void BtnCountCustom_Click(object sender, EventArgs e) => Finish((int)anzahl.Value);

        private void RbAllStates_CheckedChanged(object sender, EventArgs e)
        {
            if (RbAllStates.Checked)
                Reload(book.Statistics.NotFullyPracticed);
        }

        private void RbUnpracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (RbUnpracticed.Checked)
                Reload(book.Statistics.Unpracticed);
        }

        private void RbWronglyPracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (RbWronglyPracticed.Checked)
                Reload(book.Statistics.WronglyPracticed);
        }

        private void RbCorrectlyPracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (RbCorrectlyPracticed.Checked)
                Reload(book.Statistics.CorrectlyPracticed);
        }

        private void Reload(int count)
        {
            anzahl.Maximum = count;

            BtnCount20.Enabled = count >= 20;
            BtnCount30.Enabled = count >= 30;
            BtnCount40.Enabled = count >= 40;
        }

        private void Finish(int count)
        {
            ListCompositor<VocabularyWord> compositor = new ListCompositor<VocabularyWord>();

            IEnumerable<VocabularyWord> unpracticedItems = book.Words
                .Where(x => x.PracticeState == PracticeState.Unpracticed);
            IEnumerable<VocabularyWord> wronglyPracticedItems = book.Words
                .Where(x => x.PracticeState == PracticeState.WronglyPracticed);
            IEnumerable<VocabularyWord> correctlyPracticedItems = book.Words
                .Where(x => x.PracticeState == PracticeState.CorrectlyPracticed);

            if (RbEarlierPracticed.Checked) // No sorting needed in case of RbAllDates.Checked
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
                    count * Properties.Settings.Default.prozent_noch_nicht / 100d);

                compositor.AddSource(wronglyPracticedItems.ToList(),
                    count * Properties.Settings.Default.prozent_falsch / 100d);

                compositor.AddSource(correctlyPracticedItems.ToList(),
                    count * Properties.Settings.Default.prozent_richtig / 100d);
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

            List<VocabularyWord> resultList = compositor.ToList(count);

            foreach (VocabularyWord item in resultList)
            {
                PracticeList.Add(new VocabularyWordPractice(item));
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}