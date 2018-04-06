namespace Vocup
{
    partial class donation
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.spende_text = new System.Windows.Forms.Label();
            this.spende_titel = new System.Windows.Forms.Label();
            this.spender = new System.Windows.Forms.LinkLabel();
            this.waehrung = new System.Windows.Forms.ComboBox();
            this.spende_button = new System.Windows.Forms.PictureBox();
            this.close_button = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spende_button)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Vocup.icons.heart;
            this.pictureBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox2.Location = new System.Drawing.Point(210, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // spende_text
            // 
            this.spende_text.AutoSize = true;
            this.spende_text.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.spende_text.Location = new System.Drawing.Point(12, 42);
            this.spende_text.Name = "spende_text";
            this.spende_text.Size = new System.Drawing.Size(315, 65);
            this.spende_text.TabIndex = 1;
            this.spende_text.Text = "Vocup wird für den privaten Gebrauch kostenlos angeboten.\r\n \r\nDamit Vocup weiterh" +
                "in kostenlos bleibt, sind wir auf Ihre Spenden\r\nangewiesen!\r\n \r\n";
            // 
            // spende_titel
            // 
            this.spende_titel.AutoSize = true;
            this.spende_titel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.spende_titel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.spende_titel.Location = new System.Drawing.Point(12, 9);
            this.spende_titel.Name = "spende_titel";
            this.spende_titel.Size = new System.Drawing.Size(192, 15);
            this.spende_titel.TabIndex = 0;
            this.spende_titel.Text = "Helfen Sie mit einer Spende!";
            // 
            // spender
            // 
            this.spender.AutoSize = true;
            this.spender.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.spender.Location = new System.Drawing.Point(12, 162);
            this.spender.Name = "spender";
            this.spender.Size = new System.Drawing.Size(93, 13);
            this.spender.TabIndex = 3;
            this.spender.TabStop = true;
            this.spender.Text = "Spender anzeigen";
            this.spender.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.spender_LinkClicked);
            // 
            // waehrung
            // 
            this.waehrung.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.waehrung.FormattingEnabled = true;
            this.waehrung.Items.AddRange(new object[] {
            "CHF",
            "EUR",
            "USD"});
            this.waehrung.Location = new System.Drawing.Point(15, 115);
            this.waehrung.Name = "waehrung";
            this.waehrung.Size = new System.Drawing.Size(83, 21);
            this.waehrung.TabIndex = 2;
            // 
            // spende_button
            // 
            this.spende_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.spende_button.Image = global::Vocup.icons.btn_donate_LG;
            this.spende_button.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.spende_button.Location = new System.Drawing.Point(106, 110);
            this.spende_button.Name = "spende_button";
            this.spende_button.Size = new System.Drawing.Size(98, 26);
            this.spende_button.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.spende_button.TabIndex = 6;
            this.spende_button.TabStop = false;
            this.spende_button.Click += new System.EventHandler(this.spende_button_Click);
            // 
            // close_button
            // 
            this.close_button.Location = new System.Drawing.Point(256, 157);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(75, 23);
            this.close_button.TabIndex = 4;
            this.close_button.Text = "15 sek";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // donation
            // 
            this.AcceptButton = this.close_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 192);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.spende_text);
            this.Controls.Add(this.spende_titel);
            this.Controls.Add(this.spender);
            this.Controls.Add(this.waehrung);
            this.Controls.Add(this.spende_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "donation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.donation_Load);
            this.Shown += new System.EventHandler(this.donation_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.donation_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spende_button)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label spende_text;
        private System.Windows.Forms.Label spende_titel;
        private System.Windows.Forms.LinkLabel spender;
        private System.Windows.Forms.ComboBox waehrung;
        private System.Windows.Forms.PictureBox spende_button;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.Timer timer;
    }
}