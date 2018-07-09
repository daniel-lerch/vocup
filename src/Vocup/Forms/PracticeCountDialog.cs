using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Vocup.Models;

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
            // TODO: Implement logic:
            // 1. Calculate count for each category
            // 2. Get all items for each category, sort and get the needed count
            // 3. Store all elements in one list
            // 4. Randomly reorder elements (https://stackoverflow.com/questions/273313/randomize-a-listt)

            Close();
        }
    }
}