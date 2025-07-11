using System;
using System.Collections.ObjectModel;
using Vocup.Models;

namespace Vocup.ViewModels;

public class BookViewModel : ViewModelBase
{
    private readonly Book _book;

    public BookViewModel(Book book)
    {
        _book = book ?? throw new ArgumentNullException(nameof(book));
    }

    public ObservableCollection<Word> Words => _book.Words;
}

public class BookDesignViewModel : BookViewModel
{
    public BookDesignViewModel() : base(SampleBook()) { }

    private static Book SampleBook()
    {
        Book book = new()
        {
            MotherTongue = "German",
            ForeignLanguage = "English"
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
