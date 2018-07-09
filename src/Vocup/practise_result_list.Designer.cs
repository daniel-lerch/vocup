namespace Vocup
{
    partial class practise_result_list
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(practise_result_list));
            this.listView = new System.Windows.Forms.ListView();
            this.listview_imagelist = new System.Windows.Forms.ImageList(this.components);
            this.log_button = new System.Windows.Forms.Button();
            this.ok_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.anzahl_richtig = new System.Windows.Forms.TextBox();
            this.anzahl_teilweise_richtig = new System.Windows.Forms.TextBox();
            this.anzahl_nicht = new System.Windows.Forms.TextBox();
            this.anzahl_falsch = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.note_label = new System.Windows.Forms.Label();
            this.prozent_label = new System.Windows.Forms.Label();
            this.prozent = new System.Windows.Forms.TextBox();
            this.note = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            resources.ApplyResources(this.listView, "listView");
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.SmallImageList = this.listview_imagelist;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listView_ColumnWidthChanging);
            // 
            // listview_imagelist
            // 
            this.listview_imagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("listview_imagelist.ImageStream")));
            this.listview_imagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.listview_imagelist.Images.SetKeyName(0, "0.png");
            this.listview_imagelist.Images.SetKeyName(1, "1.png");
            this.listview_imagelist.Images.SetKeyName(2, "2.png");
            this.listview_imagelist.Images.SetKeyName(3, "3.png");
            // 
            // log_button
            // 
            resources.ApplyResources(this.log_button, "log_button");
            this.log_button.Name = "log_button";
            this.log_button.UseVisualStyleBackColor = true;
            // 
            // ok_button
            // 
            resources.ApplyResources(this.ok_button, "ok_button");
            this.ok_button.Name = "ok_button";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // checkBox
            // 
            resources.ApplyResources(this.checkBox, "checkBox");
            this.checkBox.Name = "checkBox";
            this.checkBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.anzahl_richtig);
            this.groupBox1.Controls.Add(this.anzahl_teilweise_richtig);
            this.groupBox1.Controls.Add(this.anzahl_nicht);
            this.groupBox1.Controls.Add(this.anzahl_falsch);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox4);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pictureBox3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // anzahl_richtig
            // 
            resources.ApplyResources(this.anzahl_richtig, "anzahl_richtig");
            this.anzahl_richtig.Name = "anzahl_richtig";
            this.anzahl_richtig.ReadOnly = true;
            // 
            // anzahl_teilweise_richtig
            // 
            resources.ApplyResources(this.anzahl_teilweise_richtig, "anzahl_teilweise_richtig");
            this.anzahl_teilweise_richtig.Name = "anzahl_teilweise_richtig";
            this.anzahl_teilweise_richtig.ReadOnly = true;
            // 
            // anzahl_nicht
            // 
            resources.ApplyResources(this.anzahl_nicht, "anzahl_nicht");
            this.anzahl_nicht.Name = "anzahl_nicht";
            this.anzahl_nicht.ReadOnly = true;
            // 
            // anzahl_falsch
            // 
            resources.ApplyResources(this.anzahl_falsch, "anzahl_falsch");
            this.anzahl_falsch.Name = "anzahl_falsch";
            this.anzahl_falsch.ReadOnly = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Vocup.Properties.Icons.falsch_geübt;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Vocup.Properties.Icons.übung_abgeschlossen;
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Vocup.Properties.Icons.noch_nicht_geübt;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Vocup.Properties.Icons.richtig_geübt;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.note_label);
            this.groupBox2.Controls.Add(this.prozent_label);
            this.groupBox2.Controls.Add(this.prozent);
            this.groupBox2.Controls.Add(this.note);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // note_label
            // 
            resources.ApplyResources(this.note_label, "note_label");
            this.note_label.Name = "note_label";
            // 
            // prozent_label
            // 
            resources.ApplyResources(this.prozent_label, "prozent_label");
            this.prozent_label.Name = "prozent_label";
            // 
            // prozent
            // 
            resources.ApplyResources(this.prozent, "prozent");
            this.prozent.Name = "prozent";
            this.prozent.ReadOnly = true;
            // 
            // note
            // 
            resources.ApplyResources(this.note, "note");
            this.note.Name = "note";
            this.note.ReadOnly = true;
            // 
            // practise_result_list
            // 
            this.AcceptButton = this.ok_button;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.log_button);
            this.Controls.Add(this.listView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "practise_result_list";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.practise_result_list_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button log_button;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.ImageList listview_imagelist;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public  System.Windows.Forms.CheckBox checkBox;
        public  System.Windows.Forms.GroupBox groupBox1;
        public  System.Windows.Forms.TextBox anzahl_richtig;
        public  System.Windows.Forms.TextBox anzahl_teilweise_richtig;
        public  System.Windows.Forms.TextBox anzahl_nicht;
        public  System.Windows.Forms.TextBox anzahl_falsch;
        private System.Windows.Forms.GroupBox groupBox2;
        public  System.Windows.Forms.TextBox note;
        public  System.Windows.Forms.Label prozent_label;
        public  System.Windows.Forms.TextBox prozent;
        public  System.Windows.Forms.Label note_label;

    }
}