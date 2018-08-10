namespace Vocup
{
    partial class choose_vocables_for_print
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(choose_vocables_for_print));
            this.label1 = new System.Windows.Forms.Label();
            this.listbox = new System.Windows.Forms.CheckedListBox();
            this.check_all = new System.Windows.Forms.Button();
            this.discheck_all = new System.Windows.Forms.Button();
            this.print_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton_kaertchen = new System.Windows.Forms.RadioButton();
            this.radioButton_liste = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.foreign_own = new System.Windows.Forms.RadioButton();
            this.own_foreign = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_fertig = new System.Windows.Forms.CheckBox();
            this.checkBox_mindestens_einmal = new System.Windows.Forms.CheckBox();
            this.checkBox_falsch = new System.Windows.Forms.CheckBox();
            this.checkBox_noch_nie = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // listbox
            // 
            this.listbox.CheckOnClick = true;
            this.listbox.FormattingEnabled = true;
            resources.ApplyResources(this.listbox, "listbox");
            this.listbox.Name = "listbox";
            this.listbox.SelectedValueChanged += new System.EventHandler(this.listbox_SelectedValueChanged);
            // 
            // check_all
            // 
            resources.ApplyResources(this.check_all, "check_all");
            this.check_all.Name = "check_all";
            this.check_all.UseVisualStyleBackColor = true;
            this.check_all.Click += new System.EventHandler(this.check_all_Click);
            // 
            // discheck_all
            // 
            resources.ApplyResources(this.discheck_all, "discheck_all");
            this.discheck_all.Name = "discheck_all";
            this.discheck_all.UseVisualStyleBackColor = true;
            this.discheck_all.Click += new System.EventHandler(this.discheck_all_Click);
            // 
            // print_button
            // 
            this.print_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.print_button, "print_button");
            this.print_button.Name = "print_button";
            this.print_button.UseVisualStyleBackColor = true;
            this.print_button.Click += new System.EventHandler(this.print_button_Click);
            this.print_button.MouseEnter += new System.EventHandler(this.print_button_MouseEnter);
            // 
            // cancel_button
            // 
            resources.ApplyResources(this.cancel_button, "cancel_button");
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton_kaertchen);
            this.groupBox2.Controls.Add(this.radioButton_liste);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // radioButton_kaertchen
            // 
            resources.ApplyResources(this.radioButton_kaertchen, "radioButton_kaertchen");
            this.radioButton_kaertchen.Checked = true;
            this.radioButton_kaertchen.Name = "radioButton_kaertchen";
            this.radioButton_kaertchen.TabStop = true;
            this.radioButton_kaertchen.UseVisualStyleBackColor = true;
            // 
            // radioButton_liste
            // 
            resources.ApplyResources(this.radioButton_liste, "radioButton_liste");
            this.radioButton_liste.Name = "radioButton_liste";
            this.radioButton_liste.UseVisualStyleBackColor = true;
            this.radioButton_liste.CheckedChanged += new System.EventHandler(this.radioButton_liste_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.foreign_own);
            this.groupBox3.Controls.Add(this.own_foreign);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // foreign_own
            // 
            resources.ApplyResources(this.foreign_own, "foreign_own");
            this.foreign_own.Name = "foreign_own";
            this.foreign_own.UseVisualStyleBackColor = true;
            // 
            // own_foreign
            // 
            resources.ApplyResources(this.own_foreign, "own_foreign");
            this.own_foreign.Checked = true;
            this.own_foreign.Name = "own_foreign";
            this.own_foreign.TabStop = true;
            this.own_foreign.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_fertig);
            this.groupBox1.Controls.Add(this.checkBox_mindestens_einmal);
            this.groupBox1.Controls.Add(this.checkBox_falsch);
            this.groupBox1.Controls.Add(this.checkBox_noch_nie);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // checkBox_fertig
            // 
            resources.ApplyResources(this.checkBox_fertig, "checkBox_fertig");
            this.checkBox_fertig.Name = "checkBox_fertig";
            this.checkBox_fertig.UseVisualStyleBackColor = true;
            this.checkBox_fertig.CheckedChanged += new System.EventHandler(this.checkBox_fertig_CheckedChanged);
            // 
            // checkBox_mindestens_einmal
            // 
            resources.ApplyResources(this.checkBox_mindestens_einmal, "checkBox_mindestens_einmal");
            this.checkBox_mindestens_einmal.Name = "checkBox_mindestens_einmal";
            this.checkBox_mindestens_einmal.UseVisualStyleBackColor = true;
            this.checkBox_mindestens_einmal.CheckedChanged += new System.EventHandler(this.checkBox_mindestens_einmal_CheckedChanged);
            // 
            // checkBox_falsch
            // 
            resources.ApplyResources(this.checkBox_falsch, "checkBox_falsch");
            this.checkBox_falsch.Name = "checkBox_falsch";
            this.checkBox_falsch.UseVisualStyleBackColor = true;
            this.checkBox_falsch.CheckedChanged += new System.EventHandler(this.checkBox_falsch_CheckedChanged);
            // 
            // checkBox_noch_nie
            // 
            resources.ApplyResources(this.checkBox_noch_nie, "checkBox_noch_nie");
            this.checkBox_noch_nie.Name = "checkBox_noch_nie";
            this.checkBox_noch_nie.UseVisualStyleBackColor = true;
            this.checkBox_noch_nie.CheckedChanged += new System.EventHandler(this.checkBox_noch_nie_CheckedChanged);
            // 
            // choose_vocables_for_print
            // 
            this.AcceptButton = this.print_button;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.print_button);
            this.Controls.Add(this.discheck_all);
            this.Controls.Add(this.check_all);
            this.Controls.Add(this.listbox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "choose_vocables_for_print";
            this.ShowInTaskbar = false;
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button check_all;
        private System.Windows.Forms.Button discheck_all;
        private System.Windows.Forms.Button print_button;
        private System.Windows.Forms.Button cancel_button;
        public System.Windows.Forms.CheckedListBox listbox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.RadioButton radioButton_kaertchen;
        public System.Windows.Forms.RadioButton radioButton_liste;
        public System.Windows.Forms.RadioButton foreign_own;
        public System.Windows.Forms.RadioButton own_foreign;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckBox checkBox_falsch;
        public System.Windows.Forms.CheckBox checkBox_noch_nie;
        public System.Windows.Forms.CheckBox checkBox_mindestens_einmal;
        public System.Windows.Forms.CheckBox checkBox_fertig;
    }
}