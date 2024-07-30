using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Vocup.ViewModels;

namespace Vocup.Views;

public partial class LicensesControl : ReactiveUserControl<LicensesViewModel>
{
    public LicensesControl()
    {
        InitializeComponent();

        this.WhenActivated(d => d(ViewModel.LaunchUri.RegisterHandler(async interaction =>
        {
            var topLevel = TopLevel.GetTopLevel(this);
            bool success = await topLevel.Launcher.LaunchUriAsync(interaction.Input);
            interaction.SetOutput(success);
        })));
    }
}
