namespace Vocup
{
    public partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
            System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
            System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
            System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
            System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
            System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
            System.Windows.Forms.ToolStripSeparator infoToolStripMenuItem;
            MenuStrip = new Controls.ResponsiveMenuStrip();
            TsmiRootFile = new System.Windows.Forms.ToolStripMenuItem();
            TsmiCreateBook = new System.Windows.Forms.ToolStripMenuItem();
            TsmiOpenBook = new System.Windows.Forms.ToolStripMenuItem();
            TsmiCloseBook = new System.Windows.Forms.ToolStripMenuItem();
            TsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            TsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            TsmiShare = new System.Windows.Forms.ToolStripMenuItem();
            TsmiImport = new System.Windows.Forms.ToolStripMenuItem();
            TsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            TsmiOpenInExplorer = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            TsmiPrint = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            TsmiExitApplication = new System.Windows.Forms.ToolStripMenuItem();
            TsmiRootEdit = new System.Windows.Forms.ToolStripMenuItem();
            TsmiAddWord = new System.Windows.Forms.ToolStripMenuItem();
            TsmiEditWord = new System.Windows.Forms.ToolStripMenuItem();
            TsmiDeleteWord = new System.Windows.Forms.ToolStripMenuItem();
            TsmiRootTools = new System.Windows.Forms.ToolStripMenuItem();
            TsmiPractice = new System.Windows.Forms.ToolStripMenuItem();
            TsmiBookOptions = new System.Windows.Forms.ToolStripMenuItem();
            TsmiMerge = new System.Windows.Forms.ToolStripMenuItem();
            TsmiSpecialChar = new System.Windows.Forms.ToolStripMenuItem();
            TsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            TsmiRootHelp = new System.Windows.Forms.ToolStripMenuItem();
            TsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            TsmiEvaluationInfo = new System.Windows.Forms.ToolStripMenuItem();
            TsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            TsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            TbSearchWord = new System.Windows.Forms.TextBox();
            GroupBook = new System.Windows.Forms.GroupBox();
            BtnBookSettings = new Controls.ResponsiveButton();
            BtnPractice = new Controls.ResponsiveButton();
            GroupSearch = new System.Windows.Forms.GroupBox();
            BtnSearchWord = new Controls.ResponsiveButton();
            GroupWord = new System.Windows.Forms.GroupBox();
            BtnAddWord = new Controls.ResponsiveButton();
            BtnDeleteWord = new Controls.ResponsiveButton();
            BtnEditWord = new Controls.ResponsiveButton();
            StatusStrip = new System.Windows.Forms.StatusStrip();
            StatusLbOldVersion = new System.Windows.Forms.ToolStripStatusLabel();
            SideBar = new System.Windows.Forms.Panel();
            GroupStatistics = new Controls.StatisticsPanel();
            SplitContainer = new Controls.ResponsiveSplitContainer();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            RecentFilesAvaloniaControlHost = new Avalonia.Win32.Interoperability.WinFormsAvaloniaControlHost();
            tabPage2 = new System.Windows.Forms.TabPage();
            FileTreeView = new Controls.FileTreeView();
            LbEmptyForm = new System.Windows.Forms.Label();
            ToolStrip = new Controls.ResponsiveToolStrip();
            TsbCreateBook = new System.Windows.Forms.ToolStripButton();
            TsbOpenBook = new System.Windows.Forms.ToolStripButton();
            TsbSave = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            TsbPrint = new System.Windows.Forms.ToolStripButton();
            TsbEvaluationInfo = new System.Windows.Forms.ToolStripButton();
            toolStripLabel = new System.Windows.Forms.ToolStripLabel();
            TableLayout = new System.Windows.Forms.TableLayoutPanel();
            toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            infoToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            MenuStrip.SuspendLayout();
            GroupBook.SuspendLayout();
            GroupSearch.SuspendLayout();
            GroupWord.SuspendLayout();
            StatusStrip.SuspendLayout();
            SideBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer).BeginInit();
            SplitContainer.Panel1.SuspendLayout();
            SplitContainer.Panel2.SuspendLayout();
            SplitContainer.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ToolStrip.SuspendLayout();
            TableLayout.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripMenuItem11
            // 
            toolStripMenuItem11.Name = "toolStripMenuItem11";
            resources.ApplyResources(toolStripMenuItem11, "toolStripMenuItem11");
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            resources.ApplyResources(toolStripMenuItem10, "toolStripMenuItem10");
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            resources.ApplyResources(toolStripMenuItem9, "toolStripMenuItem9");
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(toolStripMenuItem5, "toolStripMenuItem5");
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            resources.ApplyResources(toolStripMenuItem6, "toolStripMenuItem6");
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            resources.ApplyResources(toolStripMenuItem8, "toolStripMenuItem8");
            // 
            // infoToolStripMenuItem
            // 
            infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            resources.ApplyResources(infoToolStripMenuItem, "infoToolStripMenuItem");
            // 
            // MenuStrip
            // 
            MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { TsmiRootFile, TsmiRootEdit, TsmiRootTools, TsmiRootHelp });
            resources.ApplyResources(MenuStrip, "MenuStrip");
            MenuStrip.Name = "MenuStrip";
            // 
            // TsmiRootFile
            // 
            TsmiRootFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { TsmiCreateBook, TsmiOpenBook, TsmiCloseBook, toolStripMenuItem11, TsmiSave, TsmiSaveAs, TsmiShare, toolStripMenuItem10, TsmiImport, TsmiExport, toolStripMenuItem9, TsmiOpenInExplorer, toolStripMenuItem7, TsmiPrint, toolStripMenuItem3, TsmiExitApplication });
            TsmiRootFile.Name = "TsmiRootFile";
            resources.ApplyResources(TsmiRootFile, "TsmiRootFile");
            // 
            // TsmiCreateBook
            // 
            TsmiCreateBook.Image = Properties.Icons.File;
            TsmiCreateBook.Name = "TsmiCreateBook";
            resources.ApplyResources(TsmiCreateBook, "TsmiCreateBook");
            TsmiCreateBook.Click += TsmiCreateBook_Click;
            // 
            // TsmiOpenBook
            // 
            TsmiOpenBook.Image = Properties.Icons.Open;
            TsmiOpenBook.Name = "TsmiOpenBook";
            resources.ApplyResources(TsmiOpenBook, "TsmiOpenBook");
            TsmiOpenBook.Click += TsmiOpenBook_Click;
            // 
            // TsmiCloseBook
            // 
            resources.ApplyResources(TsmiCloseBook, "TsmiCloseBook");
            TsmiCloseBook.Image = Properties.Icons.Cancel;
            TsmiCloseBook.Name = "TsmiCloseBook";
            TsmiCloseBook.Click += TsmiCloseBook_Click;
            // 
            // TsmiSave
            // 
            resources.ApplyResources(TsmiSave, "TsmiSave");
            TsmiSave.Image = Properties.Icons.Save;
            TsmiSave.Name = "TsmiSave";
            TsmiSave.Click += TsmiSave_Click;
            // 
            // TsmiSaveAs
            // 
            resources.ApplyResources(TsmiSaveAs, "TsmiSaveAs");
            TsmiSaveAs.Image = Properties.Icons.SaveAll;
            TsmiSaveAs.Name = "TsmiSaveAs";
            TsmiSaveAs.Click += TsmiSaveAs_Click;
            // 
            // TsmiShare
            // 
            resources.ApplyResources(TsmiShare, "TsmiShare");
            TsmiShare.Image = Properties.Icons.Share;
            TsmiShare.Name = "TsmiShare";
            TsmiShare.Click += TsmiShare_Click;
            // 
            // TsmiImport
            // 
            TsmiImport.Image = Properties.Icons.Import;
            TsmiImport.Name = "TsmiImport";
            resources.ApplyResources(TsmiImport, "TsmiImport");
            TsmiImport.Click += TsmiImport_Click;
            // 
            // TsmiExport
            // 
            resources.ApplyResources(TsmiExport, "TsmiExport");
            TsmiExport.Image = Properties.Icons.Export;
            TsmiExport.Name = "TsmiExport";
            TsmiExport.Click += TsmiExport_Click;
            // 
            // TsmiOpenInExplorer
            // 
            resources.ApplyResources(TsmiOpenInExplorer, "TsmiOpenInExplorer");
            TsmiOpenInExplorer.Image = Properties.Icons.Open;
            TsmiOpenInExplorer.Name = "TsmiOpenInExplorer";
            TsmiOpenInExplorer.Click += TsmiOpenInExplorer_Click;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            resources.ApplyResources(toolStripMenuItem7, "toolStripMenuItem7");
            // 
            // TsmiPrint
            // 
            resources.ApplyResources(TsmiPrint, "TsmiPrint");
            TsmiPrint.Image = Properties.Icons.Print;
            TsmiPrint.Name = "TsmiPrint";
            TsmiPrint.Click += TsmiPrint_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // TsmiExitApplication
            // 
            TsmiExitApplication.Image = Properties.Icons.DoorOpened;
            TsmiExitApplication.Name = "TsmiExitApplication";
            resources.ApplyResources(TsmiExitApplication, "TsmiExitApplication");
            TsmiExitApplication.Click += TsmiExitApplication_Click;
            // 
            // TsmiRootEdit
            // 
            TsmiRootEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { TsmiAddWord, TsmiEditWord, TsmiDeleteWord });
            TsmiRootEdit.Name = "TsmiRootEdit";
            resources.ApplyResources(TsmiRootEdit, "TsmiRootEdit");
            // 
            // TsmiAddWord
            // 
            resources.ApplyResources(TsmiAddWord, "TsmiAddWord");
            TsmiAddWord.Image = Properties.Icons.Plus;
            TsmiAddWord.Name = "TsmiAddWord";
            TsmiAddWord.Click += TsmiAddWord_Click;
            // 
            // TsmiEditWord
            // 
            resources.ApplyResources(TsmiEditWord, "TsmiEditWord");
            TsmiEditWord.Image = Properties.Icons.Edit;
            TsmiEditWord.Name = "TsmiEditWord";
            TsmiEditWord.Click += TsmiEditWord_Click;
            // 
            // TsmiDeleteWord
            // 
            resources.ApplyResources(TsmiDeleteWord, "TsmiDeleteWord");
            TsmiDeleteWord.Image = Properties.Icons.Delete;
            TsmiDeleteWord.Name = "TsmiDeleteWord";
            TsmiDeleteWord.Click += TsmiDeleteWord_Click;
            // 
            // TsmiRootTools
            // 
            TsmiRootTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { TsmiPractice, TsmiBookOptions, toolStripMenuItem4, TsmiMerge, toolStripMenuItem5, TsmiSpecialChar, toolStripMenuItem6, TsmiSettings });
            TsmiRootTools.Name = "TsmiRootTools";
            resources.ApplyResources(TsmiRootTools, "TsmiRootTools");
            // 
            // TsmiPractice
            // 
            resources.ApplyResources(TsmiPractice, "TsmiPractice");
            TsmiPractice.Image = Properties.Icons.LightningBolt;
            TsmiPractice.Name = "TsmiPractice";
            TsmiPractice.Click += TsmiPractice_Click;
            // 
            // TsmiBookOptions
            // 
            resources.ApplyResources(TsmiBookOptions, "TsmiBookOptions");
            TsmiBookOptions.Image = Properties.Icons.FileSettings;
            TsmiBookOptions.Name = "TsmiBookOptions";
            TsmiBookOptions.Click += TsmiBookOptions_Click;
            // 
            // TsmiMerge
            // 
            TsmiMerge.Image = Properties.Icons.MergeFiles;
            TsmiMerge.Name = "TsmiMerge";
            resources.ApplyResources(TsmiMerge, "TsmiMerge");
            TsmiMerge.Click += TsmiMerge_Click;
            // 
            // TsmiSpecialChar
            // 
            TsmiSpecialChar.Image = Properties.Icons.Alphabet;
            TsmiSpecialChar.Name = "TsmiSpecialChar";
            resources.ApplyResources(TsmiSpecialChar, "TsmiSpecialChar");
            TsmiSpecialChar.Click += TsmiSpecialChar_Click;
            // 
            // TsmiSettings
            // 
            TsmiSettings.Image = Properties.Icons.Settings;
            TsmiSettings.Name = "TsmiSettings";
            resources.ApplyResources(TsmiSettings, "TsmiSettings");
            TsmiSettings.Click += TsmiSettings_Click;
            // 
            // TsmiRootHelp
            // 
            TsmiRootHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { TsmiHelp, TsmiEvaluationInfo, toolStripMenuItem8, TsmiUpdate, infoToolStripMenuItem, TsmiAbout });
            TsmiRootHelp.Name = "TsmiRootHelp";
            resources.ApplyResources(TsmiRootHelp, "TsmiRootHelp");
            // 
            // TsmiHelp
            // 
            TsmiHelp.Image = Properties.Icons.Help;
            TsmiHelp.Name = "TsmiHelp";
            resources.ApplyResources(TsmiHelp, "TsmiHelp");
            TsmiHelp.Click += TsmiHelp_Click;
            // 
            // TsmiEvaluationInfo
            // 
            TsmiEvaluationInfo.Image = Properties.Icons.Info;
            TsmiEvaluationInfo.Name = "TsmiEvaluationInfo";
            resources.ApplyResources(TsmiEvaluationInfo, "TsmiEvaluationInfo");
            TsmiEvaluationInfo.Click += TsmiEvaluationInfo_Click;
            // 
            // TsmiUpdate
            // 
            TsmiUpdate.Image = Properties.Icons.Update;
            TsmiUpdate.Name = "TsmiUpdate";
            resources.ApplyResources(TsmiUpdate, "TsmiUpdate");
            TsmiUpdate.Click += TsmiUpdate_Click;
            // 
            // TsmiAbout
            // 
            TsmiAbout.Image = Properties.Icons.Info;
            TsmiAbout.Name = "TsmiAbout";
            resources.ApplyResources(TsmiAbout, "TsmiAbout");
            TsmiAbout.Click += TsmiAbout_Click;
            // 
            // TbSearchWord
            // 
            resources.ApplyResources(TbSearchWord, "TbSearchWord");
            TbSearchWord.Name = "TbSearchWord";
            TbSearchWord.TextChanged += TbSearchWord_TextChanged;
            // 
            // GroupBook
            // 
            GroupBook.Controls.Add(BtnBookSettings);
            GroupBook.Controls.Add(BtnPractice);
            resources.ApplyResources(GroupBook, "GroupBook");
            GroupBook.Name = "GroupBook";
            GroupBook.TabStop = false;
            // 
            // BtnBookSettings
            // 
            BtnBookSettings.BaseImage = Properties.Icons.FileSettings;
            resources.ApplyResources(BtnBookSettings, "BtnBookSettings");
            BtnBookSettings.Name = "BtnBookSettings";
            BtnBookSettings.UseVisualStyleBackColor = true;
            BtnBookSettings.Click += BtnBookSettings_Click;
            // 
            // BtnPractice
            // 
            BtnPractice.BaseImage = Properties.Icons.LightningBolt;
            resources.ApplyResources(BtnPractice, "BtnPractice");
            BtnPractice.Name = "BtnPractice";
            BtnPractice.UseVisualStyleBackColor = true;
            BtnPractice.Click += BtnPractice_Click;
            // 
            // GroupSearch
            // 
            GroupSearch.Controls.Add(TbSearchWord);
            GroupSearch.Controls.Add(BtnSearchWord);
            resources.ApplyResources(GroupSearch, "GroupSearch");
            GroupSearch.Name = "GroupSearch";
            GroupSearch.TabStop = false;
            // 
            // BtnSearchWord
            // 
            BtnSearchWord.BaseImage = Properties.Icons.Search;
            resources.ApplyResources(BtnSearchWord, "BtnSearchWord");
            BtnSearchWord.Name = "BtnSearchWord";
            BtnSearchWord.UseVisualStyleBackColor = true;
            BtnSearchWord.Click += BtnSearchWord_Click;
            // 
            // GroupWord
            // 
            GroupWord.Controls.Add(BtnAddWord);
            GroupWord.Controls.Add(BtnDeleteWord);
            GroupWord.Controls.Add(BtnEditWord);
            resources.ApplyResources(GroupWord, "GroupWord");
            GroupWord.Name = "GroupWord";
            GroupWord.TabStop = false;
            // 
            // BtnAddWord
            // 
            BtnAddWord.BaseImage = Properties.Icons.Plus;
            resources.ApplyResources(BtnAddWord, "BtnAddWord");
            BtnAddWord.Name = "BtnAddWord";
            BtnAddWord.UseVisualStyleBackColor = true;
            BtnAddWord.Click += BtnAddWord_Click;
            // 
            // BtnDeleteWord
            // 
            BtnDeleteWord.BaseImage = Properties.Icons.Delete;
            resources.ApplyResources(BtnDeleteWord, "BtnDeleteWord");
            BtnDeleteWord.Name = "BtnDeleteWord";
            BtnDeleteWord.UseVisualStyleBackColor = true;
            BtnDeleteWord.Click += BtnDeleteWord_Click;
            // 
            // BtnEditWord
            // 
            BtnEditWord.BaseImage = Properties.Icons.Edit;
            resources.ApplyResources(BtnEditWord, "BtnEditWord");
            BtnEditWord.Name = "BtnEditWord";
            BtnEditWord.UseVisualStyleBackColor = true;
            BtnEditWord.Click += BtnEditWord_Click;
            // 
            // StatusStrip
            // 
            StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { StatusLbOldVersion });
            resources.ApplyResources(StatusStrip, "StatusStrip");
            StatusStrip.Name = "StatusStrip";
            // 
            // StatusLbOldVersion
            // 
            StatusLbOldVersion.Image = Properties.Icons.Warning;
            StatusLbOldVersion.Name = "StatusLbOldVersion";
            StatusLbOldVersion.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            resources.ApplyResources(StatusLbOldVersion, "StatusLbOldVersion");
            StatusLbOldVersion.Click += StatusLbOldVersion_Click;
            // 
            // SideBar
            // 
            SideBar.Controls.Add(GroupStatistics);
            SideBar.Controls.Add(GroupBook);
            SideBar.Controls.Add(GroupWord);
            SideBar.Controls.Add(GroupSearch);
            resources.ApplyResources(SideBar, "SideBar");
            SideBar.Name = "SideBar";
            // 
            // GroupStatistics
            // 
            resources.ApplyResources(GroupStatistics, "GroupStatistics");
            GroupStatistics.Name = "GroupStatistics";
            // 
            // SplitContainer
            // 
            resources.ApplyResources(SplitContainer, "SplitContainer");
            SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            SplitContainer.Panel1.Controls.Add(tabControl1);
            // 
            // SplitContainer.Panel2
            // 
            SplitContainer.Panel2.Controls.Add(LbEmptyForm);
            SplitContainer.SplitterBaseDistance = 150;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            resources.ApplyResources(tabControl1, "tabControl1");
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(RecentFilesAvaloniaControlHost);
            resources.ApplyResources(tabPage1, "tabPage1");
            tabPage1.Name = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // RecentFilesAvaloniaControlHost
            // 
            RecentFilesAvaloniaControlHost.Content = null;
            resources.ApplyResources(RecentFilesAvaloniaControlHost, "RecentFilesAvaloniaControlHost");
            RecentFilesAvaloniaControlHost.Name = "RecentFilesAvaloniaControlHost";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(FileTreeView);
            resources.ApplyResources(tabPage2, "tabPage2");
            tabPage2.Name = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // FileTreeView
            // 
            resources.ApplyResources(FileTreeView, "FileTreeView");
            FileTreeView.FileFilter = "*.vhf";
            FileTreeView.Name = "FileTreeView";
            FileTreeView.SelectedPath = null;
            FileTreeView.FileSelected += FileTreeView_FileSelected;
            FileTreeView.BrowseClick += FileTreeView_BrowseClick;
            // 
            // LbEmptyForm
            // 
            LbEmptyForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(LbEmptyForm, "LbEmptyForm");
            LbEmptyForm.Name = "LbEmptyForm";
            // 
            // ToolStrip
            // 
            ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { TsbCreateBook, TsbOpenBook, TsbSave, toolStripSeparator1, TsbPrint, TsbEvaluationInfo, toolStripLabel });
            resources.ApplyResources(ToolStrip, "ToolStrip");
            ToolStrip.Name = "ToolStrip";
            // 
            // TsbCreateBook
            // 
            TsbCreateBook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            TsbCreateBook.Image = Properties.Icons.File;
            resources.ApplyResources(TsbCreateBook, "TsbCreateBook");
            TsbCreateBook.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
            TsbCreateBook.Name = "TsbCreateBook";
            TsbCreateBook.Click += TsbCreateBook_Click;
            // 
            // TsbOpenBook
            // 
            TsbOpenBook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            TsbOpenBook.Image = Properties.Icons.Open;
            resources.ApplyResources(TsbOpenBook, "TsbOpenBook");
            TsbOpenBook.Margin = new System.Windows.Forms.Padding(1);
            TsbOpenBook.Name = "TsbOpenBook";
            TsbOpenBook.Click += TsbOpenBook_Click;
            // 
            // TsbSave
            // 
            TsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(TsbSave, "TsbSave");
            TsbSave.Image = Properties.Icons.Save;
            TsbSave.Margin = new System.Windows.Forms.Padding(1);
            TsbSave.Name = "TsbSave";
            TsbSave.Click += TsbSave_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // TsbPrint
            // 
            TsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(TsbPrint, "TsbPrint");
            TsbPrint.Image = Properties.Icons.Print;
            TsbPrint.Margin = new System.Windows.Forms.Padding(1);
            TsbPrint.Name = "TsbPrint";
            TsbPrint.Click += TsbPrint_Click;
            // 
            // TsbEvaluationInfo
            // 
            TsbEvaluationInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            TsbEvaluationInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            TsbEvaluationInfo.Image = Properties.Icons.Info;
            resources.ApplyResources(TsbEvaluationInfo, "TsbEvaluationInfo");
            TsbEvaluationInfo.Margin = new System.Windows.Forms.Padding(1, 1, 2, 1);
            TsbEvaluationInfo.Name = "TsbEvaluationInfo";
            TsbEvaluationInfo.Click += TsbtnEvaluationInfo_Click;
            // 
            // toolStripLabel
            // 
            toolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStripLabel.Margin = new System.Windows.Forms.Padding(1);
            toolStripLabel.Name = "toolStripLabel";
            resources.ApplyResources(toolStripLabel, "toolStripLabel");
            // 
            // TableLayout
            // 
            resources.ApplyResources(TableLayout, "TableLayout");
            TableLayout.Controls.Add(SideBar, 1, 0);
            TableLayout.Controls.Add(SplitContainer, 0, 0);
            TableLayout.Name = "TableLayout";
            // 
            // MainForm
            // 
            AcceptButton = BtnAddWord;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(TableLayout);
            Controls.Add(StatusStrip);
            Controls.Add(ToolStrip);
            Controls.Add(MenuStrip);
            Icon = Properties.Icons.Icon;
            MainMenuStrip = MenuStrip;
            Name = "MainForm";
            FormClosing += Form_FormClosing;
            Load += Form_Load;
            Shown += Form_Shown;
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            GroupBook.ResumeLayout(false);
            GroupSearch.ResumeLayout(false);
            GroupSearch.PerformLayout();
            GroupWord.ResumeLayout(false);
            StatusStrip.ResumeLayout(false);
            StatusStrip.PerformLayout();
            SideBar.ResumeLayout(false);
            SplitContainer.Panel1.ResumeLayout(false);
            SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer).EndInit();
            SplitContainer.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ToolStrip.ResumeLayout(false);
            ToolStrip.PerformLayout();
            TableLayout.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.ResponsiveMenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem TsmiRootFile;
        private System.Windows.Forms.ToolStripMenuItem TsmiRootTools;
        private System.Windows.Forms.ToolStripMenuItem TsmiRootHelp;
        private Controls.ResponsiveToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripButton TsbOpenBook;
        private System.Windows.Forms.ToolStripButton TsbCreateBook;
        private System.Windows.Forms.ToolStripButton TsbSave;
        private System.Windows.Forms.ToolStripMenuItem TsmiCreateBook;
        private System.Windows.Forms.ToolStripMenuItem TsmiRootEdit;
        private System.Windows.Forms.ToolStripMenuItem TsmiOpenBook;
        private System.Windows.Forms.ToolStripMenuItem TsmiSave;
        private System.Windows.Forms.ToolStripMenuItem TsmiSaveAs;
        private System.Windows.Forms.ToolStripMenuItem TsmiPrint;
        private System.Windows.Forms.ToolStripMenuItem TsmiExitApplication;
        private System.Windows.Forms.ToolStripButton TsbPrint;
        private Vocup.Controls.ResponsiveButton BtnPractice;
        private Vocup.Controls.ResponsiveButton BtnSearchWord;
        private System.Windows.Forms.TextBox TbSearchWord;
        private System.Windows.Forms.GroupBox GroupBook;
        private System.Windows.Forms.GroupBox GroupSearch;
        private System.Windows.Forms.ToolStripMenuItem TsmiAddWord;
        private System.Windows.Forms.ToolStripMenuItem TsmiSettings;
        private System.Windows.Forms.ToolStripMenuItem TsmiEditWord;
        private System.Windows.Forms.ToolStripMenuItem TsmiDeleteWord;
        private System.Windows.Forms.ToolStripMenuItem TsmiPractice;
        private System.Windows.Forms.ToolStripMenuItem TsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem TsmiAbout;
        private Vocup.Controls.ResponsiveButton BtnEditWord;
        private Vocup.Controls.ResponsiveButton BtnDeleteWord;
        private Vocup.Controls.ResponsiveButton BtnAddWord;
        private System.Windows.Forms.GroupBox GroupWord;
        private System.Windows.Forms.ToolStripButton TsbEvaluationInfo;
        private System.Windows.Forms.ToolStripLabel toolStripLabel;
        private Vocup.Controls.ResponsiveButton BtnBookSettings;
        private System.Windows.Forms.ToolStripMenuItem TsmiBookOptions;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem TsmiUpdate;
        private System.Windows.Forms.ToolStripMenuItem TsmiMerge;
        private System.Windows.Forms.ToolStripMenuItem TsmiImport;
        private System.Windows.Forms.ToolStripMenuItem TsmiExport;
        private System.Windows.Forms.ToolStripMenuItem TsmiOpenInExplorer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem TsmiSpecialChar;
        private System.Windows.Forms.Panel SideBar;
        private Controls.ResponsiveSplitContainer SplitContainer;
        private System.Windows.Forms.ToolStripMenuItem TsmiCloseBook;
        private System.Windows.Forms.ToolStripMenuItem TsmiEvaluationInfo;
        private Controls.StatisticsPanel GroupStatistics;
        private System.Windows.Forms.TableLayoutPanel TableLayout;
        private Controls.FileTreeView FileTreeView;
        private System.Windows.Forms.Label LbEmptyForm;
        private System.Windows.Forms.ToolStripStatusLabel StatusLbOldVersion;
        private System.Windows.Forms.ToolStripMenuItem TsmiShare;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Avalonia.Win32.Interoperability.WinFormsAvaloniaControlHost RecentFilesAvaloniaControlHost;
    }
}

