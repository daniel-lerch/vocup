using ReactiveUI;
using System.Collections.ObjectModel;

namespace Vocup.Models;

public class Book : ReactiveObject
{
    private string _motherTongue = string.Empty;
    public string MotherTongue
    {
        get => _motherTongue;
        set => this.RaiseAndSetIfChanged(ref _motherTongue, value);
    }

    private string _foreignLanguage = string.Empty;
    public string ForeignLanguage
    {
        get => _foreignLanguage;
        set => this.RaiseAndSetIfChanged(ref _foreignLanguage, value);
    }

    private PracticeMode _practiceMode;
    public PracticeMode PracticeMode
    {
        get => _practiceMode;
        set => this.RaiseAndSetIfChanged(ref _practiceMode, value);
    }

    public ObservableCollection<Word> Words { get; } = [];
}
