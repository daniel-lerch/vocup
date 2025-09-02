using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Vocup.Models;

namespace Vocup.ViewModels;

public class SynonymViewModel : ViewModelBase, IDisposable
{
    private readonly ObservableAsPropertyHelper<string> valueHelper;
    private readonly ObservableAsPropertyHelper<double> practiceStateHelper;

    public SynonymViewModel(WordViewModel parent, Synonym synonym)
    {
        Word = parent;
        valueHelper = synonym.WhenAnyValue(s => s.Value).ToProperty(this, s => s.Value);
        practiceStateHelper = synonym.Practices.ToObservableChangeSet()
            .ToCollection()
            .Select(GetPracticeState)
            .ToProperty(this, s => s.PracticeState, initialValue: 0.0);
    }

    public WordViewModel Word { get; }
    public string Value => valueHelper.Value;
    public double PracticeState => practiceStateHelper.Value;

    private static double GetPracticeState(IReadOnlyCollection<Practice> practices)
    {
        // Zipf's law weighted average for the last 5 practices
        int lastN = 5;

        double values = 0.0, weights = 0.0;
        int counter = 1;

        foreach (Practice practice in practices.Reverse().Take(lastN))
        {
            double weight = 1.0 / counter;
            double result = practice.Result switch
            {
                PracticeResult2.Correct => 1.0,
                PracticeResult2.PartlyCorrect => 0.5,
                PracticeResult2.Wrong => 0.0,
                _ => throw new ArgumentOutOfRangeException(null, practice.Result, "Invalid practice result")
            };
            values += result * weight;
            weights += weight;
            counter++;
        }

        if (counter == 1)
            return -1.0; // No practices

        for (; counter <= lastN; counter++)
        {
            // Assume 0.0 for missing practices
            weights += 1.0 / counter;
        }

        return values / weights;
    }

    public void Dispose()
    {
        valueHelper.Dispose();
        practiceStateHelper.Dispose();
    }
}
