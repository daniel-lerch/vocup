using System;
using Vocup.Models;

namespace Vocup.ViewModels;

public class PracticeViewModel : ViewModelBase
{
    private readonly Book book;

    public PracticeViewModel(Book book)
    {
        this.book = book;
        PracticeDirection direction = book.PracticeMode switch
        {
            PracticeMode.AskForForeignLang => PracticeDirection.AskForForeignLanguage,
            PracticeMode.AskForMotherTongue => PracticeDirection.AskForMotherTongue,
            _ => Random.Shared.Next(2) == 0 ? PracticeDirection.AskForForeignLanguage : PracticeDirection.AskForMotherTongue,
        };
        CurrentView = new PracticeWordViewModel(book.Words[0], direction);
    }

    public virtual ViewModelBase CurrentView { get; private set; }
}

public class PracticeDesignViewModel : ViewModelBase
{
    private ViewModelBase _currentView = new PracticeWordDesignViewModel();
    public ViewModelBase CurrentView => _currentView;
}
