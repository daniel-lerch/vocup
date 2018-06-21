using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vocup.Models
{
    public class VocabularyWordController
    {
        private readonly ListViewItem.ListViewSubItem imageColumn;
        private readonly ListViewItem.ListViewSubItem motherTongueColumn;
        private readonly ListViewItem.ListViewSubItem foreignLangColumn;
        private readonly ListViewItem.ListViewSubItem praticeDateColumn;

        public VocabularyWordController(VocabularyWord vocabularyWord)
        {
            ListViewItem = new ListViewItem();
            imageColumn = new ListViewItem.ListViewSubItem(ListViewItem, "");
            motherTongueColumn = new ListViewItem.ListViewSubItem(ListViewItem, "");
            foreignLangColumn = new ListViewItem.ListViewSubItem(ListViewItem, "");
            praticeDateColumn = new ListViewItem.ListViewSubItem(ListViewItem, "");
            ListViewItem.SubItems.AddRange(new[] { imageColumn, motherTongueColumn, foreignLangColumn, praticeDateColumn });
            VocabularyWord = vocabularyWord;
            VocabularyWord.PropertyChanged += (a0, a1) => UpdateUI();
            UpdateUI();
        }

        public ListViewItem ListViewItem { get; }
        public VocabularyWord VocabularyWord { get; }

        private void UpdateUI()
        {
            ListViewItem.ImageIndex = (int)VocabularyWord.PracticeState;

            motherTongueColumn.Text = VocabularyWord.MotherTongue;
            foreignLangColumn.Text = VocabularyWord.ForeignLangText;
            praticeDateColumn.Text = (VocabularyWord.PracticeDate == default(DateTime)) ? "" : VocabularyWord.PracticeDate.ToString("dd.MM.yyyy HH:mm");
        }
    }
}
