using System.Drawing;
using System.Windows.Forms;
using Vocup.Models.Legacy;

namespace Vocup.Models
{
    public class VocabularyWordController
    {
        private readonly ListViewItem.ListViewSubItem motherTongueColumn;
        private readonly ListViewItem.ListViewSubItem foreignLangColumn;
        private readonly ListViewItem.ListViewSubItem praticeDateColumn;

        public VocabularyWordController(IVocabularyWord vocabularyWord)
        {
            ListViewItem = new ListViewItem { Tag = vocabularyWord, UseItemStyleForSubItems = false };
            motherTongueColumn = ListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(ListViewItem, ""));
            foreignLangColumn = ListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(ListViewItem, ""));
            praticeDateColumn = ListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(ListViewItem, ""));

            VocabularyWord = vocabularyWord;
            VocabularyWord.PropertyChanged += (a0, a1) => UpdateUI();
            UpdateUI();
        }

        public ListViewItem ListViewItem { get; }
        public IVocabularyWord VocabularyWord { get; }

        public void Highlight(string upperCaseQuery)
        {
            motherTongueColumn.BackColor = !string.IsNullOrEmpty(upperCaseQuery)
                && VocabularyWord.MotherTongueText.ToUpper().Contains(upperCaseQuery)
                ? Color.LightGreen
                : default;

            foreignLangColumn.BackColor = !string.IsNullOrEmpty(upperCaseQuery)
                && (VocabularyWord.ForeignLangText.ToUpper().Contains(upperCaseQuery) || (VocabularyWord.ForeignLangSynonym?.ToUpper().Contains(upperCaseQuery) ?? false))
                ? Color.LightGreen
                : default;
        }

        private void UpdateUI()
        {
            ListViewItem.ImageIndex = (int)VocabularyWord.PracticeState;

            motherTongueColumn.Text = VocabularyWord.MotherTongueText;
            foreignLangColumn.Text = VocabularyWord.ForeignLangCombined;
            if (VocabularyWord.PracticeDate == default)
                praticeDateColumn.Text = "";
            else
                praticeDateColumn.Text = $"{VocabularyWord.PracticeDate.ToShortDateString()} {VocabularyWord.PracticeDate.ToShortTimeString()}";
        }
    }
}
