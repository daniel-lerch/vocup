using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vocup.Controls;
using Vocup.Forms;
using Vocup.IO;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup
{
    public partial class program_form : Form, IMainForm
    {
        // Deprecated, only for compatibility
        ListView originalListView;

        public program_form()
        {
            InitializeComponent();

            FileTreeView.RootPath = Settings.Default.VhfPath;
            originalListView = listView_vokabeln;
        }

        public VocabularyBook CurrentBook { get; private set; }
        public VocabularyBookController CurrentController { get; private set; }
        public StatisticsPanel StatisticsPanel => GroupStatistics;
        public bool UnsavedChanges => CurrentBook?.UnsavedChanges ?? false;

        public void VocabularyWordSelected(bool value)
        {
            BtnEditWord.Enabled = value;
            TsmiEditWord.Enabled = value;
            BtnDeleteWord.Enabled = value;
            TsmiDeleteWord.Enabled = value;
        }
        public void VocabularyBookLoaded(bool value)
        {
            GroupBook.Enabled = value;
            GroupWord.Enabled = value;
            TsmiAddWord.Enabled = value;
            BtnBookSettings.Enabled = value;
            TsmiBookOptions.Enabled = value;
            TsmiCloseBook.Enabled = value;
            TsmiSaveAs.Enabled = value;
        }
        public void VocabularyBookHasContent(bool value)
        {
            GroupSearch.Enabled = value;
            if (!value) TbSearchWord.Text = "";

            TsmiPrint.Enabled = value;
            TsbPrint.Enabled = value;

            TsmiExport.Enabled = value;
        }
        public void VocabularyBookPracticable(bool value)
        {
            BtnPractice.Enabled = value;
            TsmiPractice.Enabled = value;
        }
        public void VocabularyBookHasFilePath(bool value)
        {
            TsmiOpenInExplorer.Enabled = value;
        }
        public void VocabularyBookUnsavedChanges(bool value)
        {
            TsmiSave.Enabled = value;
            TsbSave.Enabled = value;
        }
        public void VocabularyBookName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                Text = Words.Vocup;
            else
                Text = $"{Words.Vocup} - {value}";
        }
        public void LoadBook(VocabularyBook book)
        {
            VocabularyBookController controller = new VocabularyBookController(book) { Parent = this };
            SplitContainer.Panel2.Controls.Remove(listView_vokabeln);
            SplitContainer.Panel2.Controls.Add(controller.ListView);
            controller.ListView.PerformLayout();

            CurrentBook = book;
            CurrentController = controller;
            listView_vokabeln = controller.ListView.Control;

            VocabularyBookLoaded(true);

            FileTreeView.SelectedPath = book.FilePath;

            Settings.Default.LastFile = book.FilePath;
            Settings.Default.Save();
        }
        public void UnloadBook(bool fullUnload)
        {
            using (VocabularyBookController controller = CurrentController)
            {
                CurrentBook = null;
                CurrentController = null;
                listView_vokabeln = originalListView;
                SplitContainer.Panel2.Controls.Remove(controller.ListView);
                SplitContainer.Panel2.Controls.Add(originalListView);
                originalListView.PerformLayout();
            }

            if (fullUnload)
            {
                VocabularyBookLoaded(false);
                VocabularyWordSelected(false);
                VocabularyBookHasContent(false);
                VocabularyBookPracticable(false);
                VocabularyBookHasFilePath(false);
                VocabularyBookUnsavedChanges(false);
                VocabularyBookName(null);

                // Accidentially overriding this value when the user already has chosen another file results in a stack overflow
                FileTreeView.SelectedPath = "";
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Update();
            Activate();

            // TODO: Check online for updates or messages
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            if (Settings.Default.StartScreen == (int)StartScreen.AboutBox)
            {
                using (var dialog = new AboutBox()) dialog.ShowDialog();

                Settings.Default.StartScreen = (int)StartScreen.LastFile;
                Settings.Default.Save();
            }
        }


        private void TsmiHelp_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "Resources", "help.chm");
            Help.ShowHelp(this, new Uri(new Uri("file://"), path).ToString());
        }


        private void TsmiAbout_Click(object sender, EventArgs e)
        {
            using (var dialog = new AboutBox()) dialog.ShowDialog();
        }

        private void TsbtnEvaluationInfo_Click(object sender, EventArgs e)
        {
            using (var dialog = new EvaluationInfoDialog()) dialog.ShowDialog();
        }
        private void TsmiEvaluationInfo_Click(object sender, EventArgs e)
        {
            using (var dialog = new EvaluationInfoDialog()) dialog.ShowDialog();
        }

        private void TsmiSettings_Click(object sender, EventArgs e)
        {
            string oldVhfPath = Settings.Default.VhfPath;

            using (SettingsDialog optionen = new SettingsDialog())
            {
                if (optionen.ShowDialog() == DialogResult.OK)
                {
                    // Renew practice state for Settings.MaxPracticeCount changes
                    CurrentBook?.Words.ForEach(x => x.RenewPracticeState());

                    if (CurrentController != null)
                        CurrentController.ListView.GridLines = Settings.Default.GridLines;

                    // Eventually refresh tree view root path
                    if (oldVhfPath != Settings.Default.VhfPath)
                        FileTreeView.RootPath = Settings.Default.VhfPath;

                    //Autosave
                    if (Settings.Default.AutoSave && UnsavedChanges)
                    {
                        savefile(false);
                    }
                }
            }
        }

        private void TsmiSpecialChar_Click(object sender, EventArgs e)
        {
            using (var dialog = new SpecialCharManage()) dialog.ShowDialog();
        }

        private void TsmiUpdate_Click(object sender, EventArgs e) { } // TODO: Check online for updates or messages


        private void FileTreeView_FileSelected(object sender, FileSelectedEventArgs e)
        {
            if (CurrentBook != null)
            {
                // Prevent the file tree view from loading the book again
                // if FileTreeView.SelectedPath was assigned in IMainForm.LoadBook(VocabularyBook)
                if (CurrentBook.FilePath == e.FullName)
                    return;
                if (UnsavedChanges && !vokabelheft_ask_to_save())
                    return;

                UnloadBook(false);
            }

            readfile(e.FullName);
        }

        private void TsbCreateBook_Click(object sender, EventArgs e) => create_new_vokabelheft();
        private void TsmiCreateBook_Click(object sender, EventArgs e) => create_new_vokabelheft();

        private void BtnBookSettings_Click(object sender, EventArgs e) => edit_vokabelheft_dialog();
        private void TsmiBookOptions_Click(object sender, EventArgs e) => edit_vokabelheft_dialog();

        private void TsbOpenBook_Click(object sender, EventArgs e) => open_file();
        private void TsmiOpenBook_Click(object sender, EventArgs e) => open_file();

        private void BtnAddWord_Click(object sender, EventArgs e) => add_vokabel();
        private void TsmiAddWord_Click(object sender, EventArgs e) => add_vokabel();

        private void BtnEditWord_Click(object sender, EventArgs e) => edit_vokabel_dialog();
        private void TsmiEditWord_Click(object sender, EventArgs e) => edit_vokabel_dialog();

        private void BtnDeleteWord_Click(object sender, EventArgs e) => vokabel_delete();
        private void TsmiDeleteWord_Click(object sender, EventArgs e) => vokabel_delete();

        private void TsmiSave_Click(object sender, EventArgs e) => savefile(false);
        private void TsmiSaveAs_Click(object sender, EventArgs e) => savefile(true);
        private void TsbSave_Click(object sender, EventArgs e) => savefile(false);

        private void BtnPractice_Click(object sender, EventArgs e) => vokabeln_üben();
        private void TsmiPractice_Click(object sender, EventArgs e) => vokabeln_üben();

        private void TsbPrint_Click(object sender, EventArgs e)
        {
            using (var dialog = new PrintWordSelection(CurrentBook)) dialog.ShowDialog();
        }
        private void TsmiPrint_Click(object sender, EventArgs e)
        {
            using (var dialog = new PrintWordSelection(CurrentBook)) dialog.ShowDialog();
        }


        private async void BtnSearchWord_Click(object sender, EventArgs e)
        {
            string search_text = TbSearchWord.Text.ToUpper();

            if (search_text == "EASTER EGG")
            {
                if (CurrentBook != null) // Save and close current book
                {
                    savefile(false);
                    UnloadBook(true);
                }

                readfile(Path.Combine(Application.StartupPath, "Resources", "easter_egg.vhf"));
                CurrentBook.FilePath = null;
                VocabularyBookName(Words.EasterEgg);

                TbSearchWord.Text = "";
            }
            else if (search_text == "TRANSPARENT") // EasterEgg 2
            {
                while (Opacity > 0d)
                {
                    Opacity -= Math.Pow(2, -7);
                    await Task.Delay(10);
                }

                await Task.Delay(500);

                while (Opacity < 1d)
                {
                    Opacity += Math.Pow(2, -6);
                    await Task.Delay(10);
                }

                TbSearchWord.Text = "";
            }
            else // ListView durchsuchen
            {
                bool found = false;

                foreach (VocabularyWord word in CurrentBook.Words)
                {
                    if (word.MotherTongue.ToUpper().Contains(search_text) ||
                        word.ForeignLang.ToUpper().Contains(search_text) ||
                        (word.ForeignLangSynonym?.ToUpper().Contains(search_text) ?? false))
                    {
                        ListViewItem item = CurrentController.GetController(word).ListViewItem;
                        item.Selected = true;
                        item.Focused = true;
                        item.EnsureVisible();
                        CurrentController.ListView.Focus();
                        found = true;
                    }
                }

                Color @default = Color.White;
                Color highlight = found ? Color.FromArgb(144, 238, 144) : Color.FromArgb(255, 192, 203);

                TbSearchWord.BackColor = highlight;

                await Task.Delay(500);

                TbSearchWord.BackColor = @default;
            }
        }

        private void TbSearchWord_TextChanged(object sender, EventArgs e)
        {
            if (TbSearchWord.Text != "")
            {
                BtnSearchWord.Enabled = true;
                AcceptButton = BtnSearchWord;
            }
            else // Disable button when no query text is available
            {
                BtnSearchWord.Enabled = false;
                AcceptButton = BtnAddWord;
            }
        }

        private void TsmiCloseBook_Click(object sender, EventArgs e)
        {
            if (UnsavedChanges && !vokabelheft_ask_to_save())
                return;

            UnloadBook(true);
        }

        //-----




        //***Methoden***


        // Datei einlesen

        public void readfile(string path)
        {
            VocabularyBook book = new VocabularyBook();

            if (VocabularyFile.ReadVhfFile(path, book))
            {
                VocabularyFile.ReadVhrFile(book);
                book.Notify();
                LoadBook(book);
            }
        }

        private bool vokabelheft_ask_to_save()
        {
            DialogResult result = MessageBox.Show(Messages.GeneralSaveChanges, Messages.GeneralSaveChangesT, MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                return savefile(false); // Save file and return true which means to continue with the next action.
            }
            else if (result == DialogResult.No)
            {
                return true; // Do not save file and return true.
            }
            else
            {
                return false; // Return false to indicate the users choice to stay at the current screen.
            }
        }

        private bool savefile(bool saveAsNewFile)
        {
            //Datei-Speichern-unter Dialogfeld öffnen
            if (string.IsNullOrWhiteSpace(CurrentBook.FilePath) ||
                string.IsNullOrWhiteSpace(CurrentBook.VhrCode) ||
                saveAsNewFile)
            {
                SaveFileDialog save = new SaveFileDialog
                {
                    Title = Words.SaveVocabularyBook,
                    FileName = CurrentBook.MotherTongue + " - " + CurrentBook.ForeignLang,
                    InitialDirectory = Settings.Default.VhfPath,
                    Filter = Words.VocupVocabularyBookFile + " (*.vhf)|*.vhf"
                };

                if (save.ShowDialog() == DialogResult.OK)
                {
                    CurrentBook.FilePath = save.FileName;
                    CurrentBook.GenerateVhrCode();
                }
                else
                {
                    return false;
                }
            }

            Cursor.Current = Cursors.WaitCursor;

            if (VocabularyFile.WriteVhfFile(CurrentBook.FilePath, CurrentBook) &&
                VocabularyFile.WriteVhrFile(CurrentBook))
            {
                CurrentBook.UnsavedChanges = false;

                Settings.Default.LastFile = CurrentBook.FilePath;
                Settings.Default.Save();

                Cursor.Current = Cursors.Default;
                return true;
            }
            else
            {
                Cursor.Current = Cursors.Default;
                return false;
            }
        }

        // Öffnen Dialog

        private void open_file()
        {
            OpenFileDialog open = new OpenFileDialog
            {
                Title = Words.OpenVocabularyBook,
                InitialDirectory = Settings.Default.VhfPath,
                Filter = Words.VocupVocabularyBookFile + " (*.vhf)|*.vhf"
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                if (CurrentBook != null)
                {
                    if (UnsavedChanges && !vokabelheft_ask_to_save())
                        return;

                    UnloadBook(false);
                }

                readfile(open.FileName);
            }
        }

        //neues Vokabelheft erstellen

        private void create_new_vokabelheft()
        {
            using (VocabularyBookSettings dialog = new VocabularyBookSettings(out VocabularyBook book))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (CurrentBook != null)
                    {
                        if (UnsavedChanges && !vokabelheft_ask_to_save())
                            return;

                        UnloadBook(false);
                    }

                    // VocabularyBookSettings enables notification on creation
                    LoadBook(book);

                    BtnAddWord.Focus();
                }
            }
        }

        //Vokabel hinzufügen

        private void add_vokabel()
        {
            using (var dialog = new AddWordDialog(CurrentBook)) dialog.ShowDialog();
            BtnAddWord.Focus();
        }

        //Vokabel bearbeiten

        public void edit_vokabel_dialog()
        {
            VocabularyWord selected = (VocabularyWord)CurrentController.ListView.SelectedItem.Tag;
            using (var dialog = new EditWordDialog(CurrentBook, selected)) dialog.ShowDialog();
            CurrentController.ListView.SelectedItem.EnsureVisible();
            BtnAddWord.Focus();
        }

        //Vokabel löschen

        private void vokabel_delete()
        {
            VocabularyWord selected = (VocabularyWord)CurrentController.ListView.SelectedItem.Tag;
            CurrentBook.Words.Remove(selected);
            BtnAddWord.Focus();
        }

        //Vokabelheft Optionen

        private void edit_vokabelheft_dialog()
        {
            using (var dialog = new VocabularyBookSettings(CurrentBook) { Owner = this }) dialog.ShowDialog();
            BtnAddWord.Focus();
        }


        //Vokabeln Üben

        private void vokabeln_üben()
        {
            using (PracticeCountDialog countDialog = new PracticeCountDialog(CurrentBook))
            {
                if (countDialog.ShowDialog() != DialogResult.OK)
                    return;

                List<VocabularyWordPractice> practiceList = countDialog.PracticeList;

                int anzahl = practiceList.Count;

                CurrentController.ListView.Visible = false;

                using (var dialog = new PracticeDialog(CurrentBook, practiceList) { Owner = this }) dialog.ShowDialog();

                if (Settings.Default.PracticeShowResultList)
                {
                    using (var dialog = new PracticeResultList(CurrentBook, practiceList)) dialog.ShowDialog();
                }

                CurrentController.ListView.Visible = true;

                BtnAddWord.Focus();
            }
        }

        //Vokabelhefte zusammenführen

        private void TsmiMerge_Click(object sender, EventArgs e)
        {
            using (var dialog = new MergeFiles()) dialog.ShowDialog();
        }

        //Datensicherung erstellen

        private void TsmiBackupCreate_Click(object sender, EventArgs e)
        {
            using (var dialog = new CreateBackup()) dialog.ShowDialog();
        }

        //Datensicherung wiederherstellen

        public void restore_backup(string file_path)
        {
            //Neue Form vorbereiten
            RestoreBackup restore = new RestoreBackup();

            //Falls ein Backup geöffnet wurde
            if (file_path != "")
            {
                restore.TbFilePath.Text = file_path;
                restore.BtnFilePath.Enabled = false;
                restore.path_backup = file_path;
            }


            //Fragen, ob das Vokabelheft gespeichert werden soll

            bool result = true;

            if (UnsavedChanges)
            {
                result = vokabelheft_ask_to_save();
            }

            if (result == true)
            {
                //Falls auf wiederherstellen geklickt wurde
                if (restore.ShowDialog() == DialogResult.OK)
                {
                    //Variablen für Fehlermeldungen
                    int error_vhf = 0;
                    int error_vhr = 0;
                    int error_chars = 0;
                    bool error = false;

                    List<string> error_vhf_name = new List<string>();
                    List<string> error_chars_name = new List<string>();

                    try
                    {
                        //Cursor auf Warten setzen
                        Cursor.Current = Cursors.WaitCursor;
                        Update();

                        //Schliesst das geöffnete Vokabelheft
                        //close_vokabelheft();

                        //Backup-Datei vorbereiten

                        ZipFile backup_file = new ZipFile(restore.path_backup);

                        //Vokabelhefte wiederherstellen
                        if (restore.vhf_restore.Count > 0)
                        {
                            for (int i = 0; i < restore.vhf_restore.Count; i++)
                            {
                                try
                                {

                                    string[] temp = restore.vhf_restore[i];

                                    ZipEntry entry = backup_file.GetEntry(@"vhf\" + temp[0] + ".vhf");

                                    byte[] buffer = new byte[entry.Size + 4096];

                                    FileInfo info = new FileInfo(temp[1]);

                                    if (Directory.Exists(info.DirectoryName) == false)
                                    {
                                        Directory.CreateDirectory(info.DirectoryName);
                                    }


                                    FileStream writer = new FileStream(temp[1], FileMode.Create);

                                    StreamUtils.Copy(backup_file.GetInputStream(entry), writer, buffer);

                                    writer.Close();
                                }
                                catch
                                {
                                    error_vhf++;

                                    string[] temp = restore.vhf_restore[i];
                                    error_vhf_name.Add(temp[1]);
                                }
                            }
                        }

                        //Ergebnisse wiederherstellen

                        if (restore.vhr_restore.Count > 0)
                        {
                            for (int i = 0; i < restore.vhr_restore.Count; i++)
                            {
                                try
                                {
                                    ZipEntry entry = backup_file.GetEntry(@"vhr\" + restore.vhr_restore[i]);

                                    byte[] buffer = new byte[entry.Size + 4096];


                                    FileStream writer = new FileStream(Properties.Settings.Default.VhrPath + "\\" + restore.vhr_restore[i], FileMode.Create);

                                    StreamUtils.Copy(backup_file.GetInputStream(entry), writer, buffer);

                                    writer.Close();
                                }

                                catch
                                {
                                    error_vhr++;
                                }
                            }
                        }

                        //Sonderzeichentabellen sichern

                        if (restore.chars_restore.Count > 0)
                        {

                            for (int i = 0; i < restore.chars_restore.Count; i++)
                            {
                                try
                                {
                                    ZipEntry entry = backup_file.GetEntry(@"chars\" + restore.chars_restore[i]);

                                    byte[] buffer = new byte[entry.Size + 4096];

                                    if (Directory.Exists(Properties.Settings.Default.VhrPath + "\\specialchar\\") == false)
                                    {
                                        Directory.CreateDirectory(Properties.Settings.Default.VhrPath + "\\specialchar\\");
                                    }

                                    FileStream writer = new FileStream(Properties.Settings.Default.VhrPath + "\\specialchar\\" + restore.chars_restore[i], FileMode.Create);

                                    StreamUtils.Copy(backup_file.GetInputStream(entry), writer, buffer);

                                    writer.Close();
                                }

                                catch
                                {
                                    error_chars++;

                                    error_chars_name.Add(restore.chars_restore[i]);
                                }
                            }
                        }

                        backup_file.Close();

                        Cursor.Current = Cursors.Default;
                        Update();
                    }
                    catch
                    {
                        error = true;

                        Cursor.Current = Cursors.Default;

                        //fehlermeldung anzeigen
                        MessageBox.Show(Properties.language.messagebox_backup_restore_error,
                               Properties.language.error,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                    }

                    //Falls nötig Fehlermeldungen anzeigen

                    if (error_vhf > 0)
                    {
                        string messange = Properties.language.messagebox_backup_restore_error_vhf + Environment.NewLine;

                        for (int i = 0; i < error_vhf_name.Count; i++)
                        {
                            messange = messange + Environment.NewLine + error_vhf_name[i];
                        }

                        //Fehlermeldung anzeigen
                        MessageBox.Show(messange,
                               Properties.language.error,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                    }
                    if (error_vhr > 0)
                    {
                        //Fehlermeldung anzeigen
                        MessageBox.Show(error_vhr.ToString() + " " + Properties.language.messagebox_backup_restore_error_vhr,
                               Properties.language.error,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                    }
                    if (error_chars > 0)
                    {
                        string messange = Properties.language.messagebox_backup_restore_error_chars + Environment.NewLine;

                        for (int i = 0; i < error_chars_name.Count; i++)
                        {
                            messange = messange + Environment.NewLine + error_chars_name[i];
                        }

                        //Fehlermeldung anzeigen
                        MessageBox.Show(messange,
                               Properties.language.error,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                    }

                    //Dialog anzeigen, dass der Prozess erfolgreich war

                    if (error == false && error_vhf == 0 && error_vhr == 0 && error_chars == 0)
                    {
                        MessageBox.Show(Properties.language.messagebox_backup_restore_success,
                                  AppInfo.Name,
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void TsmiBackupRestore_Click(object sender, EventArgs e)
        {
            restore_backup("");
        }


        private void TsmiOpenInExplorer_Click(object sender, EventArgs e)
        {
            if (CurrentBook.UnsavedChanges)
            {
                DialogResult dialogResult = MessageBox.Show(Messages.OpenInExplorerSave,
                    Messages.OpenInExplorerSaveT, MessageBoxButtons.YesNoCancel);

                if (dialogResult == DialogResult.Yes)
                {
                    savefile(false);
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            FileInfo info = new FileInfo(CurrentBook.FilePath);
            if (info.Exists)
            {
                try
                {
                    string explorer = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "explorer.exe");
                    Process.Start(explorer, $"/select,\"{info.FullName}\"");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Messages.OpenInExplorerError, ex), Messages.OpenInExplorerErrorT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(Messages.OpenInExplorerNotFound, Messages.OpenInExplorerNotFoundT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TsmiImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Title = Words.Import,
                Filter = "CSV (*.csv)|*.csv"
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if (CurrentBook != null)
                {
                    VocabularyFile.ImportCsvFile(openDialog.FileName, CurrentBook, false);
                }
                else
                {
                    VocabularyBook book = new VocabularyBook();
                    if (VocabularyFile.ImportCsvFile(openDialog.FileName, book, true))
                        LoadBook(book);
                }
            }

            openDialog.Dispose();
        }

        private void TsmiExport_Click(object sender, EventArgs e)
        {
            if (UnsavedChanges)
            {
                DialogResult dialogResult = MessageBox.Show(Messages.CsvExportSave,
                    Messages.CsvExportSaveT, MessageBoxButtons.YesNoCancel);

                if (dialogResult == DialogResult.Yes)
                {
                    savefile(false);
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Title = Words.Export,
                Filter = "CSV (*.csv)|*.csv"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                VocabularyFile.ExportCsvFile(saveDialog.FileName, CurrentBook);
            }

            saveDialog.Dispose();
        }

        //-----

        //Beenden

        private void TsmiExitAppliaction_Click(object sender, EventArgs e) => Close();

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UnsavedChanges)
            {
                e.Cancel = !vokabelheft_ask_to_save();
            }
        }

        //-----

    }
}