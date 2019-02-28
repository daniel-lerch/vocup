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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MenuStrip = new Vocup.Controls.ResponsiveMenuStrip();
            this.TsmiRootFile = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiCreateBook = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiOpenBook = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiCloseBook = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiOpenInExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiBackupCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiBackupRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiExitApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiRootEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiAddWord = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiEditWord = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiDeleteWord = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiRootExtras = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiPractice = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiBookOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiMerge = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiSpecialChar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiRootHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiEvaluationInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
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
            this.listView_vokabeln = new System.Windows.Forms.ListView();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.SideBar = new System.Windows.Forms.Panel();
            this.GroupStatistics = new Vocup.Controls.StatisticsPanel();
            this.SplitContainer = new Vocup.Controls.ResponsiveSplitContainer();
            this.FileTreeView = new Vocup.Controls.FileTreeView();
            this.ToolStrip = new Vocup.Controls.ResponsiveToolStrip();
            this.TsbCreateBook = new System.Windows.Forms.ToolStripButton();
            this.TsbOpenBook = new System.Windows.Forms.ToolStripButton();
            this.TsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbPrint = new System.Windows.Forms.ToolStripButton();
            this.TsbEvaluationInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.TableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.MenuStrip.SuspendLayout();
            this.GroupBook.SuspendLayout();
            this.GroupSearch.SuspendLayout();
            this.GroupWord.SuspendLayout();
            this.SideBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.ToolStrip.SuspendLayout();
            this.TableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiRootFile,
            this.TsmiRootEdit,
            this.TsmiRootExtras,
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
            this.toolStripMenuItem11,
            this.TsmiSave,
            this.TsmiSaveAs,
            this.toolStripMenuItem10,
            this.TsmiImport,
            this.TsmiExport,
            this.toolStripMenuItem9,
            this.TsmiOpenInExplorer,
            this.toolStripMenuItem7,
            this.TsmiPrint,
            this.toolStripMenuItem1,
            this.TsmiBackup,
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
            this.TsmiCreateBook.Click += new System.EventHandler(this.TsmiCreateBook_Click);
            // 
            // TsmiOpenBook
            // 
            this.TsmiOpenBook.Image = global::Vocup.Properties.Icons.Open;
            this.TsmiOpenBook.Name = "TsmiOpenBook";
            resources.ApplyResources(this.TsmiOpenBook, "TsmiOpenBook");
            this.TsmiOpenBook.Click += new System.EventHandler(this.TsmiOpenBook_Click);
            // 
            // TsmiCloseBook
            // 
            resources.ApplyResources(this.TsmiCloseBook, "TsmiCloseBook");
            this.TsmiCloseBook.Image = global::Vocup.Properties.Icons.Cancel;
            this.TsmiCloseBook.Name = "TsmiCloseBook";
            this.TsmiCloseBook.Click += new System.EventHandler(this.TsmiCloseBook_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            resources.ApplyResources(this.toolStripMenuItem11, "toolStripMenuItem11");
            // 
            // TsmiSave
            // 
            resources.ApplyResources(this.TsmiSave, "TsmiSave");
            this.TsmiSave.Image = global::Vocup.Properties.Icons.Save;
            this.TsmiSave.Name = "TsmiSave";
            this.TsmiSave.Click += new System.EventHandler(this.TsmiSave_Click);
            // 
            // TsmiSaveAs
            // 
            resources.ApplyResources(this.TsmiSaveAs, "TsmiSaveAs");
            this.TsmiSaveAs.Image = global::Vocup.Properties.Icons.SaveAll;
            this.TsmiSaveAs.Name = "TsmiSaveAs";
            this.TsmiSaveAs.Click += new System.EventHandler(this.TsmiSaveAs_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            resources.ApplyResources(this.toolStripMenuItem10, "toolStripMenuItem10");
            // 
            // TsmiImport
            // 
            this.TsmiImport.Image = global::Vocup.Properties.Icons.Import;
            this.TsmiImport.Name = "TsmiImport";
            resources.ApplyResources(this.TsmiImport, "TsmiImport");
            this.TsmiImport.Click += new System.EventHandler(this.TsmiImport_Click);
            // 
            // TsmiExport
            // 
            resources.ApplyResources(this.TsmiExport, "TsmiExport");
            this.TsmiExport.Image = global::Vocup.Properties.Icons.Export;
            this.TsmiExport.Name = "TsmiExport";
            this.TsmiExport.Click += new System.EventHandler(this.TsmiExport_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            resources.ApplyResources(this.toolStripMenuItem9, "toolStripMenuItem9");
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
            this.TsmiPrint.Click += new System.EventHandler(this.TsmiPrint_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // TsmiBackup
            // 
            this.TsmiBackup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiBackupCreate,
            this.TsmiBackupRestore});
            this.TsmiBackup.Image = global::Vocup.Properties.Icons.Database;
            this.TsmiBackup.Name = "TsmiBackup";
            resources.ApplyResources(this.TsmiBackup, "TsmiBackup");
            // 
            // TsmiBackupCreate
            // 
            this.TsmiBackupCreate.Image = global::Vocup.Properties.Icons.DatabaseAdd;
            this.TsmiBackupCreate.Name = "TsmiBackupCreate";
            resources.ApplyResources(this.TsmiBackupCreate, "TsmiBackupCreate");
            this.TsmiBackupCreate.Click += new System.EventHandler(this.TsmiBackupCreate_Click);
            // 
            // TsmiBackupRestore
            // 
            this.TsmiBackupRestore.Image = global::Vocup.Properties.Icons.DatabaseRestore;
            this.TsmiBackupRestore.Name = "TsmiBackupRestore";
            resources.ApplyResources(this.TsmiBackupRestore, "TsmiBackupRestore");
            this.TsmiBackupRestore.Click += new System.EventHandler(this.TsmiBackupRestore_Click);
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
            this.TsmiExitApplication.Click += new System.EventHandler(this.TsmiExitAppliaction_Click);
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
            // TsmiRootExtras
            // 
            this.TsmiRootExtras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiPractice,
            this.TsmiBookOptions,
            this.toolStripMenuItem4,
            this.TsmiMerge,
            this.toolStripMenuItem5,
            this.TsmiSpecialChar,
            this.toolStripMenuItem6,
            this.TsmiSettings});
            this.TsmiRootExtras.Name = "TsmiRootExtras";
            resources.ApplyResources(this.TsmiRootExtras, "TsmiRootExtras");
            // 
            // TsmiPractice
            // 
            resources.ApplyResources(this.TsmiPractice, "TsmiPractice");
            this.TsmiPractice.Image = global::Vocup.Properties.Icons.LightningBolt;
            this.TsmiPractice.Name = "TsmiPractice";
            this.TsmiPractice.Click += new System.EventHandler(this.TsmiPractice_Click);
            // 
            // TsmiBookOptions
            // 
            resources.ApplyResources(this.TsmiBookOptions, "TsmiBookOptions");
            this.TsmiBookOptions.Image = global::Vocup.Properties.Icons.FileSettings;
            this.TsmiBookOptions.Name = "TsmiBookOptions";
            this.TsmiBookOptions.Click += new System.EventHandler(this.TsmiBookOptions_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // TsmiMerge
            // 
            this.TsmiMerge.Image = global::Vocup.Properties.Icons.MergeFiles;
            this.TsmiMerge.Name = "TsmiMerge";
            resources.ApplyResources(this.TsmiMerge, "TsmiMerge");
            this.TsmiMerge.Click += new System.EventHandler(this.TsmiMerge_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            // 
            // TsmiSpecialChar
            // 
            this.TsmiSpecialChar.Image = global::Vocup.Properties.Icons.Alphabet;
            this.TsmiSpecialChar.Name = "TsmiSpecialChar";
            resources.ApplyResources(this.TsmiSpecialChar, "TsmiSpecialChar");
            this.TsmiSpecialChar.Click += new System.EventHandler(this.TsmiSpecialChar_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
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
            this.toolStripMenuItem8,
            this.TsmiUpdate,
            this.infoToolStripMenuItem,
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
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
            // 
            // TsmiUpdate
            // 
            this.TsmiUpdate.Image = global::Vocup.Properties.Icons.Update;
            this.TsmiUpdate.Name = "TsmiUpdate";
            resources.ApplyResources(this.TsmiUpdate, "TsmiUpdate");
            this.TsmiUpdate.Click += new System.EventHandler(this.TsmiUpdate_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            resources.ApplyResources(this.infoToolStripMenuItem, "infoToolStripMenuItem");
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
            this.BtnBookSettings.Click += new System.EventHandler(this.BtnBookSettings_Click);
            // 
            // BtnPractice
            // 
            this.BtnPractice.BaseImage = global::Vocup.Properties.Icons.LightningBolt;
            resources.ApplyResources(this.BtnPractice, "BtnPractice");
            this.BtnPractice.Name = "BtnPractice";
            this.BtnPractice.UseVisualStyleBackColor = true;
            this.BtnPractice.Click += new System.EventHandler(this.BtnPractice_Click);
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
            // listView_vokabeln
            // 
            this.listView_vokabeln.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.listView_vokabeln, "listView_vokabeln");
            this.listView_vokabeln.FullRowSelect = true;
            this.listView_vokabeln.GridLines = true;
            this.listView_vokabeln.HideSelection = false;
            this.listView_vokabeln.MultiSelect = false;
            this.listView_vokabeln.Name = "listView_vokabeln";
            this.listView_vokabeln.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.listView_vokabeln.UseCompatibleStateImageBehavior = false;
            this.listView_vokabeln.View = System.Windows.Forms.View.Details;
            // 
            // StatusStrip
            // 
            resources.ApplyResources(this.StatusStrip, "StatusStrip");
            this.StatusStrip.Name = "StatusStrip";
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
            this.SplitContainer.Panel2.Controls.Add(this.listView_vokabeln);
            this.SplitContainer.SplitterBaseDistance = 150;
            // 
            // FileTreeView
            // 
            resources.ApplyResources(this.FileTreeView, "FileTreeView");
            this.FileTreeView.FileFilter = "*.vhf";
            this.FileTreeView.Name = "FileTreeView";
            this.FileTreeView.SelectedPath = null;
            this.FileTreeView.FileSelected += new System.EventHandler<Vocup.Controls.FileSelectedEventArgs>(this.FileTreeView_FileSelected);
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
            this.TsbCreateBook.Click += new System.EventHandler(this.TsbCreateBook_Click);
            // 
            // TsbOpenBook
            // 
            this.TsbOpenBook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbOpenBook.Image = global::Vocup.Properties.Icons.Open;
            resources.ApplyResources(this.TsbOpenBook, "TsbOpenBook");
            this.TsbOpenBook.Margin = new System.Windows.Forms.Padding(1);
            this.TsbOpenBook.Name = "TsbOpenBook";
            this.TsbOpenBook.Click += new System.EventHandler(this.TsbOpenBook_Click);
            // 
            // TsbSave
            // 
            this.TsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TsbSave, "TsbSave");
            this.TsbSave.Image = global::Vocup.Properties.Icons.Save;
            this.TsbSave.Margin = new System.Windows.Forms.Padding(1);
            this.TsbSave.Name = "TsbSave";
            this.TsbSave.Click += new System.EventHandler(this.TsbSave_Click);
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
            this.TsbPrint.Click += new System.EventHandler(this.TsbPrint_Click);
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
            // program_form
            // 
            this.AcceptButton = this.BtnAddWord;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayout);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.ToolStrip);
            this.Controls.Add(this.MenuStrip);
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "program_form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.GroupBook.ResumeLayout(false);
            this.GroupSearch.ResumeLayout(false);
            this.GroupSearch.PerformLayout();
            this.GroupWord.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem TsmiRootExtras;
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
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
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
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem TsmiPractice;
        private System.Windows.Forms.ToolStripMenuItem TsmiHelp;
        private System.Windows.Forms.ToolStripSeparator infoToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem TsmiImport;
        private System.Windows.Forms.ToolStripMenuItem TsmiExport;
        private System.Windows.Forms.ToolStripMenuItem TsmiOpenInExplorer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem TsmiSpecialChar;
        private System.Windows.Forms.Panel SideBar;
        private Controls.ResponsiveSplitContainer SplitContainer;
        private System.Windows.Forms.ToolStripMenuItem TsmiBackup;
        private System.Windows.Forms.ToolStripMenuItem TsmiBackupCreate;
        private System.Windows.Forms.ToolStripMenuItem TsmiBackupRestore;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem TsmiCloseBook;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem TsmiEvaluationInfo;
        private Controls.StatisticsPanel GroupStatistics;
        private System.Windows.Forms.TableLayoutPanel TableLayout;
        private System.Windows.Forms.ListView listView_vokabeln;
        private Controls.FileTreeView FileTreeView;
    }
}

