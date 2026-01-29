using Avalonia.Platform.Storage;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
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

    private string _errorMessage = string.Empty;
    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public bool HandlersRegistered { get; set; }

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

    public async void OpenFile(IStorageFile file)
    {
        if (OperatingSystem.IsAndroid())
            CurrentView = new ErrorViewModel("Loading...");

        try
        {
            Book book = new();
            await BookFileFormat2.DetectAndRead(file, book, null);
            CurrentView = new BookViewModel(book);
        }
        catch (Exception ex)
        {
            CurrentView = new ErrorViewModel($"Error opening file: {ex.Message}");
        }
    }
}
