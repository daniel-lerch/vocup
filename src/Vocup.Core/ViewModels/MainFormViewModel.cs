using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Vocup.IO;
using Vocup.Models;

namespace Vocup.ViewModels;

public class MainFormViewModel : ReactiveObject
{
    public MainFormViewModel()
    {
        OpenCommand = ReactiveCommand.CreateFromTask(OpenCommandAction);
        SaveCommand = ReactiveCommand.CreateFromTask(SaveCommandAction, this.WhenAnyValue(x => x.BookContext).Select(x => x != null));
        CloseCommand = ReactiveCommand.CreateFromTask(CloseCommandAction, this.WhenAnyValue(x => x.BookContext).Select(x => x != null));
        PracticeCommand = ReactiveCommand.CreateFromTask(PracticeCommandAction, this.WhenAnyValue(x => x.BookContext.Book.Unpracticed).Select(x => x > 0));
        BookSettingsCommand = ReactiveCommand.CreateFromTask(BookSettingsCommandAction, this.WhenAnyValue(x => x.BookContext).Select(x => x != null));

        this.WhenAnyValue(x => x.BookContext.Name)
            .Select(x => x == null ? "Vocup" : $"Vocup - {x}")
            .ToPropertyEx(this, x => x.Title, "Vocup");
    }

    [Reactive] public BookContext? BookContext { get; set; }
    [ObservableAsProperty] public string Title { get; }

    public Interaction<Unit, string?> OpenFile { get; } = new();
    public Interaction<Book, string?> SaveFile { get; } = new();
    public Interaction<Unit, bool> SaveAndContinue { get; } = new();
    public Interaction<Book, Unit> Practice { get; } = new();
    public Interaction<Book, Unit> BookSettings { get; } = new();

    public ReactiveCommand<Unit, Unit> OpenCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> CloseCommand { get; }
    public ReactiveCommand<Unit, Unit> PracticeCommand { get; }
    public ReactiveCommand<Unit, Unit> BookSettingsCommand { get; }

    public async ValueTask OpenAsync(string path)
    {
        BookContext = await new BookStorage().OpenAsync(path, null);
    }

    public async ValueTask<bool> SaveAsync()
    {
        if (BookContext == null)
            throw new InvalidOperationException("Cannot save book when no book is opened");

        if (BookContext.FileStream == null)
        {
            string? path = await SaveFile.Handle(BookContext.Book);
            if (path == null) return false;
            await new BookStorage().SaveAsync(BookContext, path, null);
        }
        else
        {
            await new BookStorage().SaveAsync(BookContext, null);
        }
        return true;
    }

    private async Task OpenCommandAction()
    {
        string? path = await OpenFile.Handle(Unit.Default);
        await OpenAsync(path);
    }

    private async Task SaveCommandAction() => await SaveAsync();

    private async Task CloseCommandAction()
    {
        if (BookContext == null) 
            throw new InvalidOperationException("Cannot close book when no book is opened");

        if (BookContext.UnsavedChanges)
        {
            bool @continue = await SaveAsync();
            if (!@continue) return;
        }

        BookContext bookContext = BookContext;
        BookContext = null;
        await bookContext.DisposeAsync();
    }

    private async Task PracticeCommandAction() => await Practice.Handle(BookContext.Book);
    private async Task BookSettingsCommandAction() => await BookSettings.Handle(BookContext.Book);
}
