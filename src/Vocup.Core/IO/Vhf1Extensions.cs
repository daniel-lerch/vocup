using Vocup.Models;

namespace Vocup.IO
{
    internal static class Vhf1Extensions
    {
        public static int GetPracticeStateNumber(this Synonym synonym)
        {
            int practiceStateNumber = 0;

            for (int i = synonym.Practices.Count - 1; i >= 0; i--)
            {
                PracticeResult result = synonym.Practices[i].Result;
                if (result == PracticeResult.Wrong && practiceStateNumber == 0)
                    practiceStateNumber = 1;
                if (result == PracticeResult.Wrong)
                    return practiceStateNumber;
                if (result == PracticeResult.Correct && practiceStateNumber == 0)
                    practiceStateNumber = 2;
                else if (result == PracticeResult.Correct)
                    practiceStateNumber++;
            }

            return practiceStateNumber;
        }
    }
}
