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
            this.LbOS = new System.Windows.Forms.Label();
            this.LbNetFramwork = new System.Windows.Forms.Label();
            this.BtnOK = new System.Windows.Forms.Button();
            this.LbVersion = new System.Windows.Forms.Label();
            this.LbCopyright = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.LlbDownload = new System.Windows.Forms.LinkLabel();
            this.LlbProjectWebsite = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LlbProjectLicense = new System.Windows.Forms.LinkLabel();
            this.LlbProjectEMail = new System.Windows.Forms.LinkLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.TpInfo = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TpComponents = new System.Windows.Forms.TabPage();
            this.LwComponents = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.software_version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lizenz = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.url = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TpUpdates = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.TpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.TpComponents.SuspendLayout();
            this.SuspendLayout();
            // 
            // LbOS
            // 
            resources.ApplyResources(this.LbOS, "LbOS");
            this.LbOS.Name = "LbOS";
            // 
            // LbNetFramwork
            // 
            resources.ApplyResources(this.LbNetFramwork, "LbNetFramwork");
            this.LbNetFramwork.Name = "LbNetFramwork";
            // 
            // BtnOK
            // 
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.BtnOK, "BtnOK");
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.UseVisualStyleBackColor = true;
            // 
            // LbVersion
            // 
            resources.ApplyResources(this.LbVersion, "LbVersion");
            this.LbVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.LbVersion.Name = "LbVersion";
            // 
            // LbCopyright
            // 
            resources.ApplyResources(this.LbCopyright, "LbCopyright");
            this.LbCopyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.LbCopyright.Name = "LbCopyright";
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.LlbDownload, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.LlbProjectWebsite, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.LlbProjectLicense, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.LlbProjectEMail, 1, 2);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // LlbDownload
            // 
            this.LlbDownload.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            resources.ApplyResources(this.LlbDownload, "LlbDownload");
            this.LlbDownload.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.LlbDownload.Name = "LlbDownload";
            this.LlbDownload.TabStop = true;
            this.LlbDownload.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.LlbDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlbDownload_LinkClicked);
            // 
            // LlbProjectWebsite
            // 
            this.LlbProjectWebsite.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            resources.ApplyResources(this.LlbProjectWebsite, "LlbProjectWebsite");
            this.LlbProjectWebsite.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.LlbProjectWebsite.Name = "LlbProjectWebsite";
            this.LlbProjectWebsite.TabStop = true;
            this.LlbProjectWebsite.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.LlbProjectWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlbProjectWebsite_LinkClicked);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.label1.Name = "label1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.label5.Name = "label5";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.label3.Name = "label3";
            // 
            // LlbProjectLicense
            // 
            this.LlbProjectLicense.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            resources.ApplyResources(this.LlbProjectLicense, "LlbProjectLicense");
            this.LlbProjectLicense.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.LlbProjectLicense.Name = "LlbProjectLicense";
            this.LlbProjectLicense.TabStop = true;
            this.LlbProjectLicense.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.LlbProjectLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlbProjectLicense_LinkClicked);
            // 
            // LlbProjectEMail
            // 
            this.LlbProjectEMail.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            resources.ApplyResources(this.LlbProjectEMail, "LlbProjectEMail");
            this.LlbProjectEMail.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.LlbProjectEMail.Name = "LlbProjectEMail";
            this.LlbProjectEMail.TabStop = true;
            this.LlbProjectEMail.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.LlbProjectEMail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlbProjectEMail_LinkClicked);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.TpInfo);
            this.tabControl.Controls.Add(this.TpUpdates);
            this.tabControl.Controls.Add(this.TpComponents);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // TpInfo
            // 
            this.TpInfo.BackColor = System.Drawing.Color.White;
            this.TpInfo.Controls.Add(this.pictureBox1);
            this.TpInfo.Controls.Add(this.LbVersion);
            this.TpInfo.Controls.Add(this.LbCopyright);
            this.TpInfo.Controls.Add(this.tableLayoutPanel);
            resources.ApplyResources(this.TpInfo, "TpInfo");
            this.TpInfo.Name = "TpInfo";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // TpComponents
            // 
            this.TpComponents.BackColor = System.Drawing.Color.White;
            this.TpComponents.Controls.Add(this.LwComponents);
            resources.ApplyResources(this.TpComponents, "TpComponents");
            this.TpComponents.Name = "TpComponents";
            // 
            // LwComponents
            // 
            this.LwComponents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.software_version,
            this.lizenz,
            this.url});
            resources.ApplyResources(this.LwComponents, "LwComponents");
            this.LwComponents.FullRowSelect = true;
            this.LwComponents.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("LwComponents.Items"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("LwComponents.Items1"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("LwComponents.Items2"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("LwComponents.Items3")))});
            this.LwComponents.Name = "LwComponents";
            this.LwComponents.UseCompatibleStateImageBehavior = false;
            this.LwComponents.View = System.Windows.Forms.View.Details;
            this.LwComponents.DoubleClick += new System.EventHandler(this.LwComponents_DoubleClick);
            // 
            // name
            // 
            resources.ApplyResources(this.name, "name");
            // 
            // software_version
            // 
            resources.ApplyResources(this.software_version, "software_version");
            // 
            // lizenz
            // 
            resources.ApplyResources(this.lizenz, "lizenz");
            // 
            // url
            // 
            resources.ApplyResources(this.url, "url");
            // 
            // TpUpdates
            // 
            resources.ApplyResources(this.TpUpdates, "TpUpdates");
            this.TpUpdates.Name = "TpUpdates";
            this.TpUpdates.UseVisualStyleBackColor = true;
            // 
            // AboutBox
            // 
            this.AcceptButton = this.BtnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.LbNetFramwork);
            this.Controls.Add(this.LbOS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.AboutBox_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.TpInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.TpComponents.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LbOS;
        private System.Windows.Forms.Label LbNetFramwork;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Label LbVersion;
        private System.Windows.Forms.Label LbCopyright;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel LlbProjectWebsite;
        private System.Windows.Forms.LinkLabel LlbProjectEMail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        public  System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage TpInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        public  System.Windows.Forms.TabPage TpComponents;
        public  System.Windows.Forms.ListView LwComponents;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader software_version;
        private System.Windows.Forms.ColumnHeader lizenz;
        private System.Windows.Forms.ColumnHeader url;
        private System.Windows.Forms.LinkLabel LlbProjectLicense;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel LlbDownload;
        private System.Windows.Forms.TabPage TpUpdates;
    }
}