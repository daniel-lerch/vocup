using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Vocup.Models
{
    public class VocabularyWord : INotifyPropertyChanged
    {
        private string _motherTongue;
        private string _foreignLang;
        private string _foreignLangSynonym;
        private PracticeState _practiceState;
        private int _practiceStateNumber;
        private DateTime _practiceDate;

        public string MotherTongue
        {
            get => _motherTongue;
            set { if (_motherTongue != value) { _motherTongue = value; OnPropertyChanged(); } }
        }
        public string ForeignLang
        {
            get => _foreignLang;
            set { if (_foreignLang != value) { _foreignLang = value; OnPropertyChanged(); } }
        }
        public string ForeignLangSynonym
        {
            get => _foreignLangSynonym;
            set { if (_foreignLangSynonym != value) { _foreignLangSynonym = value; OnPropertyChanged(); } }
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
                    ForeignLang = value;
                    ForeignLangSynonym = null;
                }
                else
                {
                    ForeignLang = value.Remove(idx);
                    ForeignLangSynonym = value.Substring(idx + 1);
                }
            }
        }

        public PracticeState PracticeState
        {
            get => _practiceState;
            private set { if (_practiceState != value) { _practiceState = value; OnPropertyChanged(); } }
        }

        public int PracticeStateNumber
        {
            get => _practiceStateNumber;
            set
            {
                if (_practiceStateNumber != value)
                {
                    _practiceStateNumber = value; OnPropertyChanged();
                    PracticeState = PracticeStateHelper.Parse(_practiceStateNumber);
                }
            }
        }
        public DateTime PracticeDate
        {
            get => _practiceDate;
            set { if (_practiceDate != value) { _practiceDate = value; OnPropertyChanged(); } }
        }

        public VocabularyBook Owner { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (Owner != null) Owner.UnsavedChanges = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void RenewPracticeState()
        {
            PracticeState = PracticeStateHelper.Parse(_practiceStateNumber);
        }

        public VocabularyWord Clone(bool copyResults)
        {
            if (copyResults)
                return new VocabularyWord()
                {
                    _motherTongue = _motherTongue,
                    _foreignLang = _foreignLang,
                    _foreignLangSynonym = _foreignLangSynonym,
                    _practiceState = _practiceState,
                    _practiceStateNumber = _practiceStateNumber,
                    _practiceDate = _practiceDate
                };
            else
                return new VocabularyWord()
                {
                    _motherTongue = _motherTongue,
                    _foreignLang = _foreignLang,
                    _foreignLangSynonym = _foreignLangSynonym
                };
        }
    }
}