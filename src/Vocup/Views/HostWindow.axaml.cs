using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using Vocup.ViewModels;

namespace Vocup.Views;

public partial class HostWindow : ReactiveWindow<HostWindowViewModel>
{
    public HostWindow()
    {
        InitializeComponent();

        this.WhenAnyValue(
            x => x.ViewModel,
            x => x.ViewModel!.Settings,
            (vm, settings) => vm?.Settings
        )
            .Subscribe(settings =>
            {
                if (settings != null)
                {
                    Dispatcher.UIThread.Post(() =>
                    {
                        Position = settings.WindowPosition;
                        Width = settings.WindowWidth;
                        Height = settings.WindowHeight;
                    });

                    // Windows does not remember the original size when the window is maximized in the same operation:
                    // https://github.com/AvaloniaUI/Avalonia/issues/14517#issuecomment-1930908206
                    Dispatcher.UIThread.Post(() =>
                    {
                        WindowState = settings.WindowState;

                        // Subscribe to changes after restoring saved values
                        PropertyChanged += HostWindow_PropertyChanged;
                        PositionChanged += HostWindow_PositionChanged;
                    });
                }
            });
    }

    private void HostWindow_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (ViewModel != null && ViewModel.Settings != null)
        {
            if (e.Property == HeightProperty)
                ViewModel.Settings.WindowHeight = Height;
            else if (e.Property == WidthProperty)
                ViewModel.Settings.WindowWidth = Width;
            else if (e.Property == WindowStateProperty)
                ViewModel.Settings.WindowState = WindowState;
        }
    }

    private void HostWindow_PositionChanged(object? sender, PixelPointEventArgs e)
    {
        if (ViewModel != null && ViewModel.Settings != null)
            ViewModel.Settings.WindowPosition = e.Point;
    }
}
