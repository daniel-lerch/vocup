using System;

namespace Vocup.Models;

public enum PracticeState
{
    Unpracticed = 0,
    WronglyPracticed = 1,
    CorrectlyPracticed = 2,
    FullyPracticed = 3
}

public static class PracticeStateHelper
{
    public static bool IsValid(this PracticeState state)
    {
        return state >= 0;
    }

    public static PracticeState Parse(int practiceStateNumber, int maxPracticeCount)
    {
        if (practiceStateNumber < 0)
            throw new ArgumentOutOfRangeException(nameof(practiceStateNumber), practiceStateNumber, $"{nameof(practiceStateNumber)} must be greater than or equal to 0");
        if (practiceStateNumber == 0)
            return PracticeState.Unpracticed;
        if (practiceStateNumber == 1)
            return PracticeState.WronglyPracticed;
        if (practiceStateNumber < maxPracticeCount)
            return PracticeState.CorrectlyPracticed;
        else // if (practiceStateNumber >= Program.Settings.MaxPracticeCount)
            return PracticeState.FullyPracticed;
    }
}
