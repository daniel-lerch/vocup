namespace Vocup.Forms
{
    partial class VocabularyWordDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VocabularyWordDialog));
            BtnContinue = new System.Windows.Forms.Button();
            BtnCancel = new System.Windows.Forms.Button();
            TbForeignLangSynonym = new System.Windows.Forms.TextBox();
            TbForeignLang = new System.Windows.Forms.TextBox();
            LbForeignLang = new System.Windows.Forms.Label();
            CbResetResults = new System.Windows.Forms.CheckBox();
            LbSynonym = new System.Windows.Forms.Label();
            GroupOptions = new System.Windows.Forms.GroupBox();
            BtnSpecialChar = new System.Windows.Forms.Button();
            GroupForeignLang = new System.Windows.Forms.GroupBox();
            TbMotherTongue = new System.Windows.Forms.TextBox();
            LbMotherTongue = new System.Windows.Forms.Label();
            GroupMotherTongue = new System.Windows.Forms.GroupBox();
            GroupOptions.SuspendLayout();
            GroupForeignLang.SuspendLayout();
            GroupMotherTongue.SuspendLayout();
            SuspendLayout();
            // 
            // BtnContinue
            // 
            resources.ApplyResources(BtnContinue, "BtnContinue");
            BtnContinue.Name = "BtnContinue";
            BtnContinue.UseVisualStyleBackColor = true;
            BtnContinue.Click += BtnContinue_Click;
            // 
            // BtnCancel
            // 
            resources.ApplyResources(BtnCancel, "BtnCancel");
            BtnCancel.Name = "BtnCancel";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // TbForeignLangSynonym
            // 
            resources.ApplyResources(TbForeignLangSynonym, "TbForeignLangSynonym");
            TbForeignLangSynonym.Name = "TbForeignLangSynonym";
            TbForeignLangSynonym.TextChanged += TextBox_TextChanged;
            TbForeignLangSynonym.Enter += TextBox_Enter;
            // 
            // TbForeignLang
            // 
            resources.ApplyResources(TbForeignLang, "TbForeignLang");
            TbForeignLang.Name = "TbForeignLang";
            TbForeignLang.TextChanged += TextBox_TextChanged;
            TbForeignLang.Enter += TextBox_Enter;
            // 
            // LbForeignLang
            // 
            resources.ApplyResources(LbForeignLang, "LbForeignLang");
            LbForeignLang.Name = "LbForeignLang";
            // 
            // CbResetResults
            // 
            resources.ApplyResources(CbResetResults, "CbResetResults");
            CbResetResults.Name = "CbResetResults";
            CbResetResults.UseVisualStyleBackColor = true;
            // 
            // LbSynonym
            // 
            resources.ApplyResources(LbSynonym, "LbSynonym");
            LbSynonym.Name = "LbSynonym";
            // 
            // GroupOptions
            // 
            resources.ApplyResources(GroupOptions, "GroupOptions");
            GroupOptions.Controls.Add(CbResetResults);
            GroupOptions.Name = "GroupOptions";
            GroupOptions.TabStop = false;
            // 
            // BtnSpecialChar
            // 
            resources.ApplyResources(BtnSpecialChar, "BtnSpecialChar");
            BtnSpecialChar.Name = "BtnSpecialChar";
            BtnSpecialChar.UseVisualStyleBackColor = true;
            // 
            // GroupForeignLang
            // 
            resources.ApplyResources(GroupForeignLang, "GroupForeignLang");
            GroupForeignLang.Controls.Add(TbForeignLang);
            GroupForeignLang.Controls.Add(TbForeignLangSynonym);
            GroupForeignLang.Controls.Add(LbSynonym);
            GroupForeignLang.Controls.Add(LbForeignLang);
            GroupForeignLang.Name = "GroupForeignLang";
            GroupForeignLang.TabStop = false;
            // 
            // TbMotherTongue
            // 
            resources.ApplyResources(TbMotherTongue, "TbMotherTongue");
            TbMotherTongue.BackColor = System.Drawing.SystemColors.Window;
            TbMotherTongue.Name = "TbMotherTongue";
            TbMotherTongue.TextChanged += TextBox_TextChanged;
            TbMotherTongue.Enter += TextBox_Enter;
            // 
            // LbMotherTongue
            // 
            resources.ApplyResources(LbMotherTongue, "LbMotherTongue");
            LbMotherTongue.Name = "LbMotherTongue";
            // 
            // GroupMotherTongue
            // 
            resources.ApplyResources(GroupMotherTongue, "GroupMotherTongue");
            GroupMotherTongue.Controls.Add(LbMotherTongue);
            GroupMotherTongue.Controls.Add(TbMotherTongue);
            GroupMotherTongue.Name = "GroupMotherTongue";
            GroupMotherTongue.TabStop = false;
            // 
            // VocabularyWordDialog
            // 
            AcceptButton = BtnContinue;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(BtnSpecialChar);
            Controls.Add(GroupOptions);
            Controls.Add(GroupMotherTongue);
            Controls.Add(GroupForeignLang);
            Controls.Add(BtnCancel);
            Controls.Add(BtnContinue);
            Name = "VocabularyWordDialog";
            ShowInTaskbar = false;
            GroupOptions.ResumeLayout(false);
            GroupOptions.PerformLayout();
            GroupForeignLang.ResumeLayout(false);
            GroupForeignLang.PerformLayout();
            GroupMotherTongue.ResumeLayout(false);
            GroupMotherTongue.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.TextBox TbForeignLangSynonym;
        public System.Windows.Forms.TextBox TbForeignLang;
        public System.Windows.Forms.CheckBox CbResetResults;
        private System.Windows.Forms.Button BtnSpecialChar;
        private System.Windows.Forms.GroupBox GroupForeignLang;
        public System.Windows.Forms.TextBox TbMotherTongue;
        private System.Windows.Forms.GroupBox GroupMotherTongue;
        public System.Windows.Forms.Button BtnContinue;
        public System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Label LbForeignLang;
        private System.Windows.Forms.Label LbSynonym;
        private System.Windows.Forms.Label LbMotherTongue;
        protected System.Windows.Forms.GroupBox GroupOptions;
    }
}