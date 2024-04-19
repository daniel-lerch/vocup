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
            BtnCancel = new System.Windows.Forms.Button();
            BtnSave = new System.Windows.Forms.Button();
            LbFiles = new System.Windows.Forms.ListBox();
            BtnAdd = new System.Windows.Forms.Button();
            BtnRemove = new System.Windows.Forms.Button();
            CbKeepResults = new System.Windows.Forms.CheckBox();
            GroupMotherTongue = new System.Windows.Forms.GroupBox();
            TbMotherTongue = new System.Windows.Forms.TextBox();
            GroupForeignTongue = new System.Windows.Forms.GroupBox();
            TbForeignLang = new System.Windows.Forms.TextBox();
            BtnSpecialChar = new System.Windows.Forms.Button();
            GroupMotherTongue.SuspendLayout();
            GroupForeignTongue.SuspendLayout();
            SuspendLayout();
            // 
            // BtnCancel
            // 
            BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(BtnCancel, "BtnCancel");
            BtnCancel.Name = "BtnCancel";
            BtnCancel.UseVisualStyleBackColor = true;
            // 
            // BtnSave
            // 
            resources.ApplyResources(BtnSave, "BtnSave");
            BtnSave.Name = "BtnSave";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // LbFiles
            // 
            LbFiles.FormattingEnabled = true;
            resources.ApplyResources(LbFiles, "LbFiles");
            LbFiles.Name = "LbFiles";
            LbFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            LbFiles.SelectedIndexChanged += LbFiles_SelectedIndexChanged;
            // 
            // BtnAdd
            // 
            resources.ApplyResources(BtnAdd, "BtnAdd");
            BtnAdd.Name = "BtnAdd";
            BtnAdd.UseVisualStyleBackColor = true;
            BtnAdd.Click += BtnAdd_Click;
            // 
            // BtnRemove
            // 
            resources.ApplyResources(BtnRemove, "BtnRemove");
            BtnRemove.Name = "BtnRemove";
            BtnRemove.UseVisualStyleBackColor = true;
            BtnRemove.Click += BtnRemove_Click;
            // 
            // CbKeepResults
            // 
            resources.ApplyResources(CbKeepResults, "CbKeepResults");
            CbKeepResults.Name = "CbKeepResults";
            CbKeepResults.UseVisualStyleBackColor = true;
            // 
            // GroupMotherTongue
            // 
            GroupMotherTongue.Controls.Add(TbMotherTongue);
            resources.ApplyResources(GroupMotherTongue, "GroupMotherTongue");
            GroupMotherTongue.Name = "GroupMotherTongue";
            GroupMotherTongue.TabStop = false;
            // 
            // TbMotherTongue
            // 
            resources.ApplyResources(TbMotherTongue, "TbMotherTongue");
            TbMotherTongue.Name = "TbMotherTongue";
            TbMotherTongue.TextChanged += TextBox_TextChanged;
            TbMotherTongue.Enter += TextBox_Enter;
            // 
            // GroupForeignTongue
            // 
            GroupForeignTongue.Controls.Add(TbForeignLang);
            resources.ApplyResources(GroupForeignTongue, "GroupForeignTongue");
            GroupForeignTongue.Name = "GroupForeignTongue";
            GroupForeignTongue.TabStop = false;
            // 
            // TbForeignLang
            // 
            resources.ApplyResources(TbForeignLang, "TbForeignLang");
            TbForeignLang.Name = "TbForeignLang";
            TbForeignLang.TextChanged += TextBox_TextChanged;
            TbForeignLang.Enter += TextBox_Enter;
            // 
            // BtnSpecialChar
            // 
            resources.ApplyResources(BtnSpecialChar, "BtnSpecialChar");
            BtnSpecialChar.Name = "BtnSpecialChar";
            BtnSpecialChar.UseVisualStyleBackColor = true;
            // 
            // MergeFiles
            // 
            AcceptButton = BtnSave;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = BtnCancel;
            Controls.Add(BtnSpecialChar);
            Controls.Add(GroupForeignTongue);
            Controls.Add(GroupMotherTongue);
            Controls.Add(CbKeepResults);
            Controls.Add(BtnRemove);
            Controls.Add(BtnAdd);
            Controls.Add(LbFiles);
            Controls.Add(BtnSave);
            Controls.Add(BtnCancel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MergeFiles";
            ShowInTaskbar = false;
            GroupMotherTongue.ResumeLayout(false);
            GroupMotherTongue.PerformLayout();
            GroupForeignTongue.ResumeLayout(false);
            GroupForeignTongue.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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