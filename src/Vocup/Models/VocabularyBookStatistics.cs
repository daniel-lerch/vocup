using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Vocup.Models;

public class VocabularyBookStatistics : INotifyPropertyChanged
{
    private readonly VocabularyBook book;

    public VocabularyBookStatistics(VocabularyBook book)
    {
        this.book = book;
        book.Words.OnAdd(word =>
        {
            word.PropertyChanged += VocabularyWord_PropertyChanged;
            Refresh();
        });
        book.Words.OnRemove(word =>
        {
            word.PropertyChanged -= VocabularyWord_PropertyChanged;
            Refresh();
        });
    }

    public int Unpracticed { get; private set; }
    public int WronglyPracticed { get; private set; }
    public int CorrectlyPracticed { get; private set; }
    public int FullyPracticed { get; private set; }

    public int NotFullyPracticed => Unpracticed + WronglyPracticed + CorrectlyPracticed;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string name = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private void VocabularyWord_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(VocabularyWord.PracticeState))
        {
            Refresh();
        }
    }

    public void Refresh()
    {
        int un = 0, wrongly = 0, correctly = 0, fully = 0;

        foreach (VocabularyWord word in book.Words)
        {
            switch (word.PracticeState)
            {
                case PracticeState.Unpracticed:
                    un++;
                    break;
                case PracticeState.WronglyPracticed:
                    wrongly++;
                    break;
                case PracticeState.CorrectlyPracticed:
                    correctly++;
                    break;
                case PracticeState.FullyPracticed:
                    fully++;
                    break;
                default:
                    break;
            }
        }

        Unpracticed = un;
        WronglyPracticed = wrongly;
        CorrectlyPracticed = correctly;
        FullyPracticed = fully;

        OnPropertyChanged();
    }
}
