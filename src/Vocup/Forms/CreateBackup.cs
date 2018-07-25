using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup.Forms
{
    public partial class CreateBackup : Form
    {
        public CreateBackup()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.backup_add.GetHicon());
        }

        public string pfad;

        private void Form_Load(object sender, EventArgs e)
        {
            {
                // Check for vocabulary book files
                DirectoryInfo booksInfo = new DirectoryInfo(Settings.Default.path_vhf);
                int count = 0;
                if (booksInfo.Exists && (count = booksInfo.EnumerateFiles("*.vhf", SearchOption.AllDirectories).Count()) > 0)
                {
                    CbSaveAllBooks.Checked = true;
                    CbSaveAllBooks.Enabled = true;
                }
                CbSaveAllBooks.Text = string.Format(CbSaveAllBooks.Text, count);
            }
            {
                // Check for pratice result files
                DirectoryInfo resultsInfo = new DirectoryInfo(Settings.Default.path_vhr);
                int count = 0;
                if (resultsInfo.Exists && (count = resultsInfo.EnumerateFiles("*.vhr", SearchOption.TopDirectoryOnly).Count()) > 0)
                {
                    RbSaveAssociatedResults.Enabled = true;
                    RbSaveAllResults.Enabled = true;
                    RbSaveAssociatedResults.Checked = true;
                }
                RbSaveAllResults.Text = string.Format(RbSaveAllResults.Text, count);
            }
            {
                // Check for special char files
                DirectoryInfo specialCharInfo = new DirectoryInfo(AppInfo.SpecialCharDirectory);
                if (specialCharInfo.Exists)
                {
                    foreach (FileInfo file in specialCharInfo.GetFiles("*.txt", SearchOption.TopDirectoryOnly))
                    {
                        ListSpecialChars.Items.Add(Path.GetFileNameWithoutExtension(file.FullName));
                    }

                    if (ListSpecialChars.Items.Count > 0)
                    {
                        GroupSpecialChar.Enabled = true;
                    }
                }
            }
        }

        private void CbSaveAllBooks_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void ResultRadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
                UpdateUI();
        }

        private void ListSpecialChars_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// Enables or disables the AcceptButton depending on the user's settings.
        /// </summary>
        private void UpdateUI()
        {
            RbSaveAssociatedResults.Enabled = RbSaveAllResults.Enabled && (CbSaveAllBooks.Checked || ListVocabularyBooks.Items.Count > 0);
            if (!RbSaveAssociatedResults.Enabled && RbSaveAssociatedResults.Checked)
                RbSaveNoResults.Checked = true;
            BtnCreateBackup.Enabled = CbSaveAllBooks.Checked || ListVocabularyBooks.Items.Count > 0 || RbSaveAllResults.Checked || ListSpecialChars.CheckedItems.Count > 0;
        }

        private void BtnAddVocabularyBook_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog add_file = new OpenFileDialog
            {
                Title = Words.AddVocabularyBooks,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                Filter = Words.VocupVocabularyBookFile + " (*.vhf)|*.vhf",
                Multiselect = true
            })
            {
                if (add_file.ShowDialog() == DialogResult.OK)
                {
                    foreach (string path in add_file.FileNames)
                    {
                        if (!ListVocabularyBooks.Items.Contains(path))
                        {
                            // TODO: Warning: A user might add a file that is backuped by setting CbSaveAllBooks.Checked to true
                            ListVocabularyBooks.Items.Add(path);
                        }
                    }

                    UpdateUI();
                }
            }
        }

        private void BtnDeleteVocabularyBook_Click(object sender, EventArgs e)
        {
            while (ListVocabularyBooks.SelectedItems.Count > 0)
            {
                ListVocabularyBooks.Items.Remove(ListVocabularyBooks.SelectedItems[0]);
            }

            BtnDeleteVocabularyBook.Enabled = false;

            UpdateUI();
        }

        private void ListVocabularyBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnDeleteVocabularyBook.Enabled = ListVocabularyBooks.SelectedItems.Count > 0;
        }

        private void BtnCreateBackup_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog save = new SaveFileDialog
            {
                Title = Words.SaveBackup,
                Filter = Words.VocupBackupFile + " (*.vdp)|*.vdp"
            })
            {
                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.backup_folder) ||
                    !Directory.Exists(Properties.Settings.Default.backup_folder))
                {
                    save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }
                else
                {
                    save.InitialDirectory = Properties.Settings.Default.backup_folder;
                }

                if (save.ShowDialog() == DialogResult.OK)
                {
                    pfad = save.FileName;
                    Properties.Settings.Default.backup_folder = new FileInfo(save.FileName).DirectoryName;
                    Properties.Settings.Default.Save();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }
            }
        }
    }
}