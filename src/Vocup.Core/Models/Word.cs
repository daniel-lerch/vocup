using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Vocup.Settings;

namespace Vocup.Models;

public class Word : ReactiveObject
{
    private readonly Book book;
    private readonly IVocupSettings settings;
    private readonly Lazy<WordPracticeState> practiceState;
    private readonly ObservableAsPropertyHelper<string> motherTongueCombined;
    private readonly ObservableAsPropertyHelper<string> foreignLanguageCombined;

    public Word(Book book, IVocupSettings settings) : this(new(), new(), book, settings) { }

    public Word(IEnumerable<Synonym> motherTongue, IEnumerable<Synonym> foreignLanguage, Book book, IVocupSettings settings)
        : this(
              new ObservableCollection<Synonym>(motherTongue),
              new ObservableCollection<Synonym>(foreignLanguage),
              book,
              settings) { }

    private Word(ObservableCollection<Synonym> motherTongue, ObservableCollection<Synonym> foreignLanguage, Book book, IVocupSettings settings)
    {
        this.book = book;
        this.settings = settings;
        MotherTongue = motherTongue;
        ForeignLanguage = foreignLanguage;

        practiceState = new(() => new(this, book), LazyThreadSafetyMode.ExecutionAndPublication);

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
    }

    public ObservableCollection<Synonym> MotherTongue { get; }
    public ObservableCollection<Synonym> ForeignLanguage { get; }
    [Reactive] public DateTimeOffset CreationDate { get; set; }

    public WordPracticeState PracticeState => practiceState.Value;

    /// <summary>
    /// A comma separated list of mother tongue synonyms
    /// </summary>
    public string MotherTongueCombined => motherTongueCombined.Value;
    /// <summary>
    /// A comma separated list of foreign language synonyms
    /// </summary>
    public string ForeignLanguageCombined => foreignLanguageCombined.Value;

    [Obsolete] public string MotherTongueText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    [Obsolete] public string ForeignLangText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    [Obsolete] public string? ForeignLangSynonym { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    [Obsolete] public string ForeignLangCombined => throw new NotImplementedException();
    [Obsolete] public int PracticeStateNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    [Obsolete] public DateTime PracticeDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    [Obsolete]
    public Word Clone(bool copyResults)
    {
        Synonym cloneSynonym(Synonym synonym)
        {
            IEnumerable<Practice> practices;
            if (copyResults)
                practices = synonym.Practices.Select(x => new Practice(x.Date, x.Result));
            else
                practices = Enumerable.Empty<Practice>();

            return new Synonym(synonym.Value, new ObservableCollection<string>(synonym.Flags), practices, settings);
        }

        return new Word(MotherTongue.Select(cloneSynonym), ForeignLanguage.Select(cloneSynonym), book, settings)
        {
            CreationDate = CreationDate
        };
    }
}
