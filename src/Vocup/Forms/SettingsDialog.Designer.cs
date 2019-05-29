namespace Vocup
{
    partial class SettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.GroupStartScreen = new System.Windows.Forms.GroupBox();
            this.BtnResetStartScreen = new System.Windows.Forms.Button();
            this.RbEmptyStart = new System.Windows.Forms.RadioButton();
            this.RbRecentFile = new System.Windows.Forms.RadioButton();
            this.TrbWrongRight = new System.Windows.Forms.TrackBar();
            this.TrbUnknown = new System.Windows.Forms.TrackBar();
            this.TabControlMain = new System.Windows.Forms.TabControl();
            this.TabGeneral = new System.Windows.Forms.TabPage();
            this.GroupVocabularyList = new System.Windows.Forms.GroupBox();
            this.CbColumnResize = new System.Windows.Forms.CheckBox();
            this.CbGridLines = new System.Windows.Forms.CheckBox();
            this.GroupVhrPath = new System.Windows.Forms.GroupBox();
            this.BtnVhrPath = new System.Windows.Forms.Button();
            this.TbVhrPath = new System.Windows.Forms.TextBox();
            this.GroupVhfPath = new System.Windows.Forms.GroupBox();
            this.BtnVhfPath = new System.Windows.Forms.Button();
            this.TbVhfPath = new System.Windows.Forms.TextBox();
            this.GroupUpdate = new System.Windows.Forms.GroupBox();
            this.CbDisableInternetServices = new System.Windows.Forms.CheckBox();
            this.GroupSave = new System.Windows.Forms.GroupBox();
            this.CbAutoSave = new System.Windows.Forms.CheckBox();
            this.TabPractice = new System.Windows.Forms.TabPage();
            this.GroupEvaluation = new System.Windows.Forms.GroupBox();
            this.CbManualCheck = new System.Windows.Forms.CheckBox();
            this.CbEvaluationSystem = new System.Windows.Forms.ComboBox();
            this.LbGradeSystem = new System.Windows.Forms.Label();
            this.CbPracticeResult = new System.Windows.Forms.CheckBox();
            this.GroupUserInterface = new System.Windows.Forms.GroupBox();
            this.CbColoredTextfield = new System.Windows.Forms.CheckBox();
            this.CbAcousticFeedback = new System.Windows.Forms.CheckBox();
            this.CbSingleContinueButton = new System.Windows.Forms.CheckBox();
            this.GroupNearlyCorrect = new System.Windows.Forms.GroupBox();
            this.CbTolerateWhiteSpace = new System.Windows.Forms.CheckBox();
            this.CbTolerateNoSynonym = new System.Windows.Forms.CheckBox();
            this.CbTolerateArticle = new System.Windows.Forms.CheckBox();
            this.CbToleratePunctuationMark = new System.Windows.Forms.CheckBox();
            this.CbTolerateSpecialChar = new System.Windows.Forms.CheckBox();
            this.TabPracticeSelect = new System.Windows.Forms.TabPage();
            this.BtnResetPracticeSelect = new System.Windows.Forms.Button();
            this.GroupSelectionMix = new System.Windows.Forms.GroupBox();
            this.LbWronglyPracticed = new System.Windows.Forms.Label();
            this.LbCorrectlyPracticed = new System.Windows.Forms.Label();
            this.LbUnpracticed = new System.Windows.Forms.Label();
            this.LbPercentageUnpracticed = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.LbPercentageWrongCorrect = new System.Windows.Forms.Label();
            this.GroupRepetitions = new System.Windows.Forms.GroupBox();
            this.LbPracticeCount = new System.Windows.Forms.Label();
            this.PnlPracticeCount = new System.Windows.Forms.Panel();
            this.LbTrb6 = new System.Windows.Forms.Label();
            this.LbTrb2 = new System.Windows.Forms.Label();
            this.LbTrb5 = new System.Windows.Forms.Label();
            this.LbTrb3 = new System.Windows.Forms.Label();
            this.LbTrb4 = new System.Windows.Forms.Label();
            this.TrbRepetitions = new System.Windows.Forms.TrackBar();
            this.GroupLanguage = new System.Windows.Forms.GroupBox();
            this.CbLanguage = new System.Windows.Forms.ComboBox();
            this.LbLanguage = new System.Windows.Forms.Label();
            this.GroupStartScreen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrbWrongRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrbUnknown)).BeginInit();
            this.TabControlMain.SuspendLayout();
            this.TabGeneral.SuspendLayout();
            this.GroupVocabularyList.SuspendLayout();
            this.GroupVhrPath.SuspendLayout();
            this.GroupVhfPath.SuspendLayout();
            this.GroupUpdate.SuspendLayout();
            this.GroupSave.SuspendLayout();
            this.TabPractice.SuspendLayout();
            this.GroupEvaluation.SuspendLayout();
            this.GroupUserInterface.SuspendLayout();
            this.GroupNearlyCorrect.SuspendLayout();
            this.TabPracticeSelect.SuspendLayout();
            this.GroupSelectionMix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.GroupRepetitions.SuspendLayout();
            this.PnlPracticeCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrbRepetitions)).BeginInit();
            this.GroupLanguage.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnOk
            // 
            resources.ApplyResources(this.BtnOk, "BtnOk");
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // GroupStartScreen
            // 
            this.GroupStartScreen.BackColor = System.Drawing.Color.Transparent;
            this.GroupStartScreen.Controls.Add(this.BtnResetStartScreen);
            this.GroupStartScreen.Controls.Add(this.RbEmptyStart);
            this.GroupStartScreen.Controls.Add(this.RbRecentFile);
            resources.ApplyResources(this.GroupStartScreen, "GroupStartScreen");
            this.GroupStartScreen.Name = "GroupStartScreen";
            this.GroupStartScreen.TabStop = false;
            // 
            // BtnResetStartScreen
            // 
            resources.ApplyResources(this.BtnResetStartScreen, "BtnResetStartScreen");
            this.BtnResetStartScreen.Name = "BtnResetStartScreen";
            this.BtnResetStartScreen.UseVisualStyleBackColor = true;
            this.BtnResetStartScreen.Click += new System.EventHandler(this.BtnResetStartScreen_Click);
            // 
            // RbEmptyStart
            // 
            resources.ApplyResources(this.RbEmptyStart, "RbEmptyStart");
            this.RbEmptyStart.Name = "RbEmptyStart";
            this.RbEmptyStart.TabStop = true;
            this.RbEmptyStart.UseVisualStyleBackColor = true;
            // 
            // RbRecentFile
            // 
            resources.ApplyResources(this.RbRecentFile, "RbRecentFile");
            this.RbRecentFile.Checked = true;
            this.RbRecentFile.Name = "RbRecentFile";
            this.RbRecentFile.TabStop = true;
            this.RbRecentFile.UseVisualStyleBackColor = true;
            // 
            // TrbWrongRight
            // 
            this.TrbWrongRight.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.TrbWrongRight, "TrbWrongRight");
            this.TrbWrongRight.Minimum = 1;
            this.TrbWrongRight.Name = "TrbWrongRight";
            this.TrbWrongRight.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.TrbWrongRight.Value = 5;
            this.TrbWrongRight.ValueChanged += new System.EventHandler(this.TrbWrongRight_ValueChanged);
            // 
            // TrbUnknown
            // 
            this.TrbUnknown.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.TrbUnknown, "TrbUnknown");
            this.TrbUnknown.Maximum = 8;
            this.TrbUnknown.Minimum = 1;
            this.TrbUnknown.Name = "TrbUnknown";
            this.TrbUnknown.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.TrbUnknown.Value = 5;
            this.TrbUnknown.ValueChanged += new System.EventHandler(this.TrbUnknown_ValueChanged);
            // 
            // TabControlMain
            // 
            this.TabControlMain.Controls.Add(this.TabGeneral);
            this.TabControlMain.Controls.Add(this.TabPractice);
            this.TabControlMain.Controls.Add(this.TabPracticeSelect);
            resources.ApplyResources(this.TabControlMain, "TabControlMain");
            this.TabControlMain.Name = "TabControlMain";
            this.TabControlMain.SelectedIndex = 0;
            // 
            // TabGeneral
            // 
            this.TabGeneral.BackColor = System.Drawing.Color.White;
            this.TabGeneral.Controls.Add(this.GroupLanguage);
            this.TabGeneral.Controls.Add(this.GroupVocabularyList);
            this.TabGeneral.Controls.Add(this.GroupVhrPath);
            this.TabGeneral.Controls.Add(this.GroupVhfPath);
            this.TabGeneral.Controls.Add(this.GroupUpdate);
            this.TabGeneral.Controls.Add(this.GroupSave);
            this.TabGeneral.Controls.Add(this.GroupStartScreen);
            resources.ApplyResources(this.TabGeneral, "TabGeneral");
            this.TabGeneral.Name = "TabGeneral";
            // 
            // GroupVocabularyList
            // 
            this.GroupVocabularyList.BackColor = System.Drawing.Color.Transparent;
            this.GroupVocabularyList.Controls.Add(this.CbColumnResize);
            this.GroupVocabularyList.Controls.Add(this.CbGridLines);
            resources.ApplyResources(this.GroupVocabularyList, "GroupVocabularyList");
            this.GroupVocabularyList.Name = "GroupVocabularyList";
            this.GroupVocabularyList.TabStop = false;
            // 
            // CbColumnResize
            // 
            resources.ApplyResources(this.CbColumnResize, "CbColumnResize");
            this.CbColumnResize.Name = "CbColumnResize";
            this.CbColumnResize.UseVisualStyleBackColor = true;
            // 
            // CbGridLines
            // 
            resources.ApplyResources(this.CbGridLines, "CbGridLines");
            this.CbGridLines.Name = "CbGridLines";
            this.CbGridLines.UseVisualStyleBackColor = true;
            // 
            // GroupVhrPath
            // 
            this.GroupVhrPath.Controls.Add(this.BtnVhrPath);
            this.GroupVhrPath.Controls.Add(this.TbVhrPath);
            resources.ApplyResources(this.GroupVhrPath, "GroupVhrPath");
            this.GroupVhrPath.Name = "GroupVhrPath";
            this.GroupVhrPath.TabStop = false;
            // 
            // BtnVhrPath
            // 
            resources.ApplyResources(this.BtnVhrPath, "BtnVhrPath");
            this.BtnVhrPath.Name = "BtnVhrPath";
            this.BtnVhrPath.UseVisualStyleBackColor = true;
            this.BtnVhrPath.Click += new System.EventHandler(this.BtnVhrPath_Click);
            // 
            // TbVhrPath
            // 
            resources.ApplyResources(this.TbVhrPath, "TbVhrPath");
            this.TbVhrPath.Name = "TbVhrPath";
            this.TbVhrPath.ReadOnly = true;
            // 
            // GroupVhfPath
            // 
            this.GroupVhfPath.Controls.Add(this.BtnVhfPath);
            this.GroupVhfPath.Controls.Add(this.TbVhfPath);
            resources.ApplyResources(this.GroupVhfPath, "GroupVhfPath");
            this.GroupVhfPath.Name = "GroupVhfPath";
            this.GroupVhfPath.TabStop = false;
            // 
            // BtnVhfPath
            // 
            resources.ApplyResources(this.BtnVhfPath, "BtnVhfPath");
            this.BtnVhfPath.Name = "BtnVhfPath";
            this.BtnVhfPath.UseVisualStyleBackColor = true;
            this.BtnVhfPath.Click += new System.EventHandler(this.BtnVhfPath_Click);
            // 
            // TbVhfPath
            // 
            resources.ApplyResources(this.TbVhfPath, "TbVhfPath");
            this.TbVhfPath.Name = "TbVhfPath";
            this.TbVhfPath.ReadOnly = true;
            // 
            // GroupUpdate
            // 
            this.GroupUpdate.BackColor = System.Drawing.Color.Transparent;
            this.GroupUpdate.Controls.Add(this.CbDisableInternetServices);
            resources.ApplyResources(this.GroupUpdate, "GroupUpdate");
            this.GroupUpdate.Name = "GroupUpdate";
            this.GroupUpdate.TabStop = false;
            // 
            // CbDisableInternetServices
            // 
            resources.ApplyResources(this.CbDisableInternetServices, "CbDisableInternetServices");
            this.CbDisableInternetServices.Name = "CbDisableInternetServices";
            this.CbDisableInternetServices.UseVisualStyleBackColor = true;
            // 
            // GroupSave
            // 
            this.GroupSave.BackColor = System.Drawing.Color.Transparent;
            this.GroupSave.Controls.Add(this.CbAutoSave);
            resources.ApplyResources(this.GroupSave, "GroupSave");
            this.GroupSave.Name = "GroupSave";
            this.GroupSave.TabStop = false;
            // 
            // CbAutoSave
            // 
            resources.ApplyResources(this.CbAutoSave, "CbAutoSave");
            this.CbAutoSave.Name = "CbAutoSave";
            this.CbAutoSave.UseVisualStyleBackColor = true;
            // 
            // TabPractice
            // 
            this.TabPractice.BackColor = System.Drawing.Color.White;
            this.TabPractice.Controls.Add(this.GroupEvaluation);
            this.TabPractice.Controls.Add(this.GroupUserInterface);
            this.TabPractice.Controls.Add(this.GroupNearlyCorrect);
            resources.ApplyResources(this.TabPractice, "TabPractice");
            this.TabPractice.Name = "TabPractice";
            // 
            // GroupEvaluation
            // 
            this.GroupEvaluation.Controls.Add(this.CbManualCheck);
            this.GroupEvaluation.Controls.Add(this.CbEvaluationSystem);
            this.GroupEvaluation.Controls.Add(this.LbGradeSystem);
            this.GroupEvaluation.Controls.Add(this.CbPracticeResult);
            resources.ApplyResources(this.GroupEvaluation, "GroupEvaluation");
            this.GroupEvaluation.Name = "GroupEvaluation";
            this.GroupEvaluation.TabStop = false;
            // 
            // CbManualCheck
            // 
            resources.ApplyResources(this.CbManualCheck, "CbManualCheck");
            this.CbManualCheck.Name = "CbManualCheck";
            this.CbManualCheck.UseVisualStyleBackColor = true;
            // 
            // CbEvaluationSystem
            // 
            this.CbEvaluationSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbEvaluationSystem.Items.AddRange(new object[] {
            resources.GetString("CbEvaluationSystem.Items"),
            resources.GetString("CbEvaluationSystem.Items1")});
            resources.ApplyResources(this.CbEvaluationSystem, "CbEvaluationSystem");
            this.CbEvaluationSystem.Name = "CbEvaluationSystem";
            // 
            // LbGradeSystem
            // 
            resources.ApplyResources(this.LbGradeSystem, "LbGradeSystem");
            this.LbGradeSystem.Name = "LbGradeSystem";
            // 
            // CbPracticeResult
            // 
            resources.ApplyResources(this.CbPracticeResult, "CbPracticeResult");
            this.CbPracticeResult.Checked = true;
            this.CbPracticeResult.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbPracticeResult.Name = "CbPracticeResult";
            this.CbPracticeResult.UseVisualStyleBackColor = true;
            // 
            // GroupUserInterface
            // 
            this.GroupUserInterface.Controls.Add(this.CbColoredTextfield);
            this.GroupUserInterface.Controls.Add(this.CbAcousticFeedback);
            this.GroupUserInterface.Controls.Add(this.CbSingleContinueButton);
            resources.ApplyResources(this.GroupUserInterface, "GroupUserInterface");
            this.GroupUserInterface.Name = "GroupUserInterface";
            this.GroupUserInterface.TabStop = false;
            // 
            // CbColoredTextfield
            // 
            resources.ApplyResources(this.CbColoredTextfield, "CbColoredTextfield");
            this.CbColoredTextfield.BackColor = System.Drawing.Color.Transparent;
            this.CbColoredTextfield.Checked = true;
            this.CbColoredTextfield.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbColoredTextfield.Name = "CbColoredTextfield";
            this.CbColoredTextfield.UseVisualStyleBackColor = false;
            // 
            // CbAcousticFeedback
            // 
            resources.ApplyResources(this.CbAcousticFeedback, "CbAcousticFeedback");
            this.CbAcousticFeedback.Checked = true;
            this.CbAcousticFeedback.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbAcousticFeedback.Name = "CbAcousticFeedback";
            this.CbAcousticFeedback.UseVisualStyleBackColor = true;
            // 
            // CbSingleContinueButton
            // 
            resources.ApplyResources(this.CbSingleContinueButton, "CbSingleContinueButton");
            this.CbSingleContinueButton.BackColor = System.Drawing.Color.Transparent;
            this.CbSingleContinueButton.Name = "CbSingleContinueButton";
            this.CbSingleContinueButton.UseVisualStyleBackColor = false;
            // 
            // GroupNearlyCorrect
            // 
            this.GroupNearlyCorrect.Controls.Add(this.CbTolerateWhiteSpace);
            this.GroupNearlyCorrect.Controls.Add(this.CbTolerateNoSynonym);
            this.GroupNearlyCorrect.Controls.Add(this.CbTolerateArticle);
            this.GroupNearlyCorrect.Controls.Add(this.CbToleratePunctuationMark);
            this.GroupNearlyCorrect.Controls.Add(this.CbTolerateSpecialChar);
            resources.ApplyResources(this.GroupNearlyCorrect, "GroupNearlyCorrect");
            this.GroupNearlyCorrect.Name = "GroupNearlyCorrect";
            this.GroupNearlyCorrect.TabStop = false;
            // 
            // CbTolerateWhiteSpace
            // 
            resources.ApplyResources(this.CbTolerateWhiteSpace, "CbTolerateWhiteSpace");
            this.CbTolerateWhiteSpace.Checked = true;
            this.CbTolerateWhiteSpace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbTolerateWhiteSpace.Name = "CbTolerateWhiteSpace";
            this.CbTolerateWhiteSpace.UseVisualStyleBackColor = true;
            // 
            // CbTolerateNoSynonym
            // 
            resources.ApplyResources(this.CbTolerateNoSynonym, "CbTolerateNoSynonym");
            this.CbTolerateNoSynonym.Checked = true;
            this.CbTolerateNoSynonym.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbTolerateNoSynonym.Name = "CbTolerateNoSynonym";
            this.CbTolerateNoSynonym.UseVisualStyleBackColor = true;
            // 
            // CbTolerateArticle
            // 
            resources.ApplyResources(this.CbTolerateArticle, "CbTolerateArticle");
            this.CbTolerateArticle.Checked = true;
            this.CbTolerateArticle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbTolerateArticle.Name = "CbTolerateArticle";
            this.CbTolerateArticle.UseVisualStyleBackColor = true;
            // 
            // CbToleratePunctuationMark
            // 
            resources.ApplyResources(this.CbToleratePunctuationMark, "CbToleratePunctuationMark");
            this.CbToleratePunctuationMark.Checked = true;
            this.CbToleratePunctuationMark.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbToleratePunctuationMark.Name = "CbToleratePunctuationMark";
            this.CbToleratePunctuationMark.UseVisualStyleBackColor = true;
            // 
            // CbTolerateSpecialChar
            // 
            resources.ApplyResources(this.CbTolerateSpecialChar, "CbTolerateSpecialChar");
            this.CbTolerateSpecialChar.Checked = true;
            this.CbTolerateSpecialChar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbTolerateSpecialChar.Name = "CbTolerateSpecialChar";
            this.CbTolerateSpecialChar.UseVisualStyleBackColor = true;
            // 
            // TabPracticeSelect
            // 
            this.TabPracticeSelect.BackColor = System.Drawing.Color.White;
            this.TabPracticeSelect.Controls.Add(this.BtnResetPracticeSelect);
            this.TabPracticeSelect.Controls.Add(this.GroupSelectionMix);
            this.TabPracticeSelect.Controls.Add(this.GroupRepetitions);
            resources.ApplyResources(this.TabPracticeSelect, "TabPracticeSelect");
            this.TabPracticeSelect.Name = "TabPracticeSelect";
            // 
            // BtnResetPracticeSelect
            // 
            resources.ApplyResources(this.BtnResetPracticeSelect, "BtnResetPracticeSelect");
            this.BtnResetPracticeSelect.Name = "BtnResetPracticeSelect";
            this.BtnResetPracticeSelect.UseVisualStyleBackColor = true;
            this.BtnResetPracticeSelect.Click += new System.EventHandler(this.BtnResetPractice_Click);
            // 
            // GroupSelectionMix
            // 
            this.GroupSelectionMix.Controls.Add(this.LbWronglyPracticed);
            this.GroupSelectionMix.Controls.Add(this.LbCorrectlyPracticed);
            this.GroupSelectionMix.Controls.Add(this.LbUnpracticed);
            this.GroupSelectionMix.Controls.Add(this.LbPercentageUnpracticed);
            this.GroupSelectionMix.Controls.Add(this.pictureBox2);
            this.GroupSelectionMix.Controls.Add(this.pictureBox1);
            this.GroupSelectionMix.Controls.Add(this.pictureBox3);
            this.GroupSelectionMix.Controls.Add(this.LbPercentageWrongCorrect);
            this.GroupSelectionMix.Controls.Add(this.TrbUnknown);
            this.GroupSelectionMix.Controls.Add(this.TrbWrongRight);
            resources.ApplyResources(this.GroupSelectionMix, "GroupSelectionMix");
            this.GroupSelectionMix.Name = "GroupSelectionMix";
            this.GroupSelectionMix.TabStop = false;
            // 
            // LbWronglyPracticed
            // 
            resources.ApplyResources(this.LbWronglyPracticed, "LbWronglyPracticed");
            this.LbWronglyPracticed.Name = "LbWronglyPracticed";
            // 
            // LbCorrectlyPracticed
            // 
            resources.ApplyResources(this.LbCorrectlyPracticed, "LbCorrectlyPracticed");
            this.LbCorrectlyPracticed.Name = "LbCorrectlyPracticed";
            // 
            // LbUnpracticed
            // 
            resources.ApplyResources(this.LbUnpracticed, "LbUnpracticed");
            this.LbUnpracticed.Name = "LbUnpracticed";
            // 
            // LbPercentageUnpracticed
            // 
            resources.ApplyResources(this.LbPercentageUnpracticed, "LbPercentageUnpracticed");
            this.LbPercentageUnpracticed.Name = "LbPercentageUnpracticed";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Vocup.Properties.Icons.WronglyPracticed;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Vocup.Properties.Icons.Unpracticed;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Vocup.Properties.Icons.CorrectlyPracticed;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // LbPercentageWrongCorrect
            // 
            resources.ApplyResources(this.LbPercentageWrongCorrect, "LbPercentageWrongCorrect");
            this.LbPercentageWrongCorrect.Name = "LbPercentageWrongCorrect";
            // 
            // GroupRepetitions
            // 
            this.GroupRepetitions.Controls.Add(this.LbPracticeCount);
            this.GroupRepetitions.Controls.Add(this.PnlPracticeCount);
            resources.ApplyResources(this.GroupRepetitions, "GroupRepetitions");
            this.GroupRepetitions.Name = "GroupRepetitions";
            this.GroupRepetitions.TabStop = false;
            // 
            // LbPracticeCount
            // 
            this.LbPracticeCount.AutoEllipsis = true;
            resources.ApplyResources(this.LbPracticeCount, "LbPracticeCount");
            this.LbPracticeCount.Name = "LbPracticeCount";
            // 
            // PnlPracticeCount
            // 
            this.PnlPracticeCount.Controls.Add(this.LbTrb6);
            this.PnlPracticeCount.Controls.Add(this.LbTrb2);
            this.PnlPracticeCount.Controls.Add(this.LbTrb5);
            this.PnlPracticeCount.Controls.Add(this.LbTrb3);
            this.PnlPracticeCount.Controls.Add(this.LbTrb4);
            this.PnlPracticeCount.Controls.Add(this.TrbRepetitions);
            resources.ApplyResources(this.PnlPracticeCount, "PnlPracticeCount");
            this.PnlPracticeCount.Name = "PnlPracticeCount";
            // 
            // LbTrb6
            // 
            resources.ApplyResources(this.LbTrb6, "LbTrb6");
            this.LbTrb6.Name = "LbTrb6";
            // 
            // LbTrb2
            // 
            resources.ApplyResources(this.LbTrb2, "LbTrb2");
            this.LbTrb2.Name = "LbTrb2";
            // 
            // LbTrb5
            // 
            resources.ApplyResources(this.LbTrb5, "LbTrb5");
            this.LbTrb5.Name = "LbTrb5";
            // 
            // LbTrb3
            // 
            resources.ApplyResources(this.LbTrb3, "LbTrb3");
            this.LbTrb3.Name = "LbTrb3";
            // 
            // LbTrb4
            // 
            resources.ApplyResources(this.LbTrb4, "LbTrb4");
            this.LbTrb4.Name = "LbTrb4";
            // 
            // TrbRepetitions
            // 
            this.TrbRepetitions.BackColor = System.Drawing.Color.White;
            this.TrbRepetitions.LargeChange = 1;
            resources.ApplyResources(this.TrbRepetitions, "TrbRepetitions");
            this.TrbRepetitions.Maximum = 6;
            this.TrbRepetitions.Minimum = 2;
            this.TrbRepetitions.Name = "TrbRepetitions";
            this.TrbRepetitions.Value = 3;
            // 
            // GroupLanguage
            // 
            this.GroupLanguage.Controls.Add(this.LbLanguage);
            this.GroupLanguage.Controls.Add(this.CbLanguage);
            resources.ApplyResources(this.GroupLanguage, "GroupLanguage");
            this.GroupLanguage.Name = "GroupLanguage";
            this.GroupLanguage.TabStop = false;
            // 
            // CbLanguage
            // 
            this.CbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbLanguage.FormattingEnabled = true;
            this.CbLanguage.Items.AddRange(new object[] {
            resources.GetString("CbLanguage.Items"),
            resources.GetString("CbLanguage.Items1"),
            resources.GetString("CbLanguage.Items2")});
            resources.ApplyResources(this.CbLanguage, "CbLanguage");
            this.CbLanguage.Name = "CbLanguage";
            // 
            // LbLanguage
            // 
            resources.ApplyResources(this.LbLanguage, "LbLanguage");
            this.LbLanguage.Name = "LbLanguage";
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.BtnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.Controls.Add(this.TabControlMain);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.SettingsDialog_Load);
            this.GroupStartScreen.ResumeLayout(false);
            this.GroupStartScreen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrbWrongRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrbUnknown)).EndInit();
            this.TabControlMain.ResumeLayout(false);
            this.TabGeneral.ResumeLayout(false);
            this.GroupVocabularyList.ResumeLayout(false);
            this.GroupVocabularyList.PerformLayout();
            this.GroupVhrPath.ResumeLayout(false);
            this.GroupVhrPath.PerformLayout();
            this.GroupVhfPath.ResumeLayout(false);
            this.GroupVhfPath.PerformLayout();
            this.GroupUpdate.ResumeLayout(false);
            this.GroupUpdate.PerformLayout();
            this.GroupSave.ResumeLayout(false);
            this.GroupSave.PerformLayout();
            this.TabPractice.ResumeLayout(false);
            this.GroupEvaluation.ResumeLayout(false);
            this.GroupEvaluation.PerformLayout();
            this.GroupUserInterface.ResumeLayout(false);
            this.GroupUserInterface.PerformLayout();
            this.GroupNearlyCorrect.ResumeLayout(false);
            this.GroupNearlyCorrect.PerformLayout();
            this.TabPracticeSelect.ResumeLayout(false);
            this.GroupSelectionMix.ResumeLayout(false);
            this.GroupSelectionMix.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.GroupRepetitions.ResumeLayout(false);
            this.GroupRepetitions.PerformLayout();
            this.PnlPracticeCount.ResumeLayout(false);
            this.PnlPracticeCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrbRepetitions)).EndInit();
            this.GroupLanguage.ResumeLayout(false);
            this.GroupLanguage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.GroupBox GroupStartScreen;
        private System.Windows.Forms.RadioButton RbEmptyStart;
        private System.Windows.Forms.RadioButton RbRecentFile;
        private System.Windows.Forms.TrackBar TrbWrongRight;
        private System.Windows.Forms.TrackBar TrbUnknown;
        private System.Windows.Forms.TabControl TabControlMain;
        private System.Windows.Forms.TabPage TabGeneral;
        private System.Windows.Forms.TabPage TabPracticeSelect;
        private System.Windows.Forms.Label LbWronglyPracticed;
        private System.Windows.Forms.Label LbCorrectlyPracticed;
        private System.Windows.Forms.Label LbUnpracticed;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LbPercentageUnpracticed;
        private System.Windows.Forms.Label LbPracticeCount;
        private System.Windows.Forms.Panel PnlPracticeCount;
        private System.Windows.Forms.Label LbTrb6;
        private System.Windows.Forms.Label LbTrb2;
        private System.Windows.Forms.Label LbTrb5;
        private System.Windows.Forms.Label LbTrb3;
        private System.Windows.Forms.Label LbTrb4;
        private System.Windows.Forms.TrackBar TrbRepetitions;
        private System.Windows.Forms.Label LbPercentageWrongCorrect;
        private System.Windows.Forms.GroupBox GroupRepetitions;
        private System.Windows.Forms.GroupBox GroupSelectionMix;
        private System.Windows.Forms.Button BtnResetPracticeSelect;
        private System.Windows.Forms.CheckBox CbAutoSave;
        private System.Windows.Forms.GroupBox GroupSave;
        private System.Windows.Forms.CheckBox CbDisableInternetServices;
        private System.Windows.Forms.GroupBox GroupUpdate;
        private System.Windows.Forms.TabPage TabPractice;
        private System.Windows.Forms.CheckBox CbColoredTextfield;
        private System.Windows.Forms.GroupBox GroupNearlyCorrect;
        private System.Windows.Forms.CheckBox CbTolerateArticle;
        private System.Windows.Forms.CheckBox CbTolerateSpecialChar;
        private System.Windows.Forms.CheckBox CbToleratePunctuationMark;
        private System.Windows.Forms.CheckBox CbTolerateNoSynonym;
        private System.Windows.Forms.GroupBox GroupUserInterface;
        private System.Windows.Forms.CheckBox CbSingleContinueButton;
        private System.Windows.Forms.CheckBox CbAcousticFeedback;
        private System.Windows.Forms.CheckBox CbManualCheck;
        private System.Windows.Forms.GroupBox GroupVhfPath;
        private System.Windows.Forms.Button BtnVhfPath;
        private System.Windows.Forms.TextBox TbVhfPath;
        private System.Windows.Forms.GroupBox GroupVhrPath;
        private System.Windows.Forms.Button BtnVhrPath;
        private System.Windows.Forms.TextBox TbVhrPath;
        private System.Windows.Forms.GroupBox GroupEvaluation;
        private System.Windows.Forms.ComboBox CbEvaluationSystem;
        private System.Windows.Forms.Label LbGradeSystem;
        private System.Windows.Forms.CheckBox CbPracticeResult;
        private System.Windows.Forms.Button BtnResetStartScreen;
        private System.Windows.Forms.GroupBox GroupVocabularyList;
        private System.Windows.Forms.CheckBox CbGridLines;
        private System.Windows.Forms.CheckBox CbColumnResize;
        private System.Windows.Forms.CheckBox CbTolerateWhiteSpace;
        private System.Windows.Forms.GroupBox GroupLanguage;
        private System.Windows.Forms.Label LbLanguage;
        private System.Windows.Forms.ComboBox CbLanguage;
    }
}