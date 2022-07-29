using ReactiveUI;
using System.Linq;
using Vocup.Models;

namespace Vocup.ViewModels;

public class BookSettingsViewModel : ReactiveObject
{
    public BookSettingsViewModel()
    {
        Book = new("Deutsch", "Englisch", PracticeMode2.AskForForeignLanguage, Enumerable.Empty<Word>());
    }

    public Book Book { get; }
}
