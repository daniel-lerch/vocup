namespace Vocup
{
    partial class vokabel_count_practise_dialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(vokabel_count_practise_dialog));
            this.vokabeln_20 = new System.Windows.Forms.Button();
            this.vokabeln_alle = new System.Windows.Forms.Button();
            this.vokabeln_40 = new System.Windows.Forms.Button();
            this.vokabeln_30 = new System.Windows.Forms.Button();
            this.anzahl = new System.Windows.Forms.NumericUpDown();
            this.vokabeln_anzahl = new System.Windows.Forms.Button();
            this.art_noch_nicht = new System.Windows.Forms.RadioButton();
            this.art_falsch = new System.Windows.Forms.RadioButton();
            this.art_richtig = new System.Windows.Forms.RadioButton();
            this.art_alle = new System.Windows.Forms.RadioButton();
            this.zeitlich_laengst = new System.Windows.Forms.RadioButton();
            this.zeitlich_kuerzlich = new System.Windows.Forms.RadioButton();
            this.zeitlich_alle = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.anzahl)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // vokabeln_20
            // 
            resources.ApplyResources(this.vokabeln_20, "vokabeln_20");
            this.vokabeln_20.Name = "vokabeln_20";
            this.vokabeln_20.UseVisualStyleBackColor = true;
            this.vokabeln_20.Click += new System.EventHandler(this.vokabeln_20_Click);
            // 
            // vokabeln_alle
            // 
            resources.ApplyResources(this.vokabeln_alle, "vokabeln_alle");
            this.vokabeln_alle.Name = "vokabeln_alle";
            this.vokabeln_alle.UseVisualStyleBackColor = true;
            this.vokabeln_alle.Click += new System.EventHandler(this.vokabeln_alle_Click);
            // 
            // vokabeln_40
            // 
            resources.ApplyResources(this.vokabeln_40, "vokabeln_40");
            this.vokabeln_40.Name = "vokabeln_40";
            this.vokabeln_40.UseVisualStyleBackColor = true;
            this.vokabeln_40.Click += new System.EventHandler(this.vokabeln_40_Click);
            // 
            // vokabeln_30
            // 
            resources.ApplyResources(this.vokabeln_30, "vokabeln_30");
            this.vokabeln_30.Name = "vokabeln_30";
            this.vokabeln_30.UseVisualStyleBackColor = true;
            this.vokabeln_30.Click += new System.EventHandler(this.vokabeln_30_Click);
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
            // vokabeln_anzahl
            // 
            resources.ApplyResources(this.vokabeln_anzahl, "vokabeln_anzahl");
            this.vokabeln_anzahl.Name = "vokabeln_anzahl";
            this.vokabeln_anzahl.UseVisualStyleBackColor = true;
            this.vokabeln_anzahl.Click += new System.EventHandler(this.vokabeln_anzahl_Click);     
            // 
            // art_alle
            //
            this.DoubleBuffered = true;
            resources.ApplyResources(this.art_alle, "art_alle");
            this.art_alle.Checked = true;
            this.art_alle.Name = "art_alle";
            this.art_alle.TabStop = true;
            this.art_alle.UseVisualStyleBackColor = true;
            this.art_alle.CheckedChanged += new System.EventHandler(this.art_alle_CheckedChanged);
            //
            // art_noch_nicht
            //
            this.DoubleBuffered = true;
            resources.ApplyResources(this.art_noch_nicht, "art_noch_nicht");
            this.art_noch_nicht.Name = "art_noch_nicht";
            this.art_noch_nicht.TabStop = true;
            this.art_noch_nicht.UseVisualStyleBackColor = true;
            this.art_noch_nicht.CheckedChanged += new System.EventHandler(this.art_noch_nicht_CheckedChanged);
            // 
            // art_falsch
            //
            this.DoubleBuffered = true;
            resources.ApplyResources(this.art_falsch, "art_falsch");
            this.art_falsch.Name = "art_falsch";
            this.art_falsch.TabStop = true;
            this.art_falsch.UseVisualStyleBackColor = true;
            this.art_falsch.CheckedChanged += new System.EventHandler(this.art_falsch_CheckedChanged);
            // 
            // art_richtig
            //
            this.DoubleBuffered = true;
            resources.ApplyResources(this.art_richtig, "art_richtig");
            this.art_richtig.Name = "art_richtig";
            this.art_richtig.TabStop = true;
            this.art_richtig.UseVisualStyleBackColor = true;
            this.art_richtig.CheckedChanged += new System.EventHandler(this.art_richtig_CheckedChanged);
            // 
            // zeitlich_laengst
            //
            this.DoubleBuffered = true;
            resources.ApplyResources(this.zeitlich_laengst, "zeitlich_laengst");
            this.zeitlich_laengst.Name = "zeitlich_laengst";
            this.zeitlich_laengst.TabStop = true;
            this.zeitlich_laengst.UseVisualStyleBackColor = true;
            this.zeitlich_laengst.CheckedChanged += new System.EventHandler(this.zeitlich_laengst_CheckedChanged);
            // 
            // zeitlich_kuerzlich
            //
            this.DoubleBuffered = true;
            resources.ApplyResources(this.zeitlich_kuerzlich, "zeitlich_kuerzlich");
            this.zeitlich_kuerzlich.Name = "zeitlich_kuerzlich";
            this.zeitlich_kuerzlich.TabStop = true;
            this.zeitlich_kuerzlich.UseVisualStyleBackColor = true;
            this.zeitlich_kuerzlich.CheckedChanged += new System.EventHandler(this.zeitlich_kuerzlich_CheckedChanged);
            // 
            // zeitlich_alle
            //
            this.DoubleBuffered = true;
            resources.ApplyResources(this.zeitlich_alle, "zeitlich_alle");
            this.zeitlich_alle.Checked = true;
            this.zeitlich_alle.Name = "zeitlich_alle";
            this.zeitlich_alle.TabStop = true;
            this.zeitlich_alle.UseVisualStyleBackColor = true;
            this.zeitlich_alle.CheckedChanged += new System.EventHandler(this.zeitlich_alle_CheckedChanged);
            // 
            // groupBox1
            //
            this.DoubleBuffered = true;
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.art_alle);
            this.groupBox1.Controls.Add(this.art_noch_nicht);
            this.groupBox1.Controls.Add(this.art_falsch);
            this.groupBox1.Controls.Add(this.art_richtig);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Vocup.Properties.Icons.richtig_geübt;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Vocup.Properties.Icons.falsch_geübt;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Vocup.Properties.Icons.noch_nicht_geübt;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // groupBox2
            //
            this.DoubleBuffered = true;
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.zeitlich_laengst);
            this.groupBox2.Controls.Add(this.zeitlich_kuerzlich);
            this.groupBox2.Controls.Add(this.zeitlich_alle);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            //
            this.DoubleBuffered = true;
            this.groupBox3.Controls.Add(this.vokabeln_20);
            this.groupBox3.Controls.Add(this.vokabeln_alle);
            this.groupBox3.Controls.Add(this.anzahl);
            this.groupBox3.Controls.Add(this.vokabeln_30);
            this.groupBox3.Controls.Add(this.vokabeln_anzahl);
            this.groupBox3.Controls.Add(this.vokabeln_40);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // vokabel_count_practise_dialog
            //
            this.DoubleBuffered = true;
            this.AcceptButton = this.vokabeln_20;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "vokabel_count_practise_dialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.anzahl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public  System.Windows.Forms.Button vokabeln_20;
        public  System.Windows.Forms.Button vokabeln_alle;
        public  System.Windows.Forms.Button vokabeln_40;
        public  System.Windows.Forms.Button vokabeln_30;
        public  System.Windows.Forms.NumericUpDown anzahl;
        public  System.Windows.Forms.Button vokabeln_anzahl;
        public  System.Windows.Forms.RadioButton art_noch_nicht;
        public  System.Windows.Forms.RadioButton art_falsch;
        public  System.Windows.Forms.RadioButton art_richtig;
        public  System.Windows.Forms.RadioButton art_alle;
        public  System.Windows.Forms.RadioButton zeitlich_laengst;
        public  System.Windows.Forms.RadioButton zeitlich_kuerzlich;
        public  System.Windows.Forms.RadioButton zeitlich_alle;
        public  System.Windows.Forms.GroupBox groupBox1;
        public  System.Windows.Forms.GroupBox groupBox3;
        public  System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}