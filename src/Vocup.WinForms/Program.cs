using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Vocup.Forms;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Settings;
using Vocup.Util;
using Windows.Win32;
using Windows.Win32.Foundation;

using Application = System.Windows.Forms.Application;

namespace Vocup;

public static class Program
{
    private static Mutex? mutex;

    public static VocupSettings Settings { get; private set; } = null!;
    public static TrackingService TrackingService { get; private set; } = null!;

    /// <summary>
    /// The main entry-point for the application.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
        // Prevents the installer from executing while the program is running
        mutex = new Mutex(initiallyOwned: true, AppInfo.ProductName, out bool createdNew);

        if (!createdNew)
        {
            // Another instance of Vocup is already running so we change the focus
            SwitchFocus();
            return;
        }

        // ApplicationConfiguration.Initialize() does not handle PerMonitorV2 correctly.
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.SetDefaultFont(new Font("Microsoft Sans Serif", 8.25f));

        SplashScreen splash = new();
        splash.Show();
        Application.DoEvents();

        AppBuilder.Configure<App>().UsePlatformDetect().UseReactiveUI().SetupWithoutStarting();

        var serviceScope = InitializeServices();

        SetCulture();

#pragma warning disable WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        Application.SetColorMode(Settings.ColorMode);
#pragma warning restore WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

        if (!CreateVhfFolder() || !CreateVhrFolder())
        {
            Application.Exit();
            return;
        }

        Settings.StartupCounter++;
        Settings.Version = AppInfo.Version;
        Application.DoEvents();

        MainForm form = new();

        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            FileInfo info = new(args[0]);
            if (info.Extension == ".vhf")
            {
                form.ReadFile(info.FullName);
            }
            else
            {
                MessageBox.Show(string.Format(Messages.OpenUnknownFile, info.FullName),
                    Messages.OpenUnknownFileT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else if (Settings.StartScreen == (int)StartScreen.LastFile && File.Exists(Settings.LastFile))
        {
            form.ReadFile(Settings.LastFile);
        }

        Application.DoEvents();

        splash.Close();
        Application.Run(form);

        // After Application.Run returns, the synchronization context is broken and we need to set it to null
        SynchronizationContext.SetSynchronizationContext(null);

        // Calling .GetAwaiter().GetResult() does not work for ValueTasks
        serviceScope.DisposeAsync().AsTask().GetAwaiter().GetResult();
        TrackingService.DisposeAsync().AsTask().GetAwaiter().GetResult();
    }

    public static void ReleaseMutex()
    {
        mutex?.ReleaseMutex();
    }

    private static void SwitchFocus()
    {
        // Take the Vocup process which was started first because there might be multiple newer processes racing for bringing one to front
        Process? process = Process.GetProcessesByName(AppInfo.ProductName).OrderBy(x => x.StartTime).FirstOrDefault();
        if (process != null && process.MainWindowHandle != IntPtr.Zero)
        {
            PInvoke.SetForegroundWindow((HWND)process.MainWindowHandle);
        }
        else
        {
            MessageBox.Show(Messages.MutexLockedButNoOtherProcess, Messages.MutexLockedButNoOtherProcessT, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static IAsyncDisposable InitializeServices()
    {
        var loader = new VocupSettingsLoader();
        var settings = loader.LoadAsync().AsTask().GetAwaiter().GetResult();
        Settings = settings.Value;

        TrackingService = new TrackingService();

        return settings;
    }

    /// <summary>
    /// Checks the currently configured folder for .vhf files and creates it if not existing.
    /// </summary>
    public static bool CreateVhfFolder()
    {
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        if (string.IsNullOrWhiteSpace(Settings.VhfPath))
        {
            Settings.VhfPath = folder;
        }
        else if (!Directory.Exists(Settings.VhfPath))
        {
            if (MessageBox.Show(string.Format(Messages.VhfPathNotFound, Settings.VhfPath), Messages.VhfPathNotFoundT, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                == DialogResult.OK)
            {
                Settings.VhfPath = folder;
            }
            else return false;
        }

        return true;
    }

    /// <summary>
    /// Checks the currently configured folder for .vhr files and creates it if not existing.
    /// </summary>
    public static bool CreateVhrFolder()
    {
        string folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            AppInfo.ProductName);

        if (string.IsNullOrWhiteSpace(Settings.VhrPath))
        {
            Directory.CreateDirectory(folder);
            Settings.VhrPath = folder;
        }
        else if (!Directory.Exists(Settings.VhrPath))
        {
            if (MessageBox.Show(string.Format(Messages.VhrPathNotFound, Settings.VhrPath), Messages.VhrPathNotFoundT, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                == DialogResult.OK)
            {
                Directory.CreateDirectory(folder);
                Settings.VhrPath = folder;
            }
            else return false;
        }

        return true;
    }

    internal static void SetCulture()
    {
        if (!string.IsNullOrWhiteSpace(Settings.OverrideCulture))
        {
            try
            {
                CultureInfo culture = new(Settings.OverrideCulture);
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
            catch (CultureNotFoundException)
            {
                Settings.OverrideCulture = null;
            }
        }
    }
}
