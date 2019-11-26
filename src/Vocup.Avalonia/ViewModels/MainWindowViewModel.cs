using System;
using System.Collections.Generic;
using System.Text;
using Vocup.Avalonia.Controls;

namespace Vocup.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            MenuStrip = new MenuStripViewModel();
            ToolBar = new ToolBarViewModel();
        }

        public string Greeting => "Welcome to Avalonia!";
        public MenuStripViewModel MenuStrip { get; }
        public ToolBarViewModel ToolBar { get; }
    }
}
