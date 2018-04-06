namespace Vocup
{
    partial class backup_restore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(backup_restore));
            this.path_field = new System.Windows.Forms.TextBox();
            this.path_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.exact_path = new System.Windows.Forms.CheckBox();
            this.listbox_vhf = new System.Windows.Forms.CheckedListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.listbox_special_chars = new System.Windows.Forms.CheckedListBox();
            this.restore_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.replace_newer = new System.Windows.Forms.RadioButton();
            this.replace_nothing = new System.Windows.Forms.RadioButton();
            this.replace_all = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.results_restore_nothing = new System.Windows.Forms.RadioButton();
            this.results_restore_all = new System.Windows.Forms.RadioButton();
            this.results_restore_choosed = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // path_field
            // 
            resources.ApplyResources(this.path_field, "path_field");
            this.path_field.Name = "path_field";
            this.path_field.ReadOnly = true;
            this.path_field.TextChanged += new System.EventHandler(this.path_field_TextChanged);
            // 
            // path_button
            // 
            resources.ApplyResources(this.path_button, "path_button");
            this.path_button.Name = "path_button";
            this.path_button.UseVisualStyleBackColor = true;
            this.path_button.Click += new System.EventHandler(this.path_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.path_field);
            this.groupBox1.Controls.Add(this.path_button);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.exact_path);
            this.groupBox3.Controls.Add(this.listbox_vhf);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // exact_path
            // 
            resources.ApplyResources(this.exact_path, "exact_path");
            this.exact_path.Name = "exact_path";
            this.exact_path.UseVisualStyleBackColor = true;
            this.exact_path.CheckedChanged += new System.EventHandler(this.exact_path_CheckedChanged);
            // 
            // listbox_vhf
            // 
            this.listbox_vhf.CheckOnClick = true;
            this.listbox_vhf.FormattingEnabled = true;
            resources.ApplyResources(this.listbox_vhf, "listbox_vhf");
            this.listbox_vhf.Name = "listbox_vhf";
            this.listbox_vhf.SelectedValueChanged += new System.EventHandler(this.listbox_vhf_SelectedValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.listbox_special_chars);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // listbox_special_chars
            // 
            this.listbox_special_chars.CheckOnClick = true;
            this.listbox_special_chars.FormattingEnabled = true;
            resources.ApplyResources(this.listbox_special_chars, "listbox_special_chars");
            this.listbox_special_chars.Name = "listbox_special_chars";
            this.listbox_special_chars.SelectedValueChanged += new System.EventHandler(this.listbox_special_chars_SelectedValueChanged);
            // 
            // restore_button
            // 
            this.restore_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.restore_button, "restore_button");
            this.restore_button.Name = "restore_button";
            this.restore_button.UseVisualStyleBackColor = true;
            this.restore_button.Click += new System.EventHandler(this.restore_button_Click);
            this.restore_button.MouseEnter += new System.EventHandler(this.restore_button_MouseEnter);
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancel_button, "cancel_button");
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.replace_newer);
            this.groupBox2.Controls.Add(this.replace_nothing);
            this.groupBox2.Controls.Add(this.replace_all);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // replace_newer
            // 
            resources.ApplyResources(this.replace_newer, "replace_newer");
            this.replace_newer.Name = "replace_newer";
            this.replace_newer.UseVisualStyleBackColor = true;
            this.replace_newer.CheckedChanged += new System.EventHandler(this.replace_newer_CheckedChanged);
            // 
            // replace_nothing
            // 
            resources.ApplyResources(this.replace_nothing, "replace_nothing");
            this.replace_nothing.Name = "replace_nothing";
            this.replace_nothing.UseVisualStyleBackColor = true;
            this.replace_nothing.CheckedChanged += new System.EventHandler(this.replace_nothing_CheckedChanged);
            // 
            // replace_all
            // 
            resources.ApplyResources(this.replace_all, "replace_all");
            this.replace_all.Checked = true;
            this.replace_all.Name = "replace_all";
            this.replace_all.TabStop = true;
            this.replace_all.UseVisualStyleBackColor = true;
            this.replace_all.CheckedChanged += new System.EventHandler(this.replace_all_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.results_restore_nothing);
            this.groupBox4.Controls.Add(this.results_restore_all);
            this.groupBox4.Controls.Add(this.results_restore_choosed);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // results_restore_nothing
            // 
            resources.ApplyResources(this.results_restore_nothing, "results_restore_nothing");
            this.results_restore_nothing.Name = "results_restore_nothing";
            this.results_restore_nothing.UseVisualStyleBackColor = true;
            this.results_restore_nothing.CheckedChanged += new System.EventHandler(this.results_restore_nothing_CheckedChanged);
            // 
            // results_restore_all
            // 
            resources.ApplyResources(this.results_restore_all, "results_restore_all");
            this.results_restore_all.Name = "results_restore_all";
            this.results_restore_all.UseVisualStyleBackColor = true;
            this.results_restore_all.CheckedChanged += new System.EventHandler(this.results_restore_all_CheckedChanged);
            // 
            // results_restore_choosed
            // 
            resources.ApplyResources(this.results_restore_choosed, "results_restore_choosed");
            this.results_restore_choosed.Checked = true;
            this.results_restore_choosed.Name = "results_restore_choosed";
            this.results_restore_choosed.TabStop = true;
            this.results_restore_choosed.UseVisualStyleBackColor = true;
            this.results_restore_choosed.CheckedChanged += new System.EventHandler(this.results_restore_choosed_CheckedChanged);
            // 
            // backup_restore
            // 
            this.AcceptButton = this.path_button;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_button;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.restore_button);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "backup_restore";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.backup_restore_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public  System.Windows.Forms.TextBox path_field;
        public  System.Windows.Forms.Button path_button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        public  System.Windows.Forms.CheckedListBox listbox_vhf;
        private System.Windows.Forms.GroupBox groupBox5;
        public  System.Windows.Forms.CheckedListBox listbox_special_chars;
        private System.Windows.Forms.Button restore_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.GroupBox groupBox2;
        public  System.Windows.Forms.RadioButton replace_newer;
        public  System.Windows.Forms.RadioButton replace_nothing;
        public  System.Windows.Forms.RadioButton replace_all;
        private System.Windows.Forms.GroupBox groupBox4;
        public  System.Windows.Forms.RadioButton results_restore_all;
        public  System.Windows.Forms.RadioButton results_restore_choosed;
        public  System.Windows.Forms.RadioButton results_restore_nothing;
        private System.Windows.Forms.CheckBox exact_path;
    }
}