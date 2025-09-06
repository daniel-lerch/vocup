using System;
using System.Collections.Generic;
using Vocup.Models;

namespace Vocup.IO;

internal static class VhfExtensions
{
    public static int GetPracticeStateNumber(this Synonym synonym)
    {
        int practiceStateNumber = 0;

        for (int i = synonym.Practices.Count - 1; i >= 0; i--)
        {
            PracticeResult2 result = synonym.Practices[i].Result;
            if (result == PracticeResult2.Wrong && practiceStateNumber == 0)
                practiceStateNumber = 1;
            if (result == PracticeResult2.Wrong)
                return practiceStateNumber;
            if (result == PracticeResult2.Correct && practiceStateNumber == 0)
                practiceStateNumber = 2;
            else if (result == PracticeResult2.Correct)
                practiceStateNumber++;
        }

        return practiceStateNumber;
    }

    public static int GetPracticeStateNumber(this Word word, PracticeMode practiceMode)
    { 
        int practiceStateNumber = int.MaxValue; // Minimum of all synonyms i.e. Unpracticed if one synonym is Unpracticed

        foreach (Synonym synonym in word.GetSynonyms(practiceMode))
        {
            int synonymPracticeStateNumber = synonym.GetPracticeStateNumber();
            if (synonymPracticeStateNumber == 1) // Only WronglyPracticed has a higher priority than Unpracticed (benefitial for soon repetition)
                return 1;
            else if (synonymPracticeStateNumber < practiceStateNumber)
                practiceStateNumber = synonymPracticeStateNumber;
        }

        return practiceStateNumber;
    }

    public static DateTimeOffset GetLastPracticeDate(this Word word, PracticeMode practiceMode)
    {
        DateTimeOffset lastPractice = default;

        foreach (Synonym synonym in word.GetSynonyms(practiceMode))
        {
            foreach (Practice practice in synonym.Practices)
            {
                if (practice.Date > lastPractice)
                    lastPractice = practice.Date;
            }
        }

        return lastPractice;
    }

    public static IEnumerable<Synonym> GetSynonyms(this Word word, PracticeMode practiceMode)
    {
        if (practiceMode != PracticeMode.AskForForeignLang)
        {
            foreach (Synonym synonym in word.MotherTongue)
            {
                yield return synonym;
            }
        }

        if (practiceMode != PracticeMode.AskForMotherTongue)
        {
            foreach (Synonym synonym in word.ForeignLanguage)
            {
                yield return synonym;
            }
        }
    }
}
