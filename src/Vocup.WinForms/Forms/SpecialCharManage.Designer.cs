namespace Vocup.Forms
{
    partial class SpecialCharManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpecialCharManage));
            this.LanguageList = new System.Windows.Forms.ListBox();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.GroupSpecialCharTables = new System.Windows.Forms.GroupBox();
            this.BtnNew = new System.Windows.Forms.Button();
            this.LbLanguage = new System.Windows.Forms.Label();
            this.LbSpecialChars = new System.Windows.Forms.Label();
            this.TbChars = new System.Windows.Forms.TextBox();
            this.TbLanguage = new System.Windows.Forms.TextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.LbInformation = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Button();
            this.GroupSpecialCharTables.SuspendLayout();
            this.SuspendLayout();
            // 
            // LanguageList
            // 
            this.LanguageList.FormattingEnabled = true;
            resources.ApplyResources(this.LanguageList, "LanguageList");
            this.LanguageList.Name = "LanguageList";
            this.LanguageList.SelectedIndexChanged += new System.EventHandler(this.LanguageList_SelectedIndexChanged);
            // 
            // BtnDelete
            // 
            resources.ApplyResources(this.BtnDelete, "BtnDelete");
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // GroupSpecialCharTables
            // 
            this.GroupSpecialCharTables.Controls.Add(this.BtnNew);
            this.GroupSpecialCharTables.Controls.Add(this.LbLanguage);
            this.GroupSpecialCharTables.Controls.Add(this.LbSpecialChars);
            this.GroupSpecialCharTables.Controls.Add(this.TbChars);
            this.GroupSpecialCharTables.Controls.Add(this.TbLanguage);
            this.GroupSpecialCharTables.Controls.Add(this.BtnSave);
            this.GroupSpecialCharTables.Controls.Add(this.LanguageList);
            this.GroupSpecialCharTables.Controls.Add(this.BtnDelete);
            resources.ApplyResources(this.GroupSpecialCharTables, "GroupSpecialCharTables");
            this.GroupSpecialCharTables.Name = "GroupSpecialCharTables";
            this.GroupSpecialCharTables.TabStop = false;
            // 
            // BtnNew
            // 
            resources.ApplyResources(this.BtnNew, "BtnNew");
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.UseVisualStyleBackColor = true;
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // LbLanguage
            // 
            resources.ApplyResources(this.LbLanguage, "LbLanguage");
            this.LbLanguage.Name = "LbLanguage";
            // 
            // LbSpecialChars
            // 
            resources.ApplyResources(this.LbSpecialChars, "LbSpecialChars");
            this.LbSpecialChars.Name = "LbSpecialChars";
            // 
            // TbChars
            // 
            resources.ApplyResources(this.TbChars, "TbChars");
            this.TbChars.Name = "TbChars";
            this.TbChars.TextChanged += new System.EventHandler(this.TbLanguage_TbChars_TextChanged);
            // 
            // TbLanguage
            // 
            resources.ApplyResources(this.TbLanguage, "TbLanguage");
            this.TbLanguage.Name = "TbLanguage";
            this.TbLanguage.TextChanged += new System.EventHandler(this.TbLanguage_TbChars_TextChanged);
            // 
            // BtnSave
            // 
            resources.ApplyResources(this.BtnSave, "BtnSave");
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // LbInformation
            // 
            resources.ApplyResources(this.LbInformation, "LbInformation");
            this.LbInformation.Name = "LbInformation";
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // SpecialCharManage
            // 
            this.AcceptButton = this.BtnClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.LbInformation);
            this.Controls.Add(this.GroupSpecialCharTables);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpecialCharManage";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.SpecialCharManage_Load);
            this.GroupSpecialCharTables.ResumeLayout(false);
            this.GroupSpecialCharTables.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LanguageList;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.GroupBox GroupSpecialCharTables;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Label LbInformation;
        private System.Windows.Forms.Label LbLanguage;
        private System.Windows.Forms.Label LbSpecialChars;
        private System.Windows.Forms.TextBox TbChars;
        private System.Windows.Forms.TextBox TbLanguage;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnNew;
    }
}