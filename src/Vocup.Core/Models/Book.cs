using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Vocup.IO;

namespace Vocup.Models
{
    public class Book : ReactiveObject
    {
        private string motherTongue;
        private string foreignLanguage;
        private PracticeMode practiceMode;

        private BookSerializer? serializer;
        private string? fileName;
        private string? vhrCode;

        public Book(string motherTongue, string foreignLanguage)
        {
            this.motherTongue = motherTongue;
            this.foreignLanguage = foreignLanguage;
            Words = new ObservableCollection<Word>();
        }

        [JsonConstructor]
        public Book(string motherTongue, string foreignLanguage, List<Word> words)
        {
            this.motherTongue = motherTongue;
            this.foreignLanguage = foreignLanguage;
            Words = new ObservableCollection<Word>(words);
        }

        public string MotherTongue
        {
            get => motherTongue;
            set => this.RaiseAndSetIfChanged(ref motherTongue, value);
        }
        public string ForeignLanguage
        {
            get => foreignLanguage;
            set => this.RaiseAndSetIfChanged(ref foreignLanguage, value);
        }
        public PracticeMode PracticeMode
        {
            get => practiceMode;
            set => this.RaiseAndSetIfChanged(ref practiceMode, value);
        }
        public ObservableCollection<Word> Words { get; }

        [JsonIgnore]
        public BookSerializer? Serializer
        {
            get => serializer;
            set => this.RaiseAndSetIfChanged(ref serializer, value);
        }
        [JsonIgnore]
        public string? FileName
        {
            get => fileName;
            set => this.RaiseAndSetIfChanged(ref fileName, value);
        }
        [JsonIgnore]
        public string? VhrCode
        {
            get => vhrCode;
            set => this.RaiseAndSetIfChanged(ref vhrCode, value);
        }
    }
}
