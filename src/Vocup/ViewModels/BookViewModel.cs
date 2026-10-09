using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Vocup.Models;

namespace Vocup.ViewModels;

// Imported after the namespace line to take priority over System.ObservableExtensions.Subscribe() from System.Reactive,
// which DynamicData still depends on. Move it back up once DynamicData uses ReactiveUI.Primitives:
// https://github.com/reactivemarbles/DynamicData/pull/1116
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Concurrency;

public partial class BookViewModel : ViewModelBase, IDisposable
{
    public static readonly TimeSpan SearchThrottle = TimeSpan.FromMilliseconds(100);

    private readonly IDisposable wordsOperation;
    private readonly ObservableAsPropertyHelper<PracticeMode> practiceModeHelper;
    private readonly Book book;

    /// <param name="book">The book to display.</param>
    /// <param name="taskpoolScheduler">Scheduler for throttling the search. Defaults to <see cref="RxSchedulers.TaskpoolScheduler"/>.</param>
    /// <param name="mainThreadScheduler">Scheduler for updating <see cref="Words"/>. Defaults to <see cref="RxSchedulers.MainThreadScheduler"/>.</param>
    public BookViewModel(Book book, ISequencer? taskpoolScheduler = null, ISequencer? mainThreadScheduler = null)
    {
        this.book = book ?? throw new ArgumentNullException(nameof(book));

        var filter = this.WhenAnyValue(vm => vm.SearchText)
            // This filter operation is inefficient so we throttle it to keep the application responsive.
            .Throttle(SearchThrottle, taskpoolScheduler ?? RxSchedulers.TaskpoolScheduler)
            .Select(BuildFilter)
            // DynamicData updates the bound collection on the thread the filter is emitted on.
            .ObserveOn(mainThreadScheduler ?? RxSchedulers.MainThreadScheduler);

        wordsOperation = book.Words.ToObservableChangeSet()
            .Filter(filter)
            .Transform(word => new WordViewModel(this, word.MotherTongue, word.ForeignLanguage))
            .Bind(out _words)
            .DisposeMany()
            .Subscribe();

        practiceModeHelper = book.WhenAnyValue(b => b.PracticeMode)
            .ToProperty(this, vm => vm.PracticeMode);

        AddWord = ReactiveCommand.Create(() => book.Words.Insert(0, new Word(["Test"], ["test"])));
        AddSynonym = ReactiveCommand.Create(() => book.Words[0].ForeignLanguage.Add(new("test")));
    }

    private ReadOnlyObservableCollection<WordViewModel> _words;
    public ReadOnlyObservableCollection<WordViewModel> Words => _words;
    public PracticeMode PracticeMode => practiceModeHelper.Value;

    private string? _searchText;
    public string? SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }

    public ReactiveCommand<RxVoid, RxVoid> AddWord { get; }
    public ReactiveCommand<RxVoid, RxVoid> AddSynonym { get; }

    public void Dispose()
    {
        wordsOperation.Dispose();
        practiceModeHelper.Dispose();
    }

    private static Func<Word, bool> BuildFilter(string? searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return _ => true;

        return word => word.MotherTongue.Any(s => s.Value.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            || word.ForeignLanguage.Any(s => s.Value.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }
}

public class BookDesignViewModel : BookViewModel
{
    public BookDesignViewModel() : base(SampleBook()) { }

    private static Book SampleBook()
    {
        Book book = new()
        {
            MotherTongue = "German",
            ForeignLanguage = "English",
            PracticeMode = PracticeMode.AskForForeignLang,
        };
        book.Words.Add(new Word(["Apfel"], ["apple"]));
        book.Words.Add(new Word(["Banane"], ["banana"]));
        book.Words.Add(new Word(["Ananas"], ["pineapple"]));
        book.Words.Add(new Word(["Kirsche"], ["cherry"]));
        book.Words.Add(new Word(["Birne"], ["pear"]));
        book.Words.Add(new Word(["Traube"], ["grape"]));
        book.Words.Add(new Word(["Brombeere"], ["blackberry"]));
        book.Words.Add(new Word(["Himbeere"], ["raspberry"]));
        book.Words.Add(new Word(["Pflaume"], ["plum"]));
        book.Words.Add(new Word(["Johannisbeere"], ["currant"]));
        book.Words.Add(new Word(["Preiselbeere"], ["lingonberry"]));
        book.Words.Add(new Word(["Zitrone"], ["lemon"]));
        book.Words.Add(new Word(
            ["Hänschen klein, geht allein\r\nIn die weite Welt hinein,\r\nStock und Hut steht ihm gut,\r\nIst auch wohlgemuth.\r\nAber Mutter weinet sehr,\r\nHat ja nun kein Hänschen mehr.\r\nWünsch dir Glück, sagt ihr Blick,\r\nKomm nur bald zurück!"],
            ["Little Hans goes alone\r\nInto the wide world,\r\nStick and hat suit him well,\r\nIs also in good spirits.\r\nBut mother weeps a lot,\r\nNow she has no little one left.\r\nWish you luck, says her look,\r\nCome back soon!"]));
        book.Words.Add(new Word(
            ["Vater unser im Himmel! Geheiligt werde dein Name. Dein Reich komme. Dein Wille geschehe, wie im Himmel, so auf Erden. Unser tägliches Brot gib uns heute. Und vergib uns unsere Schuld, wie auch wir vergeben unsern Schuldigern. Und führe uns nicht in Versuchung, sondern erlöse uns von dem Bösen. Denn dein ist das Reich und die Kraft und die Herrlichkeit in Ewigkeit. Amen."],
            ["Our Father who art in heaven, hallowed be thy name; thy kingdom come; Thy will be done on earth as it is in heaven. Give us this day our daily bread, and forgive us our trespasses, as we forgive those who trespass against us, and lead us not into temptation, but deliver us from evil. For thine is the kingdom, and the power, and the glory, for ever and ever. Amen."]));
        return book;
    }
}
