using ReactiveUI;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;

namespace Vocup.ViewModels;

public class MainViewModel : ViewModelBase
{
    private long _fileLength;
    public long FileLength
    {
        get => _fileLength;
        set => this.RaiseAndSetIfChanged(ref _fileLength, value);
    }

    public AboutViewModel About { get; } = new();

    public Interaction<Unit, Avalonia.Platform.Storage.IStorageFile?> PickFileInteraction { get; } = new();

    public ICommand OpenFileCommand { get; }

    public MainViewModel()
    {
        OpenFileCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var file = await PickFileInteraction.Handle(Unit.Default);
            if (file != null)
            {
                using Stream stream = await file.OpenReadAsync();
                FileLength = stream.Length;
            }
        });
    }
}
