namespace Vocup.Forms
{
    partial class EditWordDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditWordDialog));
            this.BtnContinue = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.TbForeignLangSynonym = new System.Windows.Forms.TextBox();
            this.TbForeignLang = new System.Windows.Forms.TextBox();
            this.LbForeignLang = new System.Windows.Forms.Label();
            this.CbResetResults = new System.Windows.Forms.CheckBox();
            this.LbSynonym = new System.Windows.Forms.Label();
            this.GroupOptions = new System.Windows.Forms.GroupBox();
            this.BtnSpecialChar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TbMotherTongue = new System.Windows.Forms.TextBox();
            this.LbMotherTongue = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.GroupOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnContinue
            // 
            this.BtnContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.BtnContinue, "BtnContinue");
            this.BtnContinue.Name = "BtnContinue";
            this.BtnContinue.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
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
            this.BtnSpecialChar.Click += new System.EventHandler(this.BtnSpecialChar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TbForeignLang);
            this.groupBox1.Controls.Add(this.TbForeignLangSynonym);
            this.groupBox1.Controls.Add(this.LbSynonym);
            this.groupBox1.Controls.Add(this.LbForeignLang);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LbMotherTongue);
            this.groupBox2.Controls.Add(this.TbMotherTongue);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // EditWordDialog
            // 
            this.AcceptButton = this.BtnContinue;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnSpecialChar);
            this.Controls.Add(this.GroupOptions);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnContinue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditWordDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Form_Load);
            this.GroupOptions.ResumeLayout(false);
            this.GroupOptions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.TextBox TbForeignLangSynonym;
        public System.Windows.Forms.TextBox TbForeignLang;
        public System.Windows.Forms.Label LbForeignLang;
        public System.Windows.Forms.CheckBox CbResetResults;
        public System.Windows.Forms.Label LbSynonym;
        private System.Windows.Forms.Button BtnSpecialChar;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox TbMotherTongue;
        public System.Windows.Forms.Label LbMotherTongue;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.GroupBox GroupOptions;
        public System.Windows.Forms.Button BtnContinue;
        public System.Windows.Forms.Button BtnCancel;
    }
}