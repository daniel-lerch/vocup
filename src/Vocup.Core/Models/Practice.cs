using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;

namespace Vocup.Models;

public class Practice : ReactiveObject
{
    [Reactive] public DateTimeOffset Date { get; set; }
    [Reactive] public PracticeResult2 Result { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Practice practice &&
               Date.Equals(practice.Date) &&
               Result == practice.Result;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Date, Result);
    }
}
