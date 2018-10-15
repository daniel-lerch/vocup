using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Vocup.Util;

namespace Vocup.Models
{
    public class VocabularyBook : INotifyPropertyChanged
    {
        private string _filePath;
        private string _vhrCode;
        private string _motherTongue;
        private string _foreignLang;
        private PracticeMode _practiceMode = PracticeMode.AskForForeignLang;
        private bool _unsafedChanges;

        public VocabularyBook()
        {
            Words = new ReactiveCollection<VocabularyWord>();
            Words.OnAdd(x => x.Owner = this);
            Words.OnRemove(x => x.Owner = null);
            Words.CollectionChanged += OnCollectionChanged;
            Statistics = new VocabularyBookStatistics(this);
        }

        public string FilePath
        {
            get => _filePath;
            set { if (_filePath != value) { _filePath = value; OnPropertyChanged(); } }
        }
        public string VhrCode
        {
            get => _vhrCode;
            set { if (_vhrCode != value) { _vhrCode = value; OnPropertyChanged(); } }
        }
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
        public PracticeMode PracticeMode
        {
            get => _practiceMode;
            set { if (_practiceMode != value) { _practiceMode = value; OnPropertyChanged(); } }
        }
        public bool UnsavedChanges
        {
            get => _unsafedChanges;
            set { if (_unsafedChanges != value) { _unsafedChanges = value; OnPropertyChanged(); } }
        }
        public string Name => string.IsNullOrWhiteSpace(_filePath) ? null : Path.GetFileNameWithoutExtension(_filePath);
        public ReactiveCollection<VocabularyWord> Words { get; }
        public VocabularyBookStatistics Statistics { get; }
        public bool Notifies { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (Notifies)
            {
                if (name != nameof(UnsavedChanges))
                    UnsavedChanges = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Notifies)
            {
                UnsavedChanges = true;
                CollectionChanged?.Invoke(sender, e);
            }
        }

        /// <summary>
        /// Activates raising events by setting <see cref="Notifies"/> to true.
        /// </summary>
        public void Notify()
        {
            Statistics.Refresh();
            Notifies = true;
        }

        public void GenerateVhrCode()
        {
            int number1 = '0', number2 = '9';
            int bigLetter1 = 'A', bigLetter2 = 'Z';
            int smallLetter1 = 'a', smallLetter2 = 'z';

            Random random = new Random(); // No need for RNGCryptoServiceProvider here because this is not security critical.
            char[] code = new char[24];

            do
            {
                int i = 0;
                while (i < code.Length)
                {
                    int character = random.Next(number1, smallLetter2);
                    if ((character >= number1 && character <= number2) ||
                        (character >= bigLetter1 && character <= bigLetter2) ||
                        (character >= smallLetter1 && character <= smallLetter2))
                    {
                        code[i] = (char)character;
                        i++;
                    }
                }

            } while (File.Exists(Path.Combine(Properties.Settings.Default.VhrPath, code + ".vhr")));

            VhrCode = new string(code);
        }
    }
}
