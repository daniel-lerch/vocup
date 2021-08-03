﻿namespace Vocup.Models
{
    public enum PracticeMode
    {
        /// <summary>
        /// The word is displayed in the mother tongue and the user has to enter the word in the foreign language.
        /// </summary>
        AskForForeignLanguage,
        /// <summary>
        /// The word is displayed in the foreign language and the user has to enter the word in the mother tongue.
        /// </summary>
        AskForMotherTongue
    }

    public static class PracticeModeExtensions
    {
        public static bool IsValid(this PracticeMode practiceMode)
        {
            return practiceMode == PracticeMode.AskForForeignLanguage 
                || practiceMode == PracticeMode.AskForMotherTongue;
        }
    }
}