using Vocup.Avalonia.Controls;

namespace Vocup.Avalonia.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            MenuStrip = new MenuStripViewModel();
            ToolBar = new ToolBarViewModel();
            MenuPage = new MenuPageVM();
        }

        public MenuStripViewModel MenuStrip { get; }
        public ToolBarViewModel ToolBar { get; }
        public MenuPageVM MenuPage { get; }
    }
}
