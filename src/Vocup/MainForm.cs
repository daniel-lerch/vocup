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

#nullable disable

namespace Vocup;

public partial class MainForm : Form, IMainForm
{
    private string updateUrl;
    private int lastSearchResult;

    public MainForm()
    {
        InitializeComponent();

        FileTreeView.RootPath = Settings.Default.VhfPath;
        if (AppInfo.IsUwp)
        {
            TsmiUpdate.Enabled = false;
            if (AppInfo.TryGetVocupInstallation(out Version version, out _, out _) && version < AppInfo.Version)
                StatusLbOldVersion.Visible = true;
        }
        else if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240))
        {
            if (AppInfo.TryGetVocupUwpApp(out Version version))
                StatusLbOpenUwpApp.Visible = version >= AppInfo.Version;
            else
                StatusLbInstallUwpApp.Visible = true;
        }
    }

    public VocabularyBook CurrentBook { get; private set; }
    public VocabularyBookController CurrentController { get; private set; }
    public StatisticsPanel StatisticsPanel => GroupStatistics;
    public TextBox SearchText => TbSearchWord;
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
        SplitContainer.Panel2.Controls.Add(controller.ListView);
        controller.ListView.PerformLayout();
        controller.ListView.BringToFront();

        CurrentBook = book;
        CurrentController = controller;

        VocabularyBookLoaded(true);

        FileTreeView.SelectedPath = book.FilePath;

        Settings.Default.LastFile = book.FilePath;
        Settings.Default.Save();
    }
    public void UnloadBook(bool fullUnload)
    {
        if (CurrentController is null)
            throw new InvalidOperationException("Cannot unload if no book is loaded");

        using (VocabularyBookController controller = CurrentController)
        {
            CurrentBook = null;
            CurrentController = null;
            SplitContainer.Panel2.Controls.Remove(controller.ListView);
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

    /// <summary>
    /// Save the bounds and the splitter distance of this form.
    /// </summary>
    private void StoreSettings()
    {
        Rectangle bounds = WindowState switch
        {
            FormWindowState.Normal => Bounds,
            FormWindowState.Maximized => RestoreBounds,
            FormWindowState.Minimized => RestoreBounds,
            _ => throw new ArgumentException($"Unknown FormWindowState {WindowState}")
        };

        Rectangle logicalBounds = new(bounds.Location, bounds.Size.Multiply(96f / DeviceDpi).Round());

        Settings.Default.MainFormBounds = logicalBounds;

        Settings.Default.MainFormWindowState = WindowState;

        Settings.Default.MainFormSplitterDistance = SplitContainer.SplitterDistance;

        Settings.Default.Save();
    }

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
        base.ScaleControl(factor, specified);
    }

    /// <summary>
    /// Restore the saved bounds and the splitter distance of this form.
    /// </summary>
    private void RestoreSettings()
    {
        // check if stored form bound is visible on any screen
        bool isVisible = false;
        foreach (Screen screen in Screen.AllScreens)
        {
            if (screen.Bounds.IntersectsWith(Settings.Default.MainFormBounds))
            {
                isVisible = true;
                break;
            }
        }

        if (Settings.Default.MainFormSplitterDistance != 0)
        {
            SplitContainer.SplitterDistance = Settings.Default.MainFormSplitterDistance;
        }

        if (isVisible && Settings.Default.MainFormBounds != default)
        {
            // visible => restore the bounds of the main form
            Bounds = Settings.Default.MainFormBounds;

            // Do not restore the window state when the form was minimzed
            if (Settings.Default.MainFormWindowState != FormWindowState.Minimized)
            {
                WindowState = Settings.Default.MainFormWindowState;
            }
        }
        else
        {
            // Not visible => use default do nothing
        }
    }

    #region Event handlers
    private async void Form_Load(object sender, EventArgs e)
    {
        RestoreSettings();

        Update();
        Activate();

        TrackingService.Action("App/Start");

        // Check online for updates
        if (!Settings.Default.DisableInternetServices && !AppInfo.IsUwp)
        {
            updateUrl = await UpdateService.GetUpdateUrl();
            StatusLbUpdateAvailable.Visible = !string.IsNullOrWhiteSpace(updateUrl);
        }
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

    private void Form_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (UnsavedChanges)
        {
            e.Cancel = !EnsureSaved();
        }

        if (!e.Cancel)
        {
            // Tracking is done in Program.cs because starting tasks while the scheduler is shutting down does not work
            StoreSettings();
        }
    }

    private void FileTreeView_FileSelected(object sender, FileSelectedEventArgs e)
    {
        if (CurrentBook != null)
        {
            // Prevent the file tree view from loading the book again
            // if FileTreeView.SelectedPath was assigned in IMainForm.LoadBook(VocabularyBook)
            if (CurrentBook.FilePath == e.FullName)
                return;
            if (UnsavedChanges && !EnsureSaved())
                return;

            UnloadBook(false);
        }

        ReadFile(e.FullName);
    }

    private void FileTreeView_BrowseClick(object sender, EventArgs e)
    {
        using (FolderBrowserDialog dialog = new FolderBrowserDialog
        {
            Description = Messages.BrowseVhfPath,
            SelectedPath = Settings.Default.VhfPath
        })
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // This call fails for inaccessible paths like optical disk drives
                    _ = Directory.GetFiles(dialog.SelectedPath);

                    // Eventually refresh tree view root path
                    if (dialog.SelectedPath != Settings.Default.VhfPath)
                    {
                        Settings.Default.VhfPath = dialog.SelectedPath;
                        FileTreeView.RootPath = dialog.SelectedPath;
                        Settings.Default.Save();
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show(Messages.VhfPathInvalid, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

    private void TsbtnEvaluationInfo_Click(object sender, EventArgs e) => EvaluationInfo();
    private void TsmiEvaluationInfo_Click(object sender, EventArgs e) => EvaluationInfo();

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
                    SaveFile(false);
                }
            }
        }
    }

    private void TsmiSpecialChar_Click(object sender, EventArgs e)
    {
        using (var dialog = new SpecialCharManage()) dialog.ShowDialog();
    }

    private async void TsmiUpdate_Click(object sender, EventArgs e)
    {
        // Check online for updates
        updateUrl = await UpdateService.GetUpdateUrl();
        StatusLbUpdateAvailable.Visible = !string.IsNullOrWhiteSpace(updateUrl);
    }

    private void TsbCreateBook_Click(object sender, EventArgs e) => CreateBook();
    private void TsmiCreateBook_Click(object sender, EventArgs e) => CreateBook();

    private void BtnBookSettings_Click(object sender, EventArgs e) => EditBook();
    private void TsmiBookOptions_Click(object sender, EventArgs e) => EditBook();

    private void TsbOpenBook_Click(object sender, EventArgs e) => OpenFile();
    private void TsmiOpenBook_Click(object sender, EventArgs e) => OpenFile();

    private void BtnAddWord_Click(object sender, EventArgs e) => AddWord();
    private void TsmiAddWord_Click(object sender, EventArgs e) => AddWord();

    private void BtnEditWord_Click(object sender, EventArgs e) => EditWord();
    private void TsmiEditWord_Click(object sender, EventArgs e) => EditWord();

    private void BtnDeleteWord_Click(object sender, EventArgs e) => DeleteWord();
    private void TsmiDeleteWord_Click(object sender, EventArgs e) => DeleteWord();

    private void TsmiSave_Click(object sender, EventArgs e) => SaveFile(false);
    private void TsmiSaveAs_Click(object sender, EventArgs e) => SaveFile(true);
    private void TsbSave_Click(object sender, EventArgs e) => SaveFile(false);

    private void BtnPractice_Click(object sender, EventArgs e) => PracticeWords();
    private void TsmiPractice_Click(object sender, EventArgs e) => PracticeWords();

    private void TsbPrint_Click(object sender, EventArgs e) => PrintFile();
    private void TsmiPrint_Click(object sender, EventArgs e) => PrintFile();

    private void TsmiCloseBook_Click(object sender, EventArgs e)
    {
        if (UnsavedChanges && !EnsureSaved())
            return;

        UnloadBook(true);
        Settings.Default.LastFile = "";
        Settings.Default.Save();
    }

    private void TsmiMerge_Click(object sender, EventArgs e)
    {
        using (var dialog = new MergeFiles()) dialog.ShowDialog();
    }

    private void TsmiBackupRestore_Click(object sender, EventArgs e)
    {
        if (UnsavedChanges && !EnsureSaved()) return;

        using (OpenFileDialog dialog = new OpenFileDialog
        {
            Title = Words.OpenBackup,
            Filter = Words.VocupBackupFile + " (*.vdp)|*.vdp",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        })
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (RestoreBackup restore = new RestoreBackup(dialog.FileName))
                    restore.ShowDialog();
            }
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
                SaveFile(false);
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

    private void TsmiImport_Click(object sender, EventArgs e) => ImportCsv();

    private void TsmiImportAnsi_Click(object sender, EventArgs e) => ImportCsv(ansiEncoding: true);

    private void TsmiExport_Click(object sender, EventArgs e)
    {
        if (UnsavedChanges)
        {
            DialogResult dialogResult = MessageBox.Show(Messages.CsvExportSave,
                Messages.CsvExportSaveT, MessageBoxButtons.YesNoCancel);

            if (dialogResult == DialogResult.Yes)
            {
                SaveFile(false);
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
        }

        using (SaveFileDialog saveDialog = new SaveFileDialog
        {
            Title = Words.Export,
            Filter = "CSV (*.csv)|*.csv",
            FileName = CurrentBook.Name
        })
        {
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                VocabularyFile.ExportCsvFile(saveDialog.FileName, CurrentBook);
            }
        }
    }

    private void TsmiExitAppliaction_Click(object sender, EventArgs e) => Close();

    private void StatusLbOldVersion_Click(object sender, EventArgs e)
    {
        if (AppInfo.TryGetVocupInstallation(out _, out _, out string uninstallString) &&
            MessageBox.Show(Messages.LegacyVersionUninstall,
            Messages.LegacyVersionUninstallT, MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            Process.Start(uninstallString);
        }
    }

    private void StatusLbUpdateAvailable_Click(object sender, EventArgs e)
    {
        Process.Start(updateUrl);
    }

    private void StatusLbOpenUwpApp_Click(object sender, EventArgs e)
    {
        if (!UnsavedChanges || EnsureSaved())
        {
            Close();
            Program.ReleaseMutex();
            Process.Start("explorer.exe", @"shell:appsFolder\9961VectorData.Vocup_ffrs9s78t67f2!App");
        }
    }

    private void StatusLbInstallUwpApp_Click(object sender, EventArgs e)
    {
        Process.Start("ms-windows-store://pdp/?ProductId=9N6W2H3QJQMM");
    }

    private async void BtnSearchWord_Click(object sender, EventArgs e)
    {
        string search_text = TbSearchWord.Text.ToUpper();

        if (search_text == "EASTER EGG")
        {
            if (CurrentBook != null) // Save and close current book
            {
                SaveFile(false);
                UnloadBook(true);
            }

            ReadFile(Path.Combine(Application.StartupPath, "Resources", "easter_egg.vhf"));
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
            int index = CurrentBook.Words.NextIndexOf(word =>
            {
                return word.MotherTongue.ToUpper().Contains(search_text)
                       || word.ForeignLang.ToUpper().Contains(search_text)
                       || (word.ForeignLangSynonym?.ToUpper().Contains(search_text) ?? false);
            }, lastSearchResult);

            if (index != -1)
            {
                ListViewItem item = CurrentController.GetController(CurrentBook.Words[index]).ListViewItem;
                item.Selected = true;
                item.Focused = true;
                item.EnsureVisible();
                CurrentController.ListView.Focus();
                lastSearchResult = index;
            }

            Color @default = Color.White;
            Color highlight = index == -1 ? Color.FromArgb(255, 192, 203) : Color.FromArgb(144, 238, 144);

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
    #endregion

    #region Utility methods
    public void ReadFile(string path)
    {
        VocabularyBook book = new VocabularyBook();

        if (VocabularyFile.ReadVhfFile(path, book))
        {
            VocabularyFile.ReadVhrFile(book);
            book.Notify();
            LoadBook(book);
        }
    }

    private bool EnsureSaved()
    {
        DialogResult result = MessageBox.Show(Messages.GeneralSaveChanges, Messages.GeneralSaveChangesT, MessageBoxButtons.YesNoCancel);

        if (result == DialogResult.Yes)
        {
            return SaveFile(false); // Save file and return true which means to continue with the next action.
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

    private bool SaveFile(bool saveAsNewFile)
    {
        //Datei-Speichern-unter Dialogfeld öffnen
        if (string.IsNullOrWhiteSpace(CurrentBook.FilePath) ||
            string.IsNullOrWhiteSpace(CurrentBook.VhrCode) ||
            saveAsNewFile)
        {
            using (SaveFileDialog save = new SaveFileDialog
            {
                Title = Words.SaveVocabularyBook,
                FileName = CurrentBook.MotherTongue + " - " + CurrentBook.ForeignLang,
                InitialDirectory = Settings.Default.VhfPath,
                Filter = Words.VocupVocabularyBookFile + " (*.vhf)|*.vhf"
            })
            {
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

    private void OpenFile()
    {
        using (OpenFileDialog open = new OpenFileDialog
        {
            Title = Words.OpenVocabularyBook,
            InitialDirectory = Settings.Default.VhfPath,
            Filter = Words.VocupVocabularyBookFile + " (*.vhf)|*.vhf"
        })
        {
            if (open.ShowDialog() == DialogResult.OK)
            {
                if (CurrentBook != null)
                {
                    if (UnsavedChanges && !EnsureSaved())
                        return;

                    UnloadBook(false);
                }

                ReadFile(open.FileName);
            }
        }
    }

    private void CreateBook()
    {
        using (VocabularyBookSettings dialog = new VocabularyBookSettings(out VocabularyBook book))
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (CurrentBook != null)
                {
                    if (UnsavedChanges && !EnsureSaved())
                        return;

                    UnloadBook(false);
                }

                TrackingService.Action("Book/Create");

                // VocabularyBookSettings enables notification on creation
                LoadBook(book);

                BtnAddWord.Focus();
            }
        }
    }

    private void ImportCsv(bool ansiEncoding = false)
    {
        using (OpenFileDialog openDialog = new OpenFileDialog
        {
            Title = Words.Import,
            Filter = $"CSV (*.csv)|*.csv"
        })
        {
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                TrackingService.Action("Book/Import");

                if (CurrentBook != null)
                {
                    VocabularyFile.ImportCsvFile(openDialog.FileName, CurrentBook, false, ansiEncoding);
                }
                else
                {
                    VocabularyBook book = new VocabularyBook();
                    if (VocabularyFile.ImportCsvFile(openDialog.FileName, book, true, ansiEncoding))
                    {
                        book.Notify();
                        book.UnsavedChanges = true;
                        LoadBook(book);
                    }
                }
            }
        }
    }

    private void AddWord()
    {
        using (var dialog = new AddWordDialog(CurrentBook) { Owner = this }) dialog.ShowDialog();
        BtnAddWord.Focus();
    }

    public void EditWord()
    {
        VocabularyWord selected = (VocabularyWord)CurrentController.ListView.SelectedItem.Tag;
        using (var dialog = new EditWordDialog(CurrentBook, selected) { Owner = this }) dialog.ShowDialog();
        CurrentController.ListView.SelectedItem.EnsureVisible();
        BtnAddWord.Focus();
    }

    private void DeleteWord()
    {
        int index = CurrentController.ListView.SelectedItem.Index;
        VocabularyWord selected = (VocabularyWord)CurrentController.ListView.SelectedItem.Tag;
        CurrentBook.Words.Remove(selected);

        // Limit index of the deleted word to the highest possible index
        index = Math.Min(index, CurrentBook.Words.Count - 1);

        foreach (ListViewItem item in CurrentController.ListView.Items)
        {
            if (item.Index == index) item.Selected = true;
        }

        BtnAddWord.Focus();
    }

    private void EditBook()
    {
        using (var dialog = new VocabularyBookSettings(CurrentBook) { Owner = this }) dialog.ShowDialog();
        BtnAddWord.Focus();
    }

    private void PracticeWords()
    {
        using (PracticeCountDialog countDialog = new PracticeCountDialog(CurrentBook))
        {
            if (countDialog.ShowDialog() != DialogResult.OK)
                return;

            List<VocabularyWordPractice> practiceList = countDialog.PracticeList;

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

    private void EvaluationInfo()
    {
        using (var dialog = new EvaluationInfoDialog()) dialog.ShowDialog();
    }

    private void PrintFile()
    {
        using (var dialog = new PrintWordSelection(CurrentBook)) dialog.ShowDialog();
    }
    #endregion
}
