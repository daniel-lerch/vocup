using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Vocup.Settings.Model;

namespace Vocup.ViewModels;

public class RecentFilesViewModel
{
    public RecentFilesViewModel(ObservableCollection<RecentFile> files)
    {
        Files = files;
        Groups = [];
    }

    public List<Group> Groups { get; }

    public ObservableCollection<RecentFile> Files { get; }

    public class Group
    {
        public Group(string name, List<File> files)
        {
            Name = name;
            Files = files;
        }

        public string Name { get; }

        public List<File> Files { get; }
    }

    public class File
    {
        public File(string name, string info, string fileName)
        {
            Name = name;
            Info = info;
            FileName = fileName;
        }

        public string Name { get; }
        public string Info { get; }
        public string FileName { get; }
    }
}

public class DesignRecentFilesViewModel : RecentFilesViewModel
{
    public DesignRecentFilesViewModel() : base(
        [
            new("C:\\Users\\maxmu\\Documents\\Englisch - Deutsch.vhf", DateTime.Now, DateTime.Now),
        ])
    {
        Groups.Add(new("Today", [new("Englisch - Deutsch", "~\\Documents", "C:\\Users\\maxmu\\Documents\\Englisch - Deutsch.vhf")]));
        Groups.Add(new("Last Year", [new("2023", "~\\Documents", "C:\\Users\\maxmu\\Documents\\2023.vhf")]));
    }
}
