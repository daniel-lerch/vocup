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

public class Book : ReactiveObject
{
    private readonly ObservableAsPropertyHelper<int> unpracticed;

    public Book(string motherTongue, string foreignLanguage)
        : this(motherTongue, foreignLanguage, PracticeMode.AskForForeignLanguage, new ObservableCollection<Word>()) { }

    public Book(string motherTongue, string foreignLanguage, IEnumerable<Word> words)
        : this(motherTongue, foreignLanguage, PracticeMode.AskForForeignLanguage, words) { }

    public Book(string motherTongue, string foreignLanguage, PracticeMode practiceMode, IEnumerable<Word> words)
        : this(motherTongue, foreignLanguage, practiceMode, new ObservableCollection<Word>(words)) { }

    private Book(string motherTongue, string foreignLanguage, PracticeMode practiceMode, ObservableCollection<Word> words)
    {
        MotherTongue = motherTongue;
        ForeignLanguage = foreignLanguage;
        PracticeMode = practiceMode;
        Words = words;

        var unpracticedForeignLanguage = Words
            .ToObservableChangeSet()
            .AutoRefresh(word => word.ForeignLanguagePracticeState)
            .Filter(word => word.ForeignLanguagePracticeState == 0)
            .Count()
            .ToProperty(this, "unpracticedForeignLanguage");

        var unpracticedMotherTongue = Words
            .ToObservableChangeSet()
            .AutoRefresh(word => word.MotherTonguePracticeState)
            .Filter(word => word.MotherTonguePracticeState == 0)
            .Count()
            .ToProperty(this, "unpracticedMotherTongue");

        unpracticed = Words
            .ToObservableChangeSet()
            .AutoRefresh(word => word.ForeignLanguagePracticeState)
            .AutoRefresh(word => word.MotherTonguePracticeState)
            .Filter(
                this.WhenAnyValue(x => x.PracticeMode)
                    .Select<PracticeMode, Func<Word, bool>>(practiceMode => practiceMode != PracticeMode.AskForMotherTongue ?
                        word => word.ForeignLanguagePracticeState == 0 :
                        word => word.MotherTonguePracticeState == 0))
            .ToCollection() // Creating a collection may be inefficient but .Count() does not work here
            .Select(x => x.Count) 
            .ToProperty(this, x => x.Unpracticed);
    }

    [Reactive] public string MotherTongue { get; set; }
    [Reactive] public string ForeignLanguage { get; set; }
    [Reactive] public PracticeMode PracticeMode { get; set; }
    public ObservableCollection<Word> Words { get; }

    public override bool Equals(object? obj)
    {
        return obj is Book book &&
               MotherTongue == book.MotherTongue &&
               ForeignLanguage == book.ForeignLanguage &&
               PracticeMode == book.PracticeMode &&
               Enumerable.SequenceEqual(Words, book.Words);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MotherTongue, ForeignLanguage, PracticeMode, Words);
    }

    // TODO implement change listeners
    public int Unpracticed => unpracticed.Value;
    [Obsolete] public int WronglyPracticed => 0;
    [Obsolete] public int CorrectlyPracticed => 0;
    [Obsolete] public int FullyPracticed => 0;
    [Obsolete] public int NotFullyPracticed => 0;
}
