using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vocup.Models;

public class Word
{
    public Word()
    {
        MotherTongue = [];
        ForeignLanguage = [];
    }

    public Word(IEnumerable<string> motherTongue, IEnumerable<string> foreignLanguage)
    {
        MotherTongue = new(motherTongue);
        ForeignLanguage = new(foreignLanguage);
    }

    public ObservableCollection<string> MotherTongue { get; }
    public ObservableCollection<string> ForeignLanguage { get; }
}
