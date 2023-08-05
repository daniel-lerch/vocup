using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Vocup.IO;
using Vocup.Models;
using Vocup.Settings;

namespace Vocup.ViewModels;

public class MainFormViewModel : ReactiveObject
{
    private readonly IVocupSettings settings;
    private readonly BookStorage bookStorage;

    public MainFormViewModel(IVocupSettings settings)
    {
        this.settings = settings;
        bookStorage = new BookStorage(settings);

        OpenCommand = ReactiveCommand.CreateFromTask(OpenCommandAction);
        SaveCommand = ReactiveCommand.CreateFromTask(SaveCommandAction,
            this.WhenAnyValue(x => x.BookContext, x => x.BookContext!.UnsavedChanges, (_, _) => BookContext?.UnsavedChanges ?? false));
        SaveAsCommand = ReactiveCommand.CreateFromTask(SaveAsCommandAction, this.WhenAnyValue(x => x.BookContext).Select(x => x != null));
        CloseCommand = ReactiveCommand.CreateFromTask(CloseCommandAction, this.WhenAnyValue(x => x.BookContext).Select(x => x != null));
        PracticeCommand = ReactiveCommand.CreateFromTask(PracticeCommandAction, this.WhenAnyValue(x => x.BookContext.Book.PracticeState.Unpracticed).Select(x => x > 0));
        CreateBookCommand = ReactiveCommand.CreateFromTask(CreateBookCommandAction);
        BookSettingsCommand = ReactiveCommand.CreateFromTask(BookSettingsCommandAction, this.WhenAnyValue(x => x.BookContext).Select(x => x != null));
        PrintCommand = ReactiveCommand.CreateFromTask(PrintCommandAction, this.WhenAnyValue(x => x.BookContext.Book.Words.Count).Select(x => x > 0));
        OpenInExplorerCommand = ReactiveCommand.CreateFromTask(OpenInExplorerCommandAction,
            this.WhenAnyValue(x => x.BookContext, x => x.BookContext!.FilePath, (_, _) => BookContext?.FilePath != null));

        this.WhenAnyValue(x => x.BookContext, x => x.BookContext!.FilePath, (_, _) => BookContext?.FilePath)
            .Select(x => x == null ? "Vocup" : $"Vocup - {Path.GetFileNameWithoutExtension(x)}")
            .ToPropertyEx(this, x => x.Title);

        this.WhenAnyValue(x => x.BookContext, x => x.BookContext!.Book.PracticeState.Unpracticed, (_, _) => BookContext?.Book.PracticeState.Unpracticed ?? 0)
            .ToPropertyEx(this, x => x.Unpracticed);
        this.WhenAnyValue(x => x.BookContext, x => x.BookContext!.Book.PracticeState.WronglyPracticed, (_, _) => BookContext?.Book.PracticeState.WronglyPracticed ?? 0)
            .ToPropertyEx(this, x => x.WronglyPracticed);
        this.WhenAnyValue(x => x.BookContext, x => x.BookContext!.Book.PracticeState.CorrectlyPracticed, (_, _) => BookContext?.Book.PracticeState.CorrectlyPracticed ?? 0)
            .ToPropertyEx(this, x => x.CorrectlyPracticed);
        this.WhenAnyValue(x => x.BookContext, x => x.BookContext!.Book.PracticeState.FullyPracticed, (_, _) => BookContext?.Book.PracticeState.FullyPracticed ?? 0)
            .ToPropertyEx(this, x => x.FullyPracticed);
    }

    // TODO: Replace this nullable property with BookViewModel to have less nullability issues.
    [Reactive] public BookContext? BookContext { get; set; }
    [Reactive] public string SearchText { get; set; } = string.Empty;
    [ObservableAsProperty] public string Title { get; } = "Vocup";
    [ObservableAsProperty] public int Unpracticed { get; }
    [ObservableAsProperty] public int WronglyPracticed { get; }
    [ObservableAsProperty] public int CorrectlyPracticed { get; }
    [ObservableAsProperty] public int FullyPracticed { get; }

    public Interaction<Unit, string?> OpenFile { get; } = new();
    public Interaction<Book, string?> SaveFile { get; } = new();
    public Interaction<Unit, bool?> SaveBeforeContinue { get; } = new();
    public Interaction<Book, Unit> Practice { get; } = new();
    public Interaction<Unit, Book?> CreateBook { get; set; } = new();
    public Interaction<Book, Unit> BookSettings { get; } = new();
    public Interaction<BookContext, Unit> Print { get; } = new();
    public Interaction<string, Unit> OpenInExplorer { get; } = new();

    public ReactiveCommand<Unit, Unit> OpenCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveAsCommand { get; }
    public ReactiveCommand<Unit, Unit> CloseCommand { get; }
    public ReactiveCommand<Unit, Unit> PracticeCommand { get; }
    public ReactiveCommand<Unit, Unit> CreateBookCommand { get; }
    public ReactiveCommand<Unit, Unit> BookSettingsCommand { get; }
    public ReactiveCommand<Unit, Unit> PrintCommand { get; }
    public ReactiveCommand<Unit, Unit> OpenInExplorerCommand { get; }

    public async ValueTask OpenAsync(string path)
    {
        BookContext = await bookStorage.OpenAsync(path, settings.VhrPath);
    }

    public async ValueTask<bool> SaveAsync()
    {
        if (BookContext == null)
            throw new InvalidOperationException("Cannot save book when no book is opened");

        if (BookContext.FileStream == null)
        {
            string? path = await SaveFile.Handle(BookContext.Book);
            if (path == null) return false;
            await bookStorage.SaveAsync(BookContext, path, settings.VhrPath);
        }
        else
        {
            await bookStorage.SaveAsync(BookContext, settings.VhrPath);
        }
        return true;
    }

    public async ValueTask<bool> AskForSaveAsync()
    {
        if (BookContext == null)
            throw new InvalidOperationException();

        if (!BookContext.UnsavedChanges) return true;

        bool? saveBeforeContinue = await SaveBeforeContinue.Handle(Unit.Default);
        if (saveBeforeContinue == true)
        {
            return await SaveAsync();
        }
        else if (saveBeforeContinue == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private async Task OpenCommandAction()
    {
        string? path = await OpenFile.Handle(Unit.Default);
        await OpenAsync(path);
    }

    private async Task SaveCommandAction() => await SaveAsync();

    private async Task SaveAsCommandAction() => throw new NotImplementedException();

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

    private async Task PracticeCommandAction()
    {
        if (BookContext == null) throw new InvalidOperationException();
        await Practice.Handle(BookContext.Book);
    }
    private async Task CreateBookCommandAction()
    {
        Book? book = await CreateBook.Handle(Unit.Default);
        if (book != null)
        {
            await AskForSaveAsync();

            // TODO: Load new book
        }
    }
    private async Task BookSettingsCommandAction()
    {
        if (BookContext == null) throw new InvalidOperationException();
        await BookSettings.Handle(BookContext.Book);
    }
    private async Task PrintCommandAction()
    {
        if (BookContext == null) throw new InvalidOperationException();
        await Print.Handle(BookContext);
    }
    private async Task OpenInExplorerCommandAction()
    {
        if (BookContext == null || BookContext.FilePath == null) throw new InvalidOperationException();
        await OpenInExplorer.Handle(BookContext.FilePath);
    }
}
