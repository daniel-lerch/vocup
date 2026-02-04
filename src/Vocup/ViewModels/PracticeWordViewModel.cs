using System.Collections.Generic;
using System.Linq;
using Vocup.Models;

namespace Vocup.ViewModels;

public class PracticeWordViewModel
{
    public PracticeWordViewModel(Word word, PracticeDirection direction)
    {
        if (direction == PracticeDirection.AskForForeignLanguage)
        {
            ReferenceSynonyms = [.. word.MotherTongue.Select(s => s.Value)];
            InputSynonyms = [.. word.ForeignLanguage.Select(s => new PracticeSynonymViewModel())];
        }
        else // if (direction == PracticeDirection.AskForMotherTongue)
        {
            ReferenceSynonyms = [.. word.ForeignLanguage.Select(s => s.Value)];
            InputSynonyms = [.. word.MotherTongue.Select(s => new PracticeSynonymViewModel())];
        }
    }

    public IReadOnlyList<string> ReferenceSynonyms { get; }
    public IReadOnlyList<PracticeSynonymViewModel> InputSynonyms { get; }
}
