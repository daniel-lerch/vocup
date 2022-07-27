using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Vocup.Models;

public class Book : ReactiveObject
{
    private string motherTongue;
    private string foreignLanguage;
    private PracticeMode2 practiceMode;

    public Book(string motherTongue, string foreignLanguage)
    {
        this.motherTongue = motherTongue;
        this.foreignLanguage = foreignLanguage;
        Words = new ObservableCollection<Word>();
    }

    public Book(string motherTongue, string foreignLanguage, IEnumerable<Word> words)
    {
        this.motherTongue = motherTongue;
        this.foreignLanguage = foreignLanguage;
        Words = new ObservableCollection<Word>(words);
    }

    public Book(string motherTongue, string foreignLanguage, PracticeMode2 practiceMode, IEnumerable<Word> words)
    {
        this.motherTongue = motherTongue;
        this.foreignLanguage = foreignLanguage;
        this.practiceMode = practiceMode;
        Words = new ObservableCollection<Word>(words);
    }

    public string MotherTongue
    {
        get => motherTongue;
        set => this.RaiseAndSetIfChanged(ref motherTongue, value);
    }
    public string ForeignLanguage
    {
        get => foreignLanguage;
        set => this.RaiseAndSetIfChanged(ref foreignLanguage, value);
    }
    public PracticeMode2 PracticeMode
    {
        get => practiceMode;
        set => this.RaiseAndSetIfChanged(ref practiceMode, value);
    }
    public ObservableCollection<Word> Words { get; }

    public override bool Equals(object? obj)
    {
        return obj is Book book &&
               motherTongue == book.motherTongue &&
               foreignLanguage == book.foreignLanguage &&
               practiceMode == book.practiceMode &&
               Enumerable.SequenceEqual(Words, book.Words);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(motherTongue, foreignLanguage, practiceMode, Words);
    }
}
