namespace Vocup.Avalonia.ViewModels
{
    public class MenuPageVM : ViewModelBase
    {
        public MenuPageVM()
        {
            Content = new MenuPageNewVM();
        }

        public ViewModelBase Content { get; set; }
    }
}
