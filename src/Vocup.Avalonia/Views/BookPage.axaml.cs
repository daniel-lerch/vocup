using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Vocup.Avalonia.Views
{
    public class BookPage : UserControl
    {
        public BookPage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
