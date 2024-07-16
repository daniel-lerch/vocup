namespace Vocup.Models
{
    public enum PracticeDirection
    {
        /// <summary>
        /// The word is displayed in the mother tongue and the user has to enter the word in the foreign language.
        /// </summary>
        MotherTongueToForeignLang = 1,
        /// <summary>
        /// The word is displayed in the foreign language and the user has to enter the word in the mother tongue.
        /// </summary>
        ForeignLangToMotherTongue = 2
    }
}