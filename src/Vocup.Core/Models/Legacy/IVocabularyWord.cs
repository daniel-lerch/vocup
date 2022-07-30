using System;
using System.ComponentModel;

namespace Vocup.Models.Legacy;

// This interface wraps a legacy data model for easier migration
public interface IVocabularyWord : INotifyPropertyChanged
{
    string MotherTongue { get; set; }
    string ForeignLang { get; set; }
    string? ForeignLangSynonym { get; set; }
    string ForeignLangText { get; }
    int PracticeStateNumber { get; set; }
    PracticeState PracticeState { get; }
    DateTime PracticeDate { get; set; }
    void RenewPracticeState();
    IVocabularyWord Clone(bool copyResults);
}
