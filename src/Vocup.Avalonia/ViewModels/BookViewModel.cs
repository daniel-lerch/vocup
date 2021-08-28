using Vocup.Models;

namespace Vocup.Avalonia.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        private readonly Book book;

        public BookViewModel(Book book)
        {
            this.book = book;
        }

        public string MotherTongue => book.MotherTongue;

        public string ForeignLanguage => book.ForeignLanguage;
    }
}
