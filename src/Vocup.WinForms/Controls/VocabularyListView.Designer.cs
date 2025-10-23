namespace Vocup.Controls
{
    partial class VocabularyListView
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
            if (disposing && (components != null))
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VocabularyListView));
            MainListView = new System.Windows.Forms.ListView();
            imageColumn = new System.Windows.Forms.ColumnHeader();
            motherTongueColumn = new System.Windows.Forms.ColumnHeader();
            foreignLangColumn = new System.Windows.Forms.ColumnHeader();
            lastPracticedColumn = new System.Windows.Forms.ColumnHeader();
            creationTimeColumn = new System.Windows.Forms.ColumnHeader();
            IconImageList = new System.Windows.Forms.ImageList(components);
            SuspendLayout();
            // 
            // MainListView
            // 
            MainListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { imageColumn, motherTongueColumn, foreignLangColumn, lastPracticedColumn, creationTimeColumn });
            resources.ApplyResources(MainListView, "MainListView");
            MainListView.FullRowSelect = true;
            MainListView.GridLines = true;
            MainListView.MultiSelect = false;
            MainListView.Name = "MainListView";
            MainListView.UseCompatibleStateImageBehavior = false;
            MainListView.View = System.Windows.Forms.View.Details;
            MainListView.ColumnClick += MainListView_ColumnClick;
            MainListView.ColumnWidthChanging += MainListView_ColumnWidthChanging;
            MainListView.Resize += MainListView_Resize;
            // 
            // imageColumn
            // 
            resources.ApplyResources(imageColumn, "imageColumn");
            // 
            // motherTongueColumn
            // 
            resources.ApplyResources(motherTongueColumn, "motherTongueColumn");
            // 
            // foreignLangColumn
            // 
            resources.ApplyResources(foreignLangColumn, "foreignLangColumn");
            // 
            // lastPracticedColumn
            // 
            resources.ApplyResources(lastPracticedColumn, "lastPracticedColumn");
            // 
            // creationTimeColumn
            // 
            resources.ApplyResources(creationTimeColumn, "creationTimeColumn");
            // 
            // IconImageList
            // 
            IconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            IconImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("IconImageList.ImageStream");
            IconImageList.TransparentColor = System.Drawing.Color.Transparent;
            IconImageList.Images.SetKeyName(0, "0.png");
            IconImageList.Images.SetKeyName(1, "1.png");
            IconImageList.Images.SetKeyName(2, "2.png");
            IconImageList.Images.SetKeyName(3, "3.png");
            // 
            // VocabularyListView
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(MainListView);
            Name = "VocabularyListView";
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView MainListView;
        private System.Windows.Forms.ColumnHeader imageColumn;
        private System.Windows.Forms.ColumnHeader motherTongueColumn;
        private System.Windows.Forms.ColumnHeader foreignLangColumn;
        private System.Windows.Forms.ColumnHeader lastPracticedColumn;
        private System.Windows.Forms.ImageList IconImageList;
        private System.Windows.Forms.ColumnHeader creationTimeColumn;
    }
}
