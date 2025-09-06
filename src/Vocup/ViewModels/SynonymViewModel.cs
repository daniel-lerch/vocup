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
            .ToProperty(this, s => s.PracticeState, initialValue: -1.0);
    }

    public WordViewModel Word { get; }
    public string Value => valueHelper.Value;
    public double PracticeState => practiceStateHelper.Value;

    private static double GetPracticeState(IReadOnlyCollection<Practice> practices)
    {
        // Zipf's law weighted average for the last 5 practices
        int maxPracticesToConsider = 5;

        double weightedSum = 0.0, sumOfWeights = 0.0;
        int practicePosition = 1;

        foreach (Practice practice in practices.Reverse().Take(maxPracticesToConsider))
        {
            double weight = 1.0 / practicePosition;
            double result = practice.Result switch
            {
                PracticeResult2.Correct => 1.0,
                PracticeResult2.PartlyCorrect => 0.5,
                PracticeResult2.Wrong => 0.0,
                _ => throw new ArgumentOutOfRangeException(null, practice.Result, "Invalid practice result")
            };
            weightedSum += result * weight;
            sumOfWeights += weight;
            practicePosition++;
        }

        if (practicePosition == 1)
            return -1.0; // No practices

        for (; practicePosition <= maxPracticesToConsider; practicePosition++)
        {
            // Assume 0.0 for missing practices
            sumOfWeights += 1.0 / practicePosition;
        }

        return weightedSum / sumOfWeights;
    }

    public void Dispose()
    {
        valueHelper.Dispose();
        practiceStateHelper.Dispose();
    }
}
