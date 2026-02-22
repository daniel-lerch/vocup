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

    private bool _isPaneOpen = true;
    public bool IsPaneOpen
    {
        get => _isPaneOpen;
        set => this.RaiseAndSetIfChanged(ref _isPaneOpen, value);
    }


    public AboutViewModel About { get; }

    public Interaction<Unit, IStorageFile?> PickFileInteraction { get; } = new();

    public ICommand OpenFileCommand { get; }
    public ICommand AboutCommand { get; }
    public ICommand TogglePaneCommand { get; }

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

            IsPaneOpen = false;
        });

        AboutCommand = ReactiveCommand.Create(() =>
        {
            CurrentView = About;
            IsPaneOpen = false;
        });

        TogglePaneCommand = ReactiveCommand.Create(() =>
        {
            IsPaneOpen = !IsPaneOpen;
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
