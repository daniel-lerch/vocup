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
            this.option_box = new System.Windows.Forms.GroupBox();
            this.CbResetResults = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.TbMotherTongue = new System.Windows.Forms.TextBox();
            this.muttersprache = new System.Windows.Forms.Label();
            this.fremdsprache = new System.Windows.Forms.Label();
            this.TbForeignTongue = new System.Windows.Forms.TextBox();
            this.uebersetzungsrichtung = new System.Windows.Forms.GroupBox();
            this.RbModeAskMotherTongue = new System.Windows.Forms.RadioButton();
            this.RbModeAskForeignTongue = new System.Windows.Forms.RadioButton();
            this.option_box.SuspendLayout();
            this.uebersetzungsrichtung.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSpecialChar
            // 
            resources.ApplyResources(this.BtnSpecialChar, "BtnSpecialChar");
            this.BtnSpecialChar.Name = "BtnSpecialChar";
            this.BtnSpecialChar.UseVisualStyleBackColor = true;
            this.BtnSpecialChar.Click += new System.EventHandler(this.BtnSpecialChar_Click);
            // 
            // option_box
            // 
            this.option_box.Controls.Add(this.CbResetResults);
            resources.ApplyResources(this.option_box, "option_box");
            this.option_box.Name = "option_box";
            this.option_box.TabStop = false;
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
            // TbForeignTongue
            // 
            resources.ApplyResources(this.TbForeignTongue, "TbForeignTongue");
            this.TbForeignTongue.Name = "TbForeignTongue";
            this.TbForeignTongue.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TbForeignTongue.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // uebersetzungsrichtung
            // 
            this.uebersetzungsrichtung.Controls.Add(this.RbModeAskMotherTongue);
            this.uebersetzungsrichtung.Controls.Add(this.RbModeAskForeignTongue);
            resources.ApplyResources(this.uebersetzungsrichtung, "uebersetzungsrichtung");
            this.uebersetzungsrichtung.Name = "uebersetzungsrichtung";
            this.uebersetzungsrichtung.TabStop = false;
            // 
            // RbModeAskMotherTongue
            // 
            resources.ApplyResources(this.RbModeAskMotherTongue, "RbModeAskMotherTongue");
            this.RbModeAskMotherTongue.Name = "RbModeAskMotherTongue";
            this.RbModeAskMotherTongue.TabStop = true;
            this.RbModeAskMotherTongue.UseVisualStyleBackColor = true;
            // 
            // RbModeAskForeignTongue
            // 
            resources.ApplyResources(this.RbModeAskForeignTongue, "RbModeAskForeignTongue");
            this.RbModeAskForeignTongue.Checked = true;
            this.RbModeAskForeignTongue.Name = "RbModeAskForeignTongue";
            this.RbModeAskForeignTongue.TabStop = true;
            this.RbModeAskForeignTongue.UseVisualStyleBackColor = true;
            // 
            // VocabularyBookSettings
            // 
            this.AcceptButton = this.BtnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.BtnCancel;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.uebersetzungsrichtung);
            this.Controls.Add(this.fremdsprache);
            this.Controls.Add(this.TbForeignTongue);
            this.Controls.Add(this.muttersprache);
            this.Controls.Add(this.TbMotherTongue);
            this.Controls.Add(this.BtnSpecialChar);
            this.Controls.Add(this.option_box);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VocabularyBookSettings";
            this.ShowInTaskbar = false;
            this.option_box.ResumeLayout(false);
            this.option_box.PerformLayout();
            this.uebersetzungsrichtung.ResumeLayout(false);
            this.uebersetzungsrichtung.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSpecialChar;
        public System.Windows.Forms.GroupBox option_box;
        public System.Windows.Forms.CheckBox CbResetResults;
        private System.Windows.Forms.Button BtnCancel;
        public System.Windows.Forms.Button BtnOK;
        public System.Windows.Forms.TextBox TbMotherTongue;
        private System.Windows.Forms.Label muttersprache;
        private System.Windows.Forms.Label fremdsprache;
        public System.Windows.Forms.TextBox TbForeignTongue;
        private System.Windows.Forms.GroupBox uebersetzungsrichtung;
        public System.Windows.Forms.RadioButton RbModeAskMotherTongue;
        public System.Windows.Forms.RadioButton RbModeAskForeignTongue;
    }
}