namespace Vocup.Forms
{
    partial class SplashScreen
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
            this.LbCopyright = new System.Windows.Forms.Label();
            this.PbLogo = new System.Windows.Forms.PictureBox();
            this.PbTitle = new System.Windows.Forms.PictureBox();
            this.LbVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // LbCopyright
            // 
            this.LbCopyright.AutoSize = true;
            this.LbCopyright.BackColor = System.Drawing.Color.Transparent;
            this.LbCopyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.LbCopyright.Location = new System.Drawing.Point(12, 178);
            this.LbCopyright.Name = "LbCopyright";
            this.LbCopyright.Size = new System.Drawing.Size(67, 13);
            this.LbCopyright.TabIndex = 0;
            this.LbCopyright.Text = "%Copyright%";
            // 
            // PbLogo
            // 
            this.PbLogo.BackColor = System.Drawing.Color.Transparent;
            this.PbLogo.Image = global::Vocup.Properties.Icons.logo_splash;
            this.PbLogo.Location = new System.Drawing.Point(57, 63);
            this.PbLogo.Name = "PbLogo";
            this.PbLogo.Size = new System.Drawing.Size(75, 75);
            this.PbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PbLogo.TabIndex = 5;
            this.PbLogo.TabStop = false;
            // 
            // PbTitle
            // 
            this.PbTitle.BackColor = System.Drawing.Color.Transparent;
            this.PbTitle.Image = global::Vocup.Properties.Icons.logo;
            this.PbTitle.Location = new System.Drawing.Point(146, 60);
            this.PbTitle.Name = "PbTitle";
            this.PbTitle.Size = new System.Drawing.Size(248, 80);
            this.PbTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PbTitle.TabIndex = 3;
            this.PbTitle.TabStop = false;
            // 
            // LbVersion
            // 
            this.LbVersion.AutoSize = true;
            this.LbVersion.BackColor = System.Drawing.Color.Transparent;
            this.LbVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.LbVersion.Location = new System.Drawing.Point(370, 178);
            this.LbVersion.Name = "LbVersion";
            this.LbVersion.Size = new System.Drawing.Size(62, 13);
            this.LbVersion.TabIndex = 1;
            this.LbVersion.Text = "Version: {0}";
            this.LbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 200);
            this.ControlBox = false;
            this.Controls.Add(this.LbVersion);
            this.Controls.Add(this.PbLogo);
            this.Controls.Add(this.LbCopyright);
            this.Controls.Add(this.PbTitle);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "splashscreen";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SplashScreen_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.PbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbTitle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PbTitle;
        private System.Windows.Forms.Label LbCopyright;
        private System.Windows.Forms.PictureBox PbLogo;
        private System.Windows.Forms.Label LbVersion;
    }
}