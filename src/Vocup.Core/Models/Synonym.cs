using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Vocup.Models;

public class Synonym : ReactiveObject
{
    public Synonym(string value)
    {
        Value = value;
        Flags = new ObservableCollection<string>();
        Practices = new ObservableCollection<Practice>();
    }

    public Synonym(string value, IEnumerable<string> flags, IEnumerable<Practice> practices)
    {
        Value = value;
        Flags = new ObservableCollection<string>(flags);
        Practices = new ObservableCollection<Practice>(practices);
    }

    [Reactive] public string Value { get; set; }
    public ObservableCollection<string> Flags { get; }
    public ObservableCollection<Practice> Practices { get; }

    public override bool Equals(object? obj)
    {
        return obj is Synonym synonym &&
               Value == synonym.Value &&
               Enumerable.SequenceEqual(Flags, synonym.Flags) &&
               Enumerable.SequenceEqual(Practices, synonym.Practices);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, Flags, Practices);
    }
}
