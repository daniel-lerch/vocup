using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Vocup.Settings;

namespace Vocup.Models;

public class Synonym : ReactiveObject
{
    private readonly Lazy<SynonymPracticeState> practiceState;

    public Synonym(string value, IVocupSettings settings) : this(value, new(), new(), settings) { }

    public Synonym(string value, IEnumerable<string> flags, IEnumerable<Practice> practices, IVocupSettings settings)
        : this(
              value,
              new ObservableCollection<string>(flags),
              new ObservableCollection<Practice>(practices),
              settings) { }

    private Synonym(string value, ObservableCollection<string> flags, ObservableCollection<Practice> practices, IVocupSettings settings)
    {
        Value = value;
        Flags = flags;
        Practices = practices;

        practiceState = new(() => new(this, settings), LazyThreadSafetyMode.ExecutionAndPublication);
    }

    [Reactive] public string Value { get; set; }
    public ObservableCollection<string> Flags { get; }
    public ObservableCollection<Practice> Practices { get; }

    public SynonymPracticeState PracticeState => practiceState.Value;
}
