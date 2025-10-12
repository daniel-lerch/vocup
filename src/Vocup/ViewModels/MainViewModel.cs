using Avalonia.Platform.Storage;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Vocup.IO;
using Vocup.Models;

namespace Vocup.ViewModels;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase? _currentView;
    public ViewModelBase? CurrentView
    {
        get => _currentView;
        set => this.RaiseAndSetIfChanged(ref _currentView, value);
    }

    public AboutViewModel About { get; }

    public Interaction<Unit, IStorageFile?> PickFileInteraction { get; } = new();
    public Interaction<Uri, IStorageFile?> FileFromUriInteraction { get; } = new();

    public ICommand OpenFileCommand { get; }
    public ICommand AboutCommand { get; }

    public MainViewModel()
    {
        About = new(deployment: "Google Play");

        OpenFileCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            try
            {
                var file = await PickFileInteraction.Handle(Unit.Default);

                if (OperatingSystem.IsAndroid())
                    CurrentView = new ErrorViewModel("Loading...");

                if (file != null)
                {
                    Book book = new();
                    await BookFileFormat2.DetectAndRead(file, book, null);
                    CurrentView = new BookViewModel(book);
                }
            }
            catch (Exception ex)
            {
                CurrentView = new ErrorViewModel($"Error opening file: {ex.Message}");
            }
        });

        AboutCommand = ReactiveCommand.Create(() =>
        {
            CurrentView = About;
        });
    }

    public async void OpenFile(Uri path)
    {
        if (OperatingSystem.IsAndroid())
            CurrentView = new ErrorViewModel("Loading...");
        try
        {
            IStorageFile? file = null;
            // FileFromUriInteraction will be registered after view activation.
            // Workaround: Wait up to 5 seconds for view to register interaction.
            for (int attempt = 0; attempt < 100; attempt++)
            {
                try
                {
                    file = await FileFromUriInteraction.Handle(path);
                    break;
                }
                catch (UnhandledInteractionException<Uri, IStorageFile?>)
                {
                    await Task.Delay(50);
                }
            }
            if (file != null)
            {
                Book book = new();
                await BookFileFormat2.DetectAndRead(file, book, null);
                CurrentView = new BookViewModel(book);
            }
        }
        catch (Exception ex)
        {
            CurrentView = new ErrorViewModel($"Error opening file: {ex.Message}");
        }
    }
}
