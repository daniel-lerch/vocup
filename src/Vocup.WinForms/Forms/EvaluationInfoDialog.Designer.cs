namespace Vocup.Forms
{
    partial class EvaluationInfoDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EvaluationInfoDialog));
            this.BtnOK = new System.Windows.Forms.Button();
            this.pictureBox0 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LbUnpracticed = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LbWronglyPracticed = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.LbFullyPracticed = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LbCorrectlyPracticed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnOK
            // 
            resources.ApplyResources(this.BtnOK, "BtnOK");
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.UseVisualStyleBackColor = true;
            // 
            // pictureBox0
            // 
            this.pictureBox0.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox0, "pictureBox0");
            this.pictureBox0.Name = "pictureBox0";
            this.pictureBox0.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LbUnpracticed);
            this.groupBox1.Controls.Add(this.pictureBox0);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // LbUnpracticed
            // 
            resources.ApplyResources(this.LbUnpracticed, "LbUnpracticed");
            this.LbUnpracticed.Name = "LbUnpracticed";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LbWronglyPracticed);
            this.groupBox2.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // LbWronglyPracticed
            // 
            resources.ApplyResources(this.LbWronglyPracticed, "LbWronglyPracticed");
            this.LbWronglyPracticed.Name = "LbWronglyPracticed";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.LbFullyPracticed);
            this.groupBox4.Controls.Add(this.pictureBox3);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // LbFullyPracticed
            // 
            resources.ApplyResources(this.LbFullyPracticed, "LbFullyPracticed");
            this.LbFullyPracticed.Name = "LbFullyPracticed";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LbCorrectlyPracticed);
            this.groupBox3.Controls.Add(this.pictureBox2);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // LbCorrectlyPracticed
            // 
            resources.ApplyResources(this.LbCorrectlyPracticed, "LbCorrectlyPracticed");
            this.LbCorrectlyPracticed.Name = "LbCorrectlyPracticed";
            // 
            // EvaluationInfoDialog
            // 
            this.AcceptButton = this.BtnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnOK;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EvaluationInfoDialog";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.PictureBox pictureBox0;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label LbUnpracticed;
        private System.Windows.Forms.Label LbWronglyPracticed;
        private System.Windows.Forms.Label LbCorrectlyPracticed;
        private System.Windows.Forms.Label LbFullyPracticed;
    }
}