namespace Vocup.Forms
{
    partial class AboutBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            BtnOK = new System.Windows.Forms.Button();
            LbVersion = new System.Windows.Forms.Label();
            LbCopyright = new System.Windows.Forms.Label();
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            LlbDownload = new System.Windows.Forms.LinkLabel();
            LlbProjectWebsite = new System.Windows.Forms.LinkLabel();
            LbWebsite = new System.Windows.Forms.Label();
            LbDownload = new System.Windows.Forms.Label();
            LbLicense = new System.Windows.Forms.Label();
            LbMail = new System.Windows.Forms.Label();
            LlbProjectLicense = new System.Windows.Forms.LinkLabel();
            LlbProjectMail = new System.Windows.Forms.LinkLabel();
            tabControl = new System.Windows.Forms.TabControl();
            TpInfo = new System.Windows.Forms.TabPage();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            TpComponents = new System.Windows.Forms.TabPage();
            AvaloniaControlHost = new Avalonia.Win32.Interoperability.WinFormsAvaloniaControlHost();
            tableLayoutPanel.SuspendLayout();
            tabControl.SuspendLayout();
            TpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            TpComponents.SuspendLayout();
            SuspendLayout();
            // 
            // BtnOK
            // 
            BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(BtnOK, "BtnOK");
            BtnOK.Name = "BtnOK";
            BtnOK.UseVisualStyleBackColor = true;
            // 
            // LbVersion
            // 
            resources.ApplyResources(LbVersion, "LbVersion");
            LbVersion.ForeColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LbVersion.Name = "LbVersion";
            // 
            // LbCopyright
            // 
            resources.ApplyResources(LbCopyright, "LbCopyright");
            LbCopyright.ForeColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LbCopyright.Name = "LbCopyright";
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(tableLayoutPanel, "tableLayoutPanel");
            tableLayoutPanel.Controls.Add(LlbDownload, 1, 1);
            tableLayoutPanel.Controls.Add(LlbProjectWebsite, 1, 0);
            tableLayoutPanel.Controls.Add(LbWebsite, 0, 0);
            tableLayoutPanel.Controls.Add(LbDownload, 0, 1);
            tableLayoutPanel.Controls.Add(LbLicense, 0, 3);
            tableLayoutPanel.Controls.Add(LbMail, 0, 2);
            tableLayoutPanel.Controls.Add(LlbProjectLicense, 1, 3);
            tableLayoutPanel.Controls.Add(LlbProjectMail, 1, 2);
            tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // LlbDownload
            // 
            LlbDownload.ActiveLinkColor = System.Drawing.Color.FromArgb(99, 99, 99);
            resources.ApplyResources(LlbDownload, "LlbDownload");
            LlbDownload.LinkColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LlbDownload.Name = "LlbDownload";
            LlbDownload.TabStop = true;
            LlbDownload.VisitedLinkColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LlbDownload.LinkClicked += LlbDownload_LinkClicked;
            // 
            // LlbProjectWebsite
            // 
            LlbProjectWebsite.ActiveLinkColor = System.Drawing.Color.FromArgb(99, 99, 99);
            resources.ApplyResources(LlbProjectWebsite, "LlbProjectWebsite");
            LlbProjectWebsite.LinkColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LlbProjectWebsite.Name = "LlbProjectWebsite";
            LlbProjectWebsite.TabStop = true;
            LlbProjectWebsite.VisitedLinkColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LlbProjectWebsite.LinkClicked += LlbProjectWebsite_LinkClicked;
            // 
            // LbWebsite
            // 
            resources.ApplyResources(LbWebsite, "LbWebsite");
            LbWebsite.ForeColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LbWebsite.Name = "LbWebsite";
            // 
            // LbDownload
            // 
            resources.ApplyResources(LbDownload, "LbDownload");
            LbDownload.ForeColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LbDownload.Name = "LbDownload";
            // 
            // LbLicense
            // 
            resources.ApplyResources(LbLicense, "LbLicense");
            LbLicense.ForeColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LbLicense.Name = "LbLicense";
            // 
            // LbMail
            // 
            resources.ApplyResources(LbMail, "LbMail");
            LbMail.ForeColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LbMail.Name = "LbMail";
            // 
            // LlbProjectLicense
            // 
            LlbProjectLicense.ActiveLinkColor = System.Drawing.Color.FromArgb(99, 99, 99);
            resources.ApplyResources(LlbProjectLicense, "LlbProjectLicense");
            LlbProjectLicense.LinkColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LlbProjectLicense.Name = "LlbProjectLicense";
            LlbProjectLicense.TabStop = true;
            LlbProjectLicense.VisitedLinkColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LlbProjectLicense.LinkClicked += LlbProjectLicense_LinkClicked;
            // 
            // LlbProjectMail
            // 
            LlbProjectMail.ActiveLinkColor = System.Drawing.Color.FromArgb(99, 99, 99);
            resources.ApplyResources(LlbProjectMail, "LlbProjectMail");
            LlbProjectMail.LinkColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LlbProjectMail.Name = "LlbProjectMail";
            LlbProjectMail.TabStop = true;
            LlbProjectMail.VisitedLinkColor = System.Drawing.Color.FromArgb(99, 99, 99);
            LlbProjectMail.LinkClicked += LlbProjectMail_LinkClicked;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(TpInfo);
            tabControl.Controls.Add(TpComponents);
            resources.ApplyResources(tabControl, "tabControl");
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            // 
            // TpInfo
            // 
            TpInfo.BackColor = System.Drawing.Color.White;
            TpInfo.Controls.Add(pictureBox1);
            TpInfo.Controls.Add(LbVersion);
            TpInfo.Controls.Add(LbCopyright);
            TpInfo.Controls.Add(tableLayoutPanel);
            resources.ApplyResources(TpInfo, "TpInfo");
            TpInfo.Name = "TpInfo";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // TpComponents
            // 
            TpComponents.BackColor = System.Drawing.Color.White;
            TpComponents.Controls.Add(AvaloniaControlHost);
            resources.ApplyResources(TpComponents, "TpComponents");
            TpComponents.Name = "TpComponents";
            // 
            // AvaloniaControlHost
            // 
            AvaloniaControlHost.Content = null;
            resources.ApplyResources(AvaloniaControlHost, "AvaloniaControlHost");
            AvaloniaControlHost.Name = "AvaloniaControlHost";
            // 
            // AboutBox
            // 
            AcceptButton = BtnOK;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tabControl);
            Controls.Add(BtnOK);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutBox";
            ShowInTaskbar = false;
            Load += AboutBox_Load;
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            tabControl.ResumeLayout(false);
            TpInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            TpComponents.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Label LbVersion;
        private System.Windows.Forms.Label LbCopyright;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label LbWebsite;
        private System.Windows.Forms.LinkLabel LlbProjectWebsite;
        private System.Windows.Forms.LinkLabel LlbProjectMail;
        private System.Windows.Forms.Label LbMail;
        private System.Windows.Forms.Label LbLicense;
        public  System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage TpInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        public  System.Windows.Forms.TabPage TpComponents;
        private System.Windows.Forms.LinkLabel LlbProjectLicense;
        private System.Windows.Forms.Label LbDownload;
        private System.Windows.Forms.LinkLabel LlbDownload;
        private Avalonia.Win32.Interoperability.WinFormsAvaloniaControlHost AvaloniaControlHost;
    }
}