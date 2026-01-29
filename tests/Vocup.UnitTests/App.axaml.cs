using Avalonia;
using Avalonia.Markup.Xaml;

namespace Vocup.UnitTests;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
