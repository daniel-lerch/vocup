using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Vocup.Avalonia;

namespace Vocup.Windows
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        static int Main(string[] args)
        {
            // Show splash screen directly to make Vocup feel responsive.
            // This call only loads as few assemblies as possible.
            SplashScreen.StartShow();

            // This call loads and compiles Avalonia's assemblies and can take ~100ms,
            // measured using a ready to run single file executable on an Intel Core i5 2400.
            WinformsSplashScreenLifetime lifetime = new()
            {
                Args = args,
                ShutdownMode = ShutdownMode.OnLastWindowClose
            };

            // This call can take ~500ms but we still don't have our window opened.
            BuildAvaloniaApp().SetupWithLifetime(lifetime);

            return lifetime.Start(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}
