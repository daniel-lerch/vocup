using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Vocup.Models;

public class Word : ReactiveObject
{
    private DateTimeOffset creationDate;

    [JsonConstructor]
    public Word(ObservableCollection<Synonym> motherTongue, ObservableCollection<Synonym> foreignLanguage)
    {
        MotherTongue = motherTongue;
        ForeignLanguage = foreignLanguage;
    }

    public ObservableCollection<Synonym> MotherTongue { get; }
    public ObservableCollection<Synonym> ForeignLanguage { get; }
    public DateTimeOffset CreationDate
    {
        get => creationDate;
        set => this.RaiseAndSetIfChanged(ref creationDate, value);
    }
}
