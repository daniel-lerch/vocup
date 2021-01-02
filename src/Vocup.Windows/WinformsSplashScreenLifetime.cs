using Avalonia.Controls.ApplicationLifetimes;
using System;
using Vocup.Avalonia.Controls;

namespace Vocup.Windows
{
    internal class WinformsSplashScreenLifetime : ClassicDesktopStyleApplicationLifetime, ISplashScreenLifetime
    {
        public void WindowCreated(IntPtr handle)
        {
            SplashScreen.Close(handle);
        }
    }
}
