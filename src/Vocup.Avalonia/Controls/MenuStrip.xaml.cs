using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Vocup.Avalonia.Controls
{
    public class MenuStrip : UserControl
    {
        public MenuStrip()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
