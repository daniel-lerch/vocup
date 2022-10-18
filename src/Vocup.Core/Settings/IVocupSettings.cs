using System.ComponentModel;

namespace Vocup.Settings;

public interface IVocupSettings : INotifyPropertyChanged
{
    string VhrPath { get; set; }
    int MaxPracticeCount { get; set; }
}
