using System;
using Vocup.Models;

namespace Vocup.IO.Vhf1;

internal static class Vhf1Extensions
{
    public static void GenerateVhrCode(this BookContext bookContext)
    {
        int number1 = '0', number2 = '9';
        int bigLetter1 = 'A', bigLetter2 = 'Z';
        int smallLetter1 = 'a', smallLetter2 = 'z';

        var random = new Random();
        char[] code = new char[24];

        for (int i = 0; i < code.Length;)
        {
            int character = random.Next(number1, smallLetter2);
            if (character >= number1 && character <= number2 ||
                character >= bigLetter1 && character <= bigLetter2 ||
                character >= smallLetter1 && character <= smallLetter2)
            {
                code[i] = (char)character;
                i++;
            }
        }

        bookContext.VhrCode = new string(code);
    }

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

    public static DateTimeOffset GetLastPracticeDate(this Synonym synonym)
    {
        DateTimeOffset lastPractice = default;

        foreach (Practice pratice in synonym.Practices)
        {
            if (pratice.Date > lastPractice)
                lastPractice = pratice.Date;
        }

        return lastPractice;
    }

    public static void GeneratePracticeHistory(this Synonym synonym, int practiceStateNumber, DateTime practiceDate)
    {
        if (practiceStateNumber == 1)
        {
            synonym.Practices.Add(new Practice
            {
                Result = PracticeResult2.Wrong,
                Date = practiceDate
            });
        }
        else if (practiceStateNumber > 1)
        {
            for (int i = 0; i < practiceStateNumber - 2; i++)
            {
                synonym.Practices.Add(new Practice
                {
                    Result = PracticeResult2.Correct
                });
            }

            synonym.Practices.Add(new Practice
            {
                Result = PracticeResult2.Correct,
                Date = practiceDate
            });
        }
    }
}
