using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vocup.Models
{
    public class VocabularyWord
    {
        private string _motherTongue;
        private string _foreignLang;
        private string _foreignLangSynonym;
        private PracticeState _practiceState;
        private DateTime _practiceDate;
        private readonly ListViewItem.ListViewSubItem imageColumn;
        private readonly ListViewItem.ListViewSubItem motherTongueColumn;
        private readonly ListViewItem.ListViewSubItem foreignLangColumn;
        private readonly ListViewItem.ListViewSubItem praticeDateColumn;

        public VocabularyWord()
        {
            ListViewItem = new ListViewItem();
            imageColumn = new ListViewItem.ListViewSubItem(ListViewItem, "");
            motherTongueColumn = new ListViewItem.ListViewSubItem(ListViewItem, "");
            foreignLangColumn = new ListViewItem.ListViewSubItem(ListViewItem, "");
            praticeDateColumn = new ListViewItem.ListViewSubItem(ListViewItem, "");
            ListViewItem.SubItems.AddRange(new[] { imageColumn, motherTongueColumn, foreignLangColumn, praticeDateColumn });
        }

        public string MotherTongue
        {
            get => _motherTongue;
            set
            {
                _motherTongue = value;
                motherTongueColumn.Text = value;
            }
        }
        public string ForeignLang
        {
            get => _foreignLang;
            set
            {
                _foreignLang = value;
                ReloadForeignLang();
            }
        }
        public string ForeignLangSynonym
        {
            get => _foreignLangSynonym;
            set
            {
                _foreignLangSynonym = value;
                ReloadForeignLang();
            }
        }
        public PracticeState PracticeState
        {
            get => _practiceState;
            set
            {
                _practiceState = value;
                ListViewItem.ImageIndex = (int)value;
            }
        }
        public DateTime PracticeDate
        {
            get => _practiceDate;
            set
            {
                _practiceDate = value;
                praticeDateColumn.Text = (value == default(DateTime)) ? "" : value.ToString("dd.MM.yyyy HH:mm");
            }
        }
        public ListViewItem ListViewItem { get; }
        public VocabularyBook Owner { get; set; }

        private void ReloadForeignLang()
        {
            if (string.IsNullOrWhiteSpace(_foreignLangSynonym))
                foreignLangColumn.Text = _foreignLang;
            else
                foreignLangColumn.Text = $"{_foreignLang}={_foreignLangSynonym}";
        }
    }
}
