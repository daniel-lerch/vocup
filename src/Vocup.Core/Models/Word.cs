using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace Vocup.Models;

public class Word : ReactiveObject
{
    private readonly ObservableAsPropertyHelper<string> motherTongueCombined;
    private readonly ObservableAsPropertyHelper<string> foreignLanguageCombined;
    private readonly ObservableAsPropertyHelper<int> motherTonguePracticeState;
    private readonly ObservableAsPropertyHelper<int> foreignLanguagePracticeState;

    public Word() : this(new(), new()) { }

    public Word(IEnumerable<Synonym> motherTongue, IEnumerable<Synonym> foreignLanguage)
        : this(
              new ObservableCollection<Synonym>(motherTongue),
              new ObservableCollection<Synonym>(foreignLanguage)) { }

    private Word(ObservableCollection<Synonym> motherTongue, ObservableCollection<Synonym> foreignLanguage)
    {
        MotherTongue = motherTongue;
        ForeignLanguage = foreignLanguage;

        motherTongueCombined = MotherTongue
            .ToObservableChangeSet()
            .AutoRefresh(synonym => synonym.Value)
            .ToCollection()
            .Select(x => string.Join(", ", x.Select(synonym => synonym.Value)))
            .ToProperty(this, x => x.MotherTongueCombined);

        foreignLanguageCombined = ForeignLanguage
            .ToObservableChangeSet()
            .AutoRefresh(s => s.Value)
            .ToCollection()
            .Select(x => string.Join(", ", x.Select(s => s.Value)))
            .ToProperty(this, x => x.ForeignLanguageCombined);

        motherTonguePracticeState = MotherTongue
            .ToObservableChangeSet()
            .AutoRefresh(synonym => synonym.PracticeState)
            .ToCollection()
            .Select(x => x.Min(synonym => synonym.PracticeState))
            .ToProperty(this, x => x.MotherTonguePracticeState);

        foreignLanguagePracticeState = ForeignLanguage
            .ToObservableChangeSet()
            .AutoRefresh(synonym => synonym.PracticeState)
            .ToCollection()
            .Select(x => x.Min(synonym => synonym.PracticeState))
            .ToProperty(this, x => x.ForeignLanguagePracticeState);
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

    public string MotherTongueCombined => motherTongueCombined.Value;
    public string ForeignLanguageCombined => foreignLanguageCombined.Value;
    public int MotherTonguePracticeState => motherTonguePracticeState.Value;
    public int ForeignLanguagePracticeState => foreignLanguagePracticeState.Value;

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
