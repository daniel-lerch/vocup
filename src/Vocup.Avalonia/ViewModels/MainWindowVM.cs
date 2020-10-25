using ReactiveUI;
using Vocup.Models;

namespace Vocup.Avalonia.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        private ViewModelBase content;

        public MainWindowVM()
        {
            Content = MenuPage = new MenuPageVM(ShowBook);
        }

        public ViewModelBase Content
        {
            get => content; 
            set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public MenuPageVM MenuPage { get; }

        public void ShowBook(Book book)
        {
            Content = new BookPageVM(book);
        }
    }
}
