using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Styling;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading.Tasks;
using Vocup.Avalonia.Controls;
using Vocup.Avalonia.ViewModels;

namespace Vocup.Avalonia.Views
{
    public class MainWindow : ReactiveWindow<MainWindowVM>
    {
        public MainWindow()
        {
            InitializeComponent();

            Style style = new Style(x => x.OfType<Button>());
            style.Setters.Add(new Setter(TransitionsProperty, new Transitions()
            {
                new SolidColorBrushTransition
                {
                    Duration = TimeSpan.FromMilliseconds(250),
                    Property = BackgroundProperty,
                }
            }));

            Styles.Add(style);

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
