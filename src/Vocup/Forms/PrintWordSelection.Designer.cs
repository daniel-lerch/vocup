namespace Vocup.Forms
{
    partial class PrintWordSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintWordSelection));
            this.label1 = new System.Windows.Forms.Label();
            this.ListBox = new System.Windows.Forms.CheckedListBox();
            this.BtnCheckAll = new System.Windows.Forms.Button();
            this.BtnUncheckAll = new System.Windows.Forms.Button();
            this.BtnContinue = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.GroupPracticeMode = new System.Windows.Forms.GroupBox();
            this.RbAskForMotherTongue = new System.Windows.Forms.RadioButton();
            this.RbAskForForeignLang = new System.Windows.Forms.RadioButton();
            this.GroupFilter = new System.Windows.Forms.GroupBox();
            this.CbFullyPracticed = new System.Windows.Forms.CheckBox();
            this.CbCorrectlyPracticed = new System.Windows.Forms.CheckBox();
            this.CbWronglyPracticed = new System.Windows.Forms.CheckBox();
            this.CbUnpracticed = new System.Windows.Forms.CheckBox();
            this.PrintList = new System.Drawing.Printing.PrintDocument();
            this.GroupPracticeMode.SuspendLayout();
            this.GroupFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ListBox
            // 
            this.ListBox.CheckOnClick = true;
            this.ListBox.FormattingEnabled = true;
            resources.ApplyResources(this.ListBox, "ListBox");
            this.ListBox.Name = "ListBox";
            this.ListBox.SelectedValueChanged += new System.EventHandler(this.ListBox_SelectedValueChanged);
            // 
            // BtnCheckAll
            // 
            resources.ApplyResources(this.BtnCheckAll, "BtnCheckAll");
            this.BtnCheckAll.Name = "BtnCheckAll";
            this.BtnCheckAll.UseVisualStyleBackColor = true;
            this.BtnCheckAll.Click += new System.EventHandler(this.BtnCheckAll_Click);
            // 
            // BtnUncheckAll
            // 
            resources.ApplyResources(this.BtnUncheckAll, "BtnUncheckAll");
            this.BtnUncheckAll.Name = "BtnUncheckAll";
            this.BtnUncheckAll.UseVisualStyleBackColor = true;
            this.BtnUncheckAll.Click += new System.EventHandler(this.BtnUncheckAll_Click);
            // 
            // BtnContinue
            // 
            this.BtnContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.BtnContinue, "BtnContinue");
            this.BtnContinue.Name = "BtnContinue";
            this.BtnContinue.UseVisualStyleBackColor = true;
            this.BtnContinue.Click += new System.EventHandler(this.BtnContinue_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // GroupPracticeMode
            // 
            this.GroupPracticeMode.Controls.Add(this.RbAskForMotherTongue);
            this.GroupPracticeMode.Controls.Add(this.RbAskForForeignLang);
            resources.ApplyResources(this.GroupPracticeMode, "GroupPracticeMode");
            this.GroupPracticeMode.Name = "GroupPracticeMode";
            this.GroupPracticeMode.TabStop = false;
            // 
            // RbAskForMotherTongue
            // 
            resources.ApplyResources(this.RbAskForMotherTongue, "RbAskForMotherTongue");
            this.RbAskForMotherTongue.Name = "RbAskForMotherTongue";
            this.RbAskForMotherTongue.UseVisualStyleBackColor = true;
            // 
            // RbAskForForeignLang
            // 
            resources.ApplyResources(this.RbAskForForeignLang, "RbAskForForeignLang");
            this.RbAskForForeignLang.Checked = true;
            this.RbAskForForeignLang.Name = "RbAskForForeignLang";
            this.RbAskForForeignLang.TabStop = true;
            this.RbAskForForeignLang.UseVisualStyleBackColor = true;
            // 
            // GroupFilter
            // 
            this.GroupFilter.Controls.Add(this.CbFullyPracticed);
            this.GroupFilter.Controls.Add(this.CbCorrectlyPracticed);
            this.GroupFilter.Controls.Add(this.CbWronglyPracticed);
            this.GroupFilter.Controls.Add(this.CbUnpracticed);
            resources.ApplyResources(this.GroupFilter, "GroupFilter");
            this.GroupFilter.Name = "GroupFilter";
            this.GroupFilter.TabStop = false;
            // 
            // CbFullyPracticed
            // 
            resources.ApplyResources(this.CbFullyPracticed, "CbFullyPracticed");
            this.CbFullyPracticed.Name = "CbFullyPracticed";
            this.CbFullyPracticed.UseVisualStyleBackColor = true;
            this.CbFullyPracticed.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // CbCorrectlyPracticed
            // 
            resources.ApplyResources(this.CbCorrectlyPracticed, "CbCorrectlyPracticed");
            this.CbCorrectlyPracticed.Name = "CbCorrectlyPracticed";
            this.CbCorrectlyPracticed.UseVisualStyleBackColor = true;
            this.CbCorrectlyPracticed.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // CbWronglyPracticed
            // 
            resources.ApplyResources(this.CbWronglyPracticed, "CbWronglyPracticed");
            this.CbWronglyPracticed.Name = "CbWronglyPracticed";
            this.CbWronglyPracticed.UseVisualStyleBackColor = true;
            this.CbWronglyPracticed.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // CbUnpracticed
            // 
            resources.ApplyResources(this.CbUnpracticed, "CbUnpracticed");
            this.CbUnpracticed.Name = "CbUnpracticed";
            this.CbUnpracticed.UseVisualStyleBackColor = true;
            this.CbUnpracticed.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // PrintList
            // 
            this.PrintList.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintList_PrintPage);
            // 
            // PrintWordSelection
            // 
            this.AcceptButton = this.BtnContinue;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupFilter);
            this.Controls.Add(this.GroupPracticeMode);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnContinue);
            this.Controls.Add(this.BtnUncheckAll);
            this.Controls.Add(this.BtnCheckAll);
            this.Controls.Add(this.ListBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintWordSelection";
            this.ShowInTaskbar = false;
            this.GroupPracticeMode.ResumeLayout(false);
            this.GroupPracticeMode.PerformLayout();
            this.GroupFilter.ResumeLayout(false);
            this.GroupFilter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnCheckAll;
        private System.Windows.Forms.Button BtnUncheckAll;
        private System.Windows.Forms.Button BtnContinue;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.CheckedListBox ListBox;
        private System.Windows.Forms.GroupBox GroupPracticeMode;
        private System.Windows.Forms.RadioButton RbAskForMotherTongue;
        private System.Windows.Forms.RadioButton RbAskForForeignLang;
        private System.Windows.Forms.GroupBox GroupFilter;
        private System.Windows.Forms.CheckBox CbWronglyPracticed;
        private System.Windows.Forms.CheckBox CbUnpracticed;
        private System.Windows.Forms.CheckBox CbCorrectlyPracticed;
        private System.Windows.Forms.CheckBox CbFullyPracticed;
        private System.Drawing.Printing.PrintDocument PrintList;
    }
}