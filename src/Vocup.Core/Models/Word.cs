using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Vocup.Models
{
    public class Word : ReactiveObject
    {
        private DateTimeOffset creationDate;

        [JsonConstructor]
        public Word(List<Synonym> motherTongue, List<Synonym> foreignLanguage)
        {
            MotherTongue = new ObservableCollection<Synonym>(motherTongue);
            ForeignLanguage = new ObservableCollection<Synonym>(foreignLanguage);
        }

        public ObservableCollection<Synonym> MotherTongue { get; }
        public ObservableCollection<Synonym> ForeignLanguage { get; }
        public DateTimeOffset CreationDate
        {
            get => creationDate;
            set => this.RaiseAndSetIfChanged(ref creationDate, value);
        }
    }
}
