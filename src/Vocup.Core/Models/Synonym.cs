using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Vocup.Models;

public class Synonym : ReactiveObject
{
    private string value;

    public Synonym(string value)
    {
        this.value = value;
        Flags = new ObservableCollection<string>();
        Practices = new ObservableCollection<Practice>();
    }

    public Synonym(string value, IEnumerable<string> flags, IEnumerable<Practice> practices)
    {
        this.value = value;
        Flags = new ObservableCollection<string>(flags);
        Practices = new ObservableCollection<Practice>(practices);
    }

    public string Value
    {
        get => value;
        set => this.RaiseAndSetIfChanged(ref this.value, value);
    }
    public ObservableCollection<string> Flags { get; }
    public ObservableCollection<Practice> Practices { get; }

    public override bool Equals(object? obj)
    {
        return obj is Synonym synonym &&
               value == synonym.value &&
               Enumerable.SequenceEqual(Flags, synonym.Flags) &&
               Enumerable.SequenceEqual(Practices, synonym.Practices);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(value, Flags, Practices);
    }
}
