namespace Vocup.Forms
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
            BtnOk = new System.Windows.Forms.Button();
            BtnCancel = new System.Windows.Forms.Button();
            GroupStartScreen = new System.Windows.Forms.GroupBox();
            BtnResetStartScreen = new System.Windows.Forms.Button();
            RbEmptyStart = new System.Windows.Forms.RadioButton();
            RbRecentFile = new System.Windows.Forms.RadioButton();
            TrbWrongRight = new System.Windows.Forms.TrackBar();
            TrbUnknown = new System.Windows.Forms.TrackBar();
            TabControlMain = new System.Windows.Forms.TabControl();
            TabGeneral = new System.Windows.Forms.TabPage();
            GroupUserInterface = new System.Windows.Forms.GroupBox();
            CbColorTheme = new System.Windows.Forms.ComboBox();
            LbColorTheme = new System.Windows.Forms.Label();
            LbLanguage = new System.Windows.Forms.Label();
            CbLanguage = new System.Windows.Forms.ComboBox();
            GroupVocabularyList = new System.Windows.Forms.GroupBox();
            CbColumnResize = new System.Windows.Forms.CheckBox();
            GroupVhrPath = new System.Windows.Forms.GroupBox();
            BtnVhrPath = new System.Windows.Forms.Button();
            TbVhrPath = new System.Windows.Forms.TextBox();
            GroupVhfPath = new System.Windows.Forms.GroupBox();
            BtnVhfPath = new System.Windows.Forms.Button();
            TbVhfPath = new System.Windows.Forms.TextBox();
            GroupUpdate = new System.Windows.Forms.GroupBox();
            CbDisableInternetServices = new System.Windows.Forms.CheckBox();
            GroupSave = new System.Windows.Forms.GroupBox();
            CbAutoSave = new System.Windows.Forms.CheckBox();
            TabPractice = new System.Windows.Forms.TabPage();
            GroupEvaluation = new System.Windows.Forms.GroupBox();
            CbOptionalExpressions = new System.Windows.Forms.CheckBox();
            CbManualCheck = new System.Windows.Forms.CheckBox();
            CbShowPracticeResult = new System.Windows.Forms.CheckBox();
            GroupPracticeUserInterface = new System.Windows.Forms.GroupBox();
            CbAcousticFeedback = new System.Windows.Forms.CheckBox();
            CbSingleContinueButton = new System.Windows.Forms.CheckBox();
            GroupNearlyCorrect = new System.Windows.Forms.GroupBox();
            CbTolerateWhiteSpace = new System.Windows.Forms.CheckBox();
            CbTolerateNoSynonym = new System.Windows.Forms.CheckBox();
            CbTolerateArticle = new System.Windows.Forms.CheckBox();
            CbToleratePunctuationMark = new System.Windows.Forms.CheckBox();
            CbTolerateSpecialChar = new System.Windows.Forms.CheckBox();
            TabPracticeSelect = new System.Windows.Forms.TabPage();
            BtnResetPracticeSelect = new System.Windows.Forms.Button();
            GroupSelectionMix = new System.Windows.Forms.GroupBox();
            LbWronglyPracticed = new System.Windows.Forms.Label();
            LbCorrectlyPracticed = new System.Windows.Forms.Label();
            LbUnpracticed = new System.Windows.Forms.Label();
            LbPercentageUnpracticed = new System.Windows.Forms.Label();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            pictureBox3 = new System.Windows.Forms.PictureBox();
            LbPercentageWrongCorrect = new System.Windows.Forms.Label();
            GroupRepetitions = new System.Windows.Forms.GroupBox();
            LbPracticeCount = new System.Windows.Forms.Label();
            PnlPracticeCount = new System.Windows.Forms.Panel();
            LbTrb6 = new System.Windows.Forms.Label();
            LbTrb2 = new System.Windows.Forms.Label();
            LbTrb5 = new System.Windows.Forms.Label();
            LbTrb3 = new System.Windows.Forms.Label();
            LbTrb4 = new System.Windows.Forms.Label();
            TrbRepetitions = new System.Windows.Forms.TrackBar();
            GroupStartScreen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TrbWrongRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrbUnknown).BeginInit();
            TabControlMain.SuspendLayout();
            TabGeneral.SuspendLayout();
            GroupUserInterface.SuspendLayout();
            GroupVocabularyList.SuspendLayout();
            GroupVhrPath.SuspendLayout();
            GroupVhfPath.SuspendLayout();
            GroupUpdate.SuspendLayout();
            GroupSave.SuspendLayout();
            TabPractice.SuspendLayout();
            GroupEvaluation.SuspendLayout();
            GroupPracticeUserInterface.SuspendLayout();
            GroupNearlyCorrect.SuspendLayout();
            TabPracticeSelect.SuspendLayout();
            GroupSelectionMix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            GroupRepetitions.SuspendLayout();
            PnlPracticeCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TrbRepetitions).BeginInit();
            SuspendLayout();
            // 
            // BtnOk
            // 
            resources.ApplyResources(BtnOk, "BtnOk");
            BtnOk.Name = "BtnOk";
            BtnOk.UseVisualStyleBackColor = true;
            BtnOk.Click += BtnOk_Click;
            // 
            // BtnCancel
            // 
            resources.ApplyResources(BtnCancel, "BtnCancel");
            BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            BtnCancel.Name = "BtnCancel";
            BtnCancel.UseVisualStyleBackColor = true;
            // 
            // GroupStartScreen
            // 
            GroupStartScreen.BackColor = System.Drawing.Color.Transparent;
            GroupStartScreen.Controls.Add(BtnResetStartScreen);
            GroupStartScreen.Controls.Add(RbEmptyStart);
            GroupStartScreen.Controls.Add(RbRecentFile);
            resources.ApplyResources(GroupStartScreen, "GroupStartScreen");
            GroupStartScreen.Name = "GroupStartScreen";
            GroupStartScreen.TabStop = false;
            // 
            // BtnResetStartScreen
            // 
            resources.ApplyResources(BtnResetStartScreen, "BtnResetStartScreen");
            BtnResetStartScreen.Name = "BtnResetStartScreen";
            BtnResetStartScreen.UseVisualStyleBackColor = true;
            BtnResetStartScreen.Click += BtnResetStartScreen_Click;
            // 
            // RbEmptyStart
            // 
            resources.ApplyResources(RbEmptyStart, "RbEmptyStart");
            RbEmptyStart.Name = "RbEmptyStart";
            RbEmptyStart.TabStop = true;
            RbEmptyStart.UseVisualStyleBackColor = true;
            // 
            // RbRecentFile
            // 
            resources.ApplyResources(RbRecentFile, "RbRecentFile");
            RbRecentFile.Checked = true;
            RbRecentFile.Name = "RbRecentFile";
            RbRecentFile.TabStop = true;
            RbRecentFile.UseVisualStyleBackColor = true;
            // 
            // TrbWrongRight
            // 
            TrbWrongRight.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(TrbWrongRight, "TrbWrongRight");
            TrbWrongRight.Minimum = 1;
            TrbWrongRight.Name = "TrbWrongRight";
            TrbWrongRight.TickStyle = System.Windows.Forms.TickStyle.Both;
            TrbWrongRight.Value = 5;
            TrbWrongRight.ValueChanged += TrbWrongRight_ValueChanged;
            // 
            // TrbUnknown
            // 
            TrbUnknown.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(TrbUnknown, "TrbUnknown");
            TrbUnknown.Maximum = 8;
            TrbUnknown.Minimum = 1;
            TrbUnknown.Name = "TrbUnknown";
            TrbUnknown.TickStyle = System.Windows.Forms.TickStyle.Both;
            TrbUnknown.Value = 5;
            TrbUnknown.ValueChanged += TrbUnknown_ValueChanged;
            // 
            // TabControlMain
            // 
            TabControlMain.Controls.Add(TabGeneral);
            TabControlMain.Controls.Add(TabPractice);
            TabControlMain.Controls.Add(TabPracticeSelect);
            resources.ApplyResources(TabControlMain, "TabControlMain");
            TabControlMain.Name = "TabControlMain";
            TabControlMain.SelectedIndex = 0;
            TabControlMain.Selected += TabControlMain_Selected;
            // 
            // TabGeneral
            // 
            TabGeneral.BackColor = System.Drawing.Color.White;
            TabGeneral.Controls.Add(GroupUserInterface);
            TabGeneral.Controls.Add(GroupVocabularyList);
            TabGeneral.Controls.Add(GroupVhrPath);
            TabGeneral.Controls.Add(GroupVhfPath);
            TabGeneral.Controls.Add(GroupUpdate);
            TabGeneral.Controls.Add(GroupSave);
            TabGeneral.Controls.Add(GroupStartScreen);
            resources.ApplyResources(TabGeneral, "TabGeneral");
            TabGeneral.Name = "TabGeneral";
            // 
            // GroupUserInterface
            // 
            GroupUserInterface.Controls.Add(CbColorTheme);
            GroupUserInterface.Controls.Add(LbColorTheme);
            GroupUserInterface.Controls.Add(LbLanguage);
            GroupUserInterface.Controls.Add(CbLanguage);
            resources.ApplyResources(GroupUserInterface, "GroupUserInterface");
            GroupUserInterface.Name = "GroupUserInterface";
            GroupUserInterface.TabStop = false;
            // 
            // CbColorTheme
            // 
            CbColorTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            CbColorTheme.FormattingEnabled = true;
            CbColorTheme.Items.AddRange(new object[] { resources.GetString("CbColorTheme.Items"), resources.GetString("CbColorTheme.Items1"), resources.GetString("CbColorTheme.Items2") });
            resources.ApplyResources(CbColorTheme, "CbColorTheme");
            CbColorTheme.Name = "CbColorTheme";
            // 
            // LbColorTheme
            // 
            resources.ApplyResources(LbColorTheme, "LbColorTheme");
            LbColorTheme.Name = "LbColorTheme";
            // 
            // LbLanguage
            // 
            resources.ApplyResources(LbLanguage, "LbLanguage");
            LbLanguage.Name = "LbLanguage";
            // 
            // CbLanguage
            // 
            CbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            CbLanguage.FormattingEnabled = true;
            CbLanguage.Items.AddRange(new object[] { resources.GetString("CbLanguage.Items"), resources.GetString("CbLanguage.Items1"), resources.GetString("CbLanguage.Items2"), resources.GetString("CbLanguage.Items3") });
            resources.ApplyResources(CbLanguage, "CbLanguage");
            CbLanguage.Name = "CbLanguage";
            // 
            // GroupVocabularyList
            // 
            GroupVocabularyList.BackColor = System.Drawing.Color.Transparent;
            GroupVocabularyList.Controls.Add(CbColumnResize);
            resources.ApplyResources(GroupVocabularyList, "GroupVocabularyList");
            GroupVocabularyList.Name = "GroupVocabularyList";
            GroupVocabularyList.TabStop = false;
            // 
            // CbColumnResize
            // 
            resources.ApplyResources(CbColumnResize, "CbColumnResize");
            CbColumnResize.Name = "CbColumnResize";
            CbColumnResize.UseVisualStyleBackColor = true;
            // 
            // GroupVhrPath
            // 
            GroupVhrPath.Controls.Add(BtnVhrPath);
            GroupVhrPath.Controls.Add(TbVhrPath);
            resources.ApplyResources(GroupVhrPath, "GroupVhrPath");
            GroupVhrPath.Name = "GroupVhrPath";
            GroupVhrPath.TabStop = false;
            // 
            // BtnVhrPath
            // 
            resources.ApplyResources(BtnVhrPath, "BtnVhrPath");
            BtnVhrPath.Name = "BtnVhrPath";
            BtnVhrPath.UseVisualStyleBackColor = true;
            BtnVhrPath.Click += BtnVhrPath_Click;
            // 
            // TbVhrPath
            // 
            resources.ApplyResources(TbVhrPath, "TbVhrPath");
            TbVhrPath.Name = "TbVhrPath";
            TbVhrPath.ReadOnly = true;
            // 
            // GroupVhfPath
            // 
            GroupVhfPath.Controls.Add(BtnVhfPath);
            GroupVhfPath.Controls.Add(TbVhfPath);
            resources.ApplyResources(GroupVhfPath, "GroupVhfPath");
            GroupVhfPath.Name = "GroupVhfPath";
            GroupVhfPath.TabStop = false;
            // 
            // BtnVhfPath
            // 
            resources.ApplyResources(BtnVhfPath, "BtnVhfPath");
            BtnVhfPath.Name = "BtnVhfPath";
            BtnVhfPath.UseVisualStyleBackColor = true;
            BtnVhfPath.Click += BtnVhfPath_Click;
            // 
            // TbVhfPath
            // 
            resources.ApplyResources(TbVhfPath, "TbVhfPath");
            TbVhfPath.Name = "TbVhfPath";
            TbVhfPath.ReadOnly = true;
            // 
            // GroupUpdate
            // 
            GroupUpdate.BackColor = System.Drawing.Color.Transparent;
            GroupUpdate.Controls.Add(CbDisableInternetServices);
            resources.ApplyResources(GroupUpdate, "GroupUpdate");
            GroupUpdate.Name = "GroupUpdate";
            GroupUpdate.TabStop = false;
            // 
            // CbDisableInternetServices
            // 
            resources.ApplyResources(CbDisableInternetServices, "CbDisableInternetServices");
            CbDisableInternetServices.Name = "CbDisableInternetServices";
            CbDisableInternetServices.UseVisualStyleBackColor = true;
            // 
            // GroupSave
            // 
            GroupSave.BackColor = System.Drawing.Color.Transparent;
            GroupSave.Controls.Add(CbAutoSave);
            resources.ApplyResources(GroupSave, "GroupSave");
            GroupSave.Name = "GroupSave";
            GroupSave.TabStop = false;
            // 
            // CbAutoSave
            // 
            resources.ApplyResources(CbAutoSave, "CbAutoSave");
            CbAutoSave.Name = "CbAutoSave";
            CbAutoSave.UseVisualStyleBackColor = true;
            // 
            // TabPractice
            // 
            TabPractice.BackColor = System.Drawing.Color.White;
            TabPractice.Controls.Add(GroupEvaluation);
            TabPractice.Controls.Add(GroupPracticeUserInterface);
            TabPractice.Controls.Add(GroupNearlyCorrect);
            resources.ApplyResources(TabPractice, "TabPractice");
            TabPractice.Name = "TabPractice";
            // 
            // GroupEvaluation
            // 
            GroupEvaluation.Controls.Add(CbOptionalExpressions);
            GroupEvaluation.Controls.Add(CbManualCheck);
            GroupEvaluation.Controls.Add(CbShowPracticeResult);
            resources.ApplyResources(GroupEvaluation, "GroupEvaluation");
            GroupEvaluation.Name = "GroupEvaluation";
            GroupEvaluation.TabStop = false;
            // 
            // CbOptionalExpressions
            // 
            resources.ApplyResources(CbOptionalExpressions, "CbOptionalExpressions");
            CbOptionalExpressions.Checked = true;
            CbOptionalExpressions.CheckState = System.Windows.Forms.CheckState.Checked;
            CbOptionalExpressions.Name = "CbOptionalExpressions";
            CbOptionalExpressions.UseVisualStyleBackColor = true;
            // 
            // CbManualCheck
            // 
            resources.ApplyResources(CbManualCheck, "CbManualCheck");
            CbManualCheck.Name = "CbManualCheck";
            CbManualCheck.UseVisualStyleBackColor = true;
            // 
            // CbShowPracticeResult
            // 
            resources.ApplyResources(CbShowPracticeResult, "CbShowPracticeResult");
            CbShowPracticeResult.Checked = true;
            CbShowPracticeResult.CheckState = System.Windows.Forms.CheckState.Checked;
            CbShowPracticeResult.Name = "CbShowPracticeResult";
            CbShowPracticeResult.UseVisualStyleBackColor = true;
            // 
            // GroupPracticeUserInterface
            // 
            GroupPracticeUserInterface.Controls.Add(CbAcousticFeedback);
            GroupPracticeUserInterface.Controls.Add(CbSingleContinueButton);
            resources.ApplyResources(GroupPracticeUserInterface, "GroupPracticeUserInterface");
            GroupPracticeUserInterface.Name = "GroupPracticeUserInterface";
            GroupPracticeUserInterface.TabStop = false;
            // 
            // CbAcousticFeedback
            // 
            resources.ApplyResources(CbAcousticFeedback, "CbAcousticFeedback");
            CbAcousticFeedback.Checked = true;
            CbAcousticFeedback.CheckState = System.Windows.Forms.CheckState.Checked;
            CbAcousticFeedback.Name = "CbAcousticFeedback";
            CbAcousticFeedback.UseVisualStyleBackColor = true;
            // 
            // CbSingleContinueButton
            // 
            resources.ApplyResources(CbSingleContinueButton, "CbSingleContinueButton");
            CbSingleContinueButton.BackColor = System.Drawing.Color.Transparent;
            CbSingleContinueButton.Name = "CbSingleContinueButton";
            CbSingleContinueButton.UseVisualStyleBackColor = false;
            // 
            // GroupNearlyCorrect
            // 
            GroupNearlyCorrect.Controls.Add(CbTolerateWhiteSpace);
            GroupNearlyCorrect.Controls.Add(CbTolerateNoSynonym);
            GroupNearlyCorrect.Controls.Add(CbTolerateArticle);
            GroupNearlyCorrect.Controls.Add(CbToleratePunctuationMark);
            GroupNearlyCorrect.Controls.Add(CbTolerateSpecialChar);
            resources.ApplyResources(GroupNearlyCorrect, "GroupNearlyCorrect");
            GroupNearlyCorrect.Name = "GroupNearlyCorrect";
            GroupNearlyCorrect.TabStop = false;
            // 
            // CbTolerateWhiteSpace
            // 
            resources.ApplyResources(CbTolerateWhiteSpace, "CbTolerateWhiteSpace");
            CbTolerateWhiteSpace.Checked = true;
            CbTolerateWhiteSpace.CheckState = System.Windows.Forms.CheckState.Checked;
            CbTolerateWhiteSpace.Name = "CbTolerateWhiteSpace";
            CbTolerateWhiteSpace.UseVisualStyleBackColor = true;
            // 
            // CbTolerateNoSynonym
            // 
            resources.ApplyResources(CbTolerateNoSynonym, "CbTolerateNoSynonym");
            CbTolerateNoSynonym.Checked = true;
            CbTolerateNoSynonym.CheckState = System.Windows.Forms.CheckState.Checked;
            CbTolerateNoSynonym.Name = "CbTolerateNoSynonym";
            CbTolerateNoSynonym.UseVisualStyleBackColor = true;
            // 
            // CbTolerateArticle
            // 
            resources.ApplyResources(CbTolerateArticle, "CbTolerateArticle");
            CbTolerateArticle.Checked = true;
            CbTolerateArticle.CheckState = System.Windows.Forms.CheckState.Checked;
            CbTolerateArticle.Name = "CbTolerateArticle";
            CbTolerateArticle.UseVisualStyleBackColor = true;
            // 
            // CbToleratePunctuationMark
            // 
            resources.ApplyResources(CbToleratePunctuationMark, "CbToleratePunctuationMark");
            CbToleratePunctuationMark.Checked = true;
            CbToleratePunctuationMark.CheckState = System.Windows.Forms.CheckState.Checked;
            CbToleratePunctuationMark.Name = "CbToleratePunctuationMark";
            CbToleratePunctuationMark.UseVisualStyleBackColor = true;
            // 
            // CbTolerateSpecialChar
            // 
            resources.ApplyResources(CbTolerateSpecialChar, "CbTolerateSpecialChar");
            CbTolerateSpecialChar.Checked = true;
            CbTolerateSpecialChar.CheckState = System.Windows.Forms.CheckState.Checked;
            CbTolerateSpecialChar.Name = "CbTolerateSpecialChar";
            CbTolerateSpecialChar.UseVisualStyleBackColor = true;
            // 
            // TabPracticeSelect
            // 
            TabPracticeSelect.BackColor = System.Drawing.Color.White;
            TabPracticeSelect.Controls.Add(BtnResetPracticeSelect);
            TabPracticeSelect.Controls.Add(GroupSelectionMix);
            TabPracticeSelect.Controls.Add(GroupRepetitions);
            resources.ApplyResources(TabPracticeSelect, "TabPracticeSelect");
            TabPracticeSelect.Name = "TabPracticeSelect";
            // 
            // BtnResetPracticeSelect
            // 
            resources.ApplyResources(BtnResetPracticeSelect, "BtnResetPracticeSelect");
            BtnResetPracticeSelect.Name = "BtnResetPracticeSelect";
            BtnResetPracticeSelect.UseVisualStyleBackColor = true;
            BtnResetPracticeSelect.Click += BtnResetPractice_Click;
            // 
            // GroupSelectionMix
            // 
            GroupSelectionMix.Controls.Add(LbWronglyPracticed);
            GroupSelectionMix.Controls.Add(LbCorrectlyPracticed);
            GroupSelectionMix.Controls.Add(LbUnpracticed);
            GroupSelectionMix.Controls.Add(LbPercentageUnpracticed);
            GroupSelectionMix.Controls.Add(pictureBox2);
            GroupSelectionMix.Controls.Add(pictureBox1);
            GroupSelectionMix.Controls.Add(pictureBox3);
            GroupSelectionMix.Controls.Add(LbPercentageWrongCorrect);
            GroupSelectionMix.Controls.Add(TrbUnknown);
            GroupSelectionMix.Controls.Add(TrbWrongRight);
            resources.ApplyResources(GroupSelectionMix, "GroupSelectionMix");
            GroupSelectionMix.Name = "GroupSelectionMix";
            GroupSelectionMix.TabStop = false;
            // 
            // LbWronglyPracticed
            // 
            resources.ApplyResources(LbWronglyPracticed, "LbWronglyPracticed");
            LbWronglyPracticed.Name = "LbWronglyPracticed";
            // 
            // LbCorrectlyPracticed
            // 
            resources.ApplyResources(LbCorrectlyPracticed, "LbCorrectlyPracticed");
            LbCorrectlyPracticed.Name = "LbCorrectlyPracticed";
            // 
            // LbUnpracticed
            // 
            resources.ApplyResources(LbUnpracticed, "LbUnpracticed");
            LbUnpracticed.Name = "LbUnpracticed";
            // 
            // LbPercentageUnpracticed
            // 
            resources.ApplyResources(LbPercentageUnpracticed, "LbPercentageUnpracticed");
            LbPercentageUnpracticed.Name = "LbPercentageUnpracticed";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Icons.WronglyPracticed;
            resources.ApplyResources(pictureBox2, "pictureBox2");
            pictureBox2.Name = "pictureBox2";
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Icons.Unpracticed;
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Icons.CorrectlyPracticed;
            resources.ApplyResources(pictureBox3, "pictureBox3");
            pictureBox3.Name = "pictureBox3";
            pictureBox3.TabStop = false;
            // 
            // LbPercentageWrongCorrect
            // 
            resources.ApplyResources(LbPercentageWrongCorrect, "LbPercentageWrongCorrect");
            LbPercentageWrongCorrect.Name = "LbPercentageWrongCorrect";
            // 
            // GroupRepetitions
            // 
            GroupRepetitions.Controls.Add(LbPracticeCount);
            GroupRepetitions.Controls.Add(PnlPracticeCount);
            resources.ApplyResources(GroupRepetitions, "GroupRepetitions");
            GroupRepetitions.Name = "GroupRepetitions";
            GroupRepetitions.TabStop = false;
            // 
            // LbPracticeCount
            // 
            LbPracticeCount.AutoEllipsis = true;
            resources.ApplyResources(LbPracticeCount, "LbPracticeCount");
            LbPracticeCount.Name = "LbPracticeCount";
            // 
            // PnlPracticeCount
            // 
            PnlPracticeCount.Controls.Add(LbTrb6);
            PnlPracticeCount.Controls.Add(LbTrb2);
            PnlPracticeCount.Controls.Add(LbTrb5);
            PnlPracticeCount.Controls.Add(LbTrb3);
            PnlPracticeCount.Controls.Add(LbTrb4);
            PnlPracticeCount.Controls.Add(TrbRepetitions);
            resources.ApplyResources(PnlPracticeCount, "PnlPracticeCount");
            PnlPracticeCount.Name = "PnlPracticeCount";
            // 
            // LbTrb6
            // 
            resources.ApplyResources(LbTrb6, "LbTrb6");
            LbTrb6.Name = "LbTrb6";
            // 
            // LbTrb2
            // 
            resources.ApplyResources(LbTrb2, "LbTrb2");
            LbTrb2.Name = "LbTrb2";
            // 
            // LbTrb5
            // 
            resources.ApplyResources(LbTrb5, "LbTrb5");
            LbTrb5.Name = "LbTrb5";
            // 
            // LbTrb3
            // 
            resources.ApplyResources(LbTrb3, "LbTrb3");
            LbTrb3.Name = "LbTrb3";
            // 
            // LbTrb4
            // 
            resources.ApplyResources(LbTrb4, "LbTrb4");
            LbTrb4.Name = "LbTrb4";
            // 
            // TrbRepetitions
            // 
            TrbRepetitions.BackColor = System.Drawing.Color.White;
            TrbRepetitions.LargeChange = 1;
            resources.ApplyResources(TrbRepetitions, "TrbRepetitions");
            TrbRepetitions.Maximum = 6;
            TrbRepetitions.Minimum = 2;
            TrbRepetitions.Name = "TrbRepetitions";
            TrbRepetitions.Value = 3;
            // 
            // SettingsDialog
            // 
            AcceptButton = BtnOk;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = BtnCancel;
            Controls.Add(TabControlMain);
            Controls.Add(BtnCancel);
            Controls.Add(BtnOk);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsDialog";
            ShowInTaskbar = false;
            Load += SettingsDialog_Load;
            GroupStartScreen.ResumeLayout(false);
            GroupStartScreen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TrbWrongRight).EndInit();
            ((System.ComponentModel.ISupportInitialize)TrbUnknown).EndInit();
            TabControlMain.ResumeLayout(false);
            TabGeneral.ResumeLayout(false);
            GroupUserInterface.ResumeLayout(false);
            GroupUserInterface.PerformLayout();
            GroupVocabularyList.ResumeLayout(false);
            GroupVocabularyList.PerformLayout();
            GroupVhrPath.ResumeLayout(false);
            GroupVhrPath.PerformLayout();
            GroupVhfPath.ResumeLayout(false);
            GroupVhfPath.PerformLayout();
            GroupUpdate.ResumeLayout(false);
            GroupUpdate.PerformLayout();
            GroupSave.ResumeLayout(false);
            GroupSave.PerformLayout();
            TabPractice.ResumeLayout(false);
            GroupEvaluation.ResumeLayout(false);
            GroupEvaluation.PerformLayout();
            GroupPracticeUserInterface.ResumeLayout(false);
            GroupPracticeUserInterface.PerformLayout();
            GroupNearlyCorrect.ResumeLayout(false);
            GroupNearlyCorrect.PerformLayout();
            TabPracticeSelect.ResumeLayout(false);
            GroupSelectionMix.ResumeLayout(false);
            GroupSelectionMix.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            GroupRepetitions.ResumeLayout(false);
            GroupRepetitions.PerformLayout();
            PnlPracticeCount.ResumeLayout(false);
            PnlPracticeCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TrbRepetitions).EndInit();
            ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox GroupNearlyCorrect;
        private System.Windows.Forms.CheckBox CbTolerateArticle;
        private System.Windows.Forms.CheckBox CbTolerateSpecialChar;
        private System.Windows.Forms.CheckBox CbToleratePunctuationMark;
        private System.Windows.Forms.CheckBox CbTolerateNoSynonym;
        private System.Windows.Forms.GroupBox GroupPracticeUserInterface;
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
        private System.Windows.Forms.CheckBox CbShowPracticeResult;
        private System.Windows.Forms.Button BtnResetStartScreen;
        private System.Windows.Forms.GroupBox GroupVocabularyList;
        private System.Windows.Forms.CheckBox CbColumnResize;
        private System.Windows.Forms.CheckBox CbTolerateWhiteSpace;
        private System.Windows.Forms.GroupBox GroupUserInterface;
        private System.Windows.Forms.Label LbLanguage;
        private System.Windows.Forms.ComboBox CbLanguage;
        private System.Windows.Forms.CheckBox CbOptionalExpressions;
        private System.Windows.Forms.Label LbColorTheme;
        private System.Windows.Forms.ComboBox CbColorTheme;
    }
}