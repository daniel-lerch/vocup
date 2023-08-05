using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Vocup.Models;

public class Book : ReactiveObject
{
    private readonly Lazy<BookPracticeState> practiceState;

    public Book(string motherTongue, string foreignLanguage)
        : this(motherTongue, foreignLanguage, PracticeMode.AskForForeignLanguage, new ObservableCollection<Word>()) { }

    public Book(string motherTongue, string foreignLanguage, IEnumerable<Word> words)
        : this(motherTongue, foreignLanguage, PracticeMode.AskForForeignLanguage, new(words)) { }

    public Book(string motherTongue, string foreignLanguage, PracticeMode practiceMode)
        : this(motherTongue, foreignLanguage, practiceMode, new()) { }

    private Book(string motherTongue, string foreignLanguage, PracticeMode practiceMode, ObservableCollection<Word> words)
    {
        MotherTongue = motherTongue;
        ForeignLanguage = foreignLanguage;
        PracticeMode = practiceMode;
        Words = words;

        practiceState = new(() => new(this), LazyThreadSafetyMode.ExecutionAndPublication);
    }

    [Reactive] public string MotherTongue { get; set; }
    [Reactive] public string ForeignLanguage { get; set; }
    [Reactive] public PracticeMode PracticeMode { get; set; }
    public ObservableCollection<Word> Words { get; }

    public BookPracticeState PracticeState => practiceState.Value;
}
