using Vocup.Settings;

namespace Vocup.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly VocupSettings? settings;

    public MainViewModel()
    {
    }

    public MainViewModel(VocupSettings settings)
    {
        this.settings = settings;
    }

    public AboutViewModel About { get; } = new();
    public RecentFilesViewModel RecentFiles => new(settings?.RecentFiles ?? []);
}
