using Vocup.Models;

namespace Vocup.Avalonia.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        public BookViewModel(Book book)
        {
            Book = book;
        }

        public Book Book { get; }
    }
}
