using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vocup.Util;

namespace Vocup.Forms
{
    public partial class VocabularyBookSettings : Form
    {
        private const string InvalidChars = "#=:\\/|<>*?\"";
        private readonly Color redBgColor = Color.FromArgb(255, 192, 203);
        private SpecialCharKeyboard specialCharDialog;

        public VocabularyBookSettings()
        {
            InitializeComponent();
            specialCharDialog = new SpecialCharKeyboard();
            specialCharDialog.Initialize(this);
            specialCharDialog.VisibleChanged += (a0, a1) => BtnSpecialChar.Enabled = true;
            specialCharDialog.RegisterTextBox(TbMotherTongue);
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            specialCharDialog.RegisterTextBox((TextBox)sender);
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            bool mValid = !TbMotherTongue.Text.ContainsAny(InvalidChars);
            TbMotherTongue.BackColor = mValid ? Color.White : redBgColor;
            bool fValid = !TbForeignLang.Text.ContainsAny(InvalidChars);
            TbForeignLang.BackColor = fValid ? Color.White : redBgColor;

            if (mValid && fValid &&
                !string.IsNullOrWhiteSpace(TbMotherTongue.Text) &&
                !string.IsNullOrWhiteSpace(TbForeignLang.Text) &&
                TbMotherTongue.Text != TbForeignLang.Text)
            {
                BtnOK.Enabled = true;
                AcceptButton = BtnOK;
            }
            else
            {
                BtnOK.Enabled = false;
                AcceptButton = BtnCancel;
            }
        }

        private void BtnSpecialChar_Click(object sender, EventArgs e)
        {
            specialCharDialog.Show();
            BtnSpecialChar.Enabled = false;
        }
    }
}