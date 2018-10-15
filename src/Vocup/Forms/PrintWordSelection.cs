using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup.Forms
{
    public partial class PrintWordSelection : Form
    {
        VocabularyBook book;

        public PrintWordSelection(VocabularyBook book)
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.Print.GetHicon());

            this.book = book;

            ListBox.BeginUpdate();
            foreach (VocabularyWord word in book.Words)
                ListBox.Items.Add($"{word.MotherTongue} - {word.ForeignLangText}");
            ListBox.EndUpdate();

            CbUnpracticed.Enabled = book.Statistics.Unpracticed > 0;
            CbWronglyPracticed.Enabled = book.Statistics.WronglyPracticed > 0;
            CbCorrectlyPracticed.Enabled = book.Statistics.CorrectlyPracticed > 0;
            CbFullyPracticed.Enabled = book.Statistics.FullyPracticed > 0;
        }

        private void BtnCheckAll_Click(object sender, EventArgs e)
        {
            SetItemsChecked(x => true, true);
            BtnContinue.Enabled = true;
            BtnContinue.Focus(); //Fokus auf weiter-Button
        }

        private void BtnUncheckAll_Click(object sender, EventArgs e)
        {
            SetItemsChecked(x => true, false);
            BtnContinue.Enabled = false;
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            PrintDialog dialog = new PrintDialog()
            {
                AllowCurrentPage = false,
                AllowSomePages = false,
                UseEXDialog = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < book.Words.Count; i++)
                {
                    if (ListBox.GetItemChecked(i))
                    {
                        printList.Add(book.Words[i]);
                    }
                }

                PrintList.PrinterSettings = dialog.PrinterSettings;
                PrintList.DocumentName = book.Name ?? Words.Vocup;
                PrintList.Print();
            }

            dialog.Dispose();
        }

        private void RbList_CheckedChanged(object sender, EventArgs e)
        {
            GroupPracticeMode.Enabled = RbList.Checked;
        }

        private void ListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            BtnContinue.Enabled = ListBox.CheckedItems.Count > 0;
        }

        private void CbUnpracticed_CheckedChanged(object sender, EventArgs e)
        {
            SetItemsChecked(x => x.PracticeState == PracticeState.Unpracticed, CbUnpracticed.Checked);
            CheckBox_CheckedChanged(sender, e);
        }

        private void CbWronglyPracticed_CheckedChanged(object sender, EventArgs e)
        {
            SetItemsChecked(x => x.PracticeState == PracticeState.WronglyPracticed, CbUnpracticed.Checked);
            CheckBox_CheckedChanged(sender, e);
        }

        private void CbCorrectlyPracticed_CheckedChanged(object sender, EventArgs e)
        {
            SetItemsChecked(x => x.PracticeState == PracticeState.CorrectlyPracticed, CbUnpracticed.Checked);
            CheckBox_CheckedChanged(sender, e);
        }

        private void CbFullyPracticed_CheckedChanged(object sender, EventArgs e)
        {
            SetItemsChecked(x => x.PracticeState == PracticeState.FullyPracticed, CbUnpracticed.Checked);
            CheckBox_CheckedChanged(sender, e);
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool selection = CbUnpracticed.Checked || CbWronglyPracticed.Checked || CbCorrectlyPracticed.Checked || CbFullyPracticed.Checked;

            BtnCheckAll.Enabled = !selection;
            BtnUncheckAll.Enabled = !selection;
        }

        private void SetItemsChecked(Func<VocabularyWord, bool> predicate, bool value)
        {
            ListBox.BeginUpdate();

            for (int i = 0; i < book.Words.Count; i++)
            {
                if (predicate(book.Words[i]))
                {
                    ListBox.SetItemChecked(i, value);
                }
            }

            ListBox.EndUpdate();
        }

        private List<VocabularyWord> printList = new List<VocabularyWord>();
        private int wordNumber = 0;
        private int pageNumber = 1;

        private void PrintList_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Display;

            int hoffset = 0;
            int siteWordNumber = 0;

            using (Font siteFont = new Font("Arial", 11))
            using (StringFormat centerFormat = new StringFormat() { Alignment = StringAlignment.Center })
            {
                g.DrawString($"{Words.Site} {pageNumber}", siteFont, Brushes.Black, e.MarginBounds.SetHeight(20).Move(0, -20), centerFormat);
            }

            using (Font titleFont = new Font("Arial", 12, FontStyle.Bold))
            using (StringFormat centerFormat = new StringFormat() { Alignment = StringAlignment.Center })
            {
                string name = string.IsNullOrWhiteSpace(book.Name) ? "" : book.Name + ": ";

                string title;
                if (book.PracticeMode == PracticeMode.AskForForeignLang)
                    title = $"{book.MotherTongue} - {book.ForeignLang}";
                else
                    title = $"{book.ForeignLang} - {book.MotherTongue}";

                g.DrawString(name + title, titleFont, Brushes.Black, e.MarginBounds.MarginTop(hoffset).SetHeight(25), centerFormat);
                hoffset += 25;
            }

            using (Font font = new Font("Arial", 10))
            using (StringFormat nearFormat = new StringFormat() { Alignment = StringAlignment.Near })
            using (Pen pen = new Pen(Brushes.Black, 1F))
            {
                for (; ; wordNumber++, siteWordNumber++) // loop through printList
                {
                    int sideoffset = 0;
                    int lineoffset = 1;

                    Rectangle rect = e.MarginBounds.MarginTop(hoffset += lineoffset).MarginSide(sideoffset);
                    g.DrawLine(pen, rect.Left, rect.Top, rect.Right, rect.Top);
                    hoffset += (int)pen.Width + lineoffset;

                    if (wordNumber >= book.Words.Count) break;

                    rect = e.MarginBounds.MarginTop(hoffset).MarginSide(sideoffset);
                    VocabularyWord word = printList[wordNumber];
                    Rectangle left = new Rectangle(rect.X, rect.Y, rect.Width / 2, rect.Height);
                    Rectangle right = new Rectangle(left.Right, rect.Y, rect.Width / 2, rect.Height);

                    SizeF leftSize = g.MeasureString(word.MotherTongue, font, left.Size, nearFormat, out int leftChars, out int leftLines);
                    SizeF rightSize = g.MeasureString(word.ForeignLangText, font, right.Size, nearFormat, out int rightChars, out int rightLines);
                    bool missingChars = leftChars < word.MotherTongue.Length || rightChars < word.ForeignLangText.Length;
                    int textHeight = (int)Math.Max(leftSize.Height, rightSize.Height);

                    if (siteWordNumber > 0 && (missingChars || textHeight > rect.Height))
                    {
                        e.HasMorePages = true;
                        break;
                    }

                    g.DrawString(word.MotherTongue, font, Brushes.Black, left, nearFormat);
                    g.DrawString(word.ForeignLangText, font, Brushes.Black, right, nearFormat);
                    hoffset += textHeight;
                }
            }

            pageNumber++;
        }
    }
}