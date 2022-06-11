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

namespace Vocup;

public static class Program
{
    private static Mutex? mutex;

    public static VocupSettings Settings { get; private set; } = null!;

    /// <summary>
    /// The main entry-point for the application.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
        // Prevents the installer from executing while the program is running
        mutex = new Mutex(initiallyOwned: true, AppInfo.ProductName, out bool createdNew);

        // ApplicationConfiguration.Initialize() does not handle PerMonitorV2 correctly.
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.SetDefaultFont(new Font("Microsoft Sans Serif", 8.25f));

        if (!createdNew)
        {
            // Another instance of Vocup is already running so we change the focus
            SwitchFocus();
            return;
        }

        SplashScreen splash = new SplashScreen();
        splash.Show();
        Application.DoEvents();

        //SettingsImporter.Run();
        var loader = new Settings.Core.VersionedSettingsLoader<VocupSettings>(new(Path.GetTempPath()), "vocup_settings");
        var settings = loader.LoadAsync().GetAwaiter().GetResult();
        Settings = settings.Value;

        if (Settings.StartupCounter == 0)
        {
            //Settings.Default.Upgrade(); // Keep old settings with new version
            //                            // Warning: Unsaved changes are overridden
            
            // Reset DisableInternetSettings on update to 1.8.4
            if (AppInfo.IsUwp && Settings.Version < new Version(1, 8, 4))
            {
                Settings.DisableInternetServices = false;
            }
        }

        SetCulture();
        if (!CreateVhfFolder() || !CreateVhrFolder())
        {
            Application.Exit();
            return;
        }

        Settings.StartupCounter++;
        Settings.Version = AppInfo.Version;
        Application.DoEvents();

        Form form;

        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            FileInfo info = new FileInfo(args[0]);
            if (info.Extension == ".vhf")
            {
                var mainForm = new MainForm();
                mainForm.ReadFile(info.FullName);
                form = mainForm;
            }
            else if (info.Extension == ".vdp")
            {
                form = new RestoreBackup(info.FullName);
            }
            else
            {
                form = new MainForm();
                MessageBox.Show(string.Format(Messages.OpenUnknownFile, info.FullName),
                    Messages.OpenUnknownFileT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else if (Settings.StartScreen == (int)StartScreen.LastFile && File.Exists(Settings.LastFile))
        {
            var mainForm = new MainForm();
            mainForm.ReadFile(Settings.LastFile);
            form = mainForm;
        }
        else
        {
            form = new MainForm();
        }

        Application.DoEvents();

        splash.Close();
        Application.Run(form);

        // Calling .GetAwaiter().GetResult() does not work for ValueTasks
        settings.DisposeAsync().AsTask().GetAwaiter().GetResult();
        TrackingService.ActionAsync("App/Close").GetAwaiter().GetResult();
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
            PInvoke.User32.SetForegroundWindow(process.MainWindowHandle);
        }
        else
        {
            MessageBox.Show(Messages.MutexLockedButNoOtherProcess, Messages.MutexLockedButNoOtherProcessT, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
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
                CultureInfo culture = new CultureInfo(Settings.OverrideCulture);
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
