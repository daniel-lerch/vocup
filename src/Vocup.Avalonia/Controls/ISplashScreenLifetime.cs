using Avalonia.Controls.ApplicationLifetimes;
using System;

namespace Vocup.Avalonia.Controls
{
    public interface ISplashScreenLifetime : IClassicDesktopStyleApplicationLifetime
    {
        void WindowCreated(IntPtr handle);
    }
}
