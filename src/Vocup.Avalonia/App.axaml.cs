using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;
using Vocup.Avalonia.ViewModels;
using Vocup.Avalonia.Views;

namespace Vocup.Avalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowVM(),
                };

#if WINDOWS
                desktop.MainWindow.Opened += OnMainWindowOpened;
#endif
            }

            base.OnFrameworkInitializationCompleted();
        }

#if WINDOWS
        private static void OnMainWindowOpened(object sender, EventArgs e)
        {
            if (sender is not Window window)
                throw new ArgumentException("This event handler must be registered on a window", nameof(sender));

            window.Opened -= OnMainWindowOpened;
            Program.CloseSplashScreen(window.PlatformImpl.Handle.Handle);
        }
#endif
    }
}
