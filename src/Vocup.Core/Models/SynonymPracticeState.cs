using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System.Linq;
using System.Reactive.Linq;
using Vocup.Settings;

namespace Vocup.Models;

public class SynonymPracticeState : ReactiveObject
{
    private readonly ObservableAsPropertyHelper<PracticeState> practiceState;

    public SynonymPracticeState(Synonym synonym, IVocupSettings settings)
    {
        practiceState = synonym.Practices
            .ToObservableChangeSet()
            .AutoRefreshOnObservable(_ => settings.WhenAnyValue(x => x.MaxPracticeCount))
            .ToCollection()
            .Select(x =>
            {
                int practiceState = 0;

                foreach (Practice practice in x)
                {
                    if (practice.Result == PracticeResult2.Wrong)
                        practiceState = 1;
                    else if (practice.Result == PracticeResult2.Correct)
                        practiceState = practiceState == 0 ? 2 : practiceState + 1;
                }

                return PracticeStateHelper.Parse(practiceState, settings.MaxPracticeCount);
            })
            .ToProperty(this, x => x.PracticeState);
    }

    public PracticeState PracticeState => practiceState.Value;
}
