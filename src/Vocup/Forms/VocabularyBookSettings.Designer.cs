namespace Vocup.Forms
{
    partial class VocabularyBookSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VocabularyBookSettings));
            this.BtnSpecialChar = new System.Windows.Forms.Button();
            this.GroupOptions = new System.Windows.Forms.GroupBox();
            this.CbResetResults = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.TbMotherTongue = new System.Windows.Forms.TextBox();
            this.muttersprache = new System.Windows.Forms.Label();
            this.fremdsprache = new System.Windows.Forms.Label();
            this.TbForeignLang = new System.Windows.Forms.TextBox();
            this.GroupPracticeMode = new System.Windows.Forms.GroupBox();
            this.RbModeAskMotherTongue = new System.Windows.Forms.RadioButton();
            this.RbModeAskForeignLang = new System.Windows.Forms.RadioButton();
            this.GroupOptions.SuspendLayout();
            this.GroupPracticeMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSpecialChar
            // 
            resources.ApplyResources(this.BtnSpecialChar, "BtnSpecialChar");
            this.BtnSpecialChar.Name = "BtnSpecialChar";
            this.BtnSpecialChar.UseVisualStyleBackColor = true;
            this.BtnSpecialChar.Click += new System.EventHandler(this.BtnSpecialChar_Click);
            // 
            // GroupOptions
            // 
            this.GroupOptions.Controls.Add(this.CbResetResults);
            resources.ApplyResources(this.GroupOptions, "GroupOptions");
            this.GroupOptions.Name = "GroupOptions";
            this.GroupOptions.TabStop = false;
            // 
            // CbResetResults
            // 
            resources.ApplyResources(this.CbResetResults, "CbResetResults");
            this.CbResetResults.Name = "CbResetResults";
            this.CbResetResults.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // BtnOK
            // 
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.BtnOK, "BtnOK");
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // TbMotherTongue
            // 
            resources.ApplyResources(this.TbMotherTongue, "TbMotherTongue");
            this.TbMotherTongue.Name = "TbMotherTongue";
            this.TbMotherTongue.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TbMotherTongue.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // muttersprache
            // 
            resources.ApplyResources(this.muttersprache, "muttersprache");
            this.muttersprache.Name = "muttersprache";
            // 
            // fremdsprache
            // 
            resources.ApplyResources(this.fremdsprache, "fremdsprache");
            this.fremdsprache.Name = "fremdsprache";
            // 
            // TbForeignLang
            // 
            resources.ApplyResources(this.TbForeignLang, "TbForeignLang");
            this.TbForeignLang.Name = "TbForeignLang";
            this.TbForeignLang.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TbForeignLang.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // GroupPracticeMode
            // 
            this.GroupPracticeMode.Controls.Add(this.RbModeAskMotherTongue);
            this.GroupPracticeMode.Controls.Add(this.RbModeAskForeignLang);
            resources.ApplyResources(this.GroupPracticeMode, "GroupPracticeMode");
            this.GroupPracticeMode.Name = "GroupPracticeMode";
            this.GroupPracticeMode.TabStop = false;
            // 
            // RbModeAskMotherTongue
            // 
            resources.ApplyResources(this.RbModeAskMotherTongue, "RbModeAskMotherTongue");
            this.RbModeAskMotherTongue.Name = "RbModeAskMotherTongue";
            this.RbModeAskMotherTongue.TabStop = true;
            this.RbModeAskMotherTongue.UseVisualStyleBackColor = true;
            // 
            // RbModeAskForeignLang
            // 
            resources.ApplyResources(this.RbModeAskForeignLang, "RbModeAskForeignLang");
            this.RbModeAskForeignLang.Checked = true;
            this.RbModeAskForeignLang.Name = "RbModeAskForeignLang";
            this.RbModeAskForeignLang.TabStop = true;
            this.RbModeAskForeignLang.UseVisualStyleBackColor = true;
            // 
            // VocabularyBookSettings
            // 
            this.AcceptButton = this.BtnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.Controls.Add(this.GroupPracticeMode);
            this.Controls.Add(this.fremdsprache);
            this.Controls.Add(this.TbForeignLang);
            this.Controls.Add(this.muttersprache);
            this.Controls.Add(this.TbMotherTongue);
            this.Controls.Add(this.BtnSpecialChar);
            this.Controls.Add(this.GroupOptions);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VocabularyBookSettings";
            this.ShowInTaskbar = false;
            this.GroupOptions.ResumeLayout(false);
            this.GroupOptions.PerformLayout();
            this.GroupPracticeMode.ResumeLayout(false);
            this.GroupPracticeMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSpecialChar;
        public System.Windows.Forms.GroupBox GroupOptions;
        public System.Windows.Forms.CheckBox CbResetResults;
        private System.Windows.Forms.Button BtnCancel;
        public System.Windows.Forms.Button BtnOK;
        public System.Windows.Forms.TextBox TbMotherTongue;
        private System.Windows.Forms.Label muttersprache;
        private System.Windows.Forms.Label fremdsprache;
        public System.Windows.Forms.TextBox TbForeignLang;
        private System.Windows.Forms.GroupBox GroupPracticeMode;
        public System.Windows.Forms.RadioButton RbModeAskMotherTongue;
        public System.Windows.Forms.RadioButton RbModeAskForeignLang;
    }
}