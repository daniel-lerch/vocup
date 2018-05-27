namespace Vocup
{
    partial class new_and_settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(new_and_settings));
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.foreign_language_2 = new System.Windows.Forms.TextBox();
            this.foreign_language = new System.Windows.Forms.TextBox();
            this.foreign_language_text = new System.Windows.Forms.Label();
            this.reset_vocabel = new System.Windows.Forms.CheckBox();
            this.foreign_language2_text = new System.Windows.Forms.Label();
            this.option_box = new System.Windows.Forms.GroupBox();
            this.sonderzeichen_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.own_language = new System.Windows.Forms.TextBox();
            this.own_language_text = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.option_box.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ok_button
            // 
            this.ok_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.ok_button, "ok_button");
            this.ok_button.Name = "ok_button";
            this.ok_button.UseVisualStyleBackColor = true;
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancel_button, "cancel_button");
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // foreign_language_2
            // 
            resources.ApplyResources(this.foreign_language_2, "foreign_language_2");
            this.foreign_language_2.Name = "foreign_language_2";
            this.foreign_language_2.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.foreign_language_2.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // foreign_language
            // 
            resources.ApplyResources(this.foreign_language, "foreign_language");
            this.foreign_language.Name = "foreign_language";
            this.foreign_language.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.foreign_language.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // foreign_language_text
            // 
            resources.ApplyResources(this.foreign_language_text, "foreign_language_text");
            this.foreign_language_text.Name = "foreign_language_text";
            // 
            // reset_vocabel
            // 
            resources.ApplyResources(this.reset_vocabel, "reset_vocabel");
            this.reset_vocabel.Name = "reset_vocabel";
            this.reset_vocabel.UseVisualStyleBackColor = true;
            // 
            // foreign_language2_text
            // 
            resources.ApplyResources(this.foreign_language2_text, "foreign_language2_text");
            this.foreign_language2_text.Name = "foreign_language2_text";
            // 
            // option_box
            // 
            this.option_box.Controls.Add(this.reset_vocabel);
            resources.ApplyResources(this.option_box, "option_box");
            this.option_box.Name = "option_box";
            this.option_box.TabStop = false;
            // 
            // sonderzeichen_button
            // 
            resources.ApplyResources(this.sonderzeichen_button, "sonderzeichen_button");
            this.sonderzeichen_button.Name = "sonderzeichen_button";
            this.sonderzeichen_button.UseVisualStyleBackColor = true;
            this.sonderzeichen_button.Click += new System.EventHandler(this.sonderzeichen_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.foreign_language);
            this.groupBox1.Controls.Add(this.foreign_language_2);
            this.groupBox1.Controls.Add(this.foreign_language2_text);
            this.groupBox1.Controls.Add(this.foreign_language_text);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // own_language
            // 
            this.own_language.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.own_language, "own_language");
            this.own_language.Name = "own_language";
            this.own_language.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.own_language.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // own_language_text
            // 
            resources.ApplyResources(this.own_language_text, "own_language_text");
            this.own_language_text.Name = "own_language_text";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.own_language_text);
            this.groupBox2.Controls.Add(this.own_language);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // new_and_settings
            // 
            this.AcceptButton = this.ok_button;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.sonderzeichen_button);
            this.Controls.Add(this.option_box);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "new_and_settings";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.new_and_settings_Load);
            this.option_box.ResumeLayout(false);
            this.option_box.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button ok_button;
        public  System.Windows.Forms.TextBox foreign_language_2;
        public System.Windows.Forms.TextBox foreign_language;
        public System.Windows.Forms.Label foreign_language_text;
        public  System.Windows.Forms.CheckBox reset_vocabel;
        public System.Windows.Forms.Label foreign_language2_text;
        public System.Windows.Forms.GroupBox option_box;
        private System.Windows.Forms.Button sonderzeichen_button;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox own_language;
        public System.Windows.Forms.Label own_language_text;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button cancel_button;
    }
}