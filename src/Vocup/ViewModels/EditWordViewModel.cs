using System.Collections.ObjectModel;

namespace Vocup.ViewModels;

public class EditWordViewModel : ViewModelBase
{
    public ObservableCollection<EditSynonymViewModel> MotherTongue { get; } = new();
    public ObservableCollection<EditSynonymViewModel> ForeignLanguage { get; } = new();
}

public class EditWordDesignViewModel : EditWordViewModel
{
    public EditWordDesignViewModel()
    {
        MotherTongue.Add(new(new("Apfel")));
        MotherTongue.Add(new(new("Birne")));
        ForeignLanguage.Add(new(new("apple")));
        ForeignLanguage.Add(new(new("pear")));
    }
}
