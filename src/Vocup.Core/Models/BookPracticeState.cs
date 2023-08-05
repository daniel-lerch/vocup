using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;

namespace Vocup.Models;

public class BookPracticeState : ReactiveObject
{
    public BookPracticeState(Book book)
    {
        book.Words.ToObservableChangeSet()
            .AutoRefresh(word => word.PracticeState.PracticeState)
            .Filter(word => word.PracticeState.PracticeState == PracticeState.Unpracticed)
            .ToCollection()
            .Select(words => words.Count)
            .ToPropertyEx(this, x => x.Unpracticed);

        book.Words.ToObservableChangeSet()
            .AutoRefresh(word => word.PracticeState.PracticeState)
            .Filter(word => word.PracticeState.PracticeState == PracticeState.WronglyPracticed)
            .ToCollection()
            .Select(words => words.Count)
            .ToPropertyEx(this, x => x.WronglyPracticed);

        book.Words.ToObservableChangeSet()
            .AutoRefresh(word => word.PracticeState.PracticeState)
            .Filter(word => word.PracticeState.PracticeState == PracticeState.CorrectlyPracticed)
            .ToCollection()
            .Select(words => words.Count)
            .ToPropertyEx(this, x => x.CorrectlyPracticed);

        book.Words.ToObservableChangeSet()
            .AutoRefresh(word => word.PracticeState.PracticeState)
            .Filter(word => word.PracticeState.PracticeState == PracticeState.FullyPracticed)
            .ToCollection()
            .Select(words => words.Count)
            .ToPropertyEx(this, x => x.FullyPracticed);

        book.Words.ToObservableChangeSet()
            .AutoRefresh(word => word.PracticeState.PracticeState)
            .Filter(word => word.PracticeState.PracticeState != PracticeState.FullyPracticed)
            .ToCollection()
            .Select(words => words.Count)
            .ToPropertyEx(this, x => x.NotFullyPracticed);
    }

    [ObservableAsProperty] public int Unpracticed { get; }
    [ObservableAsProperty] public int WronglyPracticed { get; }
    [ObservableAsProperty] public int CorrectlyPracticed { get; }
    [ObservableAsProperty] public int FullyPracticed { get; }
    [ObservableAsProperty] public int NotFullyPracticed { get; }
}
