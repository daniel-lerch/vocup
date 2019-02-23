namespace Vocup.Forms
{
    partial class CreateBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateBackup));
            this.GroupVocabularyBooks = new System.Windows.Forms.GroupBox();
            this.LbSaveSpecificBooks = new System.Windows.Forms.Label();
            this.CbSaveAllBooks = new System.Windows.Forms.CheckBox();
            this.BtnAddVocabularyBook = new System.Windows.Forms.Button();
            this.BtnDeleteVocabularyBook = new System.Windows.Forms.Button();
            this.ListVocabularyBooks = new System.Windows.Forms.ListBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnCreateBackup = new System.Windows.Forms.Button();
            this.GroupSpecialChar = new System.Windows.Forms.GroupBox();
            this.ListSpecialChars = new System.Windows.Forms.CheckedListBox();
            this.CbSaveResults = new System.Windows.Forms.CheckBox();
            this.GroupPracticeResults = new System.Windows.Forms.GroupBox();
            this.GroupVocabularyBooks.SuspendLayout();
            this.GroupSpecialChar.SuspendLayout();
            this.GroupPracticeResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupVocabularyBooks
            // 
            resources.ApplyResources(this.GroupVocabularyBooks, "GroupVocabularyBooks");
            this.GroupVocabularyBooks.Controls.Add(this.LbSaveSpecificBooks);
            this.GroupVocabularyBooks.Controls.Add(this.CbSaveAllBooks);
            this.GroupVocabularyBooks.Controls.Add(this.BtnAddVocabularyBook);
            this.GroupVocabularyBooks.Controls.Add(this.BtnDeleteVocabularyBook);
            this.GroupVocabularyBooks.Controls.Add(this.ListVocabularyBooks);
            this.GroupVocabularyBooks.Name = "GroupVocabularyBooks";
            this.GroupVocabularyBooks.TabStop = false;
            // 
            // LbSaveSpecificBooks
            // 
            resources.ApplyResources(this.LbSaveSpecificBooks, "LbSaveSpecificBooks");
            this.LbSaveSpecificBooks.Name = "LbSaveSpecificBooks";
            // 
            // CbSaveAllBooks
            // 
            resources.ApplyResources(this.CbSaveAllBooks, "CbSaveAllBooks");
            this.CbSaveAllBooks.Name = "CbSaveAllBooks";
            this.CbSaveAllBooks.UseVisualStyleBackColor = true;
            this.CbSaveAllBooks.CheckedChanged += new System.EventHandler(this.CbSaveAllBooks_CheckedChanged);
            // 
            // BtnAddVocabularyBook
            // 
            resources.ApplyResources(this.BtnAddVocabularyBook, "BtnAddVocabularyBook");
            this.BtnAddVocabularyBook.Name = "BtnAddVocabularyBook";
            this.BtnAddVocabularyBook.UseVisualStyleBackColor = true;
            this.BtnAddVocabularyBook.Click += new System.EventHandler(this.BtnAddVocabularyBook_Click);
            // 
            // BtnDeleteVocabularyBook
            // 
            resources.ApplyResources(this.BtnDeleteVocabularyBook, "BtnDeleteVocabularyBook");
            this.BtnDeleteVocabularyBook.Name = "BtnDeleteVocabularyBook";
            this.BtnDeleteVocabularyBook.UseVisualStyleBackColor = true;
            this.BtnDeleteVocabularyBook.Click += new System.EventHandler(this.BtnDeleteVocabularyBook_Click);
            // 
            // ListVocabularyBooks
            // 
            resources.ApplyResources(this.ListVocabularyBooks, "ListVocabularyBooks");
            this.ListVocabularyBooks.FormattingEnabled = true;
            this.ListVocabularyBooks.Name = "ListVocabularyBooks";
            this.ListVocabularyBooks.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ListVocabularyBooks.SelectedIndexChanged += new System.EventHandler(this.ListVocabularyBooks_SelectedIndexChanged);
            // 
            // BtnCancel
            // 
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // BtnCreateBackup
            // 
            resources.ApplyResources(this.BtnCreateBackup, "BtnCreateBackup");
            this.BtnCreateBackup.Name = "BtnCreateBackup";
            this.BtnCreateBackup.UseVisualStyleBackColor = true;
            this.BtnCreateBackup.Click += new System.EventHandler(this.BtnCreateBackup_Click);
            // 
            // GroupSpecialChar
            // 
            resources.ApplyResources(this.GroupSpecialChar, "GroupSpecialChar");
            this.GroupSpecialChar.Controls.Add(this.ListSpecialChars);
            this.GroupSpecialChar.Name = "GroupSpecialChar";
            this.GroupSpecialChar.TabStop = false;
            // 
            // ListSpecialChars
            // 
            resources.ApplyResources(this.ListSpecialChars, "ListSpecialChars");
            this.ListSpecialChars.CheckOnClick = true;
            this.ListSpecialChars.FormattingEnabled = true;
            this.ListSpecialChars.Name = "ListSpecialChars";
            this.ListSpecialChars.SelectedValueChanged += new System.EventHandler(this.ListSpecialChars_SelectedValueChanged);
            // 
            // CbSaveResults
            // 
            resources.ApplyResources(this.CbSaveResults, "CbSaveResults");
            this.CbSaveResults.Name = "CbSaveResults";
            this.CbSaveResults.UseVisualStyleBackColor = true;
            // 
            // GroupPracticeResults
            // 
            this.GroupPracticeResults.Controls.Add(this.CbSaveResults);
            resources.ApplyResources(this.GroupPracticeResults, "GroupPracticeResults");
            this.GroupPracticeResults.Name = "GroupPracticeResults";
            this.GroupPracticeResults.TabStop = false;
            // 
            // CreateBackup
            // 
            this.AcceptButton = this.BtnCreateBackup;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.Controls.Add(this.GroupPracticeResults);
            this.Controls.Add(this.GroupSpecialChar);
            this.Controls.Add(this.BtnCreateBackup);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.GroupVocabularyBooks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateBackup";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Form_Load);
            this.GroupVocabularyBooks.ResumeLayout(false);
            this.GroupVocabularyBooks.PerformLayout();
            this.GroupSpecialChar.ResumeLayout(false);
            this.GroupPracticeResults.ResumeLayout(false);
            this.GroupPracticeResults.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupVocabularyBooks;
        private System.Windows.Forms.Button BtnAddVocabularyBook;
        private System.Windows.Forms.Button BtnDeleteVocabularyBook;
        public System.Windows.Forms.ListBox ListVocabularyBooks;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnCreateBackup;
        public System.Windows.Forms.CheckBox CbSaveAllBooks;
        private System.Windows.Forms.GroupBox GroupSpecialChar;
        public System.Windows.Forms.CheckedListBox ListSpecialChars;
        private System.Windows.Forms.Label LbSaveSpecificBooks;
        private System.Windows.Forms.CheckBox CbSaveResults;
        private System.Windows.Forms.GroupBox GroupPracticeResults;
    }
}