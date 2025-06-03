using ReactiveUI;

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
}
