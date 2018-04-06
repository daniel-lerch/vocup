namespace Vocup
{
    partial class backup_add
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(backup_add));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.folgende_vokabelhefte = new System.Windows.Forms.CheckBox();
            this.alle_vokabelhefte = new System.Windows.Forms.CheckBox();
            this.add_vokabelheft_button = new System.Windows.Forms.Button();
            this.delete_vokabelheft_button = new System.Windows.Forms.Button();
            this.listbox_vokabelhefte = new System.Windows.Forms.ListBox();
            this.cancel_button = new System.Windows.Forms.Button();
            this.create_backup_button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.keine_ergebnisse = new System.Windows.Forms.RadioButton();
            this.gewaehlte_ergebnisse = new System.Windows.Forms.RadioButton();
            this.alle_ergebnisse = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listbox_special_chars = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.folgende_vokabelhefte);
            this.groupBox1.Controls.Add(this.alle_vokabelhefte);
            this.groupBox1.Controls.Add(this.add_vokabelheft_button);
            this.groupBox1.Controls.Add(this.delete_vokabelheft_button);
            this.groupBox1.Controls.Add(this.listbox_vokabelhefte);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // folgende_vokabelhefte
            // 
            resources.ApplyResources(this.folgende_vokabelhefte, "folgende_vokabelhefte");
            this.folgende_vokabelhefte.Name = "folgende_vokabelhefte";
            this.folgende_vokabelhefte.UseVisualStyleBackColor = true;
            this.folgende_vokabelhefte.CheckedChanged += new System.EventHandler(this.folgende_vokabelhefte_CheckedChanged);
            // 
            // alle_vokabelhefte
            // 
            resources.ApplyResources(this.alle_vokabelhefte, "alle_vokabelhefte");
            this.alle_vokabelhefte.Checked = true;
            this.alle_vokabelhefte.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alle_vokabelhefte.Name = "alle_vokabelhefte";
            this.alle_vokabelhefte.UseVisualStyleBackColor = true;
            this.alle_vokabelhefte.CheckedChanged += new System.EventHandler(this.alle_vokabelhefte_CheckedChanged);
            // 
            // add_vokabelheft_button
            // 
            resources.ApplyResources(this.add_vokabelheft_button, "add_vokabelheft_button");
            this.add_vokabelheft_button.Name = "add_vokabelheft_button";
            this.add_vokabelheft_button.UseVisualStyleBackColor = true;
            this.add_vokabelheft_button.Click += new System.EventHandler(this.add_vokabelheft_button_Click);
            // 
            // delete_vokabelheft_button
            // 
            resources.ApplyResources(this.delete_vokabelheft_button, "delete_vokabelheft_button");
            this.delete_vokabelheft_button.Name = "delete_vokabelheft_button";
            this.delete_vokabelheft_button.UseVisualStyleBackColor = true;
            this.delete_vokabelheft_button.Click += new System.EventHandler(this.delete_vokabelheft_button_Click);
            // 
            // listbox_vokabelhefte
            // 
            resources.ApplyResources(this.listbox_vokabelhefte, "listbox_vokabelhefte");
            this.listbox_vokabelhefte.FormattingEnabled = true;
            this.listbox_vokabelhefte.Name = "listbox_vokabelhefte";
            this.listbox_vokabelhefte.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listbox_vokabelhefte.SelectedIndexChanged += new System.EventHandler(this.listbox_vokabelhefte_SelectedIndexChanged);
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancel_button, "cancel_button");
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // create_backup_button
            // 
            resources.ApplyResources(this.create_backup_button, "create_backup_button");
            this.create_backup_button.Name = "create_backup_button";
            this.create_backup_button.UseVisualStyleBackColor = true;
            this.create_backup_button.Click += new System.EventHandler(this.create_backup_button_Click);
            this.create_backup_button.MouseEnter += new System.EventHandler(this.create_backup_button_MouseEnter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.keine_ergebnisse);
            this.groupBox2.Controls.Add(this.gewaehlte_ergebnisse);
            this.groupBox2.Controls.Add(this.alle_ergebnisse);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // keine_ergebnisse
            // 
            resources.ApplyResources(this.keine_ergebnisse, "keine_ergebnisse");
            this.keine_ergebnisse.Name = "keine_ergebnisse";
            this.keine_ergebnisse.UseVisualStyleBackColor = true;
            this.keine_ergebnisse.CheckedChanged += new System.EventHandler(this.keine_ergebnisse_CheckedChanged);
            // 
            // gewaehlte_ergebnisse
            // 
            resources.ApplyResources(this.gewaehlte_ergebnisse, "gewaehlte_ergebnisse");
            this.gewaehlte_ergebnisse.Checked = true;
            this.gewaehlte_ergebnisse.Name = "gewaehlte_ergebnisse";
            this.gewaehlte_ergebnisse.TabStop = true;
            this.gewaehlte_ergebnisse.UseVisualStyleBackColor = true;
            // 
            // alle_ergebnisse
            // 
            resources.ApplyResources(this.alle_ergebnisse, "alle_ergebnisse");
            this.alle_ergebnisse.Name = "alle_ergebnisse";
            this.alle_ergebnisse.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listbox_special_chars);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // listbox_special_chars
            // 
            this.listbox_special_chars.CheckOnClick = true;
            this.listbox_special_chars.FormattingEnabled = true;
            resources.ApplyResources(this.listbox_special_chars, "listbox_special_chars");
            this.listbox_special_chars.Name = "listbox_special_chars";
            this.listbox_special_chars.SelectedValueChanged += new System.EventHandler(this.listbox_special_chars_SelectedValueChanged);
            // 
            // backup_add
            // 
            this.AcceptButton = this.create_backup_button;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.cancel_button;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.create_backup_button);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "backup_add";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.backup_add_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button add_vokabelheft_button;
        private System.Windows.Forms.Button delete_vokabelheft_button;
        public  System.Windows.Forms.ListBox listbox_vokabelhefte;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button create_backup_button;
        public  System.Windows.Forms.CheckBox alle_vokabelhefte;
        private System.Windows.Forms.GroupBox groupBox2;
        public  System.Windows.Forms.RadioButton gewaehlte_ergebnisse;
        public  System.Windows.Forms.RadioButton alle_ergebnisse;
        public  System.Windows.Forms.CheckBox folgende_vokabelhefte;
        public  System.Windows.Forms.RadioButton keine_ergebnisse;
        private System.Windows.Forms.GroupBox groupBox3;
        public  System.Windows.Forms.CheckedListBox listbox_special_chars;
    }
}