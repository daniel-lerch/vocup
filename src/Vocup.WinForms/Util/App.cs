using System.Windows.Forms;

namespace Vocup.Util;

public partial class App : Vocup.App
{
    public override void OnFrameworkInitializationCompleted()
    {
#pragma warning disable WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        RequestedThemeVariant = Application.ColorMode switch
        {
            SystemColorMode.Dark => Avalonia.Styling.ThemeVariant.Dark,
            _ => Avalonia.Styling.ThemeVariant.Light,
        };
#pragma warning restore WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

        base.OnFrameworkInitializationCompleted();
    }
}
