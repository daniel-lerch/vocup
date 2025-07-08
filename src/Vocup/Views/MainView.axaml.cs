using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Vocup.ViewModels;

namespace Vocup.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();

        TopLevel.SetAutoSafeAreaPadding(this, true);

        this.WhenActivated(d => d(ViewModel.PickFileInteraction.RegisterHandler(async interaction =>
        {
            var files = await TopLevel.GetTopLevel(this).StorageProvider.OpenFilePickerAsync(new()
            {
                AllowMultiple = false,
            });
            if (files.Count > 0)
                interaction.SetOutput(files[0]);
            else
                interaction.SetOutput(null);
        })));

        this.WhenActivated(d => d(ViewModel.FileFromUriInteraction.RegisterHandler(async interaction =>
        {
            var file = await TopLevel.GetTopLevel(this).StorageProvider.TryGetFileFromPathAsync(interaction.Input);
            interaction.SetOutput(file);
        })));
    }
}
