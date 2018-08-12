namespace Vocup
{
    public partial class program_form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(program_form));
            this.menuStrip = new Vocup.Controls.ResponsiveMenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neueVokabeldateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vokabelheftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vokabelheftSchliessenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.SpeichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SpeichernUnterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.importierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiOpenInExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.druckenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.datensicherungToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datensicherungErstellenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datensicherungWiederherstellenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bearbeitenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neueVokabelHinzufügenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vokabelBearbeitenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vokabelLöschenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vokabelnÜbenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vokabelheftOptionenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.vokabelhefteZusammenführenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.sonderzeichenVerwaltenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.optionenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.infosZurBewertungToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.nachUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.ÜberVTrainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.search_vokabel_field = new System.Windows.Forms.TextBox();
            this.GroupBook = new System.Windows.Forms.GroupBox();
            this.vokabelheft_optionen = new System.Windows.Forms.Button();
            this.practice_vokabelheft = new System.Windows.Forms.Button();
            this.GroupSearch = new System.Windows.Forms.GroupBox();
            this.search_vokabel_button = new System.Windows.Forms.Button();
            this.GroupWord = new System.Windows.Forms.GroupBox();
            this.insert_vokabel = new System.Windows.Forms.Button();
            this.delet_vokabel = new System.Windows.Forms.Button();
            this.edit_vokabel = new System.Windows.Forms.Button();
            this.treeview_imagelist = new System.Windows.Forms.ImageList(this.components);
            this.listView_vokabeln = new System.Windows.Forms.ListView();
            this.listview_imagelist = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.sidebar = new System.Windows.Forms.Panel();
            this.GroupStatistics = new Vocup.Controls.StatisticsPanel();
            this.splitContainer = new Vocup.Controls.ResponsiveSplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.printDocument_cards = new System.Drawing.Printing.PrintDocument();
            this.printDocument_list = new System.Drawing.Printing.PrintDocument();
            this.toolStrip = new Vocup.Controls.ResponsiveToolStrip();
            this.new_vokabelheft = new System.Windows.Forms.ToolStripButton();
            this.open_vokabelheft = new System.Windows.Forms.ToolStripButton();
            this.save_vokabelheft = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.print_vokabelheft = new System.Windows.Forms.ToolStripButton();
            this.infos = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip.SuspendLayout();
            this.GroupBook.SuspendLayout();
            this.GroupSearch.SuspendLayout();
            this.GroupWord.SuspendLayout();
            this.sidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.bearbeitenToolStripMenuItem,
            this.extrasToolStripMenuItem,
            this.hilfeToolStripMenuItem});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neueVokabeldateiToolStripMenuItem,
            this.vokabelheftToolStripMenuItem,
            this.vokabelheftSchliessenToolStripMenuItem,
            this.toolStripMenuItem11,
            this.SpeichernToolStripMenuItem,
            this.SpeichernUnterToolStripMenuItem,
            this.toolStripMenuItem10,
            this.importierenToolStripMenuItem,
            this.exportierenToolStripMenuItem,
            this.toolStripMenuItem9,
            this.TsmiOpenInExplorer,
            this.toolStripMenuItem7,
            this.druckenToolStripMenuItem,
            this.toolStripMenuItem1,
            this.datensicherungToolStripMenuItem,
            this.toolStripMenuItem3,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            resources.ApplyResources(this.dateiToolStripMenuItem, "dateiToolStripMenuItem");
            // 
            // neueVokabeldateiToolStripMenuItem
            // 
            this.neueVokabeldateiToolStripMenuItem.Image = global::Vocup.Properties.Icons.blank_file;
            this.neueVokabeldateiToolStripMenuItem.Name = "neueVokabeldateiToolStripMenuItem";
            resources.ApplyResources(this.neueVokabeldateiToolStripMenuItem, "neueVokabeldateiToolStripMenuItem");
            this.neueVokabeldateiToolStripMenuItem.Click += new System.EventHandler(this.neueVocabeldateiToolStripMenuItem_Click);
            // 
            // vokabelheftToolStripMenuItem
            // 
            this.vokabelheftToolStripMenuItem.Image = global::Vocup.Properties.Icons.open;
            this.vokabelheftToolStripMenuItem.Name = "vokabelheftToolStripMenuItem";
            resources.ApplyResources(this.vokabelheftToolStripMenuItem, "vokabelheftToolStripMenuItem");
            this.vokabelheftToolStripMenuItem.Click += new System.EventHandler(this.vokabelheftToolStripMenuItem_Click);
            // 
            // vokabelheftSchliessenToolStripMenuItem
            // 
            resources.ApplyResources(this.vokabelheftSchliessenToolStripMenuItem, "vokabelheftSchliessenToolStripMenuItem");
            this.vokabelheftSchliessenToolStripMenuItem.Name = "vokabelheftSchliessenToolStripMenuItem";
            this.vokabelheftSchliessenToolStripMenuItem.Click += new System.EventHandler(this.vokabelheftSchliessenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            resources.ApplyResources(this.toolStripMenuItem11, "toolStripMenuItem11");
            // 
            // SpeichernToolStripMenuItem
            // 
            resources.ApplyResources(this.SpeichernToolStripMenuItem, "SpeichernToolStripMenuItem");
            this.SpeichernToolStripMenuItem.Image = global::Vocup.Properties.Icons.save;
            this.SpeichernToolStripMenuItem.Name = "SpeichernToolStripMenuItem";
            this.SpeichernToolStripMenuItem.Click += new System.EventHandler(this.SpeichernToolStripMenuItem_Click);
            // 
            // SpeichernUnterToolStripMenuItem
            // 
            resources.ApplyResources(this.SpeichernUnterToolStripMenuItem, "SpeichernUnterToolStripMenuItem");
            this.SpeichernUnterToolStripMenuItem.Image = global::Vocup.Properties.Icons.save;
            this.SpeichernUnterToolStripMenuItem.Name = "SpeichernUnterToolStripMenuItem";
            this.SpeichernUnterToolStripMenuItem.Click += new System.EventHandler(this.SpeichernUnterToolStripMenuItem_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            resources.ApplyResources(this.toolStripMenuItem10, "toolStripMenuItem10");
            // 
            // importierenToolStripMenuItem
            // 
            this.importierenToolStripMenuItem.Image = global::Vocup.Properties.Icons.import;
            this.importierenToolStripMenuItem.Name = "importierenToolStripMenuItem";
            resources.ApplyResources(this.importierenToolStripMenuItem, "importierenToolStripMenuItem");
            this.importierenToolStripMenuItem.Click += new System.EventHandler(this.importierenToolStripMenuItem_Click);
            // 
            // exportierenToolStripMenuItem
            // 
            resources.ApplyResources(this.exportierenToolStripMenuItem, "exportierenToolStripMenuItem");
            this.exportierenToolStripMenuItem.Image = global::Vocup.Properties.Icons.export;
            this.exportierenToolStripMenuItem.Name = "exportierenToolStripMenuItem";
            this.exportierenToolStripMenuItem.Click += new System.EventHandler(this.exportierenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            resources.ApplyResources(this.toolStripMenuItem9, "toolStripMenuItem9");
            // 
            // TsmiOpenInExplorer
            // 
            resources.ApplyResources(this.TsmiOpenInExplorer, "TsmiOpenInExplorer");
            this.TsmiOpenInExplorer.Image = global::Vocup.Properties.Icons.open;
            this.TsmiOpenInExplorer.Name = "TsmiOpenInExplorer";
            this.TsmiOpenInExplorer.Click += new System.EventHandler(this.TsmiOpenInExplorer_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
            // 
            // druckenToolStripMenuItem
            // 
            resources.ApplyResources(this.druckenToolStripMenuItem, "druckenToolStripMenuItem");
            this.druckenToolStripMenuItem.Image = global::Vocup.Properties.Icons.print;
            this.druckenToolStripMenuItem.Name = "druckenToolStripMenuItem";
            this.druckenToolStripMenuItem.Click += new System.EventHandler(this.druckenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // datensicherungToolStripMenuItem
            // 
            this.datensicherungToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datensicherungErstellenToolStripMenuItem,
            this.datensicherungWiederherstellenToolStripMenuItem});
            this.datensicherungToolStripMenuItem.Image = global::Vocup.Properties.Icons.backup;
            this.datensicherungToolStripMenuItem.Name = "datensicherungToolStripMenuItem";
            resources.ApplyResources(this.datensicherungToolStripMenuItem, "datensicherungToolStripMenuItem");
            // 
            // datensicherungErstellenToolStripMenuItem
            // 
            this.datensicherungErstellenToolStripMenuItem.Image = global::Vocup.Properties.Icons.backup_add;
            this.datensicherungErstellenToolStripMenuItem.Name = "datensicherungErstellenToolStripMenuItem";
            resources.ApplyResources(this.datensicherungErstellenToolStripMenuItem, "datensicherungErstellenToolStripMenuItem");
            this.datensicherungErstellenToolStripMenuItem.Click += new System.EventHandler(this.datensicherungErstellenToolStripMenuItem_Click);
            // 
            // datensicherungWiederherstellenToolStripMenuItem
            // 
            this.datensicherungWiederherstellenToolStripMenuItem.Image = global::Vocup.Properties.Icons.backup_go;
            this.datensicherungWiederherstellenToolStripMenuItem.Name = "datensicherungWiederherstellenToolStripMenuItem";
            resources.ApplyResources(this.datensicherungWiederherstellenToolStripMenuItem, "datensicherungWiederherstellenToolStripMenuItem");
            this.datensicherungWiederherstellenToolStripMenuItem.Click += new System.EventHandler(this.datensicherungWiederherstellenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Image = global::Vocup.Properties.Icons.quit;
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            resources.ApplyResources(this.beendenToolStripMenuItem, "beendenToolStripMenuItem");
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // bearbeitenToolStripMenuItem
            // 
            this.bearbeitenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neueVokabelHinzufügenToolStripMenuItem,
            this.vokabelBearbeitenToolStripMenuItem,
            this.vokabelLöschenToolStripMenuItem});
            this.bearbeitenToolStripMenuItem.Name = "bearbeitenToolStripMenuItem";
            resources.ApplyResources(this.bearbeitenToolStripMenuItem, "bearbeitenToolStripMenuItem");
            // 
            // neueVokabelHinzufügenToolStripMenuItem
            // 
            resources.ApplyResources(this.neueVokabelHinzufügenToolStripMenuItem, "neueVokabelHinzufügenToolStripMenuItem");
            this.neueVokabelHinzufügenToolStripMenuItem.Image = global::Vocup.Properties.Icons.add;
            this.neueVokabelHinzufügenToolStripMenuItem.Name = "neueVokabelHinzufügenToolStripMenuItem";
            this.neueVokabelHinzufügenToolStripMenuItem.Click += new System.EventHandler(this.neueVokabelHinzufügenToolStripMenuItem_Click);
            // 
            // vokabelBearbeitenToolStripMenuItem
            // 
            resources.ApplyResources(this.vokabelBearbeitenToolStripMenuItem, "vokabelBearbeitenToolStripMenuItem");
            this.vokabelBearbeitenToolStripMenuItem.Image = global::Vocup.Properties.Icons.edit;
            this.vokabelBearbeitenToolStripMenuItem.Name = "vokabelBearbeitenToolStripMenuItem";
            this.vokabelBearbeitenToolStripMenuItem.Click += new System.EventHandler(this.vokabelBearbeitenToolStripMenuItem_Click);
            // 
            // vokabelLöschenToolStripMenuItem
            // 
            resources.ApplyResources(this.vokabelLöschenToolStripMenuItem, "vokabelLöschenToolStripMenuItem");
            this.vokabelLöschenToolStripMenuItem.Image = global::Vocup.Properties.Icons.delete;
            this.vokabelLöschenToolStripMenuItem.Name = "vokabelLöschenToolStripMenuItem";
            this.vokabelLöschenToolStripMenuItem.Click += new System.EventHandler(this.vokabelLöschenToolStripMenuItem_Click);
            // 
            // extrasToolStripMenuItem
            // 
            this.extrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vokabelnÜbenToolStripMenuItem,
            this.vokabelheftOptionenToolStripMenuItem,
            this.toolStripMenuItem4,
            this.vokabelhefteZusammenführenToolStripMenuItem,
            this.toolStripMenuItem5,
            this.sonderzeichenVerwaltenToolStripMenuItem,
            this.toolStripMenuItem6,
            this.optionenToolStripMenuItem});
            this.extrasToolStripMenuItem.Name = "extrasToolStripMenuItem";
            resources.ApplyResources(this.extrasToolStripMenuItem, "extrasToolStripMenuItem");
            // 
            // vokabelnÜbenToolStripMenuItem
            // 
            resources.ApplyResources(this.vokabelnÜbenToolStripMenuItem, "vokabelnÜbenToolStripMenuItem");
            this.vokabelnÜbenToolStripMenuItem.Image = global::Vocup.Properties.Icons.practise;
            this.vokabelnÜbenToolStripMenuItem.Name = "vokabelnÜbenToolStripMenuItem";
            this.vokabelnÜbenToolStripMenuItem.Click += new System.EventHandler(this.vokabelnÜbenToolStripMenuItem_Click);
            // 
            // vokabelheftOptionenToolStripMenuItem
            // 
            resources.ApplyResources(this.vokabelheftOptionenToolStripMenuItem, "vokabelheftOptionenToolStripMenuItem");
            this.vokabelheftOptionenToolStripMenuItem.Image = global::Vocup.Properties.Icons.settings_file;
            this.vokabelheftOptionenToolStripMenuItem.Name = "vokabelheftOptionenToolStripMenuItem";
            this.vokabelheftOptionenToolStripMenuItem.Click += new System.EventHandler(this.vokabelheftOptionenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // vokabelhefteZusammenführenToolStripMenuItem
            // 
            this.vokabelhefteZusammenführenToolStripMenuItem.Image = global::Vocup.Properties.Icons.merge;
            this.vokabelhefteZusammenführenToolStripMenuItem.Name = "vokabelhefteZusammenführenToolStripMenuItem";
            resources.ApplyResources(this.vokabelhefteZusammenführenToolStripMenuItem, "vokabelhefteZusammenführenToolStripMenuItem");
            this.vokabelhefteZusammenführenToolStripMenuItem.Click += new System.EventHandler(this.vokabelhefteZusammenführenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            // 
            // sonderzeichenVerwaltenToolStripMenuItem
            // 
            this.sonderzeichenVerwaltenToolStripMenuItem.Image = global::Vocup.Properties.Icons.character;
            this.sonderzeichenVerwaltenToolStripMenuItem.Name = "sonderzeichenVerwaltenToolStripMenuItem";
            resources.ApplyResources(this.sonderzeichenVerwaltenToolStripMenuItem, "sonderzeichenVerwaltenToolStripMenuItem");
            this.sonderzeichenVerwaltenToolStripMenuItem.Click += new System.EventHandler(this.sonderzeichenVerwaltenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
            // 
            // optionenToolStripMenuItem
            // 
            this.optionenToolStripMenuItem.Image = global::Vocup.Properties.Icons.settings;
            this.optionenToolStripMenuItem.Name = "optionenToolStripMenuItem";
            resources.ApplyResources(this.optionenToolStripMenuItem, "optionenToolStripMenuItem");
            this.optionenToolStripMenuItem.Click += new System.EventHandler(this.optionenToolStripMenuItem_Click);
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hilfeToolStripMenuItem1,
            this.infosZurBewertungToolStripMenuItem,
            this.toolStripMenuItem8,
            this.nachUpdatesToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.ÜberVTrainingToolStripMenuItem});
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            resources.ApplyResources(this.hilfeToolStripMenuItem, "hilfeToolStripMenuItem");
            // 
            // hilfeToolStripMenuItem1
            // 
            this.hilfeToolStripMenuItem1.Image = global::Vocup.Properties.Icons.question_mark;
            this.hilfeToolStripMenuItem1.Name = "hilfeToolStripMenuItem1";
            resources.ApplyResources(this.hilfeToolStripMenuItem1, "hilfeToolStripMenuItem1");
            this.hilfeToolStripMenuItem1.Click += new System.EventHandler(this.hilfeToolStripMenuItem1_Click);
            // 
            // infosZurBewertungToolStripMenuItem
            // 
            this.infosZurBewertungToolStripMenuItem.Image = global::Vocup.Properties.Icons.info;
            this.infosZurBewertungToolStripMenuItem.Name = "infosZurBewertungToolStripMenuItem";
            resources.ApplyResources(this.infosZurBewertungToolStripMenuItem, "infosZurBewertungToolStripMenuItem");
            this.infosZurBewertungToolStripMenuItem.Click += new System.EventHandler(this.infosZurBewertungToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
            // 
            // nachUpdatesToolStripMenuItem
            // 
            this.nachUpdatesToolStripMenuItem.Image = global::Vocup.Properties.Icons.update;
            this.nachUpdatesToolStripMenuItem.Name = "nachUpdatesToolStripMenuItem";
            resources.ApplyResources(this.nachUpdatesToolStripMenuItem, "nachUpdatesToolStripMenuItem");
            this.nachUpdatesToolStripMenuItem.Click += new System.EventHandler(this.nachUpdatesToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            resources.ApplyResources(this.infoToolStripMenuItem, "infoToolStripMenuItem");
            // 
            // ÜberVTrainingToolStripMenuItem
            // 
            this.ÜberVTrainingToolStripMenuItem.Image = global::Vocup.Properties.Icons.info;
            this.ÜberVTrainingToolStripMenuItem.Name = "ÜberVTrainingToolStripMenuItem";
            resources.ApplyResources(this.ÜberVTrainingToolStripMenuItem, "ÜberVTrainingToolStripMenuItem");
            this.ÜberVTrainingToolStripMenuItem.Click += new System.EventHandler(this.infoÜberVTrainingToolStripMenuItem_Click);
            // 
            // search_vokabel_field
            // 
            resources.ApplyResources(this.search_vokabel_field, "search_vokabel_field");
            this.search_vokabel_field.Name = "search_vokabel_field";
            this.search_vokabel_field.TextChanged += new System.EventHandler(this.search_vokabel_field_TextChanged);
            // 
            // GroupBook
            // 
            this.GroupBook.Controls.Add(this.vokabelheft_optionen);
            this.GroupBook.Controls.Add(this.practice_vokabelheft);
            resources.ApplyResources(this.GroupBook, "GroupBook");
            this.GroupBook.Name = "GroupBook";
            this.GroupBook.TabStop = false;
            // 
            // vokabelheft_optionen
            // 
            this.vokabelheft_optionen.Image = global::Vocup.Properties.Icons.settings_file;
            resources.ApplyResources(this.vokabelheft_optionen, "vokabelheft_optionen");
            this.vokabelheft_optionen.Name = "vokabelheft_optionen";
            this.vokabelheft_optionen.UseVisualStyleBackColor = true;
            this.vokabelheft_optionen.Click += new System.EventHandler(this.vokabelheft_optionen_Click);
            // 
            // practice_vokabelheft
            // 
            this.practice_vokabelheft.Image = global::Vocup.Properties.Icons.practise;
            resources.ApplyResources(this.practice_vokabelheft, "practice_vokabelheft");
            this.practice_vokabelheft.Name = "practice_vokabelheft";
            this.practice_vokabelheft.UseVisualStyleBackColor = true;
            this.practice_vokabelheft.Click += new System.EventHandler(this.practice_vokabelheft_Click);
            // 
            // GroupSearch
            // 
            this.GroupSearch.Controls.Add(this.search_vokabel_field);
            this.GroupSearch.Controls.Add(this.search_vokabel_button);
            resources.ApplyResources(this.GroupSearch, "GroupSearch");
            this.GroupSearch.Name = "GroupSearch";
            this.GroupSearch.TabStop = false;
            // 
            // search_vokabel_button
            // 
            resources.ApplyResources(this.search_vokabel_button, "search_vokabel_button");
            this.search_vokabel_button.Image = global::Vocup.Properties.Icons.search;
            this.search_vokabel_button.Name = "search_vokabel_button";
            this.search_vokabel_button.UseVisualStyleBackColor = true;
            this.search_vokabel_button.Click += new System.EventHandler(this.search_vokabel_button_Click);
            // 
            // GroupWord
            // 
            this.GroupWord.Controls.Add(this.insert_vokabel);
            this.GroupWord.Controls.Add(this.delet_vokabel);
            this.GroupWord.Controls.Add(this.edit_vokabel);
            resources.ApplyResources(this.GroupWord, "GroupWord");
            this.GroupWord.Name = "GroupWord";
            this.GroupWord.TabStop = false;
            // 
            // insert_vokabel
            // 
            this.insert_vokabel.Image = global::Vocup.Properties.Icons.add;
            resources.ApplyResources(this.insert_vokabel, "insert_vokabel");
            this.insert_vokabel.Name = "insert_vokabel";
            this.insert_vokabel.UseVisualStyleBackColor = true;
            this.insert_vokabel.Click += new System.EventHandler(this.insert_vokabel_Click);
            // 
            // delet_vokabel
            // 
            resources.ApplyResources(this.delet_vokabel, "delet_vokabel");
            this.delet_vokabel.Image = global::Vocup.Properties.Icons.delete;
            this.delet_vokabel.Name = "delet_vokabel";
            this.delet_vokabel.UseVisualStyleBackColor = true;
            this.delet_vokabel.Click += new System.EventHandler(this.delet_vokabel_Click);
            // 
            // edit_vokabel
            // 
            resources.ApplyResources(this.edit_vokabel, "edit_vokabel");
            this.edit_vokabel.Image = global::Vocup.Properties.Icons.edit;
            this.edit_vokabel.Name = "edit_vokabel";
            this.edit_vokabel.UseVisualStyleBackColor = true;
            this.edit_vokabel.Click += new System.EventHandler(this.edit_vokabel_Click);
            // 
            // treeview_imagelist
            // 
            this.treeview_imagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeview_imagelist.ImageStream")));
            this.treeview_imagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.treeview_imagelist.Images.SetKeyName(0, "folder.png");
            this.treeview_imagelist.Images.SetKeyName(1, "folder.png");
            this.treeview_imagelist.Images.SetKeyName(2, "blank_file.png");
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
            this.listView_vokabeln.SmallImageList = this.listview_imagelist;
            this.listView_vokabeln.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.listView_vokabeln.UseCompatibleStateImageBehavior = false;
            this.listView_vokabeln.View = System.Windows.Forms.View.Details;
            this.listView_vokabeln.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_vokabeln_MouseDoubleClick);
            // 
            // listview_imagelist
            // 
            this.listview_imagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("listview_imagelist.ImageStream")));
            this.listview_imagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.listview_imagelist.Images.SetKeyName(0, "0.png");
            this.listview_imagelist.Images.SetKeyName(1, "1.png");
            this.listview_imagelist.Images.SetKeyName(2, "2.png");
            this.listview_imagelist.Images.SetKeyName(3, "3.png");
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // sidebar
            // 
            this.sidebar.Controls.Add(this.GroupStatistics);
            this.sidebar.Controls.Add(this.GroupBook);
            this.sidebar.Controls.Add(this.GroupWord);
            this.sidebar.Controls.Add(this.GroupSearch);
            resources.ApplyResources(this.sidebar, "sidebar");
            this.sidebar.Name = "sidebar";
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
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.listView_vokabeln);
            this.splitContainer.SplitterBaseDistance = 150;
            // 
            // treeView
            // 
            resources.ApplyResources(this.treeView, "treeView");
            this.treeView.ImageList = this.treeview_imagelist;
            this.treeView.Name = "treeView";
            this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCollapse);
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.Enter += new System.EventHandler(this.treeView_Enter);
            // 
            // printDocument_cards
            // 
            this.printDocument_cards.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_cards_BeginPrint);
            this.printDocument_cards.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_cards_PrintPage);
            // 
            // printDocument_list
            // 
            this.printDocument_list.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_list_BeginPrint);
            this.printDocument_list.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_list_PrintPage);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.new_vokabelheft,
            this.open_vokabelheft,
            this.save_vokabelheft,
            this.toolStripSeparator1,
            this.print_vokabelheft,
            this.infos,
            this.toolStripLabel});
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Name = "toolStrip";
            // 
            // new_vokabelheft
            // 
            this.new_vokabelheft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.new_vokabelheft.Image = global::Vocup.Properties.Icons.blank_file;
            resources.ApplyResources(this.new_vokabelheft, "new_vokabelheft");
            this.new_vokabelheft.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
            this.new_vokabelheft.Name = "new_vokabelheft";
            this.new_vokabelheft.Click += new System.EventHandler(this.new_vokabelheft_Click);
            // 
            // open_vokabelheft
            // 
            this.open_vokabelheft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.open_vokabelheft.Image = global::Vocup.Properties.Icons.open;
            resources.ApplyResources(this.open_vokabelheft, "open_vokabelheft");
            this.open_vokabelheft.Margin = new System.Windows.Forms.Padding(1);
            this.open_vokabelheft.Name = "open_vokabelheft";
            this.open_vokabelheft.Click += new System.EventHandler(this.open_vokabelheft_Click);
            // 
            // save_vokabelheft
            // 
            this.save_vokabelheft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.save_vokabelheft, "save_vokabelheft");
            this.save_vokabelheft.Image = global::Vocup.Properties.Icons.save;
            this.save_vokabelheft.Margin = new System.Windows.Forms.Padding(1);
            this.save_vokabelheft.Name = "save_vokabelheft";
            this.save_vokabelheft.Click += new System.EventHandler(this.save_vokabelheft_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // print_vokabelheft
            // 
            this.print_vokabelheft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.print_vokabelheft, "print_vokabelheft");
            this.print_vokabelheft.Image = global::Vocup.Properties.Icons.print;
            this.print_vokabelheft.Margin = new System.Windows.Forms.Padding(1);
            this.print_vokabelheft.Name = "print_vokabelheft";
            this.print_vokabelheft.Click += new System.EventHandler(this.print_vokabelheft_Click);
            // 
            // infos
            // 
            this.infos.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.infos.Image = global::Vocup.Properties.Icons.info;
            resources.ApplyResources(this.infos, "infos");
            this.infos.Margin = new System.Windows.Forms.Padding(1, 1, 2, 1);
            this.infos.Name = "infos";
            this.infos.Click += new System.EventHandler(this.infos_Click);
            // 
            // toolStripLabel
            // 
            this.toolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripLabel.Name = "toolStripLabel";
            resources.ApplyResources(this.toolStripLabel, "toolStripLabel");
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.sidebar, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // program_form
            // 
            this.AcceptButton = this.insert_vokabel;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "program_form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.program_form_FormClosing);
            this.Load += new System.EventHandler(this.program_form_Load);
            this.Shown += new System.EventHandler(this.program_form_Shown);
            this.SizeChanged += new System.EventHandler(this.program_form_SizeChanged);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.GroupBook.ResumeLayout(false);
            this.GroupSearch.ResumeLayout(false);
            this.GroupSearch.PerformLayout();
            this.GroupWord.ResumeLayout(false);
            this.sidebar.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ResponsiveMenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private Controls.ResponsiveToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton open_vokabelheft;
        private System.Windows.Forms.ToolStripButton new_vokabelheft;
        private System.Windows.Forms.ToolStripButton save_vokabelheft;
        private System.Windows.Forms.ToolStripMenuItem neueVokabeldateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bearbeitenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vokabelheftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SpeichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SpeichernUnterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem druckenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton print_vokabelheft;
        private System.Windows.Forms.Button practice_vokabelheft;
        private System.Windows.Forms.Button search_vokabel_button;
        private System.Windows.Forms.TextBox search_vokabel_field;
        private System.Windows.Forms.GroupBox GroupBook;
        private System.Windows.Forms.GroupBox GroupSearch;
        private System.Windows.Forms.ToolStripMenuItem neueVokabelHinzufügenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vokabelBearbeitenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vokabelLöschenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem vokabelnÜbenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ÜberVTrainingToolStripMenuItem;
        private System.Windows.Forms.Button edit_vokabel;
        private System.Windows.Forms.Button delet_vokabel;
        private System.Windows.Forms.Button insert_vokabel;
        private System.Windows.Forms.GroupBox GroupWord;
        private System.Windows.Forms.ToolStripButton infos;
        private System.Windows.Forms.ToolStripLabel toolStripLabel;
        private System.Windows.Forms.ImageList treeview_imagelist;
        public System.Windows.Forms.ListView listView_vokabeln;
        private System.Windows.Forms.Button vokabelheft_optionen;
        private System.Windows.Forms.ImageList listview_imagelist;
        private System.Windows.Forms.ToolStripMenuItem vokabelheftOptionenToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem nachUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vokabelhefteZusammenführenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem importierenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportierenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TsmiOpenInExplorer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem sonderzeichenVerwaltenToolStripMenuItem;
        private System.Windows.Forms.Panel sidebar;
        private Controls.ResponsiveSplitContainer splitContainer;
        public System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ToolStripMenuItem datensicherungToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datensicherungErstellenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datensicherungWiederherstellenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem vokabelheftSchliessenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
        private System.Drawing.Printing.PrintDocument printDocument_cards;
        private System.Drawing.Printing.PrintDocument printDocument_list;
        private System.Windows.Forms.ToolStripMenuItem infosZurBewertungToolStripMenuItem;
        private Controls.StatisticsPanel GroupStatistics;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

