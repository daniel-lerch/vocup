namespace Vocup.Controls
{
    partial class FileTreeView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileTreeView));
            this.IconImageList = new System.Windows.Forms.ImageList(this.components);
            this.MainTreeView = new System.Windows.Forms.TreeView();
            this.MainWatcher = new System.IO.FileSystemWatcher();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MainWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconImageList.ImageStream")));
            this.IconImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.IconImageList.Images.SetKeyName(0, "folder.png");
            this.IconImageList.Images.SetKeyName(1, "open.png");
            this.IconImageList.Images.SetKeyName(2, "file.png");
            // 
            // MainTreeView
            // 
            resources.ApplyResources(this.MainTreeView, "MainTreeView");
            this.MainTreeView.Name = "MainTreeView";
            this.MainTreeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.MainTreeView_AfterCollapse);
            this.MainTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.MainTreeView_BeforeExpand);
            this.MainTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.MainTreeView_AfterSelect);
            // 
            // MainWatcher
            // 
            this.MainWatcher.EnableRaisingEvents = true;
            this.MainWatcher.IncludeSubdirectories = true;
            this.MainWatcher.NotifyFilter = ((System.IO.NotifyFilters)((System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.DirectoryName)));
            this.MainWatcher.SynchronizingObject = this;
            this.MainWatcher.Created += new System.IO.FileSystemEventHandler(this.MainWatcher_Created);
            this.MainWatcher.Deleted += new System.IO.FileSystemEventHandler(this.MainWatcher_Deleted);
            this.MainWatcher.Renamed += new System.IO.RenamedEventHandler(this.MainWatcher_Renamed);
            // 
            // BrowseButton
            // 
            this.BrowseButton.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.BrowseButton, "BrowseButton");
            this.BrowseButton.Name = "BrowseButton";
            this.ToolTip.SetToolTip(this.BrowseButton, resources.GetString("BrowseButton.ToolTip"));
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // FileTreeView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.MainTreeView);
            this.Name = "FileTreeView";
            ((System.ComponentModel.ISupportInitialize)(this.MainWatcher)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList IconImageList;
        private System.Windows.Forms.TreeView MainTreeView;
        private System.IO.FileSystemWatcher MainWatcher;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}
