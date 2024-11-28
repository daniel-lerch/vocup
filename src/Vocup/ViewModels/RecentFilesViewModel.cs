using System;
using System.Collections.ObjectModel;
using Vocup.Settings.Model;

namespace Vocup.ViewModels;

public class RecentFilesViewModel
{
    public RecentFilesViewModel(ObservableCollection<RecentFile> files)
    {
        Files = files;
    }

    public ObservableCollection<RecentFile> Files { get; }
}

public class DesignRecentFilesViewModel : RecentFilesViewModel
{
    public DesignRecentFilesViewModel() : base(
        [
            new("C:\\Users\\maxmu\\Documents\\Englisch - Deutsch.vhf", DateTime.Now, DateTime.Now),
        ])
    { }
}
