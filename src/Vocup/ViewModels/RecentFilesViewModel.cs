using System.Collections.ObjectModel;
using Vocup.Settings.Model;

namespace Vocup.ViewModels;

public class RecentFilesViewModel
{
    public RecentFilesViewModel()
    {
        Files = [];
    }

    public RecentFilesViewModel(ObservableCollection<RecentFile> files)
    {
        Files = files;
    }

    public ObservableCollection<RecentFile> Files { get; }
}
