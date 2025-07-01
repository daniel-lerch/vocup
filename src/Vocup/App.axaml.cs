using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using System.IO;
using Vocup.ViewModels;
using Vocup.Views;

namespace Vocup;

public partial class App : Application
{
    private MainViewModel? mainViewModel;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public void OpenFile(Stream input)
    {
        if (mainViewModel != null)
        {
            mainViewModel.FileLength = input.Length;
        }
    }

    public override void OnFrameworkInitializationCompleted()
    {
        mainViewModel = new MainViewModel();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    protected virtual void BrowseFile()
    {

    }
}
