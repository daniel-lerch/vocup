using ReactiveUI.Avalonia;
using Vocup.ViewModels;

namespace Vocup.Views;

public partial class AboutView : ReactiveUserControl<AboutViewModel>
{
    public AboutView()
    {
        InitializeComponent();
    }
}
