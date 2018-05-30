using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vocup.Models
{
    public class VocabularyBook : INotifyPropertyChanged
    {
        private string _fileVersion;
        private string _vhrCode;
        private string _motherTongue;
        private string _foreignLang;

        public VocabularyBook()
        {
            var words = new ObservableCollection<VocabularyWord>();
            words.CollectionChanged += OnCollectionChanged;
            Words = words;
        }

        public string FileVersion
        {
            get => _fileVersion;
            set { _fileVersion = value; OnPropertyChanged(); }
        }
        public string VhrCode
        {
            get => _vhrCode;
            set { _vhrCode = value; OnPropertyChanged(); }
        }
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
        IList<VocabularyWord> Words { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(sender, e);
        }

        public void ReadFile(string path)
        {

        }

        public void WriteFile(string path)
        {

        }
    }
}
