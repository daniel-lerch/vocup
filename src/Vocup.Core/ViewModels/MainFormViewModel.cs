using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Vocup.IO;

namespace Vocup.ViewModels;

public class MainFormViewModel : ReactiveObject
{
    public MainFormViewModel()
    {
        OpenCommand = ReactiveCommand.CreateFromTask(OpenAsync);
        SaveCommand = ReactiveCommand.CreateFromTask(SaveAsync, this.WhenAnyValue(x => x.BookContext).Select(x => x != null));

        this.WhenAnyValue(x => x.BookContext.Name)
            .Select(x => x == null ? "Vocup" : $"Vocup - {x}")
            .ToPropertyEx(this, x => x.Title, "Vocup");
    }

    [Reactive] public BookContext? BookContext { get; set; }
    [ObservableAsProperty] public string Title { get; }

    public Interaction<Unit, string?> OpenFile { get; } = new();
    public Interaction<Unit, bool> SaveAndContinue { get; } = new();

    public ReactiveCommand<Unit, Unit> OpenCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }

    private async Task OpenAsync()
    {
        string? path = await OpenFile.Handle(Unit.Default);
        BookContext = await new BookStorage().OpenAsync(path, null);
    }

    private async Task SaveAsync()
    {
        bool @continue = await SaveAndContinue.Handle(Unit.Default);
        await new BookStorage().SaveAsync(BookContext, null);
    }
}
