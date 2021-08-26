using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using Vocup.Avalonia.ViewModels;

namespace Vocup.Avalonia.Views
{
    public class MainWindow : ReactiveWindow<MainWindowVM>
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(d => d(ViewModel!.BrowseVocabularyBook.RegisterHandler(DoBrowseVocabularyBookAsync)));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async Task DoBrowseVocabularyBookAsync(InteractionContext<Unit, string[]> interaction)
        {
            OpenFileDialog dialog = new();

            string[] result = await dialog.ShowAsync(this);
            interaction.SetOutput(result);
        }
    }
}
