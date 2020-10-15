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
            this.GroupBooks = new System.Windows.Forms.GroupBox();
            this.CbAbsolutePath = new System.Windows.Forms.CheckBox();
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
            this.LbDeprecated = new System.Windows.Forms.Label();
            this.GroupBooks.SuspendLayout();
            this.GroupSpecialChars.SuspendLayout();
            this.GroupReplace.SuspendLayout();
            this.GroupResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBooks
            // 
            this.GroupBooks.Controls.Add(this.CbAbsolutePath);
            this.GroupBooks.Controls.Add(this.ListBooks);
            resources.ApplyResources(this.GroupBooks, "GroupBooks");
            this.GroupBooks.Name = "GroupBooks";
            this.GroupBooks.TabStop = false;
            // 
            // CbAbsolutePath
            // 
            resources.ApplyResources(this.CbAbsolutePath, "CbAbsolutePath");
            this.CbAbsolutePath.Name = "CbAbsolutePath";
            this.CbAbsolutePath.UseVisualStyleBackColor = true;
            this.CbAbsolutePath.CheckedChanged += new System.EventHandler(this.CbAbsolutePath_CheckedChanged);
            // 
            // ListBooks
            // 
            this.ListBooks.CheckOnClick = true;
            this.ListBooks.FormattingEnabled = true;
            resources.ApplyResources(this.ListBooks, "ListBooks");
            this.ListBooks.Name = "ListBooks";
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
            // 
            // BtnRestore
            // 
            this.BtnRestore.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.BtnRestore, "BtnRestore");
            this.BtnRestore.Name = "BtnRestore";
            this.BtnRestore.Click += new System.EventHandler(this.BtnRestore_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
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
            // 
            // RbReplaceNothing
            // 
            resources.ApplyResources(this.RbReplaceNothing, "RbReplaceNothing");
            this.RbReplaceNothing.Name = "RbReplaceNothing";
            this.RbReplaceNothing.UseVisualStyleBackColor = true;
            // 
            // RbReplaceAll
            // 
            resources.ApplyResources(this.RbReplaceAll, "RbReplaceAll");
            this.RbReplaceAll.Checked = true;
            this.RbReplaceAll.Name = "RbReplaceAll";
            this.RbReplaceAll.TabStop = true;
            this.RbReplaceAll.UseVisualStyleBackColor = true;
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
            // 
            // RbRestoreAllResults
            // 
            resources.ApplyResources(this.RbRestoreAllResults, "RbRestoreAllResults");
            this.RbRestoreAllResults.Name = "RbRestoreAllResults";
            this.RbRestoreAllResults.UseVisualStyleBackColor = true;
            // 
            // RbRestoreAssociatedResults
            // 
            resources.ApplyResources(this.RbRestoreAssociatedResults, "RbRestoreAssociatedResults");
            this.RbRestoreAssociatedResults.Checked = true;
            this.RbRestoreAssociatedResults.Name = "RbRestoreAssociatedResults";
            this.RbRestoreAssociatedResults.TabStop = true;
            this.RbRestoreAssociatedResults.UseVisualStyleBackColor = true;
            // 
            // LbDeprecated
            // 
            this.LbDeprecated.BackColor = System.Drawing.Color.AntiqueWhite;
            this.LbDeprecated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LbDeprecated.ForeColor = System.Drawing.Color.DarkRed;
            resources.ApplyResources(this.LbDeprecated, "LbDeprecated");
            this.LbDeprecated.Name = "LbDeprecated";
            // 
            // RestoreBackup
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.Controls.Add(this.LbDeprecated);
            this.Controls.Add(this.GroupResults);
            this.Controls.Add(this.GroupReplace);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnRestore);
            this.Controls.Add(this.GroupSpecialChars);
            this.Controls.Add(this.GroupBooks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RestoreBackup";
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Closed);
            this.Shown += new System.EventHandler(this.Form_Shown);
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
        private System.Windows.Forms.GroupBox GroupBooks;
        private System.Windows.Forms.GroupBox GroupSpecialChars;
        private System.Windows.Forms.Button BtnRestore;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.GroupBox GroupReplace;
        private System.Windows.Forms.GroupBox GroupResults;
        private System.Windows.Forms.CheckBox CbAbsolutePath;
        private System.Windows.Forms.CheckedListBox ListBooks;
        private System.Windows.Forms.RadioButton RbReplaceOlder;
        private System.Windows.Forms.RadioButton RbReplaceNothing;
        private System.Windows.Forms.RadioButton RbReplaceAll;
        private System.Windows.Forms.CheckedListBox ListSpecialChars;
        private System.Windows.Forms.RadioButton RbRestoreAllResults;
        private System.Windows.Forms.RadioButton RbRestoreAssociatedResults;
        private System.Windows.Forms.RadioButton RbRestoreNoResults;
        private System.Windows.Forms.Label LbDeprecated;
    }
}