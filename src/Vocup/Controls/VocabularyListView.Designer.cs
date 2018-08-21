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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VocabularyListView));
            this.MainListView = new System.Windows.Forms.ListView();
            this.imageColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.motherTongueColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.foreignLangColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastPracticedColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IconImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // MainListView
            // 
            this.MainListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.imageColumn,
            this.motherTongueColumn,
            this.foreignLangColumn,
            this.lastPracticedColumn});
            this.MainListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainListView.FullRowSelect = true;
            this.MainListView.Location = new System.Drawing.Point(0, 0);
            this.MainListView.MultiSelect = false;
            this.MainListView.Name = "MainListView";
            this.MainListView.Size = new System.Drawing.Size(497, 487);
            this.MainListView.TabIndex = 0;
            this.MainListView.UseCompatibleStateImageBehavior = false;
            this.MainListView.View = System.Windows.Forms.View.Details;
            this.MainListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.MainListView_ColumnClick);
            this.MainListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.MainListView_ColumnWidthChanging);
            // 
            // imageColumn
            // 
            this.imageColumn.Text = "";
            this.imageColumn.Width = 20;
            // 
            // motherTongueColumn
            // 
            this.motherTongueColumn.Text = "Muttersprache";
            this.motherTongueColumn.Width = 150;
            // 
            // foreignLangColumn
            // 
            this.foreignLangColumn.Text = "Fremdsprache";
            this.foreignLangColumn.Width = 150;
            // 
            // lastPracticedColumn
            // 
            this.lastPracticedColumn.Text = "Zuletzt geübt";
            this.lastPracticedColumn.Width = 100;
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconImageList.ImageStream")));
            this.IconImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.IconImageList.Images.SetKeyName(0, "0.png");
            this.IconImageList.Images.SetKeyName(1, "1.png");
            this.IconImageList.Images.SetKeyName(2, "2.png");
            this.IconImageList.Images.SetKeyName(3, "3.png");
            // 
            // VocabularyListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainListView);
            this.Name = "VocabularyListView";
            this.Size = new System.Drawing.Size(497, 487);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView MainListView;
        private System.Windows.Forms.ColumnHeader imageColumn;
        private System.Windows.Forms.ColumnHeader motherTongueColumn;
        private System.Windows.Forms.ColumnHeader foreignLangColumn;
        private System.Windows.Forms.ColumnHeader lastPracticedColumn;
        private System.Windows.Forms.ImageList IconImageList;
    }
}
