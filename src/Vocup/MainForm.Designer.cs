﻿namespace Vocup
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
            this.MenuStrip = new Vocup.Controls.ResponsiveMenuStrip();
            this.TsmiRootFile = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiCreateBook = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiOpenBook = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiCloseBook = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiImportAnsi = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiOpenInExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiExitApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiRootEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiAddWord = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiEditWord = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiDeleteWord = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiRootTools = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiPractice = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiBookOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiMerge = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiSpecialChar = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiRootHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiEvaluationInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.TbSearchWord = new System.Windows.Forms.TextBox();
            this.GroupBook = new System.Windows.Forms.GroupBox();
            this.BtnBookSettings = new Vocup.Controls.ResponsiveButton();
            this.BtnPractice = new Vocup.Controls.ResponsiveButton();
            this.GroupSearch = new System.Windows.Forms.GroupBox();
            this.BtnSearchWord = new Vocup.Controls.ResponsiveButton();
            this.GroupWord = new System.Windows.Forms.GroupBox();
            this.BtnAddWord = new Vocup.Controls.ResponsiveButton();
            this.BtnDeleteWord = new Vocup.Controls.ResponsiveButton();
            this.BtnEditWord = new Vocup.Controls.ResponsiveButton();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLbOldVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.SideBar = new System.Windows.Forms.Panel();
            this.GroupStatistics = new Vocup.Controls.StatisticsPanel();
            this.SplitContainer = new Vocup.Controls.ResponsiveSplitContainer();
            this.FileTreeView = new Vocup.Controls.FileTreeView();
            this.LbEmptyForm = new System.Windows.Forms.Label();
            this.ToolStrip = new Vocup.Controls.ResponsiveToolStrip();
            this.TsbCreateBook = new System.Windows.Forms.ToolStripButton();
            this.TsbOpenBook = new System.Windows.Forms.ToolStripButton();
            this.TsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbPrint = new System.Windows.Forms.ToolStripButton();
            this.TsbEvaluationInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.TableLayout = new System.Windows.Forms.TableLayoutPanel();
            toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            infoToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStrip.SuspendLayout();
            this.GroupBook.SuspendLayout();
            this.GroupSearch.SuspendLayout();
            this.GroupWord.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.SideBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.ToolStrip.SuspendLayout();
            this.TableLayout.SuspendLayout();
            this.SuspendLayout();
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
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiRootFile,
            this.TsmiRootEdit,
            this.TsmiRootTools,
            this.TsmiRootHelp});
            resources.ApplyResources(this.MenuStrip, "MenuStrip");
            this.MenuStrip.Name = "MenuStrip";
            // 
            // TsmiRootFile
            // 
            this.TsmiRootFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiCreateBook,
            this.TsmiOpenBook,
            this.TsmiCloseBook,
            toolStripMenuItem11,
            this.TsmiSave,
            this.TsmiSaveAs,
            toolStripMenuItem10,
            this.TsmiImport,
            this.TsmiImportAnsi,
            this.TsmiExport,
            toolStripMenuItem9,
            this.TsmiOpenInExplorer,
            this.toolStripMenuItem7,
            this.TsmiPrint,
            this.toolStripMenuItem3,
            this.TsmiExitApplication});
            this.TsmiRootFile.Name = "TsmiRootFile";
            resources.ApplyResources(this.TsmiRootFile, "TsmiRootFile");
            // 
            // TsmiCreateBook
            // 
            this.TsmiCreateBook.Image = global::Vocup.Properties.Icons.File;
            this.TsmiCreateBook.Name = "TsmiCreateBook";
            resources.ApplyResources(this.TsmiCreateBook, "TsmiCreateBook");
            // 
            // TsmiOpenBook
            // 
            this.TsmiOpenBook.Image = global::Vocup.Properties.Icons.Open;
            this.TsmiOpenBook.Name = "TsmiOpenBook";
            resources.ApplyResources(this.TsmiOpenBook, "TsmiOpenBook");
            // 
            // TsmiCloseBook
            // 
            resources.ApplyResources(this.TsmiCloseBook, "TsmiCloseBook");
            this.TsmiCloseBook.Image = global::Vocup.Properties.Icons.Cancel;
            this.TsmiCloseBook.Name = "TsmiCloseBook";
            // 
            // TsmiSave
            // 
            resources.ApplyResources(this.TsmiSave, "TsmiSave");
            this.TsmiSave.Image = global::Vocup.Properties.Icons.Save;
            this.TsmiSave.Name = "TsmiSave";
            // 
            // TsmiSaveAs
            // 
            resources.ApplyResources(this.TsmiSaveAs, "TsmiSaveAs");
            this.TsmiSaveAs.Image = global::Vocup.Properties.Icons.SaveAll;
            this.TsmiSaveAs.Name = "TsmiSaveAs";
            this.TsmiSaveAs.Click += new System.EventHandler(this.TsmiSaveAs_Click);
            // 
            // TsmiImport
            // 
            this.TsmiImport.Image = global::Vocup.Properties.Icons.Import;
            this.TsmiImport.Name = "TsmiImport";
            resources.ApplyResources(this.TsmiImport, "TsmiImport");
            this.TsmiImport.Click += new System.EventHandler(this.TsmiImport_Click);
            // 
            // TsmiImportAnsi
            // 
            this.TsmiImportAnsi.Image = global::Vocup.Properties.Icons.Import;
            this.TsmiImportAnsi.Name = "TsmiImportAnsi";
            resources.ApplyResources(this.TsmiImportAnsi, "TsmiImportAnsi");
            this.TsmiImportAnsi.Click += new System.EventHandler(this.TsmiImportAnsi_Click);
            // 
            // TsmiExport
            // 
            resources.ApplyResources(this.TsmiExport, "TsmiExport");
            this.TsmiExport.Image = global::Vocup.Properties.Icons.Export;
            this.TsmiExport.Name = "TsmiExport";
            this.TsmiExport.Click += new System.EventHandler(this.TsmiExport_Click);
            // 
            // TsmiOpenInExplorer
            // 
            resources.ApplyResources(this.TsmiOpenInExplorer, "TsmiOpenInExplorer");
            this.TsmiOpenInExplorer.Image = global::Vocup.Properties.Icons.Open;
            this.TsmiOpenInExplorer.Name = "TsmiOpenInExplorer";
            this.TsmiOpenInExplorer.Click += new System.EventHandler(this.TsmiOpenInExplorer_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
            // 
            // TsmiPrint
            // 
            resources.ApplyResources(this.TsmiPrint, "TsmiPrint");
            this.TsmiPrint.Image = global::Vocup.Properties.Icons.Print;
            this.TsmiPrint.Name = "TsmiPrint";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // TsmiExitApplication
            // 
            this.TsmiExitApplication.Image = global::Vocup.Properties.Icons.DoorOpened;
            this.TsmiExitApplication.Name = "TsmiExitApplication";
            resources.ApplyResources(this.TsmiExitApplication, "TsmiExitApplication");
            this.TsmiExitApplication.Click += new System.EventHandler(this.TsmiExitApplication_Click);
            // 
            // TsmiRootEdit
            // 
            this.TsmiRootEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiAddWord,
            this.TsmiEditWord,
            this.TsmiDeleteWord});
            this.TsmiRootEdit.Name = "TsmiRootEdit";
            resources.ApplyResources(this.TsmiRootEdit, "TsmiRootEdit");
            // 
            // TsmiAddWord
            // 
            resources.ApplyResources(this.TsmiAddWord, "TsmiAddWord");
            this.TsmiAddWord.Image = global::Vocup.Properties.Icons.Plus;
            this.TsmiAddWord.Name = "TsmiAddWord";
            this.TsmiAddWord.Click += new System.EventHandler(this.TsmiAddWord_Click);
            // 
            // TsmiEditWord
            // 
            resources.ApplyResources(this.TsmiEditWord, "TsmiEditWord");
            this.TsmiEditWord.Image = global::Vocup.Properties.Icons.Edit;
            this.TsmiEditWord.Name = "TsmiEditWord";
            this.TsmiEditWord.Click += new System.EventHandler(this.TsmiEditWord_Click);
            // 
            // TsmiDeleteWord
            // 
            resources.ApplyResources(this.TsmiDeleteWord, "TsmiDeleteWord");
            this.TsmiDeleteWord.Image = global::Vocup.Properties.Icons.Delete;
            this.TsmiDeleteWord.Name = "TsmiDeleteWord";
            this.TsmiDeleteWord.Click += new System.EventHandler(this.TsmiDeleteWord_Click);
            // 
            // TsmiRootTools
            // 
            this.TsmiRootTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiPractice,
            this.TsmiBookOptions,
            toolStripMenuItem4,
            this.TsmiMerge,
            toolStripMenuItem5,
            this.TsmiSpecialChar,
            toolStripMenuItem6,
            this.TsmiSettings});
            this.TsmiRootTools.Name = "TsmiRootTools";
            resources.ApplyResources(this.TsmiRootTools, "TsmiRootTools");
            // 
            // TsmiPractice
            // 
            resources.ApplyResources(this.TsmiPractice, "TsmiPractice");
            this.TsmiPractice.Image = global::Vocup.Properties.Icons.LightningBolt;
            this.TsmiPractice.Name = "TsmiPractice";
            // 
            // TsmiBookOptions
            // 
            resources.ApplyResources(this.TsmiBookOptions, "TsmiBookOptions");
            this.TsmiBookOptions.Image = global::Vocup.Properties.Icons.FileSettings;
            this.TsmiBookOptions.Name = "TsmiBookOptions";
            // 
            // TsmiMerge
            // 
            this.TsmiMerge.Image = global::Vocup.Properties.Icons.MergeFiles;
            this.TsmiMerge.Name = "TsmiMerge";
            resources.ApplyResources(this.TsmiMerge, "TsmiMerge");
            this.TsmiMerge.Click += new System.EventHandler(this.TsmiMerge_Click);
            // 
            // TsmiSpecialChar
            // 
            this.TsmiSpecialChar.Image = global::Vocup.Properties.Icons.Alphabet;
            this.TsmiSpecialChar.Name = "TsmiSpecialChar";
            resources.ApplyResources(this.TsmiSpecialChar, "TsmiSpecialChar");
            this.TsmiSpecialChar.Click += new System.EventHandler(this.TsmiSpecialChar_Click);
            // 
            // TsmiSettings
            // 
            this.TsmiSettings.Image = global::Vocup.Properties.Icons.Settings;
            this.TsmiSettings.Name = "TsmiSettings";
            resources.ApplyResources(this.TsmiSettings, "TsmiSettings");
            this.TsmiSettings.Click += new System.EventHandler(this.TsmiSettings_Click);
            // 
            // TsmiRootHelp
            // 
            this.TsmiRootHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiHelp,
            this.TsmiEvaluationInfo,
            toolStripMenuItem8,
            this.TsmiUpdate,
            infoToolStripMenuItem,
            this.TsmiAbout});
            this.TsmiRootHelp.Name = "TsmiRootHelp";
            resources.ApplyResources(this.TsmiRootHelp, "TsmiRootHelp");
            // 
            // TsmiHelp
            // 
            this.TsmiHelp.Image = global::Vocup.Properties.Icons.Help;
            this.TsmiHelp.Name = "TsmiHelp";
            resources.ApplyResources(this.TsmiHelp, "TsmiHelp");
            this.TsmiHelp.Click += new System.EventHandler(this.TsmiHelp_Click);
            // 
            // TsmiEvaluationInfo
            // 
            this.TsmiEvaluationInfo.Image = global::Vocup.Properties.Icons.Info;
            this.TsmiEvaluationInfo.Name = "TsmiEvaluationInfo";
            resources.ApplyResources(this.TsmiEvaluationInfo, "TsmiEvaluationInfo");
            this.TsmiEvaluationInfo.Click += new System.EventHandler(this.TsmiEvaluationInfo_Click);
            // 
            // TsmiUpdate
            // 
            this.TsmiUpdate.Image = global::Vocup.Properties.Icons.Update;
            this.TsmiUpdate.Name = "TsmiUpdate";
            resources.ApplyResources(this.TsmiUpdate, "TsmiUpdate");
            this.TsmiUpdate.Click += new System.EventHandler(this.TsmiUpdate_Click);
            // 
            // TsmiAbout
            // 
            this.TsmiAbout.Image = global::Vocup.Properties.Icons.Info;
            this.TsmiAbout.Name = "TsmiAbout";
            resources.ApplyResources(this.TsmiAbout, "TsmiAbout");
            this.TsmiAbout.Click += new System.EventHandler(this.TsmiAbout_Click);
            // 
            // TbSearchWord
            // 
            resources.ApplyResources(this.TbSearchWord, "TbSearchWord");
            this.TbSearchWord.Name = "TbSearchWord";
            this.TbSearchWord.TextChanged += new System.EventHandler(this.TbSearchWord_TextChanged);
            // 
            // GroupBook
            // 
            this.GroupBook.Controls.Add(this.BtnBookSettings);
            this.GroupBook.Controls.Add(this.BtnPractice);
            resources.ApplyResources(this.GroupBook, "GroupBook");
            this.GroupBook.Name = "GroupBook";
            this.GroupBook.TabStop = false;
            // 
            // BtnBookSettings
            // 
            this.BtnBookSettings.BaseImage = global::Vocup.Properties.Icons.FileSettings;
            resources.ApplyResources(this.BtnBookSettings, "BtnBookSettings");
            this.BtnBookSettings.Name = "BtnBookSettings";
            this.BtnBookSettings.UseVisualStyleBackColor = true;
            // 
            // BtnPractice
            // 
            this.BtnPractice.BaseImage = global::Vocup.Properties.Icons.LightningBolt;
            resources.ApplyResources(this.BtnPractice, "BtnPractice");
            this.BtnPractice.Name = "BtnPractice";
            this.BtnPractice.UseVisualStyleBackColor = true;
            // 
            // GroupSearch
            // 
            this.GroupSearch.Controls.Add(this.TbSearchWord);
            this.GroupSearch.Controls.Add(this.BtnSearchWord);
            resources.ApplyResources(this.GroupSearch, "GroupSearch");
            this.GroupSearch.Name = "GroupSearch";
            this.GroupSearch.TabStop = false;
            // 
            // BtnSearchWord
            // 
            this.BtnSearchWord.BaseImage = global::Vocup.Properties.Icons.Search;
            resources.ApplyResources(this.BtnSearchWord, "BtnSearchWord");
            this.BtnSearchWord.Name = "BtnSearchWord";
            this.BtnSearchWord.UseVisualStyleBackColor = true;
            this.BtnSearchWord.Click += new System.EventHandler(this.BtnSearchWord_Click);
            // 
            // GroupWord
            // 
            this.GroupWord.Controls.Add(this.BtnAddWord);
            this.GroupWord.Controls.Add(this.BtnDeleteWord);
            this.GroupWord.Controls.Add(this.BtnEditWord);
            resources.ApplyResources(this.GroupWord, "GroupWord");
            this.GroupWord.Name = "GroupWord";
            this.GroupWord.TabStop = false;
            // 
            // BtnAddWord
            // 
            this.BtnAddWord.BaseImage = global::Vocup.Properties.Icons.Plus;
            resources.ApplyResources(this.BtnAddWord, "BtnAddWord");
            this.BtnAddWord.Name = "BtnAddWord";
            this.BtnAddWord.UseVisualStyleBackColor = true;
            this.BtnAddWord.Click += new System.EventHandler(this.BtnAddWord_Click);
            // 
            // BtnDeleteWord
            // 
            this.BtnDeleteWord.BaseImage = global::Vocup.Properties.Icons.Delete;
            resources.ApplyResources(this.BtnDeleteWord, "BtnDeleteWord");
            this.BtnDeleteWord.Name = "BtnDeleteWord";
            this.BtnDeleteWord.UseVisualStyleBackColor = true;
            this.BtnDeleteWord.Click += new System.EventHandler(this.BtnDeleteWord_Click);
            // 
            // BtnEditWord
            // 
            this.BtnEditWord.BaseImage = global::Vocup.Properties.Icons.Edit;
            resources.ApplyResources(this.BtnEditWord, "BtnEditWord");
            this.BtnEditWord.Name = "BtnEditWord";
            this.BtnEditWord.UseVisualStyleBackColor = true;
            this.BtnEditWord.Click += new System.EventHandler(this.BtnEditWord_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLbOldVersion});
            resources.ApplyResources(this.StatusStrip, "StatusStrip");
            this.StatusStrip.Name = "StatusStrip";
            // 
            // StatusLbOldVersion
            // 
            this.StatusLbOldVersion.Image = global::Vocup.Properties.Icons.Warning;
            this.StatusLbOldVersion.Name = "StatusLbOldVersion";
            this.StatusLbOldVersion.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            resources.ApplyResources(this.StatusLbOldVersion, "StatusLbOldVersion");
            this.StatusLbOldVersion.Click += new System.EventHandler(this.StatusLbOldVersion_Click);
            // 
            // SideBar
            // 
            this.SideBar.Controls.Add(this.GroupStatistics);
            this.SideBar.Controls.Add(this.GroupBook);
            this.SideBar.Controls.Add(this.GroupWord);
            this.SideBar.Controls.Add(this.GroupSearch);
            resources.ApplyResources(this.SideBar, "SideBar");
            this.SideBar.Name = "SideBar";
            // 
            // GroupStatistics
            // 
            this.GroupStatistics.CorrectlyPracticed = 0;
            this.GroupStatistics.FullyPracticed = 0;
            resources.ApplyResources(this.GroupStatistics, "GroupStatistics");
            this.GroupStatistics.Name = "GroupStatistics";
            this.GroupStatistics.Unpracticed = 0;
            this.GroupStatistics.WronglyPracticed = 0;
            // 
            // SplitContainer
            // 
            resources.ApplyResources(this.SplitContainer, "SplitContainer");
            this.SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.FileTreeView);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.LbEmptyForm);
            this.SplitContainer.SplitterBaseDistance = 150;
            // 
            // FileTreeView
            // 
            this.FileTreeView.BrowseButtonVisible = true;
            resources.ApplyResources(this.FileTreeView, "FileTreeView");
            this.FileTreeView.FileFilter = "*.vhf";
            this.FileTreeView.Name = "FileTreeView";
            this.FileTreeView.SelectedPath = null;
            this.FileTreeView.FileSelected += new System.EventHandler<Vocup.Controls.FileSelectedEventArgs>(this.FileTreeView_FileSelected);
            this.FileTreeView.BrowseClick += new System.EventHandler(this.FileTreeView_BrowseClick);
            // 
            // LbEmptyForm
            // 
            this.LbEmptyForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.LbEmptyForm, "LbEmptyForm");
            this.LbEmptyForm.Name = "LbEmptyForm";
            // 
            // ToolStrip
            // 
            this.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbCreateBook,
            this.TsbOpenBook,
            this.TsbSave,
            this.toolStripSeparator1,
            this.TsbPrint,
            this.TsbEvaluationInfo,
            this.toolStripLabel});
            resources.ApplyResources(this.ToolStrip, "ToolStrip");
            this.ToolStrip.Name = "ToolStrip";
            // 
            // TsbCreateBook
            // 
            this.TsbCreateBook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbCreateBook.Image = global::Vocup.Properties.Icons.File;
            resources.ApplyResources(this.TsbCreateBook, "TsbCreateBook");
            this.TsbCreateBook.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
            this.TsbCreateBook.Name = "TsbCreateBook";
            // 
            // TsbOpenBook
            // 
            this.TsbOpenBook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbOpenBook.Image = global::Vocup.Properties.Icons.Open;
            resources.ApplyResources(this.TsbOpenBook, "TsbOpenBook");
            this.TsbOpenBook.Margin = new System.Windows.Forms.Padding(1);
            this.TsbOpenBook.Name = "TsbOpenBook";
            // 
            // TsbSave
            // 
            this.TsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TsbSave, "TsbSave");
            this.TsbSave.Image = global::Vocup.Properties.Icons.Save;
            this.TsbSave.Margin = new System.Windows.Forms.Padding(1);
            this.TsbSave.Name = "TsbSave";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // TsbPrint
            // 
            this.TsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TsbPrint, "TsbPrint");
            this.TsbPrint.Image = global::Vocup.Properties.Icons.Print;
            this.TsbPrint.Margin = new System.Windows.Forms.Padding(1);
            this.TsbPrint.Name = "TsbPrint";
            // 
            // TsbEvaluationInfo
            // 
            this.TsbEvaluationInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsbEvaluationInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbEvaluationInfo.Image = global::Vocup.Properties.Icons.Info;
            resources.ApplyResources(this.TsbEvaluationInfo, "TsbEvaluationInfo");
            this.TsbEvaluationInfo.Margin = new System.Windows.Forms.Padding(1, 1, 2, 1);
            this.TsbEvaluationInfo.Name = "TsbEvaluationInfo";
            this.TsbEvaluationInfo.Click += new System.EventHandler(this.TsbtnEvaluationInfo_Click);
            // 
            // toolStripLabel
            // 
            this.toolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripLabel.Name = "toolStripLabel";
            resources.ApplyResources(this.toolStripLabel, "toolStripLabel");
            // 
            // TableLayout
            // 
            resources.ApplyResources(this.TableLayout, "TableLayout");
            this.TableLayout.Controls.Add(this.SideBar, 1, 0);
            this.TableLayout.Controls.Add(this.SplitContainer, 0, 0);
            this.TableLayout.Name = "TableLayout";
            // 
            // MainForm
            // 
            this.AcceptButton = this.BtnAddWord;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayout);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.ToolStrip);
            this.Controls.Add(this.MenuStrip);
            this.Icon = global::Vocup.Properties.Icons.Icon;
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.GroupBook.ResumeLayout(false);
            this.GroupSearch.ResumeLayout(false);
            this.GroupSearch.PerformLayout();
            this.GroupWord.ResumeLayout(false);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.SideBar.ResumeLayout(false);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.TableLayout.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ToolStripMenuItem TsmiImportAnsi;
    }
}

