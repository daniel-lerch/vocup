using ReactiveUI;

namespace Vocup.ViewModels;

public class BookViewModel : ViewModelBase
{
    private long _fileLength;
    public long FileLength
    {
        get => _fileLength;
        set => this.RaiseAndSetIfChanged(ref _fileLength, value);
    }
}
