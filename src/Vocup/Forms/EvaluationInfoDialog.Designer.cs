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
            this.button = new System.Windows.Forms.Button();
            this.pictureBox0 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.text_noch_nicht = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.text_falsch_2 = new System.Windows.Forms.Label();
            this.text_falsch_1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.text_fertig = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.text_richtig = new System.Windows.Forms.Label();
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
            // button
            // 
            this.button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.button, "button");
            this.button.Name = "button";
            this.button.UseVisualStyleBackColor = true;
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
            this.groupBox1.Controls.Add(this.text_noch_nicht);
            this.groupBox1.Controls.Add(this.pictureBox0);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // text_noch_nicht
            // 
            resources.ApplyResources(this.text_noch_nicht, "text_noch_nicht");
            this.text_noch_nicht.Name = "text_noch_nicht";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.text_falsch_2);
            this.groupBox2.Controls.Add(this.text_falsch_1);
            this.groupBox2.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // text_falsch_2
            // 
            resources.ApplyResources(this.text_falsch_2, "text_falsch_2");
            this.text_falsch_2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.text_falsch_2.Name = "text_falsch_2";
            // 
            // text_falsch_1
            // 
            resources.ApplyResources(this.text_falsch_1, "text_falsch_1");
            this.text_falsch_1.Name = "text_falsch_1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.text_fertig);
            this.groupBox4.Controls.Add(this.pictureBox3);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // text_fertig
            // 
            resources.ApplyResources(this.text_fertig, "text_fertig");
            this.text_fertig.Name = "text_fertig";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.text_richtig);
            this.groupBox3.Controls.Add(this.pictureBox2);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // text_richtig
            // 
            resources.ApplyResources(this.text_richtig, "text_richtig");
            this.text_richtig.Name = "text_richtig";
            // 
            // infos_dialog
            // 
            this.AcceptButton = this.button;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.button;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "infos_dialog";
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

        private System.Windows.Forms.Button button;
        private System.Windows.Forms.PictureBox pictureBox0;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label text_noch_nicht;
        private System.Windows.Forms.Label text_falsch_1;
        private System.Windows.Forms.Label text_richtig;
        private System.Windows.Forms.Label text_fertig;
        private System.Windows.Forms.Label text_falsch_2;
    }
}