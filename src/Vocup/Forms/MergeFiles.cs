using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vocup.IO;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Util;

#nullable disable

namespace Vocup.Forms;

public partial class MergeFiles : Form
{
    private readonly Color redBgColor = Color.FromArgb(255, 192, 203);
    private readonly SpecialCharKeyboard specialCharDialog;
    private bool textsValid;

    private readonly List<VocabularyBook> books;

    public MergeFiles()
    {
        InitializeComponent();
        Icon = Icon.FromHandle(Icons.MergeFiles.GetHicon());
        specialCharDialog = new SpecialCharKeyboard();
        specialCharDialog.Initialize(this, BtnSpecialChar);
        specialCharDialog.RegisterTextBox(TbMotherTongue);

        books = new List<VocabularyBook>();
    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
        OpenFileDialog addFile = new OpenFileDialog
        {
            Title = Words.AddVocabularyBooks,
            InitialDirectory = Settings.Default.VhfPath,
            Filter = Words.VocupVocabularyBookFile + " (*.vhf)|*.vhf",
            Multiselect = true
        };

        if (addFile.ShowDialog() == DialogResult.OK)
        {
            foreach (string file in addFile.FileNames)
            {
                VocabularyBook book = new VocabularyBook();
                if (!VocabularyFile.ReadVhfFile(file, book))
                    continue;
                VocabularyFile.ReadVhrFile(book);
                VocabularyBook conflict = books.Where(x => x.FilePath.Equals(book.FilePath, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (conflict != null)
                {
                    if (MessageBox.Show(Messages.MergeOverride, Messages.MergeOverrideT, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        continue;
                    books.Remove(conflict);
                    LbFiles.Items.Remove(conflict.FilePath);
                }
                books.Add(book);
                LbFiles.Items.Add(book.FilePath);
            }
            ValidateInput();
        }
        addFile.Dispose();
    }

    private void BtnRemove_Click(object sender, EventArgs e)
    {
        while (LbFiles.SelectedItems.Count > 0)
        {
            string file = LbFiles.SelectedItems[0].ToString();
            books.RemoveAll(x => x.FilePath == file);
            LbFiles.Items.Remove(file);
        }

        ValidateInput();
    }

    private void LbFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        BtnRemove.Enabled = LbFiles.SelectedItems.Count > 0;
    }

    private void TextBox_TextChanged(object sender, EventArgs e)
    {
        bool mValid = !TbMotherTongue.Text.ContainsAny(AppInfo.InvalidPathChars);
        TbMotherTongue.BackColor = mValid ? Color.White : redBgColor;
        bool fValid = !TbForeignLang.Text.ContainsAny(AppInfo.InvalidPathChars);
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

    private void BtnSave_Click(object sender, EventArgs e)
    {
        string path;

        using (SaveFileDialog save = new SaveFileDialog
        {
            Title = Words.SaveVocabularyBook,
            FileName = TbMotherTongue.Text + " - " + TbForeignLang.Text,
            InitialDirectory = Settings.Default.VhfPath,
            Filter = Words.VocupVocabularyBookFile + " (*.vhf)|*.vhf"
        })
        {
            if (save.ShowDialog() == DialogResult.OK)
            {
                path = save.FileName;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                return;
            }
        }

        Cursor.Current = Cursors.WaitCursor;

        VocabularyBook result = new VocabularyBook
        {
            MotherTongue = TbMotherTongue.Text,
            ForeignLang = TbForeignLang.Text,
            FilePath = path
        };

        foreach (VocabularyBook book in books)
        {
            foreach (VocabularyWord word in book.Words)
            {
                CopyWord(word, result);
            }
        }

        result.GenerateVhrCode();

        if (!VocabularyFile.WriteVhfFile(path, result) ||
            !VocabularyFile.WriteVhrFile(result))
        {
            MessageBox.Show(Messages.VocupFileWriteError, Messages.VocupFileWriteErrorT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.Abort;
        }
        else
        {
            DialogResult = DialogResult.OK;
        }

        Cursor.Current = Cursors.Default;
    }

    private void CopyWord(VocabularyWord word, VocabularyBook target)
    {
        VocabularyWord cloned = word.Clone(CbKeepResults.Checked);

        for (int i = 0; i < target.Words.Count; i++)
        {
            VocabularyWord comp = target.Words[i];
            if (cloned.MotherTongue == comp.MotherTongue &&
                cloned.ForeignLang == comp.ForeignLang &&
                cloned.ForeignLangSynonym == comp.ForeignLangSynonym)
            {
                if (cloned.PracticeDate > comp.PracticeDate)
                    target.Words[i] = cloned;

                return;
            }
        }

        target.Words.Add(cloned);
    }
}