using System;
using System.Drawing;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;

namespace Vocup.Forms
{
    public class EditWordDialog : VocabularyWordDialog
    {
        private readonly VocabularyWord word;

        public EditWordDialog(VocabularyBook book, VocabularyWord word) : base(book)
        {
            this.word = word;

            Icon = Icon.FromHandle(Icons.Edit.GetHicon());
            Text = Words.EditWord;
            GroupOptions.Enabled = true;
            BtnContinue.Text = Words.Ok;

            TbMotherTongue.Text = word.MotherTongue;
            TbForeignLang.Text = word.ForeignLang;
            TbForeignLangSynonym.Text = word.ForeignLangSynonym ?? "";
        }

        protected override void OnInputValidated(bool passed)
        {
            if (passed)
            {
                BtnCancel.TabIndex = 5;
                BtnContinue.TabIndex = 0;
                AcceptButton = BtnContinue;
            }
            else
            {
                BtnCancel.TabIndex = 0;
                AcceptButton = BtnCancel;
            }
        }

        protected override bool OnCommit()
        {
            if (BookContainsInput(exclude: word))
            {
                DialogResult dialogResult = 
                    MessageBox.Show(Messages.EditToDuplicate, Messages.EditDuplicateT, MessageBoxButtons.YesNoCancel);

                if (dialogResult == DialogResult.Yes)
                {
                    book.Words.Remove(word);
                    return true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else // No duplicates to handle
            {
                word.MotherTongue = TbMotherTongue.Text;
                word.ForeignLang = TbForeignLang.Text;
                word.ForeignLangSynonym = string.IsNullOrWhiteSpace(TbForeignLangSynonym.Text) ? null : TbForeignLangSynonym.Text;

                if (CbResetResults.Checked)
                {
                    word.PracticeStateNumber = 0;
                    word.PracticeDate = DateTime.MinValue;
                }

                return true;
            }
        }
    }
}
