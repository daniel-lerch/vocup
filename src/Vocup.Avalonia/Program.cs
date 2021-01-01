using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Threading;

#if WINDOWS
using PInvoke;
using System.Windows.Forms;
using WinformsApplication = System.Windows.Forms.Application;
#endif

namespace Vocup.Avalonia
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
#if WINDOWS
            ThreadPool.QueueUserWorkItem(RunSplashScreen);
#endif
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }

#if WINDOWS
        private static void RunSplashScreen(object state)
        {
            Form splashScreen = new() { StartPosition = FormStartPosition.CenterScreen };
            WinformsApplication.Run(splashScreen);
        }

        public static void CloseSplashScreen(IntPtr newWindowHandle)
        {
            User32.SetForegroundWindow(newWindowHandle);
            WinformsApplication.Exit();
        }
#endif

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}
