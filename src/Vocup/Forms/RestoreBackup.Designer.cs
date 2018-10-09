namespace Vocup.Forms
{
    partial class RestoreBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestoreBackup));
            this.TbFilePath = new System.Windows.Forms.TextBox();
            this.BtnFilePath = new System.Windows.Forms.Button();
            this.GroupFilePath = new System.Windows.Forms.GroupBox();
            this.GroupBooks = new System.Windows.Forms.GroupBox();
            this.exact_path = new System.Windows.Forms.CheckBox();
            this.ListBooks = new System.Windows.Forms.CheckedListBox();
            this.GroupSpecialChars = new System.Windows.Forms.GroupBox();
            this.ListSpecialChars = new System.Windows.Forms.CheckedListBox();
            this.BtnRestore = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.GroupReplace = new System.Windows.Forms.GroupBox();
            this.RbReplaceOlder = new System.Windows.Forms.RadioButton();
            this.RbReplaceNothing = new System.Windows.Forms.RadioButton();
            this.RbReplaceAll = new System.Windows.Forms.RadioButton();
            this.GroupResults = new System.Windows.Forms.GroupBox();
            this.RbRestoreNoResults = new System.Windows.Forms.RadioButton();
            this.RbRestoreAllResults = new System.Windows.Forms.RadioButton();
            this.RbRestoreAssociatedResults = new System.Windows.Forms.RadioButton();
            this.GroupFilePath.SuspendLayout();
            this.GroupBooks.SuspendLayout();
            this.GroupSpecialChars.SuspendLayout();
            this.GroupReplace.SuspendLayout();
            this.GroupResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // TbFilePath
            // 
            resources.ApplyResources(this.TbFilePath, "TbFilePath");
            this.TbFilePath.Name = "TbFilePath";
            this.TbFilePath.ReadOnly = true;
            this.TbFilePath.TextChanged += new System.EventHandler(this.path_field_TextChanged);
            // 
            // BtnFilePath
            // 
            resources.ApplyResources(this.BtnFilePath, "BtnFilePath");
            this.BtnFilePath.Name = "BtnFilePath";
            this.BtnFilePath.UseVisualStyleBackColor = true;
            this.BtnFilePath.Click += new System.EventHandler(this.BtnFilePath_Click);
            // 
            // GroupFilePath
            // 
            this.GroupFilePath.Controls.Add(this.TbFilePath);
            this.GroupFilePath.Controls.Add(this.BtnFilePath);
            resources.ApplyResources(this.GroupFilePath, "GroupFilePath");
            this.GroupFilePath.Name = "GroupFilePath";
            this.GroupFilePath.TabStop = false;
            // 
            // GroupBooks
            // 
            this.GroupBooks.Controls.Add(this.exact_path);
            this.GroupBooks.Controls.Add(this.ListBooks);
            resources.ApplyResources(this.GroupBooks, "GroupBooks");
            this.GroupBooks.Name = "GroupBooks";
            this.GroupBooks.TabStop = false;
            // 
            // exact_path
            // 
            resources.ApplyResources(this.exact_path, "exact_path");
            this.exact_path.Name = "exact_path";
            this.exact_path.UseVisualStyleBackColor = true;
            this.exact_path.CheckedChanged += new System.EventHandler(this.exact_path_CheckedChanged);
            // 
            // ListBooks
            // 
            this.ListBooks.CheckOnClick = true;
            this.ListBooks.FormattingEnabled = true;
            resources.ApplyResources(this.ListBooks, "ListBooks");
            this.ListBooks.Name = "ListBooks";
            this.ListBooks.SelectedValueChanged += new System.EventHandler(this.listbox_vhf_SelectedValueChanged);
            // 
            // GroupSpecialChars
            // 
            this.GroupSpecialChars.Controls.Add(this.ListSpecialChars);
            resources.ApplyResources(this.GroupSpecialChars, "GroupSpecialChars");
            this.GroupSpecialChars.Name = "GroupSpecialChars";
            this.GroupSpecialChars.TabStop = false;
            // 
            // ListSpecialChars
            // 
            this.ListSpecialChars.CheckOnClick = true;
            this.ListSpecialChars.FormattingEnabled = true;
            resources.ApplyResources(this.ListSpecialChars, "ListSpecialChars");
            this.ListSpecialChars.Name = "ListSpecialChars";
            this.ListSpecialChars.SelectedValueChanged += new System.EventHandler(this.listbox_special_chars_SelectedValueChanged);
            // 
            // BtnRestore
            // 
            this.BtnRestore.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.BtnRestore, "BtnRestore");
            this.BtnRestore.Name = "BtnRestore";
            this.BtnRestore.UseVisualStyleBackColor = true;
            this.BtnRestore.Click += new System.EventHandler(this.restore_button_Click);
            this.BtnRestore.MouseEnter += new System.EventHandler(this.restore_button_MouseEnter);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // GroupReplace
            // 
            this.GroupReplace.Controls.Add(this.RbReplaceOlder);
            this.GroupReplace.Controls.Add(this.RbReplaceNothing);
            this.GroupReplace.Controls.Add(this.RbReplaceAll);
            resources.ApplyResources(this.GroupReplace, "GroupReplace");
            this.GroupReplace.Name = "GroupReplace";
            this.GroupReplace.TabStop = false;
            // 
            // RbReplaceOlder
            // 
            resources.ApplyResources(this.RbReplaceOlder, "RbReplaceOlder");
            this.RbReplaceOlder.Name = "RbReplaceOlder";
            this.RbReplaceOlder.UseVisualStyleBackColor = true;
            this.RbReplaceOlder.CheckedChanged += new System.EventHandler(this.replace_newer_CheckedChanged);
            // 
            // RbReplaceNothing
            // 
            resources.ApplyResources(this.RbReplaceNothing, "RbReplaceNothing");
            this.RbReplaceNothing.Name = "RbReplaceNothing";
            this.RbReplaceNothing.UseVisualStyleBackColor = true;
            this.RbReplaceNothing.CheckedChanged += new System.EventHandler(this.replace_nothing_CheckedChanged);
            // 
            // RbReplaceAll
            // 
            resources.ApplyResources(this.RbReplaceAll, "RbReplaceAll");
            this.RbReplaceAll.Checked = true;
            this.RbReplaceAll.Name = "RbReplaceAll";
            this.RbReplaceAll.TabStop = true;
            this.RbReplaceAll.UseVisualStyleBackColor = true;
            this.RbReplaceAll.CheckedChanged += new System.EventHandler(this.replace_all_CheckedChanged);
            // 
            // GroupResults
            // 
            this.GroupResults.Controls.Add(this.RbRestoreNoResults);
            this.GroupResults.Controls.Add(this.RbRestoreAllResults);
            this.GroupResults.Controls.Add(this.RbRestoreAssociatedResults);
            resources.ApplyResources(this.GroupResults, "GroupResults");
            this.GroupResults.Name = "GroupResults";
            this.GroupResults.TabStop = false;
            // 
            // RbRestoreNoResults
            // 
            resources.ApplyResources(this.RbRestoreNoResults, "RbRestoreNoResults");
            this.RbRestoreNoResults.Name = "RbRestoreNoResults";
            this.RbRestoreNoResults.UseVisualStyleBackColor = true;
            this.RbRestoreNoResults.CheckedChanged += new System.EventHandler(this.results_restore_nothing_CheckedChanged);
            // 
            // RbRestoreAllResults
            // 
            resources.ApplyResources(this.RbRestoreAllResults, "RbRestoreAllResults");
            this.RbRestoreAllResults.Name = "RbRestoreAllResults";
            this.RbRestoreAllResults.UseVisualStyleBackColor = true;
            this.RbRestoreAllResults.CheckedChanged += new System.EventHandler(this.results_restore_all_CheckedChanged);
            // 
            // RbRestoreAssociatedResults
            // 
            resources.ApplyResources(this.RbRestoreAssociatedResults, "RbRestoreAssociatedResults");
            this.RbRestoreAssociatedResults.Checked = true;
            this.RbRestoreAssociatedResults.Name = "RbRestoreAssociatedResults";
            this.RbRestoreAssociatedResults.TabStop = true;
            this.RbRestoreAssociatedResults.UseVisualStyleBackColor = true;
            this.RbRestoreAssociatedResults.CheckedChanged += new System.EventHandler(this.results_restore_choosed_CheckedChanged);
            // 
            // backup_restore
            // 
            this.AcceptButton = this.BtnFilePath;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.Controls.Add(this.GroupResults);
            this.Controls.Add(this.GroupReplace);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnRestore);
            this.Controls.Add(this.GroupSpecialChars);
            this.Controls.Add(this.GroupBooks);
            this.Controls.Add(this.GroupFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "backup_restore";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Form_Load);
            this.GroupFilePath.ResumeLayout(false);
            this.GroupFilePath.PerformLayout();
            this.GroupBooks.ResumeLayout(false);
            this.GroupBooks.PerformLayout();
            this.GroupSpecialChars.ResumeLayout(false);
            this.GroupReplace.ResumeLayout(false);
            this.GroupReplace.PerformLayout();
            this.GroupResults.ResumeLayout(false);
            this.GroupResults.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox TbFilePath;
        public System.Windows.Forms.Button BtnFilePath;
        private System.Windows.Forms.GroupBox GroupFilePath;
        private System.Windows.Forms.GroupBox GroupBooks;
        public System.Windows.Forms.CheckedListBox ListBooks;
        private System.Windows.Forms.GroupBox GroupSpecialChars;
        public System.Windows.Forms.CheckedListBox ListSpecialChars;
        private System.Windows.Forms.Button BtnRestore;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.GroupBox GroupReplace;
        public System.Windows.Forms.RadioButton RbReplaceOlder;
        public System.Windows.Forms.RadioButton RbReplaceNothing;
        public System.Windows.Forms.RadioButton RbReplaceAll;
        private System.Windows.Forms.GroupBox GroupResults;
        public System.Windows.Forms.RadioButton RbRestoreAllResults;
        public System.Windows.Forms.RadioButton RbRestoreAssociatedResults;
        public System.Windows.Forms.RadioButton RbRestoreNoResults;
        private System.Windows.Forms.CheckBox exact_path;
    }
}