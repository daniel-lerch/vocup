using ReactiveUI;
using System;

namespace Vocup.Models;

public class Practice : ReactiveObject
{
    private DateTimeOffset date;
    private PracticeResult2 result;

    public DateTimeOffset Date
    {
        get => date;
        set => this.RaiseAndSetIfChanged(ref date, value);
    }
    public PracticeResult2 Result
    {
        get => result;
        set => this.RaiseAndSetIfChanged(ref result, value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Practice practice &&
               date.Equals(practice.date) &&
               result == practice.result;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(date, result);
    }
}
