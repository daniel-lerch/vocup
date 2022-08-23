using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace Vocup.Models;

public class Synonym : ReactiveObject
{
    private readonly ObservableAsPropertyHelper<int> practiceState;

    public Synonym(string value) : this(value, new(), new()) { }

    public Synonym(string value, IEnumerable<string> flags, IEnumerable<Practice> practices)
        : this(
              value,
              new ObservableCollection<string>(flags),
              new ObservableCollection<Practice>(practices)) { }

    private Synonym(string value, ObservableCollection<string> flags, ObservableCollection<Practice> practices)
    {
        Value = value;
        Flags = flags;
        Practices = practices;

        practiceState = Practices
            .ToObservableChangeSet()
            .AutoRefresh(practice => practice.Result)
            .ToCollection()
            .Select(x =>
            {
                int practiceState = 0;

                foreach (Practice practice in x)
                {
                    if (practice.Result == PracticeResult2.Wrong)
                        practiceState = 1;
                    else if (practice.Result == PracticeResult2.Correct)
                        practiceState = practiceState == 0 ? 2 : practiceState + 1;
                }

                return practiceState;
            })
            .ToProperty(this, x => x.PracticeState);
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

    public int PracticeState => practiceState.Value;
}
