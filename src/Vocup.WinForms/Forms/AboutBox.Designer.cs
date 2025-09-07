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
            AvaloniaControlHost = new Avalonia.Win32.Interoperability.WinFormsAvaloniaControlHost();
            SuspendLayout();
            // 
            // BtnOK
            // 
            resources.ApplyResources(BtnOK, "BtnOK");
            BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            BtnOK.Name = "BtnOK";
            BtnOK.UseVisualStyleBackColor = true;
            // 
            // AvaloniaControlHost
            // 
            resources.ApplyResources(AvaloniaControlHost, "AvaloniaControlHost");
            AvaloniaControlHost.Content = null;
            AvaloniaControlHost.Name = "AvaloniaControlHost";
            // 
            // AboutBox
            // 
            AcceptButton = BtnOK;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(AvaloniaControlHost);
            Controls.Add(BtnOK);
            MinimizeBox = false;
            Name = "AboutBox";
            ShowInTaskbar = false;
            Load += AboutBox_Load;
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button BtnOK;
        private Avalonia.Win32.Interoperability.WinFormsAvaloniaControlHost AvaloniaControlHost;
    }
}