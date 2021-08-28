using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Vocup.Avalonia.Views
{
    public class BookSettingsView : UserControl
    {
        public BookSettingsView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
