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
        private readonly ListViewItem.ListViewSubItem motherTongueColumn;
        private readonly ListViewItem.ListViewSubItem foreignLangColumn;
        private readonly ListViewItem.ListViewSubItem praticeDateColumn;

        public VocabularyWordController(VocabularyWord vocabularyWord)
        {
            ListViewItem = new ListViewItem();
            motherTongueColumn = ListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(ListViewItem, ""));
            foreignLangColumn = ListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(ListViewItem, ""));
            praticeDateColumn = ListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(ListViewItem, ""));

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
