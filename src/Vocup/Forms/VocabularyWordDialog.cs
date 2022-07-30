using System;
using System.Drawing;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Models.Legacy;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup.Forms
{
    public partial class VocabularyWordDialog : Form
    {
        private const string InvalidChars = "#=";
        private readonly Color redBgColor = Color.FromArgb(255, 192, 203);
        private readonly SpecialCharKeyboard specialCharDialog;
        protected readonly VocabularyBook book;

        public VocabularyWordDialog(VocabularyBook book)
        {
            InitializeComponent();

            specialCharDialog = new SpecialCharKeyboard();
            specialCharDialog.Initialize(this, BtnSpecialChar);

            this.book = book;
            LbMotherTongue.Text = book.MotherTongue;
            LbForeignLang.Text = book.ForeignLang;
            LbSynonym.Text = $"{book.ForeignLang} ({Words.Synonym})";
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            BtnContinue.Enabled =
                !string.IsNullOrWhiteSpace(TbMotherTongue.Text) &&
                !string.IsNullOrWhiteSpace(TbForeignLang.Text) &&
                TbForeignLang.Text != TbForeignLangSynonym.Text &&
                !TbMotherTongue.Text.ContainsAny(InvalidChars) &&
                !TbForeignLang.Text.ContainsAny(InvalidChars) &&
                !TbForeignLangSynonym.Text.ContainsAny(InvalidChars);

            // Visual feedback for invalid chars.
            TbMotherTongue.BackColor = TbMotherTongue.Text.ContainsAny(InvalidChars) ? redBgColor : Color.White;
            TbForeignLang.BackColor = TbForeignLang.Text.ContainsAny(InvalidChars) ? redBgColor : Color.White;
            TbForeignLangSynonym.BackColor = TbForeignLangSynonym.Text.ContainsAny(InvalidChars) ? redBgColor : Color.White;

            OnInputValidated(BtnContinue.Enabled);
        }

        protected virtual void OnInputValidated(bool passed)
        {

        }

        protected virtual bool OnCancel()
        {
            return true;
        }

        protected virtual bool OnCommit()
        {
            return true;
        }

        protected bool BookContainsInput(IVocabularyWord exclude)
        {
            foreach (IVocabularyWord word in book.Words)
            {
                if (!ReferenceEquals(word, exclude) &&
                    word.MotherTongue == TbMotherTongue.Text &&
                    word.ForeignLang == TbForeignLang.Text &&
                    (word.ForeignLangSynonym == TbForeignLangSynonym.Text ||
                    word.ForeignLangSynonym == null && TbForeignLangSynonym.Text == ""))
                {
                    return true;
                }
            }

            return false;
        }

        protected void ResetUI()
        {
            TbMotherTongue.Text = "";
            TbForeignLang.Text = "";
            TbForeignLangSynonym.Text = "";
            TbMotherTongue.Select();
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            specialCharDialog.RegisterTextBox((TextBox)sender);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (OnCancel())
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            if (OnCommit())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}