using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Vocup.Avalonia.Views
{
    public partial class BookView : UserControl
    {
        public BookView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
