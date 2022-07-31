using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.Models.Legacy;

public interface IVocabularyBook : INotifyPropertyChanged
{
    string? FilePath { get; set; }
    string? Name { get; }
    string? VhrCode { get; set; }
    bool UnsavedChanges { get; set; }
    string MotherTongue { get; set; }
    string ForeignLanguage { get; set; }
    PracticeMode PracticeMode { get; set; }
    ObservableCollection<Word> Words { get; }
    void GenerateVhrCode();

    int Unpracticed { get; }
    int WronglyPracticed { get; }
    int CorrectlyPracticed { get; }
    int FullyPracticed { get; }
    int NotFullyPracticed { get; }
}
