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
            if (disposing)
            {
                components?.Dispose();
                player.Dispose();
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
            TbForeignLangSynonym = new System.Windows.Forms.TextBox();
            TbForeignLang = new System.Windows.Forms.TextBox();
            TbMotherTongue = new System.Windows.Forms.TextBox();
            LbForeignLangSynonym = new System.Windows.Forms.Label();
            LbForeignLang = new System.Windows.Forms.Label();
            LbMotherTongue = new System.Windows.Forms.Label();
            GroupStatistics = new System.Windows.Forms.GroupBox();
            TbWrongCount = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            TbPartlyCorrectCount = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            TbCorrectCount = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            TbPracticedCount = new System.Windows.Forms.TextBox();
            TbUnpracticedCount = new System.Windows.Forms.TextBox();
            TbPracticeCount = new System.Windows.Forms.TextBox();
            PbPracticeProgress = new System.Windows.Forms.ProgressBar();
            BtnCancel = new System.Windows.Forms.Button();
            BtnContinue = new System.Windows.Forms.Button();
            TbCorrectAnswer = new System.Windows.Forms.TextBox();
            BtnSpecialChar = new System.Windows.Forms.Button();
            RbPartlyCorrect = new System.Windows.Forms.RadioButton();
            RbWrong = new System.Windows.Forms.RadioButton();
            RbCorrect = new System.Windows.Forms.RadioButton();
            GroupUserEvaluation = new System.Windows.Forms.GroupBox();
            TableLayout = new System.Windows.Forms.TableLayoutPanel();
            PanelMotherTongue = new System.Windows.Forms.Panel();
            PanelForeignLang = new System.Windows.Forms.Panel();
            PanelForeignLangSynonym = new System.Windows.Forms.Panel();
            GroupStatistics.SuspendLayout();
            GroupUserEvaluation.SuspendLayout();
            TableLayout.SuspendLayout();
            PanelMotherTongue.SuspendLayout();
            PanelForeignLang.SuspendLayout();
            PanelForeignLangSynonym.SuspendLayout();
            SuspendLayout();
            // 
            // TbForeignLangSynonym
            // 
            resources.ApplyResources(TbForeignLangSynonym, "TbForeignLangSynonym");
            TbForeignLangSynonym.Name = "TbForeignLangSynonym";
            TbForeignLangSynonym.Enter += TextBox_Enter;
            // 
            // TbForeignLang
            // 
            resources.ApplyResources(TbForeignLang, "TbForeignLang");
            TbForeignLang.Name = "TbForeignLang";
            TbForeignLang.Enter += TextBox_Enter;
            // 
            // TbMotherTongue
            // 
            resources.ApplyResources(TbMotherTongue, "TbMotherTongue");
            TbMotherTongue.Name = "TbMotherTongue";
            TbMotherTongue.Enter += TextBox_Enter;
            // 
            // LbForeignLangSynonym
            // 
            resources.ApplyResources(LbForeignLangSynonym, "LbForeignLangSynonym");
            LbForeignLangSynonym.Name = "LbForeignLangSynonym";
            // 
            // LbForeignLang
            // 
            resources.ApplyResources(LbForeignLang, "LbForeignLang");
            LbForeignLang.Name = "LbForeignLang";
            // 
            // LbMotherTongue
            // 
            resources.ApplyResources(LbMotherTongue, "LbMotherTongue");
            LbMotherTongue.Name = "LbMotherTongue";
            // 
            // GroupStatistics
            // 
            resources.ApplyResources(GroupStatistics, "GroupStatistics");
            GroupStatistics.Controls.Add(TbWrongCount);
            GroupStatistics.Controls.Add(label6);
            GroupStatistics.Controls.Add(TbPartlyCorrectCount);
            GroupStatistics.Controls.Add(label5);
            GroupStatistics.Controls.Add(label4);
            GroupStatistics.Controls.Add(TbCorrectCount);
            GroupStatistics.Controls.Add(label3);
            GroupStatistics.Controls.Add(label2);
            GroupStatistics.Controls.Add(label1);
            GroupStatistics.Controls.Add(TbPracticedCount);
            GroupStatistics.Controls.Add(TbUnpracticedCount);
            GroupStatistics.Controls.Add(TbPracticeCount);
            GroupStatistics.Controls.Add(PbPracticeProgress);
            GroupStatistics.Name = "GroupStatistics";
            GroupStatistics.TabStop = false;
            // 
            // TbWrongCount
            // 
            TbWrongCount.BackColor = System.Drawing.Color.Pink;
            resources.ApplyResources(TbWrongCount, "TbWrongCount");
            TbWrongCount.Name = "TbWrongCount";
            TbWrongCount.ReadOnly = true;
            TbWrongCount.TabStop = false;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // TbPartlyCorrectCount
            // 
            TbPartlyCorrectCount.BackColor = System.Drawing.Color.Gold;
            resources.ApplyResources(TbPartlyCorrectCount, "TbPartlyCorrectCount");
            TbPartlyCorrectCount.Name = "TbPartlyCorrectCount";
            TbPartlyCorrectCount.ReadOnly = true;
            TbPartlyCorrectCount.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // TbCorrectCount
            // 
            TbCorrectCount.BackColor = System.Drawing.Color.LightGreen;
            resources.ApplyResources(TbCorrectCount, "TbCorrectCount");
            TbCorrectCount.Name = "TbCorrectCount";
            TbCorrectCount.ReadOnly = true;
            TbCorrectCount.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // TbPracticedCount
            // 
            resources.ApplyResources(TbPracticedCount, "TbPracticedCount");
            TbPracticedCount.Name = "TbPracticedCount";
            TbPracticedCount.ReadOnly = true;
            TbPracticedCount.TabStop = false;
            // 
            // TbUnpracticedCount
            // 
            resources.ApplyResources(TbUnpracticedCount, "TbUnpracticedCount");
            TbUnpracticedCount.Name = "TbUnpracticedCount";
            TbUnpracticedCount.ReadOnly = true;
            TbUnpracticedCount.TabStop = false;
            // 
            // TbPracticeCount
            // 
            resources.ApplyResources(TbPracticeCount, "TbPracticeCount");
            TbPracticeCount.Name = "TbPracticeCount";
            TbPracticeCount.ReadOnly = true;
            TbPracticeCount.TabStop = false;
            // 
            // PbPracticeProgress
            // 
            resources.ApplyResources(PbPracticeProgress, "PbPracticeProgress");
            PbPracticeProgress.Name = "PbPracticeProgress";
            PbPracticeProgress.Step = 100;
            PbPracticeProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // BtnCancel
            // 
            resources.ApplyResources(BtnCancel, "BtnCancel");
            BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            BtnCancel.Name = "BtnCancel";
            BtnCancel.UseVisualStyleBackColor = true;
            // 
            // BtnContinue
            // 
            resources.ApplyResources(BtnContinue, "BtnContinue");
            BtnContinue.Name = "BtnContinue";
            BtnContinue.UseVisualStyleBackColor = true;
            BtnContinue.Click += BtnContinue_Click;
            // 
            // TbCorrectAnswer
            // 
            TbCorrectAnswer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            TableLayout.SetColumnSpan(TbCorrectAnswer, 2);
            resources.ApplyResources(TbCorrectAnswer, "TbCorrectAnswer");
            TbCorrectAnswer.Name = "TbCorrectAnswer";
            TbCorrectAnswer.ReadOnly = true;
            TbCorrectAnswer.TabStop = false;
            // 
            // BtnSpecialChar
            // 
            resources.ApplyResources(BtnSpecialChar, "BtnSpecialChar");
            BtnSpecialChar.Name = "BtnSpecialChar";
            BtnSpecialChar.UseVisualStyleBackColor = true;
            // 
            // RbPartlyCorrect
            // 
            resources.ApplyResources(RbPartlyCorrect, "RbPartlyCorrect");
            RbPartlyCorrect.BackColor = System.Drawing.Color.Transparent;
            RbPartlyCorrect.Name = "RbPartlyCorrect";
            RbPartlyCorrect.TabStop = true;
            RbPartlyCorrect.UseVisualStyleBackColor = false;
            // 
            // RbWrong
            // 
            resources.ApplyResources(RbWrong, "RbWrong");
            RbWrong.BackColor = System.Drawing.Color.Transparent;
            RbWrong.Name = "RbWrong";
            RbWrong.TabStop = true;
            RbWrong.UseVisualStyleBackColor = false;
            // 
            // RbCorrect
            // 
            resources.ApplyResources(RbCorrect, "RbCorrect");
            RbCorrect.BackColor = System.Drawing.Color.Transparent;
            RbCorrect.Checked = true;
            RbCorrect.Name = "RbCorrect";
            RbCorrect.TabStop = true;
            RbCorrect.UseVisualStyleBackColor = false;
            // 
            // GroupUserEvaluation
            // 
            resources.ApplyResources(GroupUserEvaluation, "GroupUserEvaluation");
            GroupUserEvaluation.Controls.Add(RbCorrect);
            GroupUserEvaluation.Controls.Add(RbWrong);
            GroupUserEvaluation.Controls.Add(RbPartlyCorrect);
            GroupUserEvaluation.Name = "GroupUserEvaluation";
            GroupUserEvaluation.TabStop = false;
            // 
            // TableLayout
            // 
            resources.ApplyResources(TableLayout, "TableLayout");
            TableLayout.Controls.Add(PanelMotherTongue, 0, 0);
            TableLayout.Controls.Add(PanelForeignLang, 1, 0);
            TableLayout.Controls.Add(TbCorrectAnswer, 0, 2);
            TableLayout.Controls.Add(PanelForeignLangSynonym, 1, 1);
            TableLayout.Name = "TableLayout";
            // 
            // PanelMotherTongue
            // 
            PanelMotherTongue.Controls.Add(LbMotherTongue);
            PanelMotherTongue.Controls.Add(TbMotherTongue);
            resources.ApplyResources(PanelMotherTongue, "PanelMotherTongue");
            PanelMotherTongue.Name = "PanelMotherTongue";
            TableLayout.SetRowSpan(PanelMotherTongue, 2);
            // 
            // PanelForeignLang
            // 
            PanelForeignLang.Controls.Add(LbForeignLang);
            PanelForeignLang.Controls.Add(TbForeignLang);
            resources.ApplyResources(PanelForeignLang, "PanelForeignLang");
            PanelForeignLang.Name = "PanelForeignLang";
            // 
            // PanelForeignLangSynonym
            // 
            PanelForeignLangSynonym.Controls.Add(TbForeignLangSynonym);
            PanelForeignLangSynonym.Controls.Add(LbForeignLangSynonym);
            resources.ApplyResources(PanelForeignLangSynonym, "PanelForeignLangSynonym");
            PanelForeignLangSynonym.Name = "PanelForeignLangSynonym";
            // 
            // PracticeDialog
            // 
            AcceptButton = BtnContinue;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = BtnCancel;
            Controls.Add(TableLayout);
            Controls.Add(BtnCancel);
            Controls.Add(BtnSpecialChar);
            Controls.Add(BtnContinue);
            Controls.Add(GroupUserEvaluation);
            Controls.Add(GroupStatistics);
            Name = "PracticeDialog";
            ShowInTaskbar = false;
            FormClosing += Form_FormClosing;
            FormClosed += Form_FormClosed;
            Load += Form_Load;
            GroupStatistics.ResumeLayout(false);
            GroupStatistics.PerformLayout();
            GroupUserEvaluation.ResumeLayout(false);
            GroupUserEvaluation.PerformLayout();
            TableLayout.ResumeLayout(false);
            TableLayout.PerformLayout();
            PanelMotherTongue.ResumeLayout(false);
            PanelMotherTongue.PerformLayout();
            PanelForeignLang.ResumeLayout(false);
            PanelForeignLang.PerformLayout();
            PanelForeignLangSynonym.ResumeLayout(false);
            PanelForeignLangSynonym.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Label LbForeignLangSynonym;
        private System.Windows.Forms.ProgressBar PbPracticeProgress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbPracticedCount;
        private System.Windows.Forms.TextBox TbPracticeCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton RbPartlyCorrect;
        private System.Windows.Forms.RadioButton RbWrong;
        private System.Windows.Forms.RadioButton RbCorrect;
        private System.Windows.Forms.GroupBox GroupUserEvaluation;
        private System.Windows.Forms.Label LbForeignLang;
        private System.Windows.Forms.Label LbMotherTongue;
        private System.Windows.Forms.GroupBox GroupStatistics;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnContinue;
        private System.Windows.Forms.Button BtnSpecialChar;
        private System.Windows.Forms.TextBox TbForeignLangSynonym;
        private System.Windows.Forms.TextBox TbForeignLang;
        private System.Windows.Forms.TextBox TbMotherTongue;
        private System.Windows.Forms.TextBox TbUnpracticedCount;
        private System.Windows.Forms.TextBox TbPartlyCorrectCount;
        private System.Windows.Forms.TextBox TbCorrectCount;
        private System.Windows.Forms.TextBox TbWrongCount;
        private System.Windows.Forms.TextBox TbCorrectAnswer;
        private System.Windows.Forms.TableLayoutPanel TableLayout;
        private System.Windows.Forms.Panel PanelMotherTongue;
        private System.Windows.Forms.Panel PanelForeignLang;
        private System.Windows.Forms.Panel PanelForeignLangSynonym;
    }
}