using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vocup.Forms;
using Vocup.Util;

namespace Vocup.Forms
{
    public partial class EditWordDialog : Form
    {
        private const string InvalidChars = "#=";
        private readonly Color redBgColor = Color.FromArgb(255, 192, 203);
        private SpecialCharKeyboard specialCharDialog;

        public bool showSpecialChars;

        public EditWordDialog()
        {
            InitializeComponent();
            specialCharDialog = new SpecialCharKeyboard();
            specialCharDialog.Initialize(this);
            specialCharDialog.VisibleChanged += (a0, a1) =>
            {
                BtnSpecialChar.Enabled = !specialCharDialog.Visible;
                showSpecialChars = specialCharDialog.Visible;
            };
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (showSpecialChars)
                specialCharDialog.Show();
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

            // TODO: Redesign this part in order to fix new bug
            /*
             * This section is about setting button texts and disabling continuation.
             * An input field has three model states:
             * 1. Started - Input data is incomplete; only first TextBox contains data.
             * 2. Valid - Input data is complete; first and second TextBox contain data.
             * 3. Aborted - Input data was complete but is no longer.
             */


            if (TbMotherTongue.Text != "" && BtnCancel.Text == Properties.Words.Finish)
            {
                BtnCancel.Text = Properties.Words.Cancel;
                BtnCancel.TabIndex = 5;
                BtnContinue.TabIndex = 0;
                AcceptButton = BtnContinue;
            }
            else if (TbMotherTongue.Text == "" && BtnCancel.Text != Properties.Words.Finish && GroupOptions.Enabled == false)
            {
                BtnCancel.Text = Properties.Words.Finish;
                BtnCancel.TabIndex = 0;
                AcceptButton = BtnCancel;
            }

            //Falls Fertig, Abbrechen anzeigen
            if (TbForeignLang.Text != "" && BtnCancel.Text == Properties.Words.Finish)
            {
                BtnCancel.Text = Properties.Words.Cancel;
                BtnCancel.TabIndex = 5;
                BtnContinue.TabIndex = 0;
                AcceptButton = BtnContinue;
            }
            else if (TbForeignLang.Text == "" && BtnCancel.Text != Properties.Words.Finish && GroupOptions.Enabled == false)
            {
                BtnCancel.Text = Properties.Words.Finish;
                BtnCancel.TabIndex = 0;
                AcceptButton = BtnCancel;
            }

            //Falls Fertig, Abbrechen anzeigen
            if (TbForeignLangSynonym.Text != "" && BtnCancel.Text == Properties.Words.Finish)
            {
                BtnCancel.Text = Properties.Words.Cancel;
                BtnCancel.TabIndex = 5;
                AcceptButton = BtnContinue;
            }
            else if (TbForeignLangSynonym.Text == "" && BtnCancel.Text != Properties.Words.Finish && GroupOptions.Enabled == false)
            {
                BtnCancel.Text = Properties.Words.Finish;
                BtnCancel.TabIndex = 0;
                AcceptButton = BtnCancel;
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            specialCharDialog.RegisterTextBox((TextBox)sender);
        }

        public void BtnSpecialChar_Click(object sender, EventArgs e)
        {
            specialCharDialog.Show();
        }
    }
}