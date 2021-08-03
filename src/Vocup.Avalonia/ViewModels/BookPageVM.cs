﻿using Vocup.Avalonia.Controls;
using Vocup.Models;

namespace Vocup.Avalonia.ViewModels
{
    public class BookPageVM : ViewModelBase
    {
        public BookPageVM(Book book)
        {
            MenuStrip = new MenuStripViewModel();
            ToolBar = new ToolBarViewModel();
        }

        public MenuStripViewModel MenuStrip { get; }
        public ToolBarViewModel ToolBar { get; }
    }
}