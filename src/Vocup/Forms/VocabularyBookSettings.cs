using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private TextBox currentInput;

        public VocabularyBookSettings()
        {
            InitializeComponent();
            currentInput = TbMotherTongue;
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            currentInput = textBox;
            specialCharDialog?.RegisterTextBox(textBox);
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            bool mValid = !TbMotherTongue.Text.ContainsAny(InvalidChars);
            TbMotherTongue.BackColor = mValid ? Color.White : redBgColor;
            bool fValid = !TbForeignTongue.Text.ContainsAny(InvalidChars);
            TbForeignTongue.BackColor = fValid ? Color.White : redBgColor;

            if (mValid && fValid &&
                !string.IsNullOrWhiteSpace(TbMotherTongue.Text) &&
                !string.IsNullOrWhiteSpace(TbForeignTongue.Text) &&
                TbMotherTongue.Text != TbForeignTongue.Text)
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
            specialCharDialog = new SpecialCharKeyboard();
            specialCharDialog.RegisterTextBox(currentInput);

            //Events definieren
            specialCharDialog.FormClosed += (a0, a1) => BtnSpecialChar.Enabled = true;

            //Position des Fensters festlegen
            specialCharDialog.Left = this.Left + (this.Width - specialCharDialog.Width) / 2;
            specialCharDialog.Top = this.Top + this.Height + 10;

            //Setzt die Form als Besitzer
            specialCharDialog.Owner = this.Owner;
            specialCharDialog.Show();

            //Sonderzeichen-Button deaktivieren
            BtnSpecialChar.Enabled = false;
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            specialCharDialog?.Close();
        }
    }
}