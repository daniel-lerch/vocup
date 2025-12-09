using Avalonia.Controls;
using ReactiveUI;
using ReactiveUI.Avalonia;
using Vocup.ViewModels;

namespace Vocup.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();

        TopLevel.SetAutoSafeAreaPadding(this, true);

        // ViewModel must be set before control activation
        this.WhenActivated(d => d(ViewModel!.PickFileInteraction.RegisterHandler(async interaction =>
        {
            // This control must be assinged to a TopLevel when it is activated
            var files = await TopLevel.GetTopLevel(this)!.StorageProvider.OpenFilePickerAsync(new()
            {
                AllowMultiple = false,
                FileTypeFilter = [new(Lang.Resources.VhfFileType) { Patterns = ["*.vhf"] }]
            });
            if (files.Count > 0)
                interaction.SetOutput(files[0]);
            else
                interaction.SetOutput(null);
        })));

        // ViewModel must be set before control activation
        this.WhenActivated(d => d(ViewModel!.FileFromUriInteraction.RegisterHandler(async interaction =>
        {
            // This control must be assinged to a TopLevel when it is activated
            var file = await TopLevel.GetTopLevel(this)!.StorageProvider.TryGetFileFromPathAsync(interaction.Input);
            interaction.SetOutput(file);
        })));
    }
}
