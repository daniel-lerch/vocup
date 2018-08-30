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
            this.BtnCount20 = new System.Windows.Forms.Button();
            this.BtnCountAll = new System.Windows.Forms.Button();
            this.BtnCount40 = new System.Windows.Forms.Button();
            this.BtnCount30 = new System.Windows.Forms.Button();
            this.anzahl = new System.Windows.Forms.NumericUpDown();
            this.BtnCountCustom = new System.Windows.Forms.Button();
            this.RbUnpracticed = new System.Windows.Forms.RadioButton();
            this.RbWronglyPracticed = new System.Windows.Forms.RadioButton();
            this.RbCorrectlyPracticed = new System.Windows.Forms.RadioButton();
            this.RbAllStates = new System.Windows.Forms.RadioButton();
            this.RbEarlierPracticed = new System.Windows.Forms.RadioButton();
            this.RbLaterPracticed = new System.Windows.Forms.RadioButton();
            this.RbAllDates = new System.Windows.Forms.RadioButton();
            this.GroupState = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.GroupPriority = new System.Windows.Forms.GroupBox();
            this.GroupCount = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.anzahl)).BeginInit();
            this.GroupState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.GroupPriority.SuspendLayout();
            this.GroupCount.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnCount20
            // 
            resources.ApplyResources(this.BtnCount20, "BtnCount20");
            this.BtnCount20.Name = "BtnCount20";
            this.BtnCount20.UseVisualStyleBackColor = true;
            this.BtnCount20.Click += new System.EventHandler(this.BtnCount20_Click);
            // 
            // BtnCountAll
            // 
            resources.ApplyResources(this.BtnCountAll, "BtnCountAll");
            this.BtnCountAll.Name = "BtnCountAll";
            this.BtnCountAll.UseVisualStyleBackColor = true;
            this.BtnCountAll.Click += new System.EventHandler(this.BtnCountAll_Click);
            // 
            // BtnCount40
            // 
            resources.ApplyResources(this.BtnCount40, "BtnCount40");
            this.BtnCount40.Name = "BtnCount40";
            this.BtnCount40.UseVisualStyleBackColor = true;
            this.BtnCount40.Click += new System.EventHandler(this.BtnCount40_Click);
            // 
            // BtnCount30
            // 
            resources.ApplyResources(this.BtnCount30, "BtnCount30");
            this.BtnCount30.Name = "BtnCount30";
            this.BtnCount30.UseVisualStyleBackColor = true;
            this.BtnCount30.Click += new System.EventHandler(this.BtnCount30_Click);
            // 
            // anzahl
            // 
            resources.ApplyResources(this.anzahl, "anzahl");
            this.anzahl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.anzahl.Name = "anzahl";
            this.anzahl.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // BtnCountCustom
            // 
            resources.ApplyResources(this.BtnCountCustom, "BtnCountCustom");
            this.BtnCountCustom.Name = "BtnCountCustom";
            this.BtnCountCustom.UseVisualStyleBackColor = true;
            this.BtnCountCustom.Click += new System.EventHandler(this.BtnCountCustom_Click);
            // 
            // RbUnpracticed
            // 
            resources.ApplyResources(this.RbUnpracticed, "RbUnpracticed");
            this.RbUnpracticed.Name = "RbUnpracticed";
            this.RbUnpracticed.TabStop = true;
            this.RbUnpracticed.UseVisualStyleBackColor = true;
            this.RbUnpracticed.CheckedChanged += new System.EventHandler(this.RbUnpracticed_CheckedChanged);
            // 
            // RbWronglyPracticed
            // 
            resources.ApplyResources(this.RbWronglyPracticed, "RbWronglyPracticed");
            this.RbWronglyPracticed.Name = "RbWronglyPracticed";
            this.RbWronglyPracticed.TabStop = true;
            this.RbWronglyPracticed.UseVisualStyleBackColor = true;
            this.RbWronglyPracticed.CheckedChanged += new System.EventHandler(this.RbWronglyPracticed_CheckedChanged);
            // 
            // RbCorrectlyPracticed
            // 
            resources.ApplyResources(this.RbCorrectlyPracticed, "RbCorrectlyPracticed");
            this.RbCorrectlyPracticed.Name = "RbCorrectlyPracticed";
            this.RbCorrectlyPracticed.TabStop = true;
            this.RbCorrectlyPracticed.UseVisualStyleBackColor = true;
            this.RbCorrectlyPracticed.CheckedChanged += new System.EventHandler(this.RbCorrectlyPracticed_CheckedChanged);
            // 
            // RbAllStates
            // 
            resources.ApplyResources(this.RbAllStates, "RbAllStates");
            this.RbAllStates.Checked = true;
            this.RbAllStates.Name = "RbAllStates";
            this.RbAllStates.TabStop = true;
            this.RbAllStates.UseVisualStyleBackColor = true;
            this.RbAllStates.CheckedChanged += new System.EventHandler(this.RbAllStates_CheckedChanged);
            // 
            // RbEarlierPracticed
            // 
            resources.ApplyResources(this.RbEarlierPracticed, "RbEarlierPracticed");
            this.RbEarlierPracticed.Name = "RbEarlierPracticed";
            this.RbEarlierPracticed.TabStop = true;
            this.RbEarlierPracticed.UseVisualStyleBackColor = true;
            // 
            // RbLaterPracticed
            // 
            resources.ApplyResources(this.RbLaterPracticed, "RbLaterPracticed");
            this.RbLaterPracticed.Name = "RbLaterPracticed";
            this.RbLaterPracticed.TabStop = true;
            this.RbLaterPracticed.UseVisualStyleBackColor = true;
            // 
            // RbAllDates
            // 
            resources.ApplyResources(this.RbAllDates, "RbAllDates");
            this.RbAllDates.Checked = true;
            this.RbAllDates.Name = "RbAllDates";
            this.RbAllDates.TabStop = true;
            this.RbAllDates.UseVisualStyleBackColor = true;
            // 
            // GroupState
            // 
            this.GroupState.Controls.Add(this.pictureBox3);
            this.GroupState.Controls.Add(this.pictureBox2);
            this.GroupState.Controls.Add(this.pictureBox1);
            this.GroupState.Controls.Add(this.RbAllStates);
            this.GroupState.Controls.Add(this.RbUnpracticed);
            this.GroupState.Controls.Add(this.RbWronglyPracticed);
            this.GroupState.Controls.Add(this.RbCorrectlyPracticed);
            resources.ApplyResources(this.GroupState, "GroupState");
            this.GroupState.Name = "GroupState";
            this.GroupState.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Vocup.Properties.Icons.CorrectlyPracticed;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
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
            // GroupPriority
            // 
            this.GroupPriority.BackColor = System.Drawing.Color.Transparent;
            this.GroupPriority.Controls.Add(this.RbEarlierPracticed);
            this.GroupPriority.Controls.Add(this.RbLaterPracticed);
            this.GroupPriority.Controls.Add(this.RbAllDates);
            resources.ApplyResources(this.GroupPriority, "GroupPriority");
            this.GroupPriority.Name = "GroupPriority";
            this.GroupPriority.TabStop = false;
            // 
            // GroupCount
            // 
            this.GroupCount.Controls.Add(this.BtnCount20);
            this.GroupCount.Controls.Add(this.BtnCountAll);
            this.GroupCount.Controls.Add(this.anzahl);
            this.GroupCount.Controls.Add(this.BtnCount30);
            this.GroupCount.Controls.Add(this.BtnCountCustom);
            this.GroupCount.Controls.Add(this.BtnCount40);
            resources.ApplyResources(this.GroupCount, "GroupCount");
            this.GroupCount.Name = "GroupCount";
            this.GroupCount.TabStop = false;
            // 
            // PracticeCountDialog
            // 
            this.AcceptButton = this.BtnCount20;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupPriority);
            this.Controls.Add(this.GroupCount);
            this.Controls.Add(this.GroupState);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PracticeCountDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.anzahl)).EndInit();
            this.GroupState.ResumeLayout(false);
            this.GroupState.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.GroupPriority.ResumeLayout(false);
            this.GroupPriority.PerformLayout();
            this.GroupCount.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button BtnCount20;
        public System.Windows.Forms.Button BtnCountAll;
        public System.Windows.Forms.Button BtnCount40;
        public System.Windows.Forms.Button BtnCount30;
        public System.Windows.Forms.NumericUpDown anzahl;
        public System.Windows.Forms.Button BtnCountCustom;
        public System.Windows.Forms.RadioButton RbUnpracticed;
        public System.Windows.Forms.RadioButton RbWronglyPracticed;
        public System.Windows.Forms.RadioButton RbCorrectlyPracticed;
        public System.Windows.Forms.RadioButton RbAllStates;
        public System.Windows.Forms.RadioButton RbEarlierPracticed;
        public System.Windows.Forms.RadioButton RbLaterPracticed;
        public System.Windows.Forms.RadioButton RbAllDates;
        public System.Windows.Forms.GroupBox GroupState;
        public System.Windows.Forms.GroupBox GroupCount;
        public System.Windows.Forms.GroupBox GroupPriority;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}