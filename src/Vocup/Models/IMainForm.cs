using Vocup.Controls;

namespace Vocup.Models
{
    public interface IMainForm
    {
        StatisticsPanel StatisticsPanel { get; }
        void VocabularyWordSelected(bool value);
        void VocabularyBookLoaded(bool value);
        void VocabularyBookHasContent(bool value);
        void VocabularyBookPracticable(bool value);
        void VocabularyBookHasFilePath(bool value);
        void VocabularyBookUnsavedChanges(bool value);
        void VocabularyBookName(string value);

        void EditWord();
    }
}
