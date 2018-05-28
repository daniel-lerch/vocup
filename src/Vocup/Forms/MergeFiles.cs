using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vocup.Forms;
using Vocup.Util;

namespace Vocup.Forms
{
    public partial class MergeFiles : Form
    {
        private const string InvalidChars = "#=:\\/|<>*?\"";
        private readonly Color redBgColor = Color.FromArgb(255, 192, 203);
        private SpecialCharKeyboard specialCharDialog;
        private bool textsValid;

        public MergeFiles()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(icons.merge.GetHicon());
            specialCharDialog = new SpecialCharKeyboard();
            specialCharDialog.Initialize(this);
            specialCharDialog.VisibleChanged += (a0, a1) => ValidateInput();
            specialCharDialog.RegisterTextBox(TbMotherTongue);
        }

        //Pfad zum Speicherort
        public string pfad;

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog addFile = new OpenFileDialog
            {
                Title = Properties.language.add_title,
                InitialDirectory = Properties.Settings.Default.path_vhf,
                Filter = Properties.language.personal_directory + " (*.vhf)|*.vhf",
                Multiselect = true
            };

            if (addFile.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in addFile.FileNames)
                {
                    if (!LbFiles.Items.Contains(file))
                        LbFiles.Items.Add(file);
                }
                ValidateInput();
            }
            addFile.Dispose();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            while (LbFiles.SelectedItems.Count > 0)
                LbFiles.Items.Remove(LbFiles.SelectedItems[0]);

            ValidateInput();
        }

        private void LbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnRemove.Enabled = LbFiles.SelectedItems.Count > 0;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            bool mValid = !TbMotherTongue.Text.ContainsAny(InvalidChars);
            TbMotherTongue.BackColor = mValid ? Color.White : redBgColor;
            bool fValid = !TbForeignLang.Text.ContainsAny(InvalidChars);
            TbForeignLang.BackColor = fValid ? Color.White : redBgColor;

            textsValid = mValid && fValid &&
                !string.IsNullOrWhiteSpace(TbMotherTongue.Text) &&
                !string.IsNullOrWhiteSpace(TbForeignLang.Text) &&
                TbMotherTongue.Text != TbForeignLang.Text;

            ValidateInput();
        }

        private void ValidateInput()
        {
            bool itemsValid = LbFiles.Items.Count > 1;
            GroupMotherTongue.Enabled = itemsValid;
            GroupForeignTongue.Enabled = itemsValid;
            CbKeepResults.Enabled = itemsValid;
            BtnSpecialChar.Enabled = itemsValid && !specialCharDialog.Visible;

            if (itemsValid && textsValid)
            {
                BtnSave.Enabled = true;
                AcceptButton = BtnSave;
            }
            else
            {
                BtnSave.Enabled = false;
                AcceptButton = BtnCancel;
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            specialCharDialog.RegisterTextBox((TextBox)sender);
        }

        private void BtnSpecialChar_Click(object sender, EventArgs e)
        {
            specialCharDialog.Show();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog
            {
                Title = Properties.language.save_title,
                FileName = TbMotherTongue.Text + " - " + TbForeignLang.Text,
                InitialDirectory = Properties.Settings.Default.path_vhf,
                Filter = Properties.language.personal_directory + " (*.vhf)|*.vhf"
            };

            if (save.ShowDialog() == DialogResult.OK)
            {
                pfad = save.FileName;
                DialogResult = DialogResult.OK;
            }

            save.Dispose();
        }
    }
}