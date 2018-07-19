namespace Vocup
{
    partial class practise_dialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(practise_dialog));
            this.GroupPractice = new System.Windows.Forms.GroupBox();
            this.TbForeignLangSynonym = new System.Windows.Forms.TextBox();
            this.TbForeignLang = new System.Windows.Forms.TextBox();
            this.TbMotherTongue = new System.Windows.Forms.TextBox();
            this.LbForeignLangSynonym = new System.Windows.Forms.Label();
            this.LbForeignLang = new System.Windows.Forms.Label();
            this.LbMotherTongue = new System.Windows.Forms.Label();
            this.statistik_groupbox = new System.Windows.Forms.GroupBox();
            this.anzahl_falsch = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.anzahl_teilweise = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.anzahl_richtig = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.anzahl_geübt = new System.Windows.Forms.TextBox();
            this.anzahl_noch_nicht = new System.Windows.Forms.TextBox();
            this.anzahl_üben = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.abbrechen_button = new System.Windows.Forms.Button();
            this.fortfahren_button = new System.Windows.Forms.Button();
            this.correction_box = new System.Windows.Forms.TextBox();
            this.sonderzeichen_button = new System.Windows.Forms.Button();
            this.radio_teilweise_korrekt = new System.Windows.Forms.RadioButton();
            this.radio_falsch = new System.Windows.Forms.RadioButton();
            this.radio_korrekt = new System.Windows.Forms.RadioButton();
            this.selber_bewerten_groupbox = new System.Windows.Forms.GroupBox();
            this.GroupPractice.SuspendLayout();
            this.statistik_groupbox.SuspendLayout();
            this.selber_bewerten_groupbox.SuspendLayout();
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
            // statistik_groupbox
            // 
            resources.ApplyResources(this.statistik_groupbox, "statistik_groupbox");
            this.statistik_groupbox.Controls.Add(this.anzahl_falsch);
            this.statistik_groupbox.Controls.Add(this.label6);
            this.statistik_groupbox.Controls.Add(this.anzahl_teilweise);
            this.statistik_groupbox.Controls.Add(this.label5);
            this.statistik_groupbox.Controls.Add(this.label4);
            this.statistik_groupbox.Controls.Add(this.anzahl_richtig);
            this.statistik_groupbox.Controls.Add(this.label3);
            this.statistik_groupbox.Controls.Add(this.label2);
            this.statistik_groupbox.Controls.Add(this.label1);
            this.statistik_groupbox.Controls.Add(this.anzahl_geübt);
            this.statistik_groupbox.Controls.Add(this.anzahl_noch_nicht);
            this.statistik_groupbox.Controls.Add(this.anzahl_üben);
            this.statistik_groupbox.Controls.Add(this.progressBar);
            this.statistik_groupbox.Name = "statistik_groupbox";
            this.statistik_groupbox.TabStop = false;
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
            // anzahl_geübt
            // 
            resources.ApplyResources(this.anzahl_geübt, "anzahl_geübt");
            this.anzahl_geübt.Name = "anzahl_geübt";
            this.anzahl_geübt.ReadOnly = true;
            // 
            // anzahl_noch_nicht
            // 
            resources.ApplyResources(this.anzahl_noch_nicht, "anzahl_noch_nicht");
            this.anzahl_noch_nicht.Name = "anzahl_noch_nicht";
            this.anzahl_noch_nicht.ReadOnly = true;
            // 
            // anzahl_üben
            // 
            resources.ApplyResources(this.anzahl_üben, "anzahl_üben");
            this.anzahl_üben.Name = "anzahl_üben";
            this.anzahl_üben.ReadOnly = true;
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            this.progressBar.Step = 100;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // abbrechen_button
            // 
            resources.ApplyResources(this.abbrechen_button, "abbrechen_button");
            this.abbrechen_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.abbrechen_button.Name = "abbrechen_button";
            this.abbrechen_button.UseVisualStyleBackColor = true;
            // 
            // fortfahren_button
            // 
            resources.ApplyResources(this.fortfahren_button, "fortfahren_button");
            this.fortfahren_button.Name = "fortfahren_button";
            this.fortfahren_button.UseVisualStyleBackColor = true;
            this.fortfahren_button.Click += new System.EventHandler(this.fortfahren_button_Click);
            // 
            // correction_box
            // 
            this.correction_box.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.correction_box, "correction_box");
            this.correction_box.Name = "correction_box";
            this.correction_box.ReadOnly = true;
            // 
            // sonderzeichen_button
            // 
            resources.ApplyResources(this.sonderzeichen_button, "sonderzeichen_button");
            this.sonderzeichen_button.Name = "sonderzeichen_button";
            this.sonderzeichen_button.UseVisualStyleBackColor = true;
            this.sonderzeichen_button.Click += new System.EventHandler(this.sonderzeichen_button_Click);
            // 
            // radio_teilweise_korrekt
            // 
            resources.ApplyResources(this.radio_teilweise_korrekt, "radio_teilweise_korrekt");
            this.radio_teilweise_korrekt.BackColor = System.Drawing.Color.Transparent;
            this.radio_teilweise_korrekt.Name = "radio_teilweise_korrekt";
            this.radio_teilweise_korrekt.TabStop = true;
            this.radio_teilweise_korrekt.UseVisualStyleBackColor = false;
            // 
            // radio_falsch
            // 
            resources.ApplyResources(this.radio_falsch, "radio_falsch");
            this.radio_falsch.BackColor = System.Drawing.Color.Transparent;
            this.radio_falsch.Name = "radio_falsch";
            this.radio_falsch.TabStop = true;
            this.radio_falsch.UseVisualStyleBackColor = false;
            // 
            // radio_korrekt
            // 
            resources.ApplyResources(this.radio_korrekt, "radio_korrekt");
            this.radio_korrekt.BackColor = System.Drawing.Color.Transparent;
            this.radio_korrekt.Checked = true;
            this.radio_korrekt.Name = "radio_korrekt";
            this.radio_korrekt.TabStop = true;
            this.radio_korrekt.UseVisualStyleBackColor = false;
            // 
            // selber_bewerten_groupbox
            // 
            this.selber_bewerten_groupbox.Controls.Add(this.radio_korrekt);
            this.selber_bewerten_groupbox.Controls.Add(this.radio_falsch);
            this.selber_bewerten_groupbox.Controls.Add(this.radio_teilweise_korrekt);
            resources.ApplyResources(this.selber_bewerten_groupbox, "selber_bewerten_groupbox");
            this.selber_bewerten_groupbox.Name = "selber_bewerten_groupbox";
            this.selber_bewerten_groupbox.TabStop = false;
            // 
            // practise_dialog
            // 
            this.AcceptButton = this.fortfahren_button;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.abbrechen_button;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.abbrechen_button);
            this.Controls.Add(this.sonderzeichen_button);
            this.Controls.Add(this.fortfahren_button);
            this.Controls.Add(this.selber_bewerten_groupbox);
            this.Controls.Add(this.correction_box);
            this.Controls.Add(this.statistik_groupbox);
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
            this.statistik_groupbox.ResumeLayout(false);
            this.statistik_groupbox.PerformLayout();
            this.selber_bewerten_groupbox.ResumeLayout(false);
            this.selber_bewerten_groupbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public  System.Windows.Forms.TextBox TbForeignLangSynonym;
        public  System.Windows.Forms.TextBox TbForeignLang;
        public  System.Windows.Forms.TextBox TbMotherTongue;
        private System.Windows.Forms.Label LbForeignLangSynonym;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox anzahl_geübt;
        public  System.Windows.Forms.TextBox anzahl_noch_nicht;
        private System.Windows.Forms.TextBox anzahl_üben;
        private System.Windows.Forms.Label label3;
        public  System.Windows.Forms.TextBox anzahl_teilweise;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public  System.Windows.Forms.TextBox anzahl_richtig;
        public  System.Windows.Forms.TextBox anzahl_falsch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox correction_box;
        private System.Windows.Forms.RadioButton radio_teilweise_korrekt;
        private System.Windows.Forms.RadioButton radio_falsch;
        private System.Windows.Forms.RadioButton radio_korrekt;
        private System.Windows.Forms.GroupBox selber_bewerten_groupbox;
        private System.Windows.Forms.GroupBox GroupPractice;
        private System.Windows.Forms.Label LbForeignLang;
        private System.Windows.Forms.Label LbMotherTongue;
        private System.Windows.Forms.GroupBox statistik_groupbox;
        private System.Windows.Forms.Button abbrechen_button;
        private System.Windows.Forms.Button fortfahren_button;
        private System.Windows.Forms.Button sonderzeichen_button;
    }
}