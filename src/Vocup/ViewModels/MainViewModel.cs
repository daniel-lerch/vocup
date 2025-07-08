using Avalonia.Platform.Storage;
using Avalonia.Threading;
using ReactiveUI;
using System;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Vocup.ViewModels;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase? _currentView;
    public ViewModelBase? CurrentView
    {
        get => _currentView;
        set => this.RaiseAndSetIfChanged(ref _currentView, value);
    }

    private string _errorMessage = string.Empty;
    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public AboutViewModel About { get; } = new();

    public Interaction<Unit, IStorageFile?> PickFileInteraction { get; } = new();
    public Interaction<Uri, IStorageFile?> FileFromUriInteraction { get; } = new();

    public ICommand OpenFileCommand { get; }
    public ICommand AboutCommand { get; }

    public MainViewModel()
    {
        OpenFileCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            try
            {
                var file = await PickFileInteraction.Handle(Unit.Default);
                if (file != null)
                {
                    using Stream stream = await Task.Run(file.OpenReadAsync);
                    CurrentView = new BookViewModel { FileLength = stream.Length };
                }
            }
            catch (Exception ex)
            {
                Dispatcher.UIThread.Invoke(() => ErrorMessage = $"Error opening file: {ex.Message}");
            }
        });

        AboutCommand = ReactiveCommand.Create(() =>
        {
            CurrentView = About;
        });
    }

    public async void OpenFile(Uri path)
    {
        try
        {
            var file = await FileFromUriInteraction.Handle(path);
            if (file != null)
            {
                using Stream stream = await Task.Run(file.OpenReadAsync);
                CurrentView = new BookViewModel { FileLength = stream.Length };
            }
        }
        catch (Exception ex)
        {
            Dispatcher.UIThread.Invoke(() => ErrorMessage = $"Error opening file: {ex.Message}");
        }
    }
}
