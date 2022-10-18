using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vocup.Controls;
using Vocup.Forms;
using Vocup.IO;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Util;
using Vocup.ViewModels;

namespace Vocup;

public partial class MainForm : Form, IMainForm, IViewFor<MainFormViewModel>
{
    private int lastSearchResult;

    public MainForm()
    {
        InitializeComponent();
        ViewModel = new MainFormViewModel(Program.Settings);

#pragma warning disable CA1416 // Validate platform compatibility
        this.OneWayBind(ViewModel, vm => vm.Title, x => x.Text);

        this.OneWayBind(ViewModel, vm => vm.BookContext, x => x.GroupBook.Enabled, context => context != null);
        this.OneWayBind(ViewModel, vm => vm.BookContext, x => x.GroupWord.Enabled, context => context != null);
        this.OneWayBind(ViewModel, vm => vm.BookContext, x => x.TsmiAddWord.Enabled, context => context != null);
        this.OneWayBind(ViewModel, vm => vm.BookContext, x => x.GroupBook.Enabled, context => context != null);
        this.OneWayBind(ViewModel, vm => vm.BookContext, x => x.TsmiSaveAs.Enabled, context => context != null);

        this.OneWayBind(ViewModel, vm => vm.BookContext.Book.Words.Count, x => x.GroupSearch.Enabled, count => count > 0);
        this.OneWayBind(ViewModel, vm => vm.BookContext.Book.Words.Count, x => x.TsmiExport.Enabled, count => count > 0);

        this.OneWayBind(ViewModel, vm => vm.BookContext.Book.Unpracticed, x => x.StatisticsPanel.Unpracticed);
        this.OneWayBind(ViewModel, vm => vm.BookContext.Book.WronglyPracticed, x => x.StatisticsPanel.WronglyPracticed);
        this.OneWayBind(ViewModel, vm => vm.BookContext.Book.CorrectlyPracticed, x => x.StatisticsPanel.CorrectlyPracticed);
        this.OneWayBind(ViewModel, vm => vm.BookContext.Book.FullyPracticed, x => x.StatisticsPanel.FullyPracticed);

        this.BindCommand(ViewModel, vm => vm.OpenCommand, x => x.TsmiOpenBook);
        this.BindCommand(ViewModel, vm => vm.OpenCommand, x => x.TsbOpenBook);
        this.BindCommand(ViewModel, vm => vm.SaveCommand, x => x.TsmiSave);
        this.BindCommand(ViewModel, vm => vm.SaveCommand, x => x.TsbSave);
        this.BindCommand(ViewModel, vm => vm.CloseCommand, x => x.TsmiCloseBook);
        this.BindCommand(ViewModel, vm => vm.PracticeCommand, x => x.TsmiPractice);
        this.BindCommand(ViewModel, vm => vm.PracticeCommand, x => x.BtnPractice);
        this.BindCommand(ViewModel, vm => vm.CreateBookCommand, x => x.TsmiCreateBook);
        this.BindCommand(ViewModel, vm => vm.CreateBookCommand, x => x.TsbCreateBook);
        this.BindCommand(ViewModel, vm => vm.BookSettingsCommand, x => x.TsmiBookOptions);
        this.BindCommand(ViewModel, vm => vm.BookSettingsCommand, x => x.BtnBookSettings);
        this.BindCommand(ViewModel, vm => vm.PrintCommand, x => x.TsmiPrint);
        this.BindCommand(ViewModel, vm => vm.PrintCommand, x => x.TsbPrint);
        this.BindCommand(ViewModel, vm => vm.OpenInExplorerCommand, x => x.TsmiOpenInExplorer);

        this.WhenActivated(d => d(ViewModel.OpenFile.RegisterHandler(interaction =>
        {
            using OpenFileDialog open = new()
            {
                Title = Words.OpenVocabularyBook,
                InitialDirectory = Program.Settings.VhfPath,
                Filter = Words.VocupVocabularyBookFile + " (*.vhf)|*.vhf"
            };

            if (open.ShowDialog() == DialogResult.OK)
                interaction.SetOutput(open.FileName);
            else
                interaction.SetOutput(null);
        })));

        this.WhenActivated(d => d(ViewModel.SaveFile.RegisterHandler(interaction =>
        {
            using SaveFileDialog save = new()
            {
                Title = Words.SaveVocabularyBook,
                FileName = interaction.Input.MotherTongue + " - " + interaction.Input.ForeignLanguage,
                InitialDirectory = Program.Settings.VhfPath,
                Filter = Words.VocupVocabularyBookFile + " (*.vhf)|*.vhf"
            };

            if (save.ShowDialog() == DialogResult.OK)
                interaction.SetOutput(save.FileName);
            else
                interaction.SetOutput(null);
        })));

        this.WhenActivated(d => d(ViewModel.SaveBeforeContinue.RegisterHandler(interaction =>
        {
            DialogResult result = MessageBox.Show(Messages.GeneralSaveChanges, Messages.GeneralSaveChangesT, MessageBoxButtons.YesNoCancel);
            
            interaction.SetOutput(result switch {
                DialogResult.Yes => true,
                DialogResult.No => false,
                _ => null,
            });
        })));

        this.WhenActivated(d => d(ViewModel.Practice.RegisterHandler(interaction =>
        {
            using (PracticeCountDialog countDialog = new(interaction.Input))
            {
                if (countDialog.ShowDialog() == DialogResult.OK)
                {
                    List<VocabularyWordPractice> practiceList = countDialog.PracticeList;

                    CurrentController.ListView.Visible = false;

                    using (PracticeDialog dialog = new(interaction.Input, practiceList) { Owner = this }) dialog.ShowDialog();

                    if (Program.Settings.PracticeShowResultList)
                    {
                        using (PracticeResultList dialog = new(interaction.Input, practiceList)) dialog.ShowDialog();
                    }

                    CurrentController.ListView.Visible = true;
                    BtnAddWord.Focus();
                }

                Program.TrackingService.Page("/book");
            }

            interaction.SetOutput(Unit.Default);
        })));

        this.WhenActivated(d => d(ViewModel.CreateBook.RegisterHandler(interaction =>
        {
            using var dialog = new VocabularyBookSettings(out Book book);
            if (dialog.ShowDialog() == DialogResult.OK)
                interaction.SetOutput(book);
            else
                interaction.SetOutput(null);
        })));

        this.WhenActivated(d => d(ViewModel.BookSettings.RegisterHandler(interaction =>
        {
            using (var dialog = new VocabularyBookSettings(interaction.Input) { Owner = this }) dialog.ShowDialog();
            BtnAddWord.Focus();
            interaction.SetOutput(Unit.Default);
        })));

        this.WhenActivated(d => d(ViewModel.Print.RegisterHandler(interaction =>
        {
            using var dialog = new PrintWordSelection(interaction.Input);
            dialog.ShowDialog();
            interaction.SetOutput(Unit.Default);
        })));

        this.WhenActivated(d => d(ViewModel.OpenInExplorer.RegisterHandler(interaction =>
        {
            // TODO: Remove Messages.OpenInExplorerSave and Messages.OpenInExplorerSaveT
            try
            {
                FileInfo info = new FileInfo(interaction.Input);
                if (info.Exists)
                {
                    string explorer = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "explorer.exe");
                    Process.Start(explorer, $"/select,\"{info.FullName}\"");
                }
                else
                {
                    MessageBox.Show(Messages.OpenInExplorerNotFound, Messages.OpenInExplorerNotFoundT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Messages.OpenInExplorerError, ex), Messages.OpenInExplorerErrorT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                interaction.SetOutput(Unit.Default);
            }
        })));
#pragma warning restore CA1416 // Validate platform compatibility

        FileTreeView.RootPath = Program.Settings.VhfPath;

        if (AppInfo.TryGetVocupInstallation(out Version? version, out _, out _) && version < AppInfo.Version)
            StatusLbOldVersion.Visible = true;
    }

    public MainFormViewModel? ViewModel { get; set; }
    object? IViewFor.ViewModel { get => ViewModel; set => ViewModel = value as MainFormViewModel; }

#nullable disable

    public BookContext CurrentBook { get; private set; }
    public VocabularyBookController CurrentController { get; private set; }
    public StatisticsPanel StatisticsPanel => GroupStatistics;
    public TextBox SearchText => TbSearchWord;

    public void VocabularyWordSelected(bool value)
    {
        BtnEditWord.Enabled = value;
        TsmiEditWord.Enabled = value;
        BtnDeleteWord.Enabled = value;
        TsmiDeleteWord.Enabled = value;
    }
    public void VocabularyBookUnsavedChanges(bool value)
    {
        TsmiSave.Enabled = value;
        TsbSave.Enabled = value;
    }
    public void LoadBook(BookContext bookContext)
    {
        VocabularyBookController controller = new VocabularyBookController(bookContext) { Parent = this };
        SplitContainer.Panel2.Controls.Add(controller.ListView);
        controller.ListView.PerformLayout();
        controller.ListView.BringToFront();

        CurrentBook = bookContext;
        CurrentController = controller;

        FileTreeView.SelectedPath = bookContext.FilePath;

        Program.Settings.LastFile = bookContext.FilePath;
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
            VocabularyWordSelected(false);
            VocabularyBookUnsavedChanges(false);

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

        Program.Settings.MainFormBounds = logicalBounds;

        Program.Settings.MainFormWindowState = WindowState;

        Program.Settings.MainFormSplitterDistance = SplitContainer.SplitterDistance;
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
            if (screen.Bounds.IntersectsWith(Program.Settings.MainFormBounds))
            {
                isVisible = true;
                break;
            }
        }

        if (Program.Settings.MainFormSplitterDistance != 0)
        {
            SplitContainer.SplitterDistance = Program.Settings.MainFormSplitterDistance;
        }

        if (isVisible && Program.Settings.MainFormBounds != default)
        {
            // visible => restore the bounds of the main form
            Bounds = Program.Settings.MainFormBounds;

            // Do not restore the window state when the form was minimzed
            if (Program.Settings.MainFormWindowState != FormWindowState.Minimized)
            {
                WindowState = Program.Settings.MainFormWindowState;
            }
        }
        else
        {
            // Not visible => use default do nothing
        }
    }

    #region Event handlers
    private void Form_Load(object sender, EventArgs e)
    {
        RestoreSettings();

        Update();
        Activate();

        Program.TrackingService.Action("/", "App/Start");
    }

    private void Form_Shown(object sender, EventArgs e)
    {
        if (Program.Settings.StartScreen == (int)StartScreen.AboutBox)
        {
            using (var dialog = new AboutBox()) dialog.ShowDialog();

            Program.Settings.StartScreen = (int)StartScreen.LastFile;
        }
    }

    private void Form_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (ViewModel.BookContext.UnsavedChanges)
        {
            //e.Cancel = !EnsureSaved();
        }

        if (!e.Cancel)
        {
            if (CurrentBook is null)
                Program.TrackingService.DeferAction("/", "App/Close");
            else
                Program.TrackingService.DeferAction("/book", "App/Close");

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
            //if (ViewModel.BookContext.UnsavedChanges && !EnsureSaved())
            //    return;

            UnloadBook(false);
        }

        //ReadFile(e.FullName);
    }

    private void FileTreeView_BrowseClick(object sender, EventArgs e)
    {
        using (FolderBrowserDialog dialog = new FolderBrowserDialog
        {
            Description = Messages.BrowseVhfPath,
            SelectedPath = Program.Settings.VhfPath
        })
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // This call fails for inaccessible paths like optical disk drives
                    _ = Directory.GetFiles(dialog.SelectedPath);

                    // Eventually refresh tree view root path
                    if (dialog.SelectedPath != Program.Settings.VhfPath)
                    {
                        Program.Settings.VhfPath = dialog.SelectedPath;
                        FileTreeView.RootPath = dialog.SelectedPath;
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
        string oldVhfPath = Program.Settings.VhfPath;

        using (SettingsDialog optionen = new SettingsDialog(Program.Settings))
        {
            if (optionen.ShowDialog() == DialogResult.OK)
            {
                // Renew practice state for Settings.MaxPracticeCount changes
                //CurrentBook?.Words.ForEach(x => x.RenewPracticeState());

                // Refresh tree view root path
                if (oldVhfPath != Program.Settings.VhfPath)
                    FileTreeView.RootPath = Program.Settings.VhfPath;

                //Autosave
                if (Program.Settings.AutoSave && ViewModel.BookContext.UnsavedChanges)
                {
                    SaveFile(false);
                }
            }
        }

        Program.TrackingService.Page(CurrentBook is null ? "/" : "/book");
    }

    private void TsmiSpecialChar_Click(object sender, EventArgs e)
    {
        using (var dialog = new SpecialCharManage()) dialog.ShowDialog();
    }

    private async void TsmiUpdate_Click(object sender, EventArgs e)
    {
        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240))
        {
            await Launcher.LaunchUriAsync("ms-windows-store://downloadsandupdates");
        }
    }

    private void BtnAddWord_Click(object sender, EventArgs e) => AddWord();
    private void TsmiAddWord_Click(object sender, EventArgs e) => AddWord();

    private void BtnEditWord_Click(object sender, EventArgs e) => EditWord();
    private void TsmiEditWord_Click(object sender, EventArgs e) => EditWord();

    private void BtnDeleteWord_Click(object sender, EventArgs e) => DeleteWord();
    private void TsmiDeleteWord_Click(object sender, EventArgs e) => DeleteWord();

    private void TsmiSaveAs_Click(object sender, EventArgs e) => SaveFile(true);

    private void TsmiMerge_Click(object sender, EventArgs e)
    {
        using (var dialog = new MergeFiles()) dialog.ShowDialog();
    }

    private void TsmiImport_Click(object sender, EventArgs e) => ImportCsv();

    private void TsmiImportAnsi_Click(object sender, EventArgs e) => ImportCsv(ansiEncoding: true);

    private void TsmiExport_Click(object sender, EventArgs e)
    {
        if (ViewModel.BookContext.UnsavedChanges)
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
                CsvFile.Instance.Export(saveDialog.FileName, CurrentBook.Book);
            }
        }
    }

    private void TsmiExitApplication_Click(object sender, EventArgs e) => Close();

    private void StatusLbOldVersion_Click(object sender, EventArgs e)
    {
        if (AppInfo.TryGetVocupInstallation(out _, out _, out string uninstallString) &&
            MessageBox.Show(Messages.LegacyVersionUninstall,
            Messages.LegacyVersionUninstallT, MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            Process.Start(uninstallString);
        }
    }

    private async void BtnSearchWord_Click(object sender, EventArgs e)
    {
        string search_text = TbSearchWord.Text.ToUpper();

        if (search_text == "TRANSPARENT") // EasterEgg 2
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
            int index = CurrentBook.Book.Words.NextIndexOf(word =>
            {
                return word.MotherTongueText.ToUpper().Contains(search_text)
                       || word.ForeignLangText.ToUpper().Contains(search_text)
                       || (word.ForeignLangSynonym?.ToUpper().Contains(search_text) ?? false);
            }, lastSearchResult);

            if (index != -1)
            {
                ListViewItem item = CurrentController.GetController(CurrentBook.Book.Words[index]).ListViewItem;
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
                FileName = CurrentBook.Book.MotherTongue + " - " + CurrentBook.Book.ForeignLanguage,
                InitialDirectory = Program.Settings.VhfPath,
                Filter = Words.VocupVocabularyBookFile + " (*.vhf)|*.vhf"
            })
            {
                if (save.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    new BookStorage().SaveAsync(CurrentBook, save.FileName, Program.Settings.VhrPath).AsTask().GetAwaiter().GetResult();
                    Cursor.Current = Cursors.Default;
                    CurrentBook.UnsavedChanges = false;
                    Program.Settings.LastFile = CurrentBook.FilePath;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            Cursor.Current = Cursors.WaitCursor;
            new BookStorage().SaveAsync(CurrentBook, Program.Settings.VhrPath).AsTask().GetAwaiter().GetResult();
            Cursor.Current = Cursors.Default;
            CurrentBook.UnsavedChanges = false;
            Program.Settings.LastFile = CurrentBook.FilePath;
            return true;
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
                if (CurrentBook != null)
                {
                    Program.TrackingService.Action("/book", "Book/Import");

                    CsvFile.Instance.Import(openDialog.FileName, CurrentBook.Book, false, ansiEncoding);
                }
                else
                {
                    Program.TrackingService.Action("/book/new", "Book/Import");

                    BookContext bookContext = new(new Book(string.Empty, string.Empty), BookFileFormat.Vhf1, Program.Settings);
                    if (CsvFile.Instance.Import(openDialog.FileName, bookContext.Book, true, ansiEncoding))
                    {
                        bookContext.UnsavedChanges = true;
                        LoadBook(bookContext);
                    }
                }
            }
        }
    }

    private void AddWord()
    {
        using (var dialog = new AddWordDialog(CurrentBook.Book) { Owner = this }) dialog.ShowDialog();
        BtnAddWord.Focus();
    }

    public void EditWord()
    {
        Word selected = (Word)CurrentController.ListView.SelectedItem.Tag;
        using (var dialog = new EditWordDialog(CurrentBook.Book, selected) { Owner = this }) dialog.ShowDialog();
        CurrentController.ListView.SelectedItem.EnsureVisible();
        BtnAddWord.Focus();
    }

    private void DeleteWord()
    {
        int index = CurrentController.ListView.SelectedItem.Index;
        Word selected = (Word)CurrentController.ListView.SelectedItem.Tag;
        CurrentBook.Book.Words.Remove(selected);

        // Limit index of the deleted word to the highest possible index
        index = Math.Min(index, CurrentBook.Book.Words.Count - 1);

        foreach (ListViewItem item in CurrentController.ListView.Items)
        {
            if (item.Index == index) item.Selected = true;
        }

        BtnAddWord.Focus();
    }

    private void EvaluationInfo()
    {
        using (var dialog = new EvaluationInfoDialog()) dialog.ShowDialog();
    }
    #endregion
}
