namespace Vocup.Forms
{
    partial class PracticeResultList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PracticeResultList));
            this.ListView = new System.Windows.Forms.ListView();
            this.imageColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.motherTongueColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.foreignLangColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.wrongInputColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listview_imagelist = new System.Windows.Forms.ImageList(this.components);
            this.BtnContinue = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CbDoNotShowAgain = new System.Windows.Forms.CheckBox();
            this.GroupStatistics = new System.Windows.Forms.GroupBox();
            this.TbCorrectCount = new System.Windows.Forms.TextBox();
            this.TbPartlyCorrectCount = new System.Windows.Forms.TextBox();
            this.TbNotPracticedCount = new System.Windows.Forms.TextBox();
            this.TbWrongCount = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.GroupGrades = new System.Windows.Forms.GroupBox();
            this.note_label = new System.Windows.Forms.Label();
            this.prozent_label = new System.Windows.Forms.Label();
            this.TbPercentage = new System.Windows.Forms.TextBox();
            this.TbGrade = new System.Windows.Forms.TextBox();
            this.GroupStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.GroupGrades.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListView
            // 
            this.ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.imageColumn,
            this.motherTongueColumn,
            this.foreignLangColumn,
            this.wrongInputColumn});
            resources.ApplyResources(this.ListView, "ListView");
            this.ListView.FullRowSelect = true;
            this.ListView.GridLines = true;
            this.ListView.HideSelection = false;
            this.ListView.MultiSelect = false;
            this.ListView.Name = "ListView";
            this.ListView.SmallImageList = this.listview_imagelist;
            this.ListView.UseCompatibleStateImageBehavior = false;
            this.ListView.View = System.Windows.Forms.View.Details;
            this.ListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_ColumnWidthChanging);
            // 
            // imageColumn
            // 
            resources.ApplyResources(this.imageColumn, "imageColumn");
            // 
            // motherTongueColumn
            // 
            resources.ApplyResources(this.motherTongueColumn, "motherTongueColumn");
            // 
            // foreignLangColumn
            // 
            resources.ApplyResources(this.foreignLangColumn, "foreignLangColumn");
            // 
            // wrongInputColumn
            // 
            resources.ApplyResources(this.wrongInputColumn, "wrongInputColumn");
            // 
            // listview_imagelist
            // 
            this.listview_imagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("listview_imagelist.ImageStream")));
            this.listview_imagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.listview_imagelist.Images.SetKeyName(0, "0.png");
            this.listview_imagelist.Images.SetKeyName(1, "1.png");
            this.listview_imagelist.Images.SetKeyName(2, "2.png");
            this.listview_imagelist.Images.SetKeyName(3, "3.png");
            // 
            // BtnContinue
            // 
            resources.ApplyResources(this.BtnContinue, "BtnContinue");
            this.BtnContinue.Name = "BtnContinue";
            this.BtnContinue.UseVisualStyleBackColor = true;
            this.BtnContinue.Click += new System.EventHandler(this.BtnContinue_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
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
            // CbDoNotShowAgain
            // 
            resources.ApplyResources(this.CbDoNotShowAgain, "CbDoNotShowAgain");
            this.CbDoNotShowAgain.Name = "CbDoNotShowAgain";
            this.CbDoNotShowAgain.UseVisualStyleBackColor = true;
            // 
            // GroupStatistics
            // 
            this.GroupStatistics.Controls.Add(this.TbCorrectCount);
            this.GroupStatistics.Controls.Add(this.TbPartlyCorrectCount);
            this.GroupStatistics.Controls.Add(this.TbNotPracticedCount);
            this.GroupStatistics.Controls.Add(this.TbWrongCount);
            this.GroupStatistics.Controls.Add(this.pictureBox1);
            this.GroupStatistics.Controls.Add(this.label1);
            this.GroupStatistics.Controls.Add(this.pictureBox4);
            this.GroupStatistics.Controls.Add(this.label4);
            this.GroupStatistics.Controls.Add(this.pictureBox2);
            this.GroupStatistics.Controls.Add(this.label2);
            this.GroupStatistics.Controls.Add(this.label3);
            this.GroupStatistics.Controls.Add(this.pictureBox3);
            resources.ApplyResources(this.GroupStatistics, "GroupStatistics");
            this.GroupStatistics.Name = "GroupStatistics";
            this.GroupStatistics.TabStop = false;
            // 
            // TbCorrectCount
            // 
            resources.ApplyResources(this.TbCorrectCount, "TbCorrectCount");
            this.TbCorrectCount.Name = "TbCorrectCount";
            this.TbCorrectCount.ReadOnly = true;
            // 
            // TbPartlyCorrectCount
            // 
            resources.ApplyResources(this.TbPartlyCorrectCount, "TbPartlyCorrectCount");
            this.TbPartlyCorrectCount.Name = "TbPartlyCorrectCount";
            this.TbPartlyCorrectCount.ReadOnly = true;
            // 
            // TbNotPracticedCount
            // 
            resources.ApplyResources(this.TbNotPracticedCount, "TbNotPracticedCount");
            this.TbNotPracticedCount.Name = "TbNotPracticedCount";
            this.TbNotPracticedCount.ReadOnly = true;
            // 
            // TbWrongCount
            // 
            resources.ApplyResources(this.TbWrongCount, "TbWrongCount");
            this.TbWrongCount.Name = "TbWrongCount";
            this.TbWrongCount.ReadOnly = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Vocup.Properties.Icons.falsch_geübt;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Vocup.Properties.Icons.übung_abgeschlossen;
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Vocup.Properties.Icons.noch_nicht_geübt;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Vocup.Properties.Icons.richtig_geübt;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // GroupGrades
            // 
            this.GroupGrades.Controls.Add(this.note_label);
            this.GroupGrades.Controls.Add(this.prozent_label);
            this.GroupGrades.Controls.Add(this.TbPercentage);
            this.GroupGrades.Controls.Add(this.TbGrade);
            resources.ApplyResources(this.GroupGrades, "GroupGrades");
            this.GroupGrades.Name = "GroupGrades";
            this.GroupGrades.TabStop = false;
            // 
            // note_label
            // 
            resources.ApplyResources(this.note_label, "note_label");
            this.note_label.Name = "note_label";
            // 
            // prozent_label
            // 
            resources.ApplyResources(this.prozent_label, "prozent_label");
            this.prozent_label.Name = "prozent_label";
            // 
            // TbPercentage
            // 
            resources.ApplyResources(this.TbPercentage, "TbPercentage");
            this.TbPercentage.Name = "TbPercentage";
            this.TbPercentage.ReadOnly = true;
            // 
            // TbGrade
            // 
            resources.ApplyResources(this.TbGrade, "TbGrade");
            this.TbGrade.Name = "TbGrade";
            this.TbGrade.ReadOnly = true;
            // 
            // PracticeResultList
            // 
            this.AcceptButton = this.BtnContinue;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupGrades);
            this.Controls.Add(this.GroupStatistics);
            this.Controls.Add(this.CbDoNotShowAgain);
            this.Controls.Add(this.BtnContinue);
            this.Controls.Add(this.ListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PracticeResultList";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Form_Load);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.GroupStatistics.ResumeLayout(false);
            this.GroupStatistics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.GroupGrades.ResumeLayout(false);
            this.GroupGrades.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListView ListView;
        private System.Windows.Forms.Button BtnContinue;
        private System.Windows.Forms.ImageList listview_imagelist;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox GroupGrades;
        public System.Windows.Forms.Label prozent_label;
        public System.Windows.Forms.Label note_label;
        private System.Windows.Forms.ColumnHeader imageColumn;
        private System.Windows.Forms.ColumnHeader motherTongueColumn;
        private System.Windows.Forms.ColumnHeader foreignLangColumn;
        private System.Windows.Forms.ColumnHeader wrongInputColumn;
        private System.Windows.Forms.CheckBox CbDoNotShowAgain;
        private System.Windows.Forms.GroupBox GroupStatistics;
        private System.Windows.Forms.TextBox TbCorrectCount;
        private System.Windows.Forms.TextBox TbPartlyCorrectCount;
        private System.Windows.Forms.TextBox TbNotPracticedCount;
        private System.Windows.Forms.TextBox TbWrongCount;
        private System.Windows.Forms.TextBox TbGrade;
        private System.Windows.Forms.TextBox TbPercentage;
    }
}