using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.Models
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
        AskForMotherTongue = 2,
        /// <summary>
        /// The word is either displayed in the foreign language and the user has to enter the word in the mother tongue, or the other way around.
        /// The direction is being mixed randomly per word.
        /// </summary>
        AskForBothMixed = 3
    }

    public static class PracticeModeHelper
    {
        public static bool IsValid(this PracticeMode mode)
        {
            int imode = (int)mode;
            return imode >= 1 && imode <= 3;
        }
    }
}
