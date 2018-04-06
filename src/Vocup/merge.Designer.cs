namespace Vocup
{
    partial class merge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(merge));
            this.cancel_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.listBox_files = new System.Windows.Forms.ListBox();
            this.add_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.take_results = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.own_language = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.foreign_language = new System.Windows.Forms.TextBox();
            this.sonderzeichen_button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancel_button, "cancel_button");
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // save_button
            // 
            resources.ApplyResources(this.save_button, "save_button");
            this.save_button.Name = "save_button";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // listBox_files
            // 
            this.listBox_files.FormattingEnabled = true;
            resources.ApplyResources(this.listBox_files, "listBox_files");
            this.listBox_files.Name = "listBox_files";
            this.listBox_files.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_files.SelectedIndexChanged += new System.EventHandler(this.listBox_files_SelectedIndexChanged);
            // 
            // add_button
            // 
            resources.ApplyResources(this.add_button, "add_button");
            this.add_button.Name = "add_button";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // delete_button
            // 
            resources.ApplyResources(this.delete_button, "delete_button");
            this.delete_button.Name = "delete_button";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // take_results
            // 
            resources.ApplyResources(this.take_results, "take_results");
            this.take_results.Name = "take_results";
            this.take_results.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.own_language);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // own_language
            // 
            resources.ApplyResources(this.own_language, "own_language");
            this.own_language.Name = "own_language";
            this.own_language.TextChanged += new System.EventHandler(this.own_language_TextChanged);
            this.own_language.Enter += new System.EventHandler(this.own_language_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.foreign_language);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // foreign_language
            // 
            resources.ApplyResources(this.foreign_language, "foreign_language");
            this.foreign_language.Name = "foreign_language";
            this.foreign_language.TextChanged += new System.EventHandler(this.foreign_language_TextChanged);
            this.foreign_language.Enter += new System.EventHandler(this.foreign_language_Enter);
            // 
            // sonderzeichen_button
            // 
            resources.ApplyResources(this.sonderzeichen_button, "sonderzeichen_button");
            this.sonderzeichen_button.Name = "sonderzeichen_button";
            this.sonderzeichen_button.UseVisualStyleBackColor = true;
            this.sonderzeichen_button.Click += new System.EventHandler(this.sonderzeichen_button_Click);
            // 
            // merge
            // 
            this.AcceptButton = this.save_button;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_button;
            this.Controls.Add(this.sonderzeichen_button);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.take_results);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.listBox_files);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.cancel_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "merge";
            this.ShowInTaskbar = false;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button save_button;
        public  System.Windows.Forms.ListBox listBox_files;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.Button delete_button;
        public  System.Windows.Forms.CheckBox take_results;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public  System.Windows.Forms.TextBox own_language;
        public  System.Windows.Forms.TextBox foreign_language;
        private System.Windows.Forms.Button sonderzeichen_button;
    }
}