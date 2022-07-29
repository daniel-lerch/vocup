using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Vocup.IO;

namespace Vocup.ViewModels;

public class MainFormViewModel : ReactiveObject
{
    public MainFormViewModel()
    {
        SaveCommand = ReactiveCommand.CreateFromTask(SaveAsync, this.WhenAny(x => x.BookContext, x => x != null));
    }

    private BookContext? bookContext;
    public BookContext? BookContext
    {
        get => bookContext;
        set => this.RaiseAndSetIfChanged(ref bookContext, value);
    }

    private string searchText = string.Empty;
    public string SearchText
    {
        get => searchText;
        set => this.RaiseAndSetIfChanged(ref searchText, value);
    }

    public Interaction<Unit, bool> SaveChanges { get; } = new();

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }

    private async Task SaveAsync()
    {
        await SaveChanges.Handle(Unit.Default);
        await new BookStorage().SaveAsync(BookContext, null).ConfigureAwait(false);
    }
}
