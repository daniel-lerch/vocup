using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Vocup.Avalonia.Views
{
    public class MenuPage : UserControl
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
