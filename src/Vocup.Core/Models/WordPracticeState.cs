using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System.Linq;
using System.Reactive.Linq;

namespace Vocup.Models;

public class WordPracticeState : ReactiveObject
{
    private readonly ObservableAsPropertyHelper<PracticeState> practiceState;

    public WordPracticeState(Word word, Book book)
    {
        // TODO: Find a solution that does not access the practice state of mother tongue when only foreign language matters
        //       to avoid computing a practice state for that side through lazy evaluation.

        //IObservable<PracticeState> motherTonguePracticeState = word.MotherTongue
        //    .ToObservableChangeSet()
        //    .AutoRefresh(synonym => synonym.PracticeState.PracticeState)
        //    .ToCollection()
        //    .Select(x => x.Min(synonym => synonym.PracticeState.PracticeState));

        //IObservable<PracticeState> foreignLanguagePracticeState = word.ForeignLanguage
        //    .ToObservableChangeSet()
        //    .AutoRefresh(synonym => synonym.PracticeState.PracticeState)
        //    .ToCollection()
        //    .Select(x => x.Min(synonym => synonym.PracticeState.PracticeState));

        //practiceState = book.WhenAnyValue(x => x.PracticeMode)
        //    .SelectMany(practiceMode => practiceMode != PracticeMode.AskForMotherTongue ? foreignLanguagePracticeState : motherTonguePracticeState)
        //    .ToProperty(this, x => x.PracticeState);

        practiceState = book.WhenAnyValue(x => x.PracticeMode)
            .Select(practiceMode => practiceMode != PracticeMode.AskForMotherTongue ? word.ForeignLanguage : word.MotherTongue)
            .SelectMany(synonyms =>
            {
                return synonyms
                    .ToObservableChangeSet()
                    .AutoRefresh(synonym => synonym.PracticeState.PracticeState)
                    .ToCollection()
                    .Select(x => x.Min(synonym => synonym.PracticeState.PracticeState));
            })
            .ToProperty(this, x => x.PracticeState);

        //PracticeState = Reactive(() =>
        //{
        //    ObservableCollection<Synonym> synonyms = book.PracticeMode != PracticeMode.AskForMotherTongue ? word.ForeignLanguage : word.MotherTongue;
        //    int practiceStateNumber = synonyms.MaxBy(synonym => synonym.PracticeState)?.PracticeState ?? 0;
        //    return PracticeStateHelper.Parse(practiceStateNumber, settings.MaxPracticeCount);
        //});
    }

    public PracticeState PracticeState => practiceState.Value;
}
