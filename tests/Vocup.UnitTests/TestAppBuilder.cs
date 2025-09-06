using Avalonia;
using Avalonia.Headless;

[assembly: AvaloniaTestApplication(typeof(Vocup.UnitTests.TestAppBuilder))]

namespace Vocup.UnitTests;
public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
