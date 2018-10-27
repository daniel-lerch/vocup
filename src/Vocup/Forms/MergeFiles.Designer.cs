namespace Vocup.Forms
{
    partial class MergeFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MergeFiles));
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.LbFiles = new System.Windows.Forms.ListBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnRemove = new System.Windows.Forms.Button();
            this.CbKeepResults = new System.Windows.Forms.CheckBox();
            this.GroupMotherTongue = new System.Windows.Forms.GroupBox();
            this.TbMotherTongue = new System.Windows.Forms.TextBox();
            this.GroupForeignTongue = new System.Windows.Forms.GroupBox();
            this.TbForeignLang = new System.Windows.Forms.TextBox();
            this.BtnSpecialChar = new System.Windows.Forms.Button();
            this.GroupMotherTongue.SuspendLayout();
            this.GroupForeignTongue.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // BtnSave
            // 
            resources.ApplyResources(this.BtnSave, "BtnSave");
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // LbFiles
            // 
            this.LbFiles.FormattingEnabled = true;
            resources.ApplyResources(this.LbFiles, "LbFiles");
            this.LbFiles.Name = "LbFiles";
            this.LbFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LbFiles.SelectedIndexChanged += new System.EventHandler(this.LbFiles_SelectedIndexChanged);
            // 
            // BtnAdd
            // 
            resources.ApplyResources(this.BtnAdd, "BtnAdd");
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnRemove
            // 
            resources.ApplyResources(this.BtnRemove, "BtnRemove");
            this.BtnRemove.Name = "BtnRemove";
            this.BtnRemove.UseVisualStyleBackColor = true;
            this.BtnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // CbKeepResults
            // 
            resources.ApplyResources(this.CbKeepResults, "CbKeepResults");
            this.CbKeepResults.Name = "CbKeepResults";
            this.CbKeepResults.UseVisualStyleBackColor = true;
            // 
            // GroupMotherTongue
            // 
            this.GroupMotherTongue.Controls.Add(this.TbMotherTongue);
            resources.ApplyResources(this.GroupMotherTongue, "GroupMotherTongue");
            this.GroupMotherTongue.Name = "GroupMotherTongue";
            this.GroupMotherTongue.TabStop = false;
            // 
            // TbMotherTongue
            // 
            resources.ApplyResources(this.TbMotherTongue, "TbMotherTongue");
            this.TbMotherTongue.Name = "TbMotherTongue";
            this.TbMotherTongue.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TbMotherTongue.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // GroupForeignTongue
            // 
            this.GroupForeignTongue.Controls.Add(this.TbForeignLang);
            resources.ApplyResources(this.GroupForeignTongue, "GroupForeignTongue");
            this.GroupForeignTongue.Name = "GroupForeignTongue";
            this.GroupForeignTongue.TabStop = false;
            // 
            // TbForeignLang
            // 
            resources.ApplyResources(this.TbForeignLang, "TbForeignLang");
            this.TbForeignLang.Name = "TbForeignLang";
            this.TbForeignLang.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TbForeignLang.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // BtnSpecialChar
            // 
            resources.ApplyResources(this.BtnSpecialChar, "BtnSpecialChar");
            this.BtnSpecialChar.Name = "BtnSpecialChar";
            this.BtnSpecialChar.UseVisualStyleBackColor = true;
            // 
            // MergeFiles
            // 
            this.AcceptButton = this.BtnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.Controls.Add(this.BtnSpecialChar);
            this.Controls.Add(this.GroupForeignTongue);
            this.Controls.Add(this.GroupMotherTongue);
            this.Controls.Add(this.CbKeepResults);
            this.Controls.Add(this.BtnRemove);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.LbFiles);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MergeFiles";
            this.ShowInTaskbar = false;
            this.GroupMotherTongue.ResumeLayout(false);
            this.GroupMotherTongue.PerformLayout();
            this.GroupForeignTongue.ResumeLayout(false);
            this.GroupForeignTongue.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnRemove;
        private System.Windows.Forms.GroupBox GroupMotherTongue;
        private System.Windows.Forms.GroupBox GroupForeignTongue;
        private System.Windows.Forms.Button BtnSpecialChar;
        private System.Windows.Forms.ListBox LbFiles;
        private System.Windows.Forms.CheckBox CbKeepResults;
        private System.Windows.Forms.TextBox TbMotherTongue;
        private System.Windows.Forms.TextBox TbForeignLang;
    }
}