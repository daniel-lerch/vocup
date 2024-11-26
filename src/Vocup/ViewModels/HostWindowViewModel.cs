using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using Vocup.Settings;
using Vocup.Settings.Core;
using Vocup.Util;

namespace Vocup.ViewModels;

public class HostWindowViewModel : ViewModelBase
{
    private readonly IServiceProvider serviceProvider;
    private readonly Action<IServiceProvider, IServiceCollection> configureServices;

    public HostWindowViewModel(IServiceProvider serviceProvider, Action<IServiceProvider, IServiceCollection> configureServices)
    {
        this.serviceProvider = serviceProvider;
        this.configureServices = configureServices;
        Initialize();
    }

    private ViewModelBase? _mainView;
    public ViewModelBase? MainView
    {
        get => _mainView;
        private set => this.RaiseAndSetIfChanged(ref _mainView, value);
    }

    private VocupSettings? _settings;
    public VocupSettings? Settings
    {
        get => _settings;
        private set => this.RaiseAndSetIfChanged(ref _settings, value);
    }

    public async void Initialize()
    {
        await default(HopToThreadPoolAwaitable); // Force blocking IO to run on a background thread

        var settingsLoader = serviceProvider.GetRequiredService<SettingsLoaderBase<VocupSettings>>();
        var settingsContext = await settingsLoader.LoadAsync();

        IServiceCollection services = new ServiceCollection();
        services.AddSingleton(settingsContext);
        services.AddSingleton(settingsContext.Value);
        configureServices(serviceProvider, services);

        MainView = new MainViewModel();
        Settings = settingsContext.Value;
    }
}
