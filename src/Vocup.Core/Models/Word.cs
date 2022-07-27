using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Vocup.Models;

public class Word : ReactiveObject
{
    private DateTimeOffset creationDate;

    public Word(IEnumerable<Synonym> motherTongue, IEnumerable<Synonym> foreignLanguage)
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

    public override bool Equals(object? obj)
    {
        return obj is Word word &&
               creationDate.Equals(word.creationDate) &&
               Enumerable.SequenceEqual(MotherTongue, word.MotherTongue) &&
               Enumerable.SequenceEqual(ForeignLanguage, word.ForeignLanguage);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(creationDate, MotherTongue, ForeignLanguage);
    }
}
