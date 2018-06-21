using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vocup.Models
{
    public class VocabularyWord : INotifyPropertyChanged
    {
        private string _motherTongue;
        private string _foreignLang;
        private string _foreignLangSynonym;
        private PracticeState _practiceState;
        private DateTime _practiceDate;

        public string MotherTongue
        {
            get => _motherTongue;
            set { _motherTongue = value; OnPropertyChanged(); }
        }
        public string ForeignLang
        {
            get => _foreignLang;
            set { _foreignLang = value; OnPropertyChanged(); }
        }
        public string ForeignLangSynonym
        {
            get => _foreignLangSynonym;
            set { _foreignLangSynonym = value; OnPropertyChanged(); }
        }
        public string ForeignLangText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_foreignLangSynonym))
                    return _foreignLang;
                else
                    return _foreignLang + "=" + _foreignLangSynonym;
            }
            set
            {
                int idx = value.LastIndexOf('=');
                if (idx == -1)
                {
                    _foreignLang = value;
                    _foreignLangSynonym = null;
                }
                else
                {
                    _foreignLang = value.Remove(idx);
                    _foreignLangSynonym = value.Substring(idx + 1);
                }
                OnPropertyChanged();
            }
        }

        public PracticeState PracticeState
        {
            get => _practiceState;
            set { _practiceState = value; OnPropertyChanged(); }
        }
        public DateTime PracticeDate
        {
            get => _practiceDate;
            set { _practiceDate = value; OnPropertyChanged(); }
        }

        public VocabularyBook Owner { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}