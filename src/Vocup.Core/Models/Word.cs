using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Vocup.Models;

public class Word : ReactiveObject
{
    public Word()
    {
        MotherTongue = new ObservableCollection<Synonym>();
        ForeignLanguage = new ObservableCollection<Synonym>();
    }

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

    [Obsolete] public string MotherTongueText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    [Obsolete] public string ForeignLangText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    [Obsolete] public string? ForeignLangSynonym { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    [Obsolete] public string ForeignLangCombined => throw new NotImplementedException();
    [Obsolete] public int PracticeStateNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    [Obsolete] public PracticeState PracticeState => throw new NotImplementedException();
    [Obsolete] public DateTime PracticeDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    [Obsolete]
    public Word Clone(bool copyResults)
    {
        Synonym cloneSynonym(Synonym synonym)
        {
            IEnumerable<Practice> practices;
            if (copyResults)
                practices = synonym.Practices.Select(x => new Practice { Date = x.Date, Result = x.Result });
            else
                practices = Enumerable.Empty<Practice>();

            return new Synonym(synonym.Value, new ObservableCollection<string>(synonym.Flags), practices);
        }

        return new Word(MotherTongue.Select(cloneSynonym), ForeignLanguage.Select(cloneSynonym))
        {
            CreationDate = CreationDate
        };
    }
}
