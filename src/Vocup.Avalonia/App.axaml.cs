using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;
using Vocup.Avalonia.Controls;
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
            }

            if (ApplicationLifetime is ISplashScreenLifetime splashScreen)
            {
                splashScreen.MainWindow.Opened += OnMainWindowOpened;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void OnMainWindowOpened(object sender, EventArgs e)
        {
            var window = (Window)sender;
            var splashScreen = (ISplashScreenLifetime)ApplicationLifetime;

            window.Opened -= OnMainWindowOpened;
            splashScreen.WindowCreated(window.PlatformImpl.Handle.Handle);
        }
    }
}
