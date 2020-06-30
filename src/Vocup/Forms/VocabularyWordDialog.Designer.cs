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
            this.BtnContinue = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.TbForeignLangSynonym = new System.Windows.Forms.TextBox();
            this.TbForeignLang = new System.Windows.Forms.TextBox();
            this.LbForeignLang = new System.Windows.Forms.Label();
            this.CbResetResults = new System.Windows.Forms.CheckBox();
            this.LbSynonym = new System.Windows.Forms.Label();
            this.GroupOptions = new System.Windows.Forms.GroupBox();
            this.BtnSpecialChar = new System.Windows.Forms.Button();
            this.GroupForeignLang = new System.Windows.Forms.GroupBox();
            this.TbMotherTongue = new System.Windows.Forms.TextBox();
            this.LbMotherTongue = new System.Windows.Forms.Label();
            this.GroupMotherTongue = new System.Windows.Forms.GroupBox();
            this.GroupOptions.SuspendLayout();
            this.GroupForeignLang.SuspendLayout();
            this.GroupMotherTongue.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnContinue
            // 
            resources.ApplyResources(this.BtnContinue, "BtnContinue");
            this.BtnContinue.Name = "BtnContinue";
            this.BtnContinue.UseVisualStyleBackColor = true;
            this.BtnContinue.Click += new System.EventHandler(this.BtnContinue_Click);
            // 
            // BtnCancel
            // 
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // TbForeignLangSynonym
            // 
            resources.ApplyResources(this.TbForeignLangSynonym, "TbForeignLangSynonym");
            this.TbForeignLangSynonym.Name = "TbForeignLangSynonym";
            this.TbForeignLangSynonym.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TbForeignLangSynonym.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // TbForeignLang
            // 
            resources.ApplyResources(this.TbForeignLang, "TbForeignLang");
            this.TbForeignLang.Name = "TbForeignLang";
            this.TbForeignLang.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TbForeignLang.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // LbForeignLang
            // 
            resources.ApplyResources(this.LbForeignLang, "LbForeignLang");
            this.LbForeignLang.Name = "LbForeignLang";
            // 
            // CbResetResults
            // 
            resources.ApplyResources(this.CbResetResults, "CbResetResults");
            this.CbResetResults.Name = "CbResetResults";
            this.CbResetResults.UseVisualStyleBackColor = true;
            // 
            // LbSynonym
            // 
            resources.ApplyResources(this.LbSynonym, "LbSynonym");
            this.LbSynonym.Name = "LbSynonym";
            // 
            // GroupOptions
            // 
            this.GroupOptions.Controls.Add(this.CbResetResults);
            resources.ApplyResources(this.GroupOptions, "GroupOptions");
            this.GroupOptions.Name = "GroupOptions";
            this.GroupOptions.TabStop = false;
            // 
            // BtnSpecialChar
            // 
            resources.ApplyResources(this.BtnSpecialChar, "BtnSpecialChar");
            this.BtnSpecialChar.Name = "BtnSpecialChar";
            this.BtnSpecialChar.UseVisualStyleBackColor = true;
            // 
            // GroupForeignLang
            // 
            this.GroupForeignLang.Controls.Add(this.TbForeignLang);
            this.GroupForeignLang.Controls.Add(this.TbForeignLangSynonym);
            this.GroupForeignLang.Controls.Add(this.LbSynonym);
            this.GroupForeignLang.Controls.Add(this.LbForeignLang);
            resources.ApplyResources(this.GroupForeignLang, "GroupForeignLang");
            this.GroupForeignLang.Name = "GroupForeignLang";
            this.GroupForeignLang.TabStop = false;
            // 
            // TbMotherTongue
            // 
            this.TbMotherTongue.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.TbMotherTongue, "TbMotherTongue");
            this.TbMotherTongue.Name = "TbMotherTongue";
            this.TbMotherTongue.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TbMotherTongue.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // LbMotherTongue
            // 
            resources.ApplyResources(this.LbMotherTongue, "LbMotherTongue");
            this.LbMotherTongue.Name = "LbMotherTongue";
            // 
            // GroupMotherTongue
            // 
            this.GroupMotherTongue.Controls.Add(this.LbMotherTongue);
            this.GroupMotherTongue.Controls.Add(this.TbMotherTongue);
            resources.ApplyResources(this.GroupMotherTongue, "GroupMotherTongue");
            this.GroupMotherTongue.Name = "GroupMotherTongue";
            this.GroupMotherTongue.TabStop = false;
            // 
            // VocabularyWordDialog
            // 
            this.AcceptButton = this.BtnContinue;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnSpecialChar);
            this.Controls.Add(this.GroupOptions);
            this.Controls.Add(this.GroupMotherTongue);
            this.Controls.Add(this.GroupForeignLang);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnContinue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VocabularyWordDialog";
            this.ShowInTaskbar = false;
            this.GroupOptions.ResumeLayout(false);
            this.GroupOptions.PerformLayout();
            this.GroupForeignLang.ResumeLayout(false);
            this.GroupForeignLang.PerformLayout();
            this.GroupMotherTongue.ResumeLayout(false);
            this.GroupMotherTongue.PerformLayout();
            this.ResumeLayout(false);

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