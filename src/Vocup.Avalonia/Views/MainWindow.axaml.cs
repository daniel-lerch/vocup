using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Vocup.Avalonia.ViewModels;

namespace Vocup.Avalonia.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
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

        private async Task DoBrowseVocabularyBookAsync(InteractionContext<Unit, string?> interaction)
        {
            OpenFileDialog dialog = new();
            dialog.Filters.Add(new FileDialogFilter() { Name = "Vocup vocabulary file", Extensions = { "vhf" } });

            string[]? result = await dialog.ShowAsync(this);
            interaction.SetOutput(result?.FirstOrDefault());
        }
    }
}
