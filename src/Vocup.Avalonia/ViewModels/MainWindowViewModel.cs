using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using Vocup.IO;
using Vocup.Models;

namespace Vocup.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase content;

        public MainWindowViewModel()
        {
            BrowseVocabularyBook = new Interaction<Unit, string?>();

            OpenBook = ReactiveCommand.CreateFromTask(async () =>
            {
                string? result = await BrowseVocabularyBook.Handle(default);
                if (result is not null)
                {
                    BookStorage storage = new();
                    Book book = await storage.ReadBookAsync(result);
                    ShowBook(book);
                }
            });
        }

        public ViewModelBase Content
        {
            get => content;
            set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public ICommand OpenBook { get; }

        public Interaction<Unit, string?> BrowseVocabularyBook { get; }

        public void CreateBook()
        {
            Content = new BookSettingsViewModel(ShowBook);
        }

        public void ShowBook(Book book)
        {
            Content = new BookViewModel(book);
        }
    }
}
