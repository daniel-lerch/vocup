using System;
using System.Drawing;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup.Forms
{
    public partial class VocabularyWordDialog : Form
    {
        private const string InvalidChars = "#=";
        private readonly Color InvalidInputBackColor;
        private readonly SpecialCharKeyboard specialCharDialog;
        protected readonly VocabularyBook book;

        public VocabularyWordDialog(VocabularyBook book)
        {
            InitializeComponent();

            specialCharDialog = new SpecialCharKeyboard();
            specialCharDialog.Initialize(this, BtnSpecialChar);

            InvalidInputBackColor = Color.FromArgb(255, 192, 203);

#pragma warning disable WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            if (Application.ColorMode == SystemColorMode.Dark)
            {
                InvalidInputBackColor = Color.FromArgb(127, 0, 0);
            }
#pragma warning restore WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

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
            TbMotherTongue.BackColor = TbMotherTongue.Text.ContainsAny(InvalidChars) ? InvalidInputBackColor : SystemColors.Window;
            TbForeignLang.BackColor = TbForeignLang.Text.ContainsAny(InvalidChars) ? InvalidInputBackColor : SystemColors.Window;
            TbForeignLangSynonym.BackColor = TbForeignLangSynonym.Text.ContainsAny(InvalidChars) ? InvalidInputBackColor : SystemColors.Window;

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

        protected bool BookContainsInput(VocabularyWord exclude)
        {
            foreach (VocabularyWord word in book.Words)
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