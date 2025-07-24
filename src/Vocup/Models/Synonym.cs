using ReactiveUI;

namespace Vocup.Models;

public class Synonym : ReactiveObject
{
    public Synonym(string value)
    {
        _value = value;
    }

    private string _value;
    public string Value
    {
        get => _value;
        set => this.RaiseAndSetIfChanged(ref _value, value);
    }
}
