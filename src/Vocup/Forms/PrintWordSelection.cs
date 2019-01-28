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
                ListBox.Items.Add($"{word.MotherTongue} - {word.ForeignLangText}", true);
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
            for (int i = 0; i < book.Words.Count; i++) // Compose print list
            {
                if (ListBox.GetItemChecked(i))
                {
                    printList.Add(book.Words[i]);
                }
            }

            if (RbList.Checked)
            {
                PrintDialog dialog = new PrintDialog()
                {
                    AllowCurrentPage = false,
                    AllowSomePages = false,
                    UseEXDialog = true
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    invertSides = RbAskForMotherTongue.Checked;

                    PrintList.PrinterSettings = dialog.PrinterSettings;
                    PrintList.DocumentName = book.Name ?? Words.Vocup;
                    PrintList.Print();
                }

                dialog.Dispose();
            }
            else // print cards
            {
                using (var dialog = new PrintCardsDialog(printList)) dialog.ShowDialog();
            }
        }

        private void RbList_CheckedChanged(object sender, EventArgs e)
        {
            GroupPracticeMode.Enabled = RbList.Checked;
        }

        private void ListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            BtnContinue.Enabled = ListBox.CheckedItems.Count > 0;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool selection = CbUnpracticed.Checked || CbWronglyPracticed.Checked || CbCorrectlyPracticed.Checked || CbFullyPracticed.Checked;

            BtnCheckAll.Enabled = !selection;
            BtnUncheckAll.Enabled = !selection;

            if (selection)
            {
                SetItemsChecked(x => x.PracticeState == PracticeState.Unpracticed, CbUnpracticed.Checked);
                SetItemsChecked(x => x.PracticeState == PracticeState.WronglyPracticed, CbWronglyPracticed.Checked);
                SetItemsChecked(x => x.PracticeState == PracticeState.CorrectlyPracticed, CbCorrectlyPracticed.Checked);
                SetItemsChecked(x => x.PracticeState == PracticeState.FullyPracticed, CbFullyPracticed.Checked);
            }
            else
            {
                SetItemsChecked(x => true, true);
            }
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
        private bool invertSides;

        private void PrintList_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Display;

            int hoffset = 0;
            int sitePrintedWords = 0;
            int tableBegin = 0;

            int sideOffset = 2;
            int lineOffset = 2;
            int lineThickness = 1;
            int textMinHeight = 17;

            using (Font siteFont = new Font("Arial", 11))
            using (StringFormat centerFormat = new StringFormat() { Alignment = StringAlignment.Center })
            {
                g.DrawString($"{Words.Site} {pageNumber}", siteFont, Brushes.Black, e.MarginBounds.SetHeight(20).Move(0, -20), centerFormat);
            }

            using (Font titleFont = new Font("Arial", 12, FontStyle.Bold))
            using (StringFormat centerFormat = new StringFormat() { Alignment = StringAlignment.Center })
            {
                string name = string.IsNullOrWhiteSpace(book.Name) ? "" : book.Name + ": ";
                string left = invertSides ? book.ForeignLang : book.MotherTongue;
                string right = invertSides ? book.MotherTongue : book.ForeignLang;
                string title = $"{name}{left} - {right}";

                g.DrawString(title, titleFont, Brushes.Black, e.MarginBounds.MarginTop(hoffset).SetHeight(25), centerFormat);
                hoffset += 25;
                tableBegin = hoffset;
            }

            using (Font font = new Font("Arial", 10))
            using (StringFormat nearFormat = new StringFormat() { Alignment = StringAlignment.Near })
            using (Pen pen = new Pen(Brushes.Black, lineThickness))
            {
                for (; ; wordNumber++, sitePrintedWords++) // loop through printList
                {
                    Rectangle rect = e.MarginBounds.MarginTop(hoffset += lineOffset);
                    g.DrawLine(pen, rect.Left, rect.Top, rect.Right, rect.Top); // Draw horizontal lines
                    hoffset += (int)pen.Width + lineOffset;

                    if (wordNumber >= book.Words.Count) break;

                    rect = e.MarginBounds.MarginTop(hoffset);
                    VocabularyWord word = printList[wordNumber];
                    Rectangle left = new Rectangle(rect.X, rect.Y, rect.Width / 2, rect.Height).MarginSide(sideOffset);
                    Rectangle right = new Rectangle(left.Right, rect.Y, rect.Width / 2, rect.Height).MarginSide(sideOffset)
                        .MarginLeft(lineThickness); // right column is smaller than the left one because of the line
                    string leftText = invertSides ? word.ForeignLangText : word.MotherTongue;
                    string rightText = invertSides ? word.MotherTongue : word.ForeignLangText;

                    SizeF leftSize = g.MeasureString(leftText, font, left.Size, nearFormat, out int leftChars, out int leftLines);
                    SizeF rightSize = g.MeasureString(rightText, font, right.Size, nearFormat, out int rightChars, out int rightLines);
                    bool missingChars = leftChars < leftText.Length || rightChars < rightText.Length;
                    int textHeight = (int)Math.Max(textMinHeight, Math.Max(leftSize.Height, rightSize.Height));

                    if (sitePrintedWords > 0 && (missingChars || textHeight > rect.Height))
                    {
                        e.HasMorePages = true;
                        break;
                    }

                    g.DrawString(leftText, font, Brushes.Black, left, nearFormat);
                    g.DrawString(rightText, font, Brushes.Black, right, nearFormat);
                    hoffset += textHeight;
                }
            }

            using (Pen pen = new Pen(Brushes.Black, lineThickness)) // Draw vertical lines
            {
                Rectangle table = e.MarginBounds.MarginTop(tableBegin + lineOffset)
                    .SetHeight(hoffset - tableBegin - lineThickness - 2 * lineOffset);
                g.DrawLine(pen, table.Left, table.Top, table.Left, table.Bottom);
                g.DrawLine(pen, table.Right, table.Top, table.Right, table.Bottom);
                int middleX = table.Left + table.Width / 2;
                g.DrawLine(pen, middleX, table.Top, middleX, table.Bottom);
            }

            pageNumber++;
        }
    }
}