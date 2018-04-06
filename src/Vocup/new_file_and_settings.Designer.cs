namespace Vocup
{
    partial class new_file_and_settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(new_file_and_settings));
            this.sonderzeichen_button = new System.Windows.Forms.Button();
            this.option_box = new System.Windows.Forms.GroupBox();
            this.reset_vocabel = new System.Windows.Forms.CheckBox();
            this.cancel_button = new System.Windows.Forms.Button();
            this.ok_button = new System.Windows.Forms.Button();
            this.muttersprache_text = new System.Windows.Forms.TextBox();
            this.muttersprache = new System.Windows.Forms.Label();
            this.fremdsprache = new System.Windows.Forms.Label();
            this.fremdsprache_text = new System.Windows.Forms.TextBox();
            this.uebersetzungsrichtung = new System.Windows.Forms.GroupBox();
            this.fremd_mutter = new System.Windows.Forms.RadioButton();
            this.mutter_fremd = new System.Windows.Forms.RadioButton();
            this.option_box.SuspendLayout();
            this.uebersetzungsrichtung.SuspendLayout();
            this.SuspendLayout();
            // 
            // sonderzeichen_button
            // 
            resources.ApplyResources(this.sonderzeichen_button, "sonderzeichen_button");
            this.sonderzeichen_button.Name = "sonderzeichen_button";
            this.sonderzeichen_button.UseVisualStyleBackColor = true;
            this.sonderzeichen_button.Click += new System.EventHandler(this.sonderzeichen_button_Click);
            // 
            // option_box
            // 
            this.option_box.Controls.Add(this.reset_vocabel);
            resources.ApplyResources(this.option_box, "option_box");
            this.option_box.Name = "option_box";
            this.option_box.TabStop = false;
            // 
            // reset_vocabel
            // 
            resources.ApplyResources(this.reset_vocabel, "reset_vocabel");
            this.reset_vocabel.Name = "reset_vocabel";
            this.reset_vocabel.UseVisualStyleBackColor = true;
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancel_button, "cancel_button");
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // ok_button
            // 
            this.ok_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.ok_button, "ok_button");
            this.ok_button.Name = "ok_button";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // muttersprache_text
            // 
            resources.ApplyResources(this.muttersprache_text, "muttersprache_text");
            this.muttersprache_text.Name = "muttersprache_text";
            this.muttersprache_text.TextChanged += new System.EventHandler(this.muttersprache_text_TextChanged);
            this.muttersprache_text.Enter += new System.EventHandler(this.muttersprache_text_Enter);
            // 
            // muttersprache
            // 
            resources.ApplyResources(this.muttersprache, "muttersprache");
            this.muttersprache.Name = "muttersprache";
            // 
            // fremdsprache
            // 
            resources.ApplyResources(this.fremdsprache, "fremdsprache");
            this.fremdsprache.Name = "fremdsprache";
            // 
            // fremdsprache_text
            // 
            resources.ApplyResources(this.fremdsprache_text, "fremdsprache_text");
            this.fremdsprache_text.Name = "fremdsprache_text";
            this.fremdsprache_text.TextChanged += new System.EventHandler(this.fremdsprache_text_TextChanged);
            this.fremdsprache_text.Enter += new System.EventHandler(this.fremdsprache_text_Enter);
            // 
            // uebersetzungsrichtung
            // 
            this.uebersetzungsrichtung.Controls.Add(this.fremd_mutter);
            this.uebersetzungsrichtung.Controls.Add(this.mutter_fremd);
            resources.ApplyResources(this.uebersetzungsrichtung, "uebersetzungsrichtung");
            this.uebersetzungsrichtung.Name = "uebersetzungsrichtung";
            this.uebersetzungsrichtung.TabStop = false;
            // 
            // fremd_mutter
            // 
            resources.ApplyResources(this.fremd_mutter, "fremd_mutter");
            this.fremd_mutter.Name = "fremd_mutter";
            this.fremd_mutter.TabStop = true;
            this.fremd_mutter.UseVisualStyleBackColor = true;
            // 
            // mutter_fremd
            // 
            resources.ApplyResources(this.mutter_fremd, "mutter_fremd");
            this.mutter_fremd.Checked = true;
            this.mutter_fremd.Name = "mutter_fremd";
            this.mutter_fremd.TabStop = true;
            this.mutter_fremd.UseVisualStyleBackColor = true;
            // 
            // new_file_and_settings
            // 
            this.AcceptButton = this.ok_button;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.cancel_button;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.uebersetzungsrichtung);
            this.Controls.Add(this.fremdsprache);
            this.Controls.Add(this.fremdsprache_text);
            this.Controls.Add(this.muttersprache);
            this.Controls.Add(this.muttersprache_text);
            this.Controls.Add(this.sonderzeichen_button);
            this.Controls.Add(this.option_box);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "new_file_and_settings";
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.new_file_and_settings_FormClosed);
            this.option_box.ResumeLayout(false);
            this.option_box.PerformLayout();
            this.uebersetzungsrichtung.ResumeLayout(false);
            this.uebersetzungsrichtung.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sonderzeichen_button;
        public  System.Windows.Forms.GroupBox option_box;
        public  System.Windows.Forms.CheckBox reset_vocabel;
        private System.Windows.Forms.Button cancel_button;
        public  System.Windows.Forms.Button ok_button;
        public  System.Windows.Forms.TextBox muttersprache_text;
        private System.Windows.Forms.Label muttersprache;
        private System.Windows.Forms.Label fremdsprache;
        public  System.Windows.Forms.TextBox fremdsprache_text;
        private System.Windows.Forms.GroupBox uebersetzungsrichtung;
        public  System.Windows.Forms.RadioButton fremd_mutter;
        public  System.Windows.Forms.RadioButton mutter_fremd;
    }
}