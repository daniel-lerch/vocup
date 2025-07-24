using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
        MotherTongue = new(motherTongue.Select(x => new Synonym(x)));
        ForeignLanguage = new(foreignLanguage.Select(x => new Synonym(x)));
    }

    public ObservableCollection<Synonym> MotherTongue { get; }
    public ObservableCollection<Synonym> ForeignLanguage { get; }
}
