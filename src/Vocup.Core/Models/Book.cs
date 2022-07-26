using ReactiveUI;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Vocup.Models;

public class Book : ReactiveObject
{
    private string motherTongue;
    private string foreignLanguage;
    private PracticeMode practiceMode;

    public Book(string motherTongue, string foreignLanguage)
    {
        this.motherTongue = motherTongue;
        this.foreignLanguage = foreignLanguage;
        Words = new ObservableCollection<Word>();
    }

    [JsonConstructor]
    public Book(string motherTongue, string foreignLanguage, ObservableCollection<Word> words)
    {
        this.motherTongue = motherTongue;
        this.foreignLanguage = foreignLanguage;
        Words = words;
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
    public PracticeMode PracticeMode
    {
        get => practiceMode;
        set => this.RaiseAndSetIfChanged(ref practiceMode, value);
    }
    public ObservableCollection<Word> Words { get; }
}
