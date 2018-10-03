using Vocup.Controls;

namespace Vocup.Models
{
    public interface IMainForm
    {
        StatisticsPanel StatisticsPanel { get; }
        void VocabularyWordSelected(bool value);
        void VocabularyBookHasContent(bool value);
        void VocabularyBookPracticable(bool value);
        void VocabularyBookUnsavedChanges(bool value);
        void LoadBook(VocabularyBook book);
        void UnloadBook();
    }
}
