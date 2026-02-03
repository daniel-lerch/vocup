namespace Vocup.Forms
{
    partial class PracticeCountDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PracticeCountDialog));
            BtnCount20 = new System.Windows.Forms.Button();
            BtnCountAll = new System.Windows.Forms.Button();
            BtnCount40 = new System.Windows.Forms.Button();
            BtnCount30 = new System.Windows.Forms.Button();
            NudCount = new System.Windows.Forms.NumericUpDown();
            BtnCountCustom = new System.Windows.Forms.Button();
            RbUnpracticed = new System.Windows.Forms.RadioButton();
            RbWronglyPracticed = new System.Windows.Forms.RadioButton();
            RbCorrectlyPracticed = new System.Windows.Forms.RadioButton();
            RbAllStates = new System.Windows.Forms.RadioButton();
            RbEarlierPracticed = new System.Windows.Forms.RadioButton();
            RbLaterPracticed = new System.Windows.Forms.RadioButton();
            RbAllDates = new System.Windows.Forms.RadioButton();
            GroupState = new System.Windows.Forms.GroupBox();
            pictureBox4 = new System.Windows.Forms.PictureBox();
            RbFullyPracticed = new System.Windows.Forms.RadioButton();
            pictureBox3 = new System.Windows.Forms.PictureBox();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            GroupPriority = new System.Windows.Forms.GroupBox();
            RbLaterCreated = new System.Windows.Forms.RadioButton();
            RbEarlierCreated = new System.Windows.Forms.RadioButton();
            GroupCount = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)NudCount).BeginInit();
            GroupState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            GroupPriority.SuspendLayout();
            GroupCount.SuspendLayout();
            SuspendLayout();
            // 
            // BtnCount20
            // 
            resources.ApplyResources(BtnCount20, "BtnCount20");
            BtnCount20.Name = "BtnCount20";
            BtnCount20.UseVisualStyleBackColor = true;
            BtnCount20.Click += BtnCount20_Click;
            // 
            // BtnCountAll
            // 
            resources.ApplyResources(BtnCountAll, "BtnCountAll");
            BtnCountAll.Name = "BtnCountAll";
            BtnCountAll.UseVisualStyleBackColor = true;
            BtnCountAll.Click += BtnCountAll_Click;
            // 
            // BtnCount40
            // 
            resources.ApplyResources(BtnCount40, "BtnCount40");
            BtnCount40.Name = "BtnCount40";
            BtnCount40.UseVisualStyleBackColor = true;
            BtnCount40.Click += BtnCount40_Click;
            // 
            // BtnCount30
            // 
            resources.ApplyResources(BtnCount30, "BtnCount30");
            BtnCount30.Name = "BtnCount30";
            BtnCount30.UseVisualStyleBackColor = true;
            BtnCount30.Click += BtnCount30_Click;
            // 
            // NudCount
            // 
            resources.ApplyResources(NudCount, "NudCount");
            NudCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NudCount.Name = "NudCount";
            NudCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // BtnCountCustom
            // 
            resources.ApplyResources(BtnCountCustom, "BtnCountCustom");
            BtnCountCustom.Name = "BtnCountCustom";
            BtnCountCustom.UseVisualStyleBackColor = true;
            BtnCountCustom.Click += BtnCountCustom_Click;
            // 
            // RbUnpracticed
            // 
            resources.ApplyResources(RbUnpracticed, "RbUnpracticed");
            RbUnpracticed.Name = "RbUnpracticed";
            RbUnpracticed.TabStop = true;
            RbUnpracticed.UseVisualStyleBackColor = true;
            RbUnpracticed.CheckedChanged += RbUnpracticed_CheckedChanged;
            // 
            // RbWronglyPracticed
            // 
            resources.ApplyResources(RbWronglyPracticed, "RbWronglyPracticed");
            RbWronglyPracticed.Name = "RbWronglyPracticed";
            RbWronglyPracticed.TabStop = true;
            RbWronglyPracticed.UseVisualStyleBackColor = true;
            RbWronglyPracticed.CheckedChanged += RbWronglyPracticed_CheckedChanged;
            // 
            // RbCorrectlyPracticed
            // 
            resources.ApplyResources(RbCorrectlyPracticed, "RbCorrectlyPracticed");
            RbCorrectlyPracticed.Name = "RbCorrectlyPracticed";
            RbCorrectlyPracticed.TabStop = true;
            RbCorrectlyPracticed.UseVisualStyleBackColor = true;
            RbCorrectlyPracticed.CheckedChanged += RbCorrectlyPracticed_CheckedChanged;
            // 
            // RbAllStates
            // 
            resources.ApplyResources(RbAllStates, "RbAllStates");
            RbAllStates.Checked = true;
            RbAllStates.Name = "RbAllStates";
            RbAllStates.TabStop = true;
            RbAllStates.UseVisualStyleBackColor = true;
            RbAllStates.CheckedChanged += RbAllStates_CheckedChanged;
            // 
            // RbEarlierPracticed
            // 
            resources.ApplyResources(RbEarlierPracticed, "RbEarlierPracticed");
            RbEarlierPracticed.Name = "RbEarlierPracticed";
            RbEarlierPracticed.TabStop = true;
            RbEarlierPracticed.UseVisualStyleBackColor = true;
            // 
            // RbLaterPracticed
            // 
            resources.ApplyResources(RbLaterPracticed, "RbLaterPracticed");
            RbLaterPracticed.Name = "RbLaterPracticed";
            RbLaterPracticed.TabStop = true;
            RbLaterPracticed.UseVisualStyleBackColor = true;
            // 
            // RbAllDates
            // 
            resources.ApplyResources(RbAllDates, "RbAllDates");
            RbAllDates.Checked = true;
            RbAllDates.Name = "RbAllDates";
            RbAllDates.TabStop = true;
            RbAllDates.UseVisualStyleBackColor = true;
            // 
            // GroupState
            // 
            GroupState.Controls.Add(pictureBox4);
            GroupState.Controls.Add(RbFullyPracticed);
            GroupState.Controls.Add(pictureBox3);
            GroupState.Controls.Add(pictureBox2);
            GroupState.Controls.Add(pictureBox1);
            GroupState.Controls.Add(RbAllStates);
            GroupState.Controls.Add(RbUnpracticed);
            GroupState.Controls.Add(RbWronglyPracticed);
            GroupState.Controls.Add(RbCorrectlyPracticed);
            resources.ApplyResources(GroupState, "GroupState");
            GroupState.Name = "GroupState";
            GroupState.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Icons.FullyPracticed;
            resources.ApplyResources(pictureBox4, "pictureBox4");
            pictureBox4.Name = "pictureBox4";
            pictureBox4.TabStop = false;
            // 
            // RbFullyPracticed
            // 
            resources.ApplyResources(RbFullyPracticed, "RbFullyPracticed");
            RbFullyPracticed.Name = "RbFullyPracticed";
            RbFullyPracticed.TabStop = true;
            RbFullyPracticed.UseVisualStyleBackColor = true;
            RbFullyPracticed.CheckedChanged += RbFullyPracticed_CheckedChanged;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Icons.CorrectlyPracticed;
            resources.ApplyResources(pictureBox3, "pictureBox3");
            pictureBox3.Name = "pictureBox3";
            pictureBox3.TabStop = false;
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
            // GroupPriority
            // 
            GroupPriority.BackColor = System.Drawing.Color.Transparent;
            GroupPriority.Controls.Add(RbLaterCreated);
            GroupPriority.Controls.Add(RbEarlierCreated);
            GroupPriority.Controls.Add(RbEarlierPracticed);
            GroupPriority.Controls.Add(RbLaterPracticed);
            GroupPriority.Controls.Add(RbAllDates);
            resources.ApplyResources(GroupPriority, "GroupPriority");
            GroupPriority.Name = "GroupPriority";
            GroupPriority.TabStop = false;
            // 
            // RbLaterCreated
            // 
            resources.ApplyResources(RbLaterCreated, "RbLaterCreated");
            RbLaterCreated.Name = "RbLaterCreated";
            RbLaterCreated.TabStop = true;
            RbLaterCreated.UseVisualStyleBackColor = true;
            // 
            // RbEarlierCreated
            // 
            resources.ApplyResources(RbEarlierCreated, "RbEarlierCreated");
            RbEarlierCreated.Name = "RbEarlierCreated";
            RbEarlierCreated.TabStop = true;
            RbEarlierCreated.UseVisualStyleBackColor = true;
            // 
            // GroupCount
            // 
            GroupCount.Controls.Add(BtnCount20);
            GroupCount.Controls.Add(BtnCountAll);
            GroupCount.Controls.Add(NudCount);
            GroupCount.Controls.Add(BtnCount30);
            GroupCount.Controls.Add(BtnCountCustom);
            GroupCount.Controls.Add(BtnCount40);
            resources.ApplyResources(GroupCount, "GroupCount");
            GroupCount.Name = "GroupCount";
            GroupCount.TabStop = false;
            // 
            // PracticeCountDialog
            // 
            AcceptButton = BtnCount20;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(GroupPriority);
            Controls.Add(GroupCount);
            Controls.Add(GroupState);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PracticeCountDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            Load += Form_Load;
            ((System.ComponentModel.ISupportInitialize)NudCount).EndInit();
            GroupState.ResumeLayout(false);
            GroupState.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            GroupPriority.ResumeLayout(false);
            GroupPriority.PerformLayout();
            GroupCount.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button BtnCount20;
        public System.Windows.Forms.Button BtnCountAll;
        public System.Windows.Forms.Button BtnCount40;
        public System.Windows.Forms.Button BtnCount30;
        public System.Windows.Forms.NumericUpDown NudCount;
        public System.Windows.Forms.Button BtnCountCustom;
        public System.Windows.Forms.RadioButton RbEarlierPracticed;
        public System.Windows.Forms.RadioButton RbLaterPracticed;
        public System.Windows.Forms.RadioButton RbAllDates;
        public System.Windows.Forms.GroupBox GroupState;
        public System.Windows.Forms.GroupBox GroupCount;
        public System.Windows.Forms.GroupBox GroupPriority;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RadioButton RbLaterCreated;
        private System.Windows.Forms.RadioButton RbEarlierCreated;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.RadioButton RbFullyPracticed;
        private System.Windows.Forms.RadioButton RbUnpracticed;
        private System.Windows.Forms.RadioButton RbWronglyPracticed;
        private System.Windows.Forms.RadioButton RbCorrectlyPracticed;
        private System.Windows.Forms.RadioButton RbAllStates;
    }
}