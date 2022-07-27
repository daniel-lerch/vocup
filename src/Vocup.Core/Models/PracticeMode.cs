namespace Vocup.Models;

public enum PracticeMode2
{
    // Do not change constants (binary serialization)

    /// <summary>
    /// The word is displayed in the mother tongue and the user has to enter the word in the foreign language.
    /// </summary>
    AskForForeignLanguage = 0,
    /// <summary>
    /// The word is displayed in the foreign language and the user has to enter the word in the mother tongue.
    /// </summary>
    AskForMotherTongue = 1,
}

public static class PracticeModeExtensions
{
    public static bool IsValid(this PracticeMode2 practiceMode)
    {
        return practiceMode == PracticeMode2.AskForForeignLanguage 
            || practiceMode == PracticeMode2.AskForMotherTongue;
    }
}
