using Vocup.Controls;

namespace Vocup.Models
{
    public interface IMainForm
    {
        StatisticsPanel StatisticsPanel { get; }
        void VocabularyWordSelected(bool value);
        void VocabularyBookPracticable(bool value);
        void VocabularyBookUnsavedChanges(bool value);
    }
}
