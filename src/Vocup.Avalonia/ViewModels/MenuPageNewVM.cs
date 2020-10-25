using System;
using Vocup.Models;

namespace Vocup.Avalonia.ViewModels
{
    public class MenuPageNewVM : ViewModelBase
    {
        private readonly Action<Book> showBook;

        public MenuPageNewVM(Action<Book> showBook)
        {
            this.showBook = showBook;
        }

        public void Commit()
        {
            showBook(new Book());
        }
    }
}
