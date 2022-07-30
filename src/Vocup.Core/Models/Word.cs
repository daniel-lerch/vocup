using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Vocup.Models;

public class Word : ReactiveObject
{
    public Word(IEnumerable<Synonym> motherTongue, IEnumerable<Synonym> foreignLanguage)
    {
        MotherTongue = new ObservableCollection<Synonym>(motherTongue);
        ForeignLanguage = new ObservableCollection<Synonym>(foreignLanguage);
    }

    public ObservableCollection<Synonym> MotherTongue { get; }
    public ObservableCollection<Synonym> ForeignLanguage { get; }
    [Reactive] public DateTimeOffset CreationDate { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Word word &&
               CreationDate.Equals(word.CreationDate) &&
               Enumerable.SequenceEqual(MotherTongue, word.MotherTongue) &&
               Enumerable.SequenceEqual(ForeignLanguage, word.ForeignLanguage);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(CreationDate, MotherTongue, ForeignLanguage);
    }
}
