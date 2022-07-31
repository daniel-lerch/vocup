using System;
using System.ComponentModel;

namespace Vocup.Models.Legacy;

// This interface wraps a legacy data model for easier migration
public interface IVocabularyWord : INotifyPropertyChanged
{
    string MotherTongueText { get; set; }
    string ForeignLangText { get; set; }
    string? ForeignLangSynonym { get; set; }
    string ForeignLangCombined { get; }
    int PracticeStateNumber { get; set; }
    PracticeState PracticeState { get; }
    DateTime PracticeDate { get; set; }
    Word Clone(bool copyResults);
}
