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

        //Liste der Vokabeln die ausgedruckt werden soll
        int[] vokabelliste;

        //von Mutter zu Fremdsprache oder umgekehrt drucken
        bool if_own_to_foreign;

        //Anzahl Vokabeln beim Drucken
        int anz_vok;

        //Anzahl zu druckende Seiten
        int anzahl_seiten;

        //Aktuelle zu druckende Seite
        int aktuelle_seite;

        //Vorder- oder Rückseite bei den Kärtchen
        bool if_foreside;

        //Papiereinzug
        bool is_front;

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
            GroupSearch.Enabled = value;
            TsmiAddWord.Enabled = value;
            BtnBookSettings.Enabled = value;
            TsmiBookOptions.Enabled = value;
            TsmiCloseBook.Enabled = value;
            TsmiSaveAs.Enabled = value;
            TsmiOpenInExplorer.Enabled = value;
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

        private void TsbPrint_Click(object sender, EventArgs e) => print_file();
        private void TsmiPrint_Click(object sender, EventArgs e) => print_file();


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
                        word.ForeignLangSynonym.ToUpper().Contains(search_text))
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

        //Vokabelheft drucken

        private void print_file()
        {
            //Dialog starten der abfrägt, was gedruckt werden soll
            PrintWordSelection choose_vocables = new PrintWordSelection();

            //Vokabeln in ListBox eintragen

            choose_vocables.vocable_state = new int[listView_vokabeln.Items.Count];

            for (int i = 0; i < listView_vokabeln.Items.Count; i++)
            {
                choose_vocables.ListBox.Items.Add(listView_vokabeln.Items[i].SubItems[1].Text + " - " + listView_vokabeln.Items[i].SubItems[2].Text, true);

                //Status ins Array eintragen
                choose_vocables.vocable_state[i] = ((VocabularyWord)listView_vokabeln.Items[i].Tag).PracticeStateNumber;
            }

            //Checkboxen falls nötig ausblenden

            choose_vocables.CbUnpracticed.Enabled = StatisticsPanel.Unpracticed > 0;
            choose_vocables.CbWronglyPracticed.Enabled = StatisticsPanel.WronglyPracticed > 0;
            choose_vocables.CbCorrectlyPracticed.Enabled = StatisticsPanel.CorrectlyPracticed > 0;
            choose_vocables.CbFullyPracticed.Enabled = StatisticsPanel.FullyPracticed > 0;

            //Dialog anzeigen

            DialogResult choose_vocables_result = choose_vocables.ShowDialog();

            //Vokabeln in Vokabelliste speichern

            if (choose_vocables_result == DialogResult.OK)
            {
                int status = 0;

                //Feststellen, wie viele Vokabeln markiert wurden

                int count = 0;

                for (int i = 0; i < choose_vocables.ListBox.Items.Count; i++)
                {
                    if (choose_vocables.ListBox.GetItemCheckState(i) == CheckState.Checked)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    print_file();
                }
                else
                {
                    int[] list = new int[count]; //Array erstellen

                    for (int i = 0; i < choose_vocables.ListBox.Items.Count; i++)
                    {
                        if (choose_vocables.ListBox.GetItemCheckState(i) == CheckState.Checked)
                        {
                            list[status] = i;
                            status++;
                        }
                    }

                    vokabelliste = list; //überschreibt die Vokabelliste
                    anz_vok = count; //Anzahl Vokabeln schreiben

                    //Liste
                    if (choose_vocables.RbList.Checked == true)
                    {
                        //Feststellen, ob von Mutter- zu Fremdsprache, oder umgekehrt gedruckt werden soll
                        if (choose_vocables.RbAskForForeignLang.Checked == true)
                        {
                            if_own_to_foreign = true;
                        }
                        else
                        {
                            if_own_to_foreign = false;
                        }

                        //Den Druckdialog starten
                        PrintDialog dialog = new PrintDialog();

                        dialog.AllowCurrentPage = false;
                        dialog.AllowSomePages = false;
                        dialog.UseEXDialog = true;

                        DialogResult result = dialog.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            printList.PrinterSettings = dialog.PrinterSettings;
                            printList.DocumentName = "Vokabelliste";
                            printList.Print();
                        }
                    }
                    else //Kärtchen
                    {
                        //Anzahl Seiten ermitteln

                        double anz_vokD = anz_vok;

                        double i = Math.Ceiling(anz_vokD / 16);

                        anzahl_seiten = (int)i;

                        //---

                        PrintCardsDialog dialog = new PrintCardsDialog();

                        //Anzahl seiten
                        dialog.LbPaperCount.Text = anzahl_seiten.ToString();

                        //Dialog starten
                        DialogResult result = dialog.ShowDialog();

                        if (result == DialogResult.Ignore) // Falls Die Vorderseite gedruckt werden soll
                        {
                            if_foreside = true;

                            //Ermittle Papiereinzug
                            is_front = dialog.RbFrontSide.Checked;

                            dialog.Close();

                            //Drucken

                            PrintDialog print_dialog = new PrintDialog();
                            print_dialog.AllowCurrentPage = false;
                            print_dialog.AllowSomePages = false;
                            print_dialog.UseEXDialog = true;

                            DialogResult print_result = print_dialog.ShowDialog();

                            if (print_result == DialogResult.OK)
                            {

                                printCards.PrinterSettings = print_dialog.PrinterSettings;
                                printCards.DocumentName = "Vokabel Kärtchen";
                                printCards.Print();


                                //Dialog nochmals starten
                                //anderer Button deaktivieren

                                PrintCardsDialog dialog2 = new PrintCardsDialog();

                                dialog2.BtnPrintForeside.Enabled = false;
                                dialog2.BtnPrintBackside.Enabled = true;
                                dialog2.LbPaperCount.Text = anzahl_seiten.ToString();

                                DialogResult result2 = dialog2.ShowDialog();

                                if (result2 == DialogResult.OK)
                                {
                                    if_foreside = false;

                                    //Papiereinzug
                                    is_front = dialog2.RbFrontSide.Checked;

                                    //Drucken
                                    printCards.Print();
                                }
                            }
                        }
                        else if (result == DialogResult.OK) // Falls die Rückseite gedruckt werden soll
                        {

                            if_foreside = false;

                            //Ermittle Papiereinzug

                            is_front = dialog.RbFrontSide.Checked;

                            dialog.Close();

                            //Drucken

                            PrintDialog print_dialog = new PrintDialog
                            {
                                AllowCurrentPage = false,
                                AllowSomePages = false
                            };

                            DialogResult print_result = print_dialog.ShowDialog();

                            if (print_result == DialogResult.OK)
                            {
                                printCards.PrinterSettings = print_dialog.PrinterSettings;
                                printCards.DocumentName = "Vokabel Kärtchen";
                                printCards.Print();

                                //Dialog nochmals starten
                                //anderer Button deaktivieren

                                PrintCardsDialog dialog3 = new PrintCardsDialog();

                                dialog3.BtnPrintForeside.Enabled = true;
                                dialog3.BtnPrintBackside.Enabled = false;
                                dialog3.LbPaperCount.Text = anzahl_seiten.ToString();

                                DialogResult result3 = dialog3.ShowDialog();

                                if (result3 == DialogResult.Ignore)
                                {
                                    if_foreside = true;

                                    //Papiereinzug
                                    is_front = dialog3.RbFrontSide.Checked;

                                    //Drucken
                                    printCards.Print();
                                }
                            }
                        }
                    }
                }
            }
        }
        //Liste drucken
        private void printDocument_list_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Display;

            //Schrift
            Font font = new Font("Arial", 10);
            Font font_bold = new Font("Arial", 10, FontStyle.Bold);
            Font font_vocable = new Font("Arial", 8);

            //stift
            Pen pen = new Pen(Brushes.Black, 1);

            //Ein zentriertes Format für Schrift erstellen
            StringFormat format_center = new StringFormat();
            format_center.Alignment = StringAlignment.Center;

            //Rechtsbündig
            StringFormat format_near = new StringFormat();
            format_near.Alignment = StringAlignment.Near;

            //Ränder
            int left = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Left, 1, MidpointRounding.AwayFromZero));
            int right = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Right, 1, MidpointRounding.AwayFromZero));
            int top = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Top, 1, MidpointRounding.AwayFromZero));
            int bottom = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Bottom, 1, MidpointRounding.AwayFromZero));


            //Seitenzahl ganz oben
            g.DrawString(Words.Site + " " + aktuelle_seite.ToString(), font, Brushes.Black, new Point(414 - left, 25 - left), format_center);


            if (aktuelle_seite == 1 && !string.IsNullOrWhiteSpace(CurrentBook.FilePath))
            {
                //Dateiname ermitteln
                string file_name = Path.GetFileNameWithoutExtension(CurrentBook.FilePath);

                if (if_own_to_foreign == true)
                {
                    g.DrawString(file_name + ":  " + listView_vokabeln.Columns[1].Text + " - " + listView_vokabeln.Columns[2].Text, font_bold, Brushes.Black, new Point(414 - left, 40 - top), format_center);
                }
                else
                {
                    g.DrawString(file_name + ":  " + listView_vokabeln.Columns[2].Text + " - " + listView_vokabeln.Columns[1].Text, font_bold, Brushes.Black, new Point(414 - left, 40 - top), format_center);
                }
            }


            //Linien und Wörter einfügen

            int noch_nicht_gedruckt = anz_vok - (aktuelle_seite - 1) * 42;
            int vok_beginnen = (aktuelle_seite - 1) * 42 + 1;

            //Falls volle Seiten gedruckt werden können
            if (noch_nicht_gedruckt >= 42)
            {
                //Oberste Linie
                g.DrawLine(pen, 60 - left, 65 - top, 767 - left, 65 - top);
                //Mittellinie
                g.DrawLine(pen, 413 - left, 65 - top, 413 - left, 1115 - top);
                //Seitenlinien
                g.DrawLine(pen, 60 - left, 65 - top, 60 - left, 1115 - top);
                g.DrawLine(pen, 767 - left, 65 - top, 767 - left, 1115 - top);
                //unterste Linie
                //g.DrawLine(pen, 60 - left, 1095 - top, 767 - left, 1120 - top);

                for (int i = 0; i < 42; i++)
                {

                    SizeF size_own = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable);
                    SizeF size_foreign = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable);

                    //Falls der Text zu gross ist
                    if (size_own.Width > 413 - 62 - left)
                    {
                        bool is_good;
                        int font_size = 8;
                        do
                        {
                            font_size--;
                            Font font_new = new Font("Arial", font_size);

                            SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                            if (string_size.Width > 413 - 62 - left && font_size > 1)
                            {
                                is_good = false;
                            }
                            else
                            {
                                is_good = true;

                                //kleinerer Text schreiben
                                if (if_own_to_foreign == true)
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                                }
                                else
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                                }
                            }

                        } while (is_good == false);

                    }
                    else //Falls Text nicht zu gross
                    {
                        if (if_own_to_foreign == true)
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                        }
                        else
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                        }
                    }
                    //Falls Text zu gross || Synonym
                    if (size_foreign.Width > 413 - 62 - left)
                    {
                        bool is_good;
                        int font_size = 8;
                        do
                        {
                            font_size--;
                            Font font_new = new Font("Arial", font_size);

                            SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new);

                            if (string_size.Width > 413 - 62 - left && font_size > 1)
                            {
                                is_good = false;
                            }
                            else
                            {
                                is_good = true;

                                //kleinerer Text schreiben
                                if (if_own_to_foreign == true)
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                                }
                                else
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);

                                }
                            }

                        } while (is_good == false);

                    }
                    else //Falls Text nicht zu gross
                    {
                        if (if_own_to_foreign == true)
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                        }
                        else
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                        }
                    }


                    //Untere Linie zeichnen
                    g.DrawLine(pen, 60 - left, 90 + i * 25 - top, 767 - left, 90 + i * 25 - top);
                }
            }
            else //Falls letzte Seite, und nicht voll
            {
                //Oberste Linie
                g.DrawLine(pen, 60 - left, 65 - top, 767 - left, 65 - top);
                //Mittellinie
                g.DrawLine(pen, 413 - left, 65 - top, 413 - left, 65 + 25 * noch_nicht_gedruckt - top);
                //Seitenlinien
                g.DrawLine(pen, 60 - left, 65 - top, 60 - left, 65 + 25 * noch_nicht_gedruckt - top);
                g.DrawLine(pen, 767 - left, 65 - top, 767 - left, 65 + 25 * noch_nicht_gedruckt - top);


                for (int i = 0; i < noch_nicht_gedruckt; i++)
                {

                    SizeF size_own = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable);
                    SizeF size_foreign = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable);

                    //Falls der Text zu gross ist
                    if (size_own.Width > 413 - 62 - left)
                    {
                        bool is_good;
                        int font_size = 8;
                        do
                        {
                            font_size--;
                            Font font_new = new Font("Arial", font_size);

                            SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                            if (string_size.Width > 413 - 62 - left && font_size > 1)
                            {
                                is_good = false;
                            }
                            else
                            {
                                is_good = true;

                                //kleinerer Text schreiben
                                if (if_own_to_foreign == true)
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                                }
                                else
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                                }
                            }

                        } while (is_good == false);

                    }
                    //Falls Text nicht zu gross
                    else
                    {
                        if (if_own_to_foreign == true)
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                        }
                        else
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                        }
                    }
                    //Falls Text zu gross || Synonym
                    if (size_foreign.Width > 413 - 62 - left)
                    {
                        bool is_good;
                        int font_size = 8;
                        do
                        {
                            font_size--;
                            Font font_new = new Font("Arial", font_size);

                            SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new);

                            if (string_size.Width > 413 - 62 - left && font_size > 1)
                            {
                                is_good = false;
                            }
                            else
                            {
                                is_good = true;

                                //kleinerer Text schreiben
                                if (if_own_to_foreign == true)
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                                }
                                else
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                                }
                            }

                        } while (is_good == false);

                    }
                    //Falls Text nicht zu gross
                    else
                    {
                        if (if_own_to_foreign == true)
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                        }
                        else
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                        }
                    }

                    //Untere Linie zeichnen
                    g.DrawLine(pen, 60 - left, 90 + i * 25 - top, 767 - left, 90 + i * 25 - top);
                }
            }


            //Schauen, ob noch mehr Seiten gedruckt werden müssen

            if (aktuelle_seite != anzahl_seiten)
            {
                e.HasMorePages = true;
                aktuelle_seite++;
            }
            else
            {
                e.HasMorePages = false;
            }

        }
        private void printDocument_list_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Anzahl Seiten festlegen
            anzahl_seiten = (int)Math.Ceiling(anz_vok / 42d);
            aktuelle_seite = 1;
        }

        //Kärtchen drucken
        private void printDocument_cards_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Display;
            //1/100 zoll * 0.254 = mm
            //1169|827 (Daten A4-Seite)
            //Seitenränder abfragen

            int left = (int)Math.Round(e.PageSettings.PrintableArea.Left, 1, MidpointRounding.AwayFromZero);
            int right = (int)Math.Round(e.PageSettings.PrintableArea.Right, 1, MidpointRounding.AwayFromZero);
            int top = (int)Math.Round(e.PageSettings.PrintableArea.Top, 1, MidpointRounding.AwayFromZero);
            int bottom = (int)Math.Round(e.PageSettings.PrintableArea.Bottom, 1, MidpointRounding.AwayFromZero);

            //Stift

            Pen pen = new Pen(Color.Black, 1);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            Font font = new Font("Arial", 12);

            //Vorderseite
            if (if_foreside == true)
            {
                //Linien zeichnen

                //Vertikal
                g.DrawLine(pen, 207 - left, 0, 207 - left, 1180);
                g.DrawLine(pen, 413 - left, 0, 413 - left, 1180);
                g.DrawLine(pen, 620 - left, 0, 620 - left, 1180);

                //Horizontal
                g.DrawLine(pen, 0, 292 - top, 866, 292 - top);
                g.DrawLine(pen, 0, 585 - top, 866, 585 - top);
                g.DrawLine(pen, 0, 877 - top, 866, 877 - top);

                //Seite rotieren ||X-Koordinaten negativ, Y-Koordinaten positiv
                g.RotateTransform(-90f);

                //Linien und Wörter einfügen

                int noch_nicht_gedruckt = anz_vok - (aktuelle_seite - 1) * 16;
                int vok_beginnen = (aktuelle_seite - 1) * 16 + 1;

                //Falls noch mehr Seiten gedruckt werden müssen
                if (noch_nicht_gedruckt >= 16)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        //Koordinaten abfragen
                        int[] coordinates = get_coordinates(i + 1);

                        //Grösse des Textes abfragen

                        SizeF size_string = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font);

                        int height = Convert.ToInt32(size_string.Height / 2);

                        //Vokabel schreiben
                        //Schriftgrösse anpassen

                        //Falls Text zu gross, string auf mehrere Zeilen aufteilen falls möglich
                        if (size_string.Width > 292)
                        {
                            bool is_good = false;
                            int font_size = 12;

                            if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text.Trim().Contains(" ") == true)
                            {
                                //Falls der String leerschläge enthält

                                string[] splitter = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text.Trim().Split(' ');

                                do
                                {
                                    Font font_new = new Font("Arial", font_size);

                                    for (int y = 1; y < splitter.Length; y++)
                                    {
                                        string part1 = "";
                                        string part2 = "";

                                        for (int x = 1; x <= splitter.Length - y; x++)
                                        {
                                            part1 = part1 + " " + splitter[x - 1];

                                            if (x == splitter.Length - y)
                                            {
                                                for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                {
                                                    part2 = part2 + " " + splitter[z];
                                                }
                                            }
                                        }

                                        SizeF size_part1 = g.MeasureString(part1, font_new);
                                        SizeF size_part2 = g.MeasureString(part2, font_new);

                                        if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                        {
                                            is_good = true;

                                            //zwei Zeilen schreiben

                                            g.DrawString(part1, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height - 20), format);
                                            g.DrawString(part2, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height + 20), format);

                                            break;
                                        }
                                    }

                                    if (is_good == false)
                                    {
                                        font_size--;
                                    }

                                } while (is_good == false);
                            }
                            else
                            {
                                do
                                {
                                    font_size--;
                                    Font font_new = new Font("Arial", font_size);

                                    SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                                    if (string_size.Width > 292 && font_size > 1)
                                    {
                                        is_good = false;
                                    }
                                    else
                                    {
                                        is_good = true;

                                        //kleinerer Text schreiben
                                        g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                                    }

                                } while (is_good == false);
                            }
                        }
                        else //Falls Text nicht zu gross
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                        }
                    }
                }
                else //Falls dies die letzte Seite ist
                {
                    for (int i = 0; i < noch_nicht_gedruckt; i++)
                    {
                        //Koordinaten abfragen
                        int[] coordinates = get_coordinates(i + 1);

                        //Grösse des Textes abfragen

                        SizeF size_string = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font);

                        int height = Convert.ToInt32(size_string.Height / 2);

                        //Vokabel schreiben
                        //Schriftgrösse anpassen

                        //Falls Text zu gross, string auf mehrere Zeilen aufteilen falls möglich
                        if (size_string.Width > 292)
                        {

                            bool is_good = false;
                            int font_size = 12;

                            if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text.Trim().Contains(" ") == true)
                            {
                                //Falls der String leerschläge enthält

                                string[] splitter = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text.Trim().Split(' ');

                                do
                                {
                                    Font font_new = new Font("Arial", font_size);

                                    for (int y = 1; y < splitter.Length; y++)
                                    {
                                        string part1 = "";
                                        string part2 = "";

                                        for (int x = 1; x <= splitter.Length - y; x++)
                                        {
                                            part1 = part1 + " " + splitter[x - 1];

                                            if (x == splitter.Length - y)
                                            {
                                                for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                {
                                                    part2 = part2 + " " + splitter[z];
                                                }
                                            }
                                        }


                                        SizeF size_part1 = g.MeasureString(part1, font_new);
                                        SizeF size_part2 = g.MeasureString(part2, font_new);

                                        if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                        {
                                            is_good = true;

                                            //zwei Zeilen schreiben

                                            g.DrawString(part1, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height - 20), format);
                                            g.DrawString(part2, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height + 20), format);

                                            break;
                                        }
                                    }

                                    if (is_good == false)
                                    {
                                        font_size--;
                                    }

                                } while (is_good == false);
                            }
                            else // Falls keine Leerzeichen vorhanden sind
                            {
                                do
                                {
                                    font_size--;
                                    Font font_new = new Font("Arial", font_size);

                                    SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                                    if (string_size.Width > 292 && font_size > 1)
                                    {
                                        is_good = false;
                                    }
                                    else
                                    {
                                        is_good = true;

                                        //kleinerer Text schreiben
                                        g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                                    }

                                } while (is_good == false);
                            }
                        }
                        else
                        {
                            //Falls Text nicht zu gross
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                        }
                    }

                    //nicht benötigte Linien entfernen
                    g.RotateTransform(+90);

                    if (noch_nicht_gedruckt <= 4)
                    {
                        g.FillRectangle(Brushes.White, new Rectangle(0, 292 - top + 1, 866, 1180));
                    }
                    else if (noch_nicht_gedruckt > 4 && noch_nicht_gedruckt <= 8)
                    {
                        g.FillRectangle(Brushes.White, new Rectangle(0, 585 - top + 1, 866, 1180));
                    }
                    else if (noch_nicht_gedruckt > 8 && noch_nicht_gedruckt <= 12)
                    {
                        g.FillRectangle(Brushes.White, new Rectangle(0, 877 - top - top + 1, 866, 1180));
                    }

                    //Vertikale Linien


                    //Vertikal
                    //g.DrawLine(pen, 207 - left, 0, 207 - left, 1180);
                    //g.DrawLine(pen, 413 - left, 0, 413 - left, 1180);
                    //g.DrawLine(pen, 620 - left, 0, 620 - left, 1180);

                    ////Horizontal
                    //g.DrawLine(pen, 0, 292 - top, 866, 292 - top);
                    //g.DrawLine(pen, 0, 585 - top, 866, 585 - top);
                    //g.DrawLine(pen, 0, 877 - top, 866, 877 - top);

                    Rectangle rect = new Rectangle();

                    if (noch_nicht_gedruckt < 4)
                    {
                        rect.Y = 0;
                        rect.Height = 1180;
                    }
                    else if (noch_nicht_gedruckt > 4 && noch_nicht_gedruckt < 8)
                    {
                        rect.Y = 292 - top + 1;
                        rect.Height = 888;
                    }
                    else if (noch_nicht_gedruckt > 8 & noch_nicht_gedruckt < 12)
                    {
                        rect.Y = 585 - top + 1;
                        rect.Height = 593;
                    }
                    else if (noch_nicht_gedruckt > 12 & noch_nicht_gedruckt < 16)
                    {
                        rect.Y = 877 - top + 1;
                        rect.Height = 298;
                    }

                    if (noch_nicht_gedruckt == 1 || noch_nicht_gedruckt == 5 || noch_nicht_gedruckt == 9 || noch_nicht_gedruckt == 13)
                    {
                        rect.X = 207 - left + 1;
                        rect.Width = 650;

                        g.FillRectangle(Brushes.White, rect);
                    }
                    else if (noch_nicht_gedruckt == 2 || noch_nicht_gedruckt == 6 || noch_nicht_gedruckt == 10 || noch_nicht_gedruckt == 14)
                    {
                        rect.X = 413 - left + 1;
                        rect.Width = 435;

                        g.FillRectangle(Brushes.White, rect);
                    }
                    else if (noch_nicht_gedruckt == 3 || noch_nicht_gedruckt == 7 || noch_nicht_gedruckt == 11)
                    {
                        rect.X = 620 - left + 1;
                        rect.Width = 218;

                        g.FillRectangle(Brushes.White, rect);
                    }
                    g.RotateTransform(-90);
                }

                //Pfeil zeichnen
                if (aktuelle_seite == anzahl_seiten || aktuelle_seite == 1)
                {
                    //rotieren

                    g.RotateTransform(+90);
                    Font pfeil = new Font("Arial", 12, FontStyle.Bold);

                    g.DrawString("↑", pfeil, Brushes.Black, new Point(413 - left - 30, 0), format);
                    g.DrawString("↑", pfeil, Brushes.Black, new Point(413 - left + 30, 0), format);
                }

            }
            else //Rückseite
            {
                //Seite rotieren ||X-Koordinaten positiv, Y-Koordinaten negativ
                g.RotateTransform(+90);

                int noch_nicht_gedruckt;
                int vok_beginnen;

                if (is_front == true)
                {
                    noch_nicht_gedruckt = anz_vok - (anzahl_seiten - aktuelle_seite) * 16;
                    vok_beginnen = ((anzahl_seiten) - (aktuelle_seite)) * 16 + 1;
                }
                else
                {
                    noch_nicht_gedruckt = anz_vok - (aktuelle_seite - 1) * 16;
                    vok_beginnen = (aktuelle_seite - 1) * 16 + 1;
                }

                //Falls noch mehr Seiten gedruckt werden müssen
                if (noch_nicht_gedruckt >= 16)
                {

                    //Positionsverschiebung der Rückseite
                    int links_rechts_verschiebung = -3;

                    for (int i = 0; i < 16; i++)
                    {
                        //Positionszugabe ändern
                        switch (links_rechts_verschiebung)
                        {
                            case 1:
                                links_rechts_verschiebung = -1;
                                break;
                            case -1:
                                links_rechts_verschiebung = -3;
                                break;
                            case -3:
                                links_rechts_verschiebung = 3;
                                break;
                            case 3:
                                links_rechts_verschiebung = 1;
                                break;
                        }

                        //Koordinaten abfragen
                        int[] coordinates = get_coordinates(i + 1 + links_rechts_verschiebung);

                        //Grösse des Textes abfragen

                        SizeF size_string = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font);

                        int height = Convert.ToInt32(size_string.Height / 2);

                        //Falls es ein Synonym gibt
                        if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Contains("=") == true)
                        {
                            string[] split_text = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Split('=');

                            SizeF size_foreign = g.MeasureString(split_text[0], font);
                            SizeF size_synonym = g.MeasureString(split_text[1], font);

                            if (size_foreign.Width > 292)
                            {
                                bool is_good = false;
                                int font_size = 12;

                                if (split_text[0].Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = split_text[0].Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }

                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben
                                                g.DrawString(part1, font_new, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 60 - height), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 20 - height), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Foreign normal schreiben
                                g.DrawString(split_text[0], font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height - 20 - height), format);
                            }

                            //Trennlinie zeichnen
                            g.DrawLine(pen, new Point((coordinates[0] * (-1)) - top - 10, (coordinates[1] * (-1)) + left - height / 2), new Point((coordinates[0] * (-1)) - top + 10, (coordinates[1] * (-1)) + left - height / 2));

                            //Falls Synonym zu gross
                            if (size_foreign.Width > 292)
                            {
                                bool is_good = false;
                                int font_size = 12;

                                if (split_text[1].Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = split_text[1].Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }


                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben
                                                g.DrawString(part1, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 60), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Synonym normal schreiben
                                g.DrawString(split_text[1], font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);
                            }
                            //Falls es kein Synonym gibt
                        }
                        else
                        {
                            //Schriftgrösse anpassen
                            //Falls Text zu gross

                            //Falls Text zu gross, string auf mehrere Zeilen aufteilen falls möglich
                            if (size_string.Width > 292)
                            {

                                bool is_good = false;
                                int font_size = 12;

                                if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }

                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben
                                                g.DrawString(part1, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height - 20), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                                else
                                {
                                    do
                                    {
                                        font_size--;
                                        Font font_new = new Font("Arial", font_size);

                                        SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                                        if (string_size.Width > 292 && font_size > 1)
                                        {
                                            is_good = false;
                                        }
                                        else
                                        {
                                            is_good = true;

                                            //kleinerer Text schreiben
                                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Normal schreiben
                                g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height), format);
                            }
                        }
                    }
                    //Falls letzte Seite
                }
                else
                {
                    //Positionsverschiebung der Rückseite
                    int links_rechts_verschiebung = -3;

                    for (int i = 0; i < noch_nicht_gedruckt; i++)
                    {

                        //Positionszugabe ändern
                        switch (links_rechts_verschiebung)
                        {
                            case 1:
                                links_rechts_verschiebung = -1;
                                break;
                            case -1:
                                links_rechts_verschiebung = -3;
                                break;
                            case -3:
                                links_rechts_verschiebung = 3;
                                break;
                            case 3:
                                links_rechts_verschiebung = 1;
                                break;
                        }

                        //Koordinaten abfragen
                        int[] coordinates = get_coordinates(i + 1 + links_rechts_verschiebung);

                        //Grösse des Textes abfragen

                        SizeF size_string = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font);

                        int height = Convert.ToInt32(size_string.Height / 2);

                        //Schriftgrösse anpassen

                        //Vokabel schreiben
                        //Falls es ein Synonym gibt
                        if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Contains("=") == true)
                        {
                            string[] split_text = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Split('=');

                            SizeF size_foreign = g.MeasureString(split_text[0], font);
                            SizeF size_synonym = g.MeasureString(split_text[1], font);

                            //Falls Foreign zu gross
                            if (size_foreign.Width > 292)
                            {
                                bool is_good = false;
                                int font_size = 12;

                                if (split_text[0].Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = split_text[0].Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }

                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben
                                                g.DrawString(part1, font_new, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 60 - height), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 20 - height), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Foreign normal schreiben
                                g.DrawString(split_text[0], font, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 20 - height), format);
                            }

                            //Trennlinie zeichnen
                            g.DrawLine(pen, new Point(-coordinates[0] - top - 10, -coordinates[1] + left - height / 2), new Point(-coordinates[0] - top + 10, -coordinates[1] + left - height / 2));

                            //Falls synonym zu gross
                            if (size_foreign.Width > 292)
                            {

                                bool is_good = false;
                                int font_size = 12;

                                if (split_text[1].Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = split_text[1].Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }


                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben


                                                g.DrawString(part1, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 60), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Synonym normal schreiben
                                g.DrawString(split_text[1], font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);
                            }
                        }
                        else //Falls es kein Synonym gibt
                        {
                            //Schriftgrösse anpassen
                            //Falls Text zu gross
                            if (size_string.Width > 292)
                            {

                                bool is_good = false;
                                int font_size = 12;

                                if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }

                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben

                                                g.DrawString(part1, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height - 20), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Text normal schreiben
                                g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height), format);
                            }
                        }
                    }
                }
            }


            //Schauen, ob noch mehr Seiten gedruckt werden müssen

            if (aktuelle_seite != anzahl_seiten)
            {
                e.HasMorePages = true;
                aktuelle_seite++;
            }
            else
            {
                e.HasMorePages = false;
            }
        }
        private void printDocument_cards_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Anzahl Seiten festlegen
            anzahl_seiten = (int)Math.Ceiling(anz_vok / 16d);
            aktuelle_seite = 1;
        }

        private int[] get_coordinates(int number)
        {
            switch (number)
            {
                case 01: return new int[] { -146, 103 };
                case 02: return new int[] { -146, 310 };
                case 03: return new int[] { -146, 516 };
                case 04: return new int[] { -146, 723 };
                case 05: return new int[] { -438, 103 };
                case 06: return new int[] { -438, 310 };
                case 07: return new int[] { -438, 516 };
                case 08: return new int[] { -438, 723 };
                case 09: return new int[] { -731, 103 };
                case 10: return new int[] { -731, 310 };
                case 11: return new int[] { -731, 516 };
                case 12: return new int[] { -731, 723 };
                case 13: return new int[] { -1023, 103 };
                case 14: return new int[] { -1023, 310 };
                case 15: return new int[] { -1023, 516 };
                default: return new int[] { -1023, 723 };
            }
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
                    VocabularyFile.ImportCsvFile(openDialog.FileName, book, true);
                    LoadBook(book);
                }
            }

            openDialog.Dispose();
        }

        private void TsmiExport_Click(object sender, EventArgs e)
        {
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