using Vocup.Settings;

namespace Vocup.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel(VocupSettings settings)
    {
        About = new("Development");
        RecentFiles = new(settings.RecentFiles);
    }

    public virtual AboutViewModel About { get; }
    public virtual RecentFilesViewModel RecentFiles { get; }
}

public class DesignMainViewModel : MainViewModel
{
    public DesignMainViewModel() : base(new VocupSettings())
    {
    }

    public override AboutViewModel About { get; } = new DesignAboutViewModel();
    public override RecentFilesViewModel RecentFiles { get; } = new DesignRecentFilesViewModel();
}
