using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Vocup.Models;

public class Book : ReactiveObject
{
    public Book(string motherTongue, string foreignLanguage)
    {
        MotherTongue = motherTongue;
        ForeignLanguage = foreignLanguage;
        Words = new ObservableCollection<Word>();
    }

    public Book(string motherTongue, string foreignLanguage, IEnumerable<Word> words)
    {
        MotherTongue = motherTongue;
        ForeignLanguage = foreignLanguage;
        Words = new ObservableCollection<Word>(words);
    }

    public Book(string motherTongue, string foreignLanguage, PracticeMode practiceMode, IEnumerable<Word> words)
    {
        MotherTongue = motherTongue;
        ForeignLanguage = foreignLanguage;
        PracticeMode = practiceMode;
        Words = new ObservableCollection<Word>(words);
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
}
