using Avalonia.Controls;
using ReactiveUI;
using ReactiveUI.Binding;
using Vocup.ViewModels;

namespace Vocup.Views;

public partial class MainView : UserControl, IViewFor<MainViewModel>
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
        })),
        // Passing the view model observable selects the trim and AOT safe overload
        this.WhenAnyValue(v => v.DataContext));
    }

    public MainViewModel? ViewModel
    {
        get => DataContext as MainViewModel;
        set => DataContext = value;
    }

    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (MainViewModel?)value;
    }
}
