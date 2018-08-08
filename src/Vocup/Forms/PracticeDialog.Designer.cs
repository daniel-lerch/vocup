namespace Vocup.Forms
{
    partial class PracticeDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PracticeDialog));
            this.GroupPractice = new System.Windows.Forms.GroupBox();
            this.TbForeignLangSynonym = new System.Windows.Forms.TextBox();
            this.TbForeignLang = new System.Windows.Forms.TextBox();
            this.TbMotherTongue = new System.Windows.Forms.TextBox();
            this.LbForeignLangSynonym = new System.Windows.Forms.Label();
            this.LbForeignLang = new System.Windows.Forms.Label();
            this.LbMotherTongue = new System.Windows.Forms.Label();
            this.GroupStatistics = new System.Windows.Forms.GroupBox();
            this.anzahl_falsch = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.anzahl_teilweise = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.anzahl_richtig = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TbPracticedCount = new System.Windows.Forms.TextBox();
            this.TbUnpracticedCount = new System.Windows.Forms.TextBox();
            this.TbPracticeCount = new System.Windows.Forms.TextBox();
            this.PbPracticeProgress = new System.Windows.Forms.ProgressBar();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnContinue = new System.Windows.Forms.Button();
            this.TbCorrectAnswer = new System.Windows.Forms.TextBox();
            this.BtnSpecialChar = new System.Windows.Forms.Button();
            this.RbPartlyCorrect = new System.Windows.Forms.RadioButton();
            this.RbWrong = new System.Windows.Forms.RadioButton();
            this.RbCorrect = new System.Windows.Forms.RadioButton();
            this.GroupUserEvaluation = new System.Windows.Forms.GroupBox();
            this.GroupPractice.SuspendLayout();
            this.GroupStatistics.SuspendLayout();
            this.GroupUserEvaluation.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupPractice
            // 
            this.GroupPractice.Controls.Add(this.TbForeignLangSynonym);
            this.GroupPractice.Controls.Add(this.TbForeignLang);
            this.GroupPractice.Controls.Add(this.TbMotherTongue);
            this.GroupPractice.Controls.Add(this.LbForeignLangSynonym);
            this.GroupPractice.Controls.Add(this.LbForeignLang);
            this.GroupPractice.Controls.Add(this.LbMotherTongue);
            resources.ApplyResources(this.GroupPractice, "GroupPractice");
            this.GroupPractice.Name = "GroupPractice";
            this.GroupPractice.TabStop = false;
            // 
            // TbForeignLangSynonym
            // 
            resources.ApplyResources(this.TbForeignLangSynonym, "TbForeignLangSynonym");
            this.TbForeignLangSynonym.Name = "TbForeignLangSynonym";
            this.TbForeignLangSynonym.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // TbForeignLang
            // 
            resources.ApplyResources(this.TbForeignLang, "TbForeignLang");
            this.TbForeignLang.Name = "TbForeignLang";
            this.TbForeignLang.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // TbMotherTongue
            // 
            resources.ApplyResources(this.TbMotherTongue, "TbMotherTongue");
            this.TbMotherTongue.Name = "TbMotherTongue";
            this.TbMotherTongue.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // LbForeignLangSynonym
            // 
            resources.ApplyResources(this.LbForeignLangSynonym, "LbForeignLangSynonym");
            this.LbForeignLangSynonym.Name = "LbForeignLangSynonym";
            // 
            // LbForeignLang
            // 
            resources.ApplyResources(this.LbForeignLang, "LbForeignLang");
            this.LbForeignLang.Name = "LbForeignLang";
            // 
            // LbMotherTongue
            // 
            resources.ApplyResources(this.LbMotherTongue, "LbMotherTongue");
            this.LbMotherTongue.Name = "LbMotherTongue";
            // 
            // GroupStatistics
            // 
            resources.ApplyResources(this.GroupStatistics, "GroupStatistics");
            this.GroupStatistics.Controls.Add(this.anzahl_falsch);
            this.GroupStatistics.Controls.Add(this.label6);
            this.GroupStatistics.Controls.Add(this.anzahl_teilweise);
            this.GroupStatistics.Controls.Add(this.label5);
            this.GroupStatistics.Controls.Add(this.label4);
            this.GroupStatistics.Controls.Add(this.anzahl_richtig);
            this.GroupStatistics.Controls.Add(this.label3);
            this.GroupStatistics.Controls.Add(this.label2);
            this.GroupStatistics.Controls.Add(this.label1);
            this.GroupStatistics.Controls.Add(this.TbPracticedCount);
            this.GroupStatistics.Controls.Add(this.TbUnpracticedCount);
            this.GroupStatistics.Controls.Add(this.TbPracticeCount);
            this.GroupStatistics.Controls.Add(this.PbPracticeProgress);
            this.GroupStatistics.Name = "GroupStatistics";
            this.GroupStatistics.TabStop = false;
            // 
            // anzahl_falsch
            // 
            this.anzahl_falsch.BackColor = System.Drawing.Color.Pink;
            this.anzahl_falsch.ForeColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.anzahl_falsch, "anzahl_falsch");
            this.anzahl_falsch.Name = "anzahl_falsch";
            this.anzahl_falsch.ReadOnly = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // anzahl_teilweise
            // 
            this.anzahl_teilweise.BackColor = System.Drawing.Color.Gold;
            this.anzahl_teilweise.ForeColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.anzahl_teilweise, "anzahl_teilweise");
            this.anzahl_teilweise.Name = "anzahl_teilweise";
            this.anzahl_teilweise.ReadOnly = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // anzahl_richtig
            // 
            this.anzahl_richtig.BackColor = System.Drawing.Color.LightGreen;
            this.anzahl_richtig.ForeColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.anzahl_richtig, "anzahl_richtig");
            this.anzahl_richtig.Name = "anzahl_richtig";
            this.anzahl_richtig.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // TbPracticedCount
            // 
            resources.ApplyResources(this.TbPracticedCount, "TbPracticedCount");
            this.TbPracticedCount.Name = "TbPracticedCount";
            this.TbPracticedCount.ReadOnly = true;
            // 
            // TbUnpracticedCount
            // 
            resources.ApplyResources(this.TbUnpracticedCount, "TbUnpracticedCount");
            this.TbUnpracticedCount.Name = "TbUnpracticedCount";
            this.TbUnpracticedCount.ReadOnly = true;
            // 
            // TbPracticeCount
            // 
            resources.ApplyResources(this.TbPracticeCount, "TbPracticeCount");
            this.TbPracticeCount.Name = "TbPracticeCount";
            this.TbPracticeCount.ReadOnly = true;
            // 
            // PbPracticeProgress
            // 
            resources.ApplyResources(this.PbPracticeProgress, "PbPracticeProgress");
            this.PbPracticeProgress.Name = "PbPracticeProgress";
            this.PbPracticeProgress.Step = 100;
            this.PbPracticeProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // BtnCancel
            // 
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // BtnContinue
            // 
            resources.ApplyResources(this.BtnContinue, "BtnContinue");
            this.BtnContinue.Name = "BtnContinue";
            this.BtnContinue.UseVisualStyleBackColor = true;
            this.BtnContinue.Click += new System.EventHandler(this.fortfahren_button_Click);
            // 
            // TbCorrectAnswer
            // 
            this.TbCorrectAnswer.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.TbCorrectAnswer, "TbCorrectAnswer");
            this.TbCorrectAnswer.Name = "TbCorrectAnswer";
            this.TbCorrectAnswer.ReadOnly = true;
            // 
            // BtnSpecialChar
            // 
            resources.ApplyResources(this.BtnSpecialChar, "BtnSpecialChar");
            this.BtnSpecialChar.Name = "BtnSpecialChar";
            this.BtnSpecialChar.UseVisualStyleBackColor = true;
            this.BtnSpecialChar.Click += new System.EventHandler(this.sonderzeichen_button_Click);
            // 
            // RbPartlyCorrect
            // 
            resources.ApplyResources(this.RbPartlyCorrect, "RbPartlyCorrect");
            this.RbPartlyCorrect.BackColor = System.Drawing.Color.Transparent;
            this.RbPartlyCorrect.Name = "RbPartlyCorrect";
            this.RbPartlyCorrect.TabStop = true;
            this.RbPartlyCorrect.UseVisualStyleBackColor = false;
            // 
            // RbWrong
            // 
            resources.ApplyResources(this.RbWrong, "RbWrong");
            this.RbWrong.BackColor = System.Drawing.Color.Transparent;
            this.RbWrong.Name = "RbWrong";
            this.RbWrong.TabStop = true;
            this.RbWrong.UseVisualStyleBackColor = false;
            // 
            // RbCorrect
            // 
            resources.ApplyResources(this.RbCorrect, "RbCorrect");
            this.RbCorrect.BackColor = System.Drawing.Color.Transparent;
            this.RbCorrect.Checked = true;
            this.RbCorrect.Name = "RbCorrect";
            this.RbCorrect.TabStop = true;
            this.RbCorrect.UseVisualStyleBackColor = false;
            // 
            // GroupUserEvaluation
            // 
            this.GroupUserEvaluation.Controls.Add(this.RbCorrect);
            this.GroupUserEvaluation.Controls.Add(this.RbWrong);
            this.GroupUserEvaluation.Controls.Add(this.RbPartlyCorrect);
            resources.ApplyResources(this.GroupUserEvaluation, "GroupUserEvaluation");
            this.GroupUserEvaluation.Name = "GroupUserEvaluation";
            this.GroupUserEvaluation.TabStop = false;
            // 
            // practise_dialog
            // 
            this.AcceptButton = this.BtnContinue;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.BtnCancel;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnSpecialChar);
            this.Controls.Add(this.BtnContinue);
            this.Controls.Add(this.GroupUserEvaluation);
            this.Controls.Add(this.TbCorrectAnswer);
            this.Controls.Add(this.GroupStatistics);
            this.Controls.Add(this.GroupPractice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "practise_dialog";
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_FormClosed);
            this.Load += new System.EventHandler(this.practise_dialog_Load);
            this.GroupPractice.ResumeLayout(false);
            this.GroupPractice.PerformLayout();
            this.GroupStatistics.ResumeLayout(false);
            this.GroupStatistics.PerformLayout();
            this.GroupUserEvaluation.ResumeLayout(false);
            this.GroupUserEvaluation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TextBox TbForeignLangSynonym;
        public System.Windows.Forms.TextBox TbForeignLang;
        public System.Windows.Forms.TextBox TbMotherTongue;
        private System.Windows.Forms.Label LbForeignLangSynonym;
        private System.Windows.Forms.ProgressBar PbPracticeProgress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbPracticedCount;
        public System.Windows.Forms.TextBox TbUnpracticedCount;
        private System.Windows.Forms.TextBox TbPracticeCount;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox anzahl_teilweise;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox anzahl_richtig;
        public System.Windows.Forms.TextBox anzahl_falsch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TbCorrectAnswer;
        private System.Windows.Forms.RadioButton RbPartlyCorrect;
        private System.Windows.Forms.RadioButton RbWrong;
        private System.Windows.Forms.RadioButton RbCorrect;
        private System.Windows.Forms.GroupBox GroupUserEvaluation;
        private System.Windows.Forms.GroupBox GroupPractice;
        private System.Windows.Forms.Label LbForeignLang;
        private System.Windows.Forms.Label LbMotherTongue;
        private System.Windows.Forms.GroupBox GroupStatistics;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnContinue;
        private System.Windows.Forms.Button BtnSpecialChar;
    }
}