using ReactiveUI;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Vocup.Models;
using Vocup.Util;

namespace Vocup.ViewModels;

public class BookSettingsViewModel : ReactiveObject
{
    private const string invalidChars = "#=:\\/|<>*?\"";

    public BookSettingsViewModel()
        : this(new Book(string.Empty, string.Empty, PracticeMode2.AskForForeignLanguage, Enumerable.Empty<Word>())) { }

    public BookSettingsViewModel(Book book)
    {
        Book = book;
        motherTongue = book.MotherTongue;
        foreignLanguage = book.ForeignLanguage;

        motherTongueValid = this
            .WhenAnyValue(x => x.MotherTongue)
            .Select(x => !x.ContainsAny(invalidChars))
            .ToProperty(this, x => x.MotherTongueValid);
        foreignLanguageValid = this
            .WhenAnyValue(x => x.ForeignLanguage)
            .Select(x => !x.ContainsAny(invalidChars))
            .ToProperty(this, x => x.ForeignLanguageValid);

        SaveCommand = ReactiveCommand.Create(
            () =>
            {
                Book.MotherTongue = MotherTongue;
                Book.ForeignLanguage = ForeignLanguage;
            },
            this.WhenAnyValue(x => x.MotherTongue, x => x.ForeignLanguage,
                (motherTongue, foreignLanguage) =>
                    !string.IsNullOrWhiteSpace(motherTongue) && !string.IsNullOrWhiteSpace(foreignLanguage)));
    }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }

    public Book Book { get; }

    private string motherTongue = string.Empty;
    public string MotherTongue
    {
        get => motherTongue;
        set => this.RaiseAndSetIfChanged(ref motherTongue, value);
    }

    private ObservableAsPropertyHelper<bool> motherTongueValid;
    public bool MotherTongueValid => motherTongueValid.Value;

    private string foreignLanguage = string.Empty;
    public string ForeignLanguage
    {
        get => foreignLanguage;
        set => this.RaiseAndSetIfChanged(ref foreignLanguage, value);
    }

    private ObservableAsPropertyHelper<bool> foreignLanguageValid;
    public bool ForeignLanguageValid => foreignLanguageValid.Value;
}
