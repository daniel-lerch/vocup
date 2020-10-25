using System;
using Vocup.Models;

namespace Vocup.Avalonia.ViewModels
{
    public class MenuPageVM : ViewModelBase
    {
        public MenuPageVM(Action<Book> showBook)
        {
            Content = new MenuPageNewVM(showBook);
        }

        public ViewModelBase Content { get; set; }
    }
}
