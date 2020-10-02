using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Vocup.Avalonia.Views
{
    public class MenuPageNew : UserControl
    {
        public MenuPageNew()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
