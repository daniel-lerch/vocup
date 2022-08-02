﻿using System.Windows.Forms;
using Vocup.Controls;

namespace Vocup.Models
{
    public interface IMainForm
    {
        StatisticsPanel StatisticsPanel { get; }
        TextBox SearchText { get; }
        void VocabularyWordSelected(bool value);
        void VocabularyBookHasFilePath(bool value);
        void VocabularyBookUnsavedChanges(bool value);

        void EditWord();
    }
}
