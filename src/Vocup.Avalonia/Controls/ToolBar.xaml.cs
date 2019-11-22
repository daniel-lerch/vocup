using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Vocup.Avalonia.Controls
{
    public class ToolBar : UserControl
    {
        public ToolBar()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
