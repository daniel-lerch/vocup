using System;
using System.Collections.Generic;
using System.Text;

namespace Vocup.Avalonia.Models
{
    public enum PracticeMode
    {
        /// <summary>
        /// The word is displayed in the mother tongue and the user has to enter the word in the foreign language.
        /// </summary>
        AskForForeignLang = 1,
        /// <summary>
        /// The word is displayed in the foreign language and the user has to enter the word in the mother tongue.
        /// </summary>
        AskForMotherTongue = 2
    }
}
