using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vocup.Models;

public class Synonym : ReactiveObject
{
    public Synonym(string value)
    {
        _value = value;
        Practices = [];
    }

    public Synonym(string value, List<Practice> practices)
    {
        _value = value;
        Practices = new(practices);
    }

    private string _value;
    public string Value
    {
        get => _value;
        set => this.RaiseAndSetIfChanged(ref _value, value);
    }

    public ObservableCollection<Practice> Practices { get; }
}
