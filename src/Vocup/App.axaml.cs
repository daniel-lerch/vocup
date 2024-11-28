using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using Vocup.ViewModels;
using Vocup.Views;

namespace Vocup;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ServiceCollection services = new();
        ConfigureSettings(services);
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new HostWindow
            {
                DataContext = new HostWindowViewModel(serviceProvider, ConfigureServices)
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel(null)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    protected virtual void ConfigureSettings(IServiceCollection services)
    {
    }

    protected virtual void ConfigureServices(IServiceProvider serviceProvider, IServiceCollection services)
    {
    }
}
